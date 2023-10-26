using AutoMapper;
using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloServico;
using ControleEstofaria.Webapi.ViewModels.ModuloServico;

namespace ControleEstofaria.Webapi.Config.AutoMapperConfig
{
    public class ServicoProfile : Profile
    {
        public ServicoProfile()
        {
            CreateMap<FormsServicoViewModel, Servico>()
                .ForMember(destino => destino.Id, opt => opt.Ignore())
                .AfterMap<ConfigurarClienteMappingAction>();

            CreateMap<Servico, ListarServicoViewModel>()
                .ForMember(d => d.DataEntradaServico, opt => opt.MapFrom(o => o.DataEntradaServico.ToShortDateString()))
                .ForMember(d => d.DataSaidaServico, opt => opt.MapFrom(o => o.DataSaidaServico.ToShortDateString()))
                
                .ForMember(d => d.FormaPagamento, opt => opt.MapFrom(o => o.FormaPagamento.GetDescription()))
                .ForMember(d => d.StatusServico, opt => opt.MapFrom(o => o.StatusServico.GetDescription()))

                .ForMember(d => d.NomeCliente, opt => opt.MapFrom(o => o.Cliente.Nome))
                .ForMember(d => d.ValorServico, opt => opt.MapFrom(o => o.ValorServico));

            CreateMap<Servico, VisualizarServicoViewModel>()
                .ForMember(d => d.FormaPagamento, opt => opt.MapFrom(o => o.FormaPagamento.GetDescription()))
                .ForMember(d => d.StatusServico, opt => opt.MapFrom(o => o.StatusServico.GetDescription()));

            CreateMap<FormsServicoViewModel, Servico>();



        }
    }
}
