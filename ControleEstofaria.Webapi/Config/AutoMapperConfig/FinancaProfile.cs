using AutoMapper;
using ControleEstofaria.Dominio.ModuloFinanca;
using ControleEstofaria.Webapi.ViewModels.ModuloFinanca;

namespace ControleEstofaria.Webapi.Config.AutoMapperConfig
{
    public class FinancaProfile : Profile
    {
        public FinancaProfile() 
        {
            CreateMap<FormsFinancaViewModel, Financa>()
                .ForMember(destino => destino.Id, opt => opt.Ignore());

            CreateMap<Financa, ListarFinancaViewModel>()
                .ForMember(d => d.Saldo, opt => opt.MapFrom(o => o.Saldo));

            CreateMap<Financa, VisualizarFinancaViewModel>();
               


        }
    }
}
