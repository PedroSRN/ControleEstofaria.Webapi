using ControleEstofaria.Aplicacao.Compartilhado;
using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloServico;
using FluentResults;
using FluentValidation.Results;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Aplicacao.ModuloServico
{
    public class ServicoServico : ServicoBase<Servico, ValidadorServico>
    {
        public IRepositorioServico repositorioServico;
        public IContextoPersistencia contextoPersistencia;

        public ServicoServico(IRepositorioServico repositorioServico, IContextoPersistencia contexto)
        {
            this.repositorioServico = repositorioServico;
            this.contextoPersistencia = contexto;
        }

        public Result<Servico> Inserindo(Servico servico)
        {
            Log.Logger.Debug("Tentando inserir Servico... {@s}", servico);

            Result resultado = Validar(servico);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioServico.Inserir(servico);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Serviço {ServicoId} inserido com sucesso", servico.Id);

                return Result.Ok(servico);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir o Serviço";

                Log.Logger.Error(ex, msgErro + " {ServicoId}", servico.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Servico> Editar(Servico servico)
        {
            Log.Logger.Debug("Tentando editar serviço... {@s}", servico);

            var resultado = Validar(servico);

            if (!resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioServico.Editar(servico);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Serviço {ServicoId} editado com sucesso", servico.Id);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar editar o Serviço";

                Log.Logger.Error(ex, msgErro + "{ServicoId}", servico.Id);

                return Result.Fail(msgErro);
            }

            return Result.Ok(servico);
        }

        public Result Excluir(Guid id)
        {
            var servicoResult = SelecionarPorId(id);

            if (servicoResult.IsSuccess)
                return Excluir(servicoResult.Value);

            return Result.Fail(servicoResult.Errors);
        }

        public Result Excluir(Servico servico)
        {
            Log.Logger.Debug("Tentando excluir servico... {@s}", servico);

            try
            {
                repositorioServico.Excluir(servico);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Serviço {ServicoId} excluido com sucesso", servico.Id);

                return Result.Ok();
            }
            catch(Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar excluir o Serviço";

                if ((bool)(ex.InnerException?.Message?.StartsWith("Cannot insert the value NULL into column 'ServicoId'")))
                {
                    msgErro = "Não foi possivel remover este servico, pois ele está vinculado a um cliente";
                }

                Log.Logger.Error(ex, msgErro + " {ServicoId}", servico.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Servico>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar serviços...");

            try
            {
                var servicos = repositorioServico.SelecionarTodos();

                Log.Logger.Information("Servicos selecionados com sucesso");

                return Result.Ok(servicos);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os Serviços";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }

        }

        public Result<Servico> SelecionarPorId(Guid id)
        {

            Log.Logger.Debug("Tentando selecionar serviço {ServiçoId}...", id);

            try
            {
                var servico = repositorioServico.SelecionarPorId(id);

                if (servico == null)
                {
                    Log.Logger.Warning("Servico {ServicoId} não encontrado", id);

                    return Result.Fail("Serviço não encontrado");
                }

                Log.Logger.Information("Serviço {ServicoId} selecionado com sucesso", id);

                return Result.Ok(servico);
            }
            catch(Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Serviço";

                Log.Logger.Error(ex, msgErro + " {ServicoId}", id);

                return Result.Fail(msgErro);
            }
        }

       /* protected override Result Validar(Servico arg)
        {
            var validador = new ValidadorServico();

            var resultadoValidacao = validador.Validate(arg);

            List<Error> erros = new List<Error>();

            foreach (ValidationFailure item in resultadoValidacao.Errors)
                erros.Add(new Error(item.ErrorMessage));

            if (NomeFilmeDuplicado(arg))
                erros.Add(new Error(""));

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }
       */
    }
}
