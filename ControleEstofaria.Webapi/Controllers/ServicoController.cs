using AutoMapper;
using ControleEstofaria.Aplicacao.ModuloServico;
using ControleEstofaria.Dominio.ModuloServico;
using ControleEstofaria.Webapi.Controllers.Compartilhado;
using ControleEstofaria.Webapi.ViewModels.ModuloServico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstofaria.Webapi.Controllers
{

    [Route("api/servicos")]
    [ApiController]
    [Authorize]
    public class ServicoController : ControleEstofariaControllerBase
    {
        private readonly ServicoServico servicoServico;
        private readonly IMapper mapeadorServico;

        public ServicoController(ServicoServico servicoServico, IMapper mapeadorServico)
        {
            this.servicoServico = servicoServico;
            this.mapeadorServico = mapeadorServico;
        }

        [HttpGet]
        public ActionResult<List<ListarServicoViewModel>> SelecionarTodos() 
        {
            var servicoResult = servicoServico.SelecionarTodos();

            if (servicoResult.IsFailed)
                return InternalError(servicoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorServico.Map<List<ListarServicoViewModel>>(servicoResult.Value)
            });
        }

        [HttpGet("Selecionar-Servicos-Prontos")]
        public ActionResult<List<ListarServicoViewModel>> SelecionarServicosProntos()
        {
            var servicoResult = servicoServico.SelecionarServicosProntos();

            if (servicoResult.IsFailed)
                return InternalError(servicoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorServico.Map<List<ListarServicoViewModel>>(servicoResult.Value)
            });
        }


        //[HttpGet("Selecionar-Servicos-Prontos-Por-Periodo")]
        //public ActionResult<List<ListarServicoViewModel>> SelecionarServicosProntosPorPeriodo(DateTime dataInicio, DateTime dataFim)
        //{
        //    var servicoResult = servicoServico.SelecionarServicosProntosPorPeriodo(dataInicio, dataFim);
        //    var ValorServicosProntos = servicoServico.repositorioServico.ObterTotalValorServicos();

        //    if (servicoResult.IsFailed)
        //        return InternalError(servicoResult);

        //    return Ok(new
        //    {
        //        sucesso = true,
        //        dados = mapeadorServico.Map<List<ListarServicoViewModel>>(servicoResult.Value),
        //        ValorServicosProntos
        //    }) ;
        //}

        [HttpGet("Somar-Servicos-Prontos-Por-Periodo")]
        public ActionResult<decimal> SomarServicosProntosPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            var servicoResult = servicoServico.SomarServicosProntosPorPeriodo(dataInicio, dataFim);

            if (servicoResult.IsFailed)
                return InternalError(servicoResult);

            return servicoResult.Value;
           
        }

      

        [HttpGet("Visualizacao-completa/{id:guid}")]
        public ActionResult<VisualizarServicoViewModel> SelecionarServicoCompleto(Guid id)
        {
            var servicoResult = servicoServico.SelecionarPorId(id);

            if(servicoResult.IsFailed && RegistroNaoEncontrado(servicoResult))
                return NotFound(servicoResult);

            if(servicoResult.IsFailed)
                return InternalError(servicoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorServico.Map<VisualizarServicoViewModel>(servicoResult.Value)
            });
        }

        [HttpGet("{id:guid}")]
        public ActionResult<FormsServicoViewModel> SelecionarServicoPorId(Guid id)
        {
            var servicoResult = servicoServico.SelecionarPorId(id);

            if (servicoResult.IsFailed && RegistroNaoEncontrado(servicoResult))
                return NotFound(servicoResult);

            if (servicoResult.IsFailed)
                return InternalError(servicoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorServico.Map<FormsServicoViewModel>(servicoResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsServicoViewModel> Inserir(FormsServicoViewModel servicoVM)
        {
            var servico = mapeadorServico.Map<Servico>(servicoVM);

            var servicoResult = servicoServico.Inserir(servico);

            if (servicoResult.IsFailed)
                return InternalError(servicoResult);

            return Ok(new
            {
                sucesso = true,
                dados = servicoVM
            });
        }


        [HttpPut("{id:guid}")]
        public ActionResult<FormsServicoViewModel> Editar(Guid id, FormsServicoViewModel servicoVM)
        {
            var servicoResult = servicoServico.SelecionarPorId(id);

            if (servicoResult.IsFailed && RegistroNaoEncontrado(servicoResult))
                return NotFound(servicoResult);

            var servico = mapeadorServico.Map(servicoVM, servicoResult.Value);

            servicoResult = servicoServico.Editar(servico);

            if (servicoResult.IsFailed)
                return InternalError(servicoResult);

            return Ok(new
            {
                sucesso = true,
                dados = servicoVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id) 
        {
            var servicoResult = servicoServico.Excluir(id);

            if(!servicoResult.IsFailed && RegistroNaoEncontrado<Servico>(servicoResult))
                return NotFound<Servico>(servicoResult);

            if(servicoResult.IsFailed)
                return InternalError<Servico>(servicoResult);

            return NoContent();
        }
    }
}
