using ControleEstofaria.Infra.Logging;
using ControleEstofaria.Orm.Compartilhado;
using Serilog;



namespace ControleEstofaria.Webapi
{
    public class Program
    {
    public static void Main(string[] args)
    {
        ConfiguracaoLogsControleEstofaria.ConfigurarEscritaLogs();


        Log.Logger.Information("Iniciando o servidor da aplicação Controle de Cinema...");

        try
        {
            var app = CreateHostBuilder(args).Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var db = services.GetRequiredService<ControleEstofariaDbContext>();

                Log.Logger.Information("Atualizando a banco de dados do Controle de Cinema...");

                var migracaoRealizada = MigradorBancoDadosControleEstofaria.AtualizarBancoDados(db);

                if (migracaoRealizada)
                    Log.Logger.Information("Banco de dados atualizado");
                else
                    Log.Logger.Information("Nenhuma migração pendente");
            }

            Log.Logger.Information("Iniciando o servidor da aplicação Controle de Cinema...");

            app.Run();
        }
        catch (Exception exc)
        {
            Log.Logger.Fatal(exc, "O servidor da aplicação Controle de Cinema parou inesperadamente.");

        }
    }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
