using AutoMapper;
using ControleEstofaria.Dominio.ModuloCliente;
using ControleEstofaria.Dominio.ModuloServico;
using ControleEstofaria.Webapi.ViewModels.ModuloServico;

namespace ControleEstofaria.Webapi.Config.AutoMapperConfig
{
    public class ConfigurarClienteMappingAction : IMappingAction<FormsServicoViewModel, Servico>
    {
        public IRepositorioCliente repositorioCliente;

        public ConfigurarClienteMappingAction(IRepositorioCliente repositorioCliente)
        {
            this.repositorioCliente = repositorioCliente;
        }

        public void Process(FormsServicoViewModel servicoVM, Servico servico, ResolutionContext context)
        {
            servico.Cliente = repositorioCliente.SelecionarPorId(servicoVM.ClienteId);
        }
    }
}
