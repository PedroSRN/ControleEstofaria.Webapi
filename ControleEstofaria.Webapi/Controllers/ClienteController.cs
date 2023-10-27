using AutoMapper;
using ControleEstofaria.Aplicacao.ModuloCliente;
using ControleEstofaria.Dominio.ModuloCliente;
using ControleEstofaria.Webapi.Controllers.Compartilhado;
using ControleEstofaria.Webapi.ViewModels.ModuloCliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstofaria.Webapi.Controllers
{
    [Route("api/Clientes")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControleEstofariaControllerBase
    {
        private readonly ServicoCliente servicoCliente;
        private readonly IMapper mapeadorClientes;

        public ClienteController(ServicoCliente servicoCliente, IMapper mapeadorClientes)
        {
            this.servicoCliente = servicoCliente;
            this.mapeadorClientes = mapeadorClientes;
        }

        [HttpGet]
        public ActionResult<List<ListarClienteViewModel>> SelecionarTodos()
        {
            var clienteResult = servicoCliente.SelecionarTodos();

            if (clienteResult.IsFailed)
                return InternalError(clienteResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorClientes.Map<List<ListarClienteViewModel>>(clienteResult.Value)
            });
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public ActionResult<VisualizarClienteViewModel> SelecionarClienteCompletoPorId(Guid id)
        {
            var clienteResult = servicoCliente.SelecionarPorId(id);

            if (clienteResult.IsFailed && RegistroNaoEncontrado(clienteResult))
                return NotFound(clienteResult);

            if (clienteResult.IsFailed)
                return InternalError(clienteResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorClientes.Map<VisualizarClienteViewModel>(clienteResult.Value)
            });
        }

        [HttpGet("{id:guid}")]
        public ActionResult<FormsClienteViewModel> SelecionarClientePorId(Guid id)
        {
            var clienteResult = servicoCliente.SelecionarPorId(id);

            if (clienteResult.IsFailed && RegistroNaoEncontrado(clienteResult))
                return NotFound(clienteResult);

            if (clienteResult.IsFailed)
                return InternalError(clienteResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorClientes.Map<FormsClienteViewModel>(clienteResult.Value)
            });
        }


        [HttpPost]
        public ActionResult<FormsClienteViewModel> Inserir(FormsClienteViewModel clienteVM)
        {
            var cliente = mapeadorClientes.Map<Cliente>(clienteVM);

            var clienteResult = servicoCliente.Inserir(cliente);

            if (clienteResult.IsFailed)
                return InternalError(clienteResult);

            return Ok(new
            { 
                sucesso = true, 
                dados = clienteVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsClienteViewModel> Editar(Guid id, FormsClienteViewModel clienteVM)
        {
            var clienteResult = servicoCliente.SelecionarPorId(id);

            if (clienteResult.IsFailed && RegistroNaoEncontrado(clienteResult))
                return NotFound(clienteResult);

            var cliente = mapeadorClientes.Map(clienteVM, clienteResult.Value);

            clienteResult = servicoCliente.Editar(cliente);

            if (clienteResult.IsFailed)
                return InternalError(clienteResult);

            return Ok(new
            {
                sucesso = true,
                dados = clienteVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id) 
        {
            var clienteResult = servicoCliente.Excluir(id);

            if(!clienteResult.IsFailed && RegistroNaoEncontrado<Cliente>(clienteResult))
                return NotFound(clienteResult);

            if (clienteResult.IsFailed)
                return InternalError<Cliente>(clienteResult);

            return NoContent();
        }
    }
}
