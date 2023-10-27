using ControleEstofaria.Aplicacao.Compartilhado;
using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloCliente;
using FluentResults;
using FluentValidation.Results;
using Serilog;

namespace ControleEstofaria.Aplicacao.ModuloCliente
{
    public class ServicoCliente : ServicoBase<Cliente, ValidadorCliente>
    {
        public IRepositorioCliente repositorioCliente;
        public IContextoPersistencia contextoPersistencia;

        public ServicoCliente(IRepositorioCliente repositorioCliente, 
                                IContextoPersistencia contexto)
        {
            this.repositorioCliente = repositorioCliente;
            this.contextoPersistencia = contexto;
        }

        public Result<Cliente> Inserir(Cliente cliente)
        {
            Log.Logger.Debug("Tentando inserir Cliente... {@c}", cliente);

            Result resultado = Validar(cliente);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioCliente.Inserir(cliente);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Cliente {ClienteId} inserido com sucesso", cliente.Id);

                return Result.Ok(cliente);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir o Cliente";

                Log.Logger.Error(ex, msgErro + "{ClienteId} ", cliente.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Cliente> Editar(Cliente cliente)
        {
            Log.Logger.Debug("Tentando editar cliente... {@c}", cliente);

            var resultado = Validar(cliente);

            if(resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioCliente.Editar(cliente);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Cliente {ClienteId} editado com sucesso", cliente.Id);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar editar o Cliente";

                Log.Logger.Error(ex, msgErro + "{ClienteId}", cliente.Id);

                return Result.Fail(msgErro);
            }
            return Result.Ok(cliente);
        }

        public Result Excluir(Guid id)
        {
            var clienteResult = SelecionarPorId(id);

            if (clienteResult.IsSuccess)
                return Excluir(clienteResult.Value);

            return Result.Fail(clienteResult.Errors);
        }

        private Result Excluir(Cliente cliente)
        {
            Log.Logger.Debug("Tentando excluir cliente... {@c}", cliente);

            try
            {
                repositorioCliente.Excluir(cliente);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Cliente {ClienteId} excluido com sucesso", cliente.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar excluir o Cliente";

                if ((bool)(ex.InnerException?.Message?.StartsWith("Cannot insert the value NULL into column 'CLienteId'")))
                {
                    msgErro = "Não foi possivel remover este cliente, pois ele está vinculado a um serviço";
                }

                Log.Logger.Error(ex, msgErro + " {ClienteId}", cliente.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Cliente>> SelecionarTodos() 
        {
            Log.Logger.Debug("Tentando selecionar clientes...");

            try
            {
                var clientes = repositorioCliente.SelecionarTodos();

                Log.Logger.Information("Clientes selecionados com sucesso");

                return Result.Ok(clientes);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os Clientes";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Cliente> SelecionarPorId(Guid id)
        {
            Log.Logger.Debug("Tentando selecionar cliente {ClienteId}...", id);

            try
            {
                var filme = repositorioCliente.SelecionarPorId(id);

                if (filme == null)
                {
                    Log.Logger.Warning("Cliente {ClienteId} não encontrado", id);

                    return Result.Fail("Cliente não encontrado");
                }

                Log.Logger.Information("Cliente {ClienteId} selecionado com sucesso", id);

                return Result.Ok(filme);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Cliente";

                Log.Logger.Error(ex, msgErro + " {ClienteId}", id);

                return Result.Fail(msgErro);
            }
        }

         protected override Result Validar(Cliente arg)
         {
             var validador = new ValidadorCliente();

             var resultadoValidacao = validador.Validate(arg);

             List<Error> erros = new List<Error>();

             foreach (ValidationFailure item in resultadoValidacao.Errors)
                 erros.Add(new Error(item.ErrorMessage));

            if (NumeroCNPJDuplicado(arg))
                 erros.Add(new Error("CNPJ do cliente está duplicado"));

            if (EmailDuplicado(arg))
                erros.Add(new Error("Email do cliente está duplicado"));

            if (erros.Any())
                 return Result.Fail(erros);

             return Result.Ok();
         }
        private bool NumeroCNPJDuplicado(Cliente arg)
        {
            var cnpjEncontrado = repositorioCliente.SelecionarCNPJ(arg.CNPJ);

            return cnpjEncontrado != null &&
                   cnpjEncontrado.CNPJ == arg.CNPJ &&
                   cnpjEncontrado.Id != arg.Id;
        }

        private bool EmailDuplicado(Cliente arg)
        {
            var emailEncontrado = repositorioCliente.SelecionarEmail(arg.Email);

            return emailEncontrado != null &&
                   emailEncontrado.Email == arg.Email &&
                   emailEncontrado.Id != arg.Id;
        }
    }

}
