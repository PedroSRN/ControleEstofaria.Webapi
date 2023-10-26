using AutoMapper;
using ControleEstofaria.Dominio.ModuloAutenticacao;
using ControleEstofaria.Webapi.ViewModels.ModuloAutenticacao;

namespace ControleEstofaria.Webapi.Config.AutoMapperConfig
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<RegistrarUsuarioViewModel, Usuario>()
                .ForMember(destino => destino.EmailConfirmed, opt => opt.MapFrom(origem => true))
                .ForMember(destino => destino.UserName, opt => opt.MapFrom(origem => origem.Email));
        }
    }
}
