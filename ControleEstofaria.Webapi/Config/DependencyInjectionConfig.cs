using ControleEstofaria.Aplicacao.ModuloCliente;
using ControleEstofaria.Aplicacao.ModuloServico;
using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloCliente;
using ControleEstofaria.Dominio.ModuloServico;
using ControleEstofaria.Infra.Configs;
using ControleEstofaria.Orm.Compartilhado;
using ControleEstofaria.Orm.ModuloCliente;
using ControleEstofaria.Orm.ModuloServico;



namespace ControleEstofaria.Webapi.Config
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services)
        {
            services.AddSingleton((x) => new ConfiguracaoAplicacaoControleEstofaria().ConnectionStrings);

            services.AddScoped<ControleEstofariaDbContext>();

            services.AddScoped<IContextoPersistencia, ControleEstofariaDbContext>();

            services.AddScoped<IRepositorioCliente, RepositorioClienteOrm>();
            services.AddTransient<ServicoCliente>();

            services.AddScoped<IRepositorioServico, RepositorioServicoOrm>();
            services.AddTransient<ServicoServico>();

        }
    }
}
