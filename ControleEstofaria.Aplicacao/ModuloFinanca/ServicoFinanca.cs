using ControleEstofaria.Aplicacao.Compartilhado;
using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloFinanca;
using FluentResults;
using FluentValidation.Results;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Aplicacao.ModuloFinanca
{
    public class ServicoFinanca : ServicoBase<Financa, ValidadorFinanca>
    {
        public IRepositorioFinanca repositorioFinanca;
        public IContextoPersistencia contextoPersistencia;


        public ServicoFinanca(IRepositorioFinanca repositorioFinanca, IContextoPersistencia contexto)
        {
            this.repositorioFinanca = repositorioFinanca;
            this.contextoPersistencia = contexto;
        }

        public Result<Financa> Inserir(Financa financa)
        {
            Log.Logger.Debug("Tentando inserir Financas... {@f}", financa);

            Result resultado = Validar(financa);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioFinanca.Inserir(financa);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Financa {FinancaId} inserindo com sucesso", financa.Id);

                return Result.Ok(financa);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir a Financa";

                Log.Logger.Error(ex, msgErro + " {FinancaId} ", financa.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Financa> Editar(Financa financa)
        {
            Log.Logger.Debug("Tentando editar Finança... {@f}", financa);

            var resultado = Validar(financa);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioFinanca.Editar(financa);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Financa {FinancaId} editado com sucesso", financa.Id);
            }
            catch(Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar editar a Finança";

                Log.Logger.Error(ex, msgErro + " {FinancaId}", financa.Id);

                return Result.Fail(msgErro);
            }
            
            return Result.Ok(financa);
        }

        public Result Excluir(Guid id)
        {
            var financaResult = SelecionarPorId(id);

            if(financaResult.IsSuccess)
                return Excluir(financaResult.Value);

            return Result.Fail(financaResult.Errors);
        }

        public Result Excluir(Financa financa)
        {
            Log.Logger.Debug("Tentando excluir finança... {@f}", financa);

            try
            {
                repositorioFinanca.Excluir(financa);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Finança {FinancaId} excluido com sucesso", financa.Id);

                return Result.Ok();
            }
            catch(Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar excluir a Finança";

                if ((bool)(ex.InnerException?.Message?.StartsWith("Cannot insert the value NULL into column 'FinancaId'")))
                {
                    msgErro = "Não foi possivel remover está finança, pois ele está vinculada a um serviço";
                }

                Log.Logger.Error(ex, msgErro + " {FinancaId}", financa.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Financa>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando Selecionar Finanças...");

            try
            {
                var financas = repositorioFinanca.SelecionarTodos();

                Log.Logger.Information("Financas selecionadas com sucesso");

                return Result.Ok(financas);
            }
            catch(Exception ex) 
            {
                string msgErro = "Falha no sistema ao tentar selecionar todas as Fianaças";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Financa> SelecionarPorId(Guid id)
        {
            Log.Logger.Debug("Tentando selecionar finanças {FinancaId}...", id);

            try
            {
                var financa = repositorioFinanca.SelecionarPorId(id);

                if (financa == null)
                {
                    Log.Logger.Warning("Finança {FinancaId} não encontrada", id);

                    return Result.Fail("Finança não encontrada");
                }

                Log.Logger.Information("Finança {FinançaId} selecionanda com sucesso", id);

                return Result.Ok(financa);
            }
            catch(Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar Finança";

                Log.Logger.Error(ex, msgErro + " {FinancaId}", id);

                return Result.Fail(msgErro);
            }
        }

        /*protected override Result Validar(Financa arg)
        {
            var validador = new ValidadorFinanca();

            var resultadoValidacao = validador.Validate(arg);

            List<Error> erros = new List<Error>();

            foreach (ValidationFailure item in resultadoValidacao.Errors)
                erros.Add(new Error(item.ErrorMessage));

            if (NomeFilmeDuplicado(arg))
                erros.Add(new Error("Título do filme duplicado"));

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }*/
    }
}
