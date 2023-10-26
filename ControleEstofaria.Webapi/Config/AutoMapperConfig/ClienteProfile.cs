using AutoMapper;
using ControleEstofaria.Dominio.ModuloCliente;
using ControleEstofaria.Webapi.ViewModels.ModuloCliente;

namespace ControleEstofaria.Webapi.Config.AutoMapperConfig
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile() 
        {
            CreateMap<FormsClienteViewModel, Cliente>()
                .ForMember(destino => destino.Id, opt => opt.Ignore());

            CreateMap<Cliente, ListarClienteViewModel>();

            CreateMap<Cliente, VisualizarClienteViewModel>()
                .ForMember(destino => destino.Servicos, opt => opt.MapFrom(origem => origem.Servicos));

            CreateMap<Cliente, FormsClienteViewModel>();
                
        }
    }
}
