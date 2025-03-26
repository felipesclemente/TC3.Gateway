using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;

namespace Contatos.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(path: "Logs/ContatosGatewayLogs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Configuration.AddJsonFile("ocelot.json");

                builder.Services.AddSerilog();

                builder.Services.AddAuthorization();
                builder.Services.AddOpenApi();

                builder.Services.AddOcelot();

                var app = builder.Build();

                app.UseOcelot().Wait();

                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();

                app.Run();
            }
            catch (Exception ex) 
            {
                Log.Fatal($"A API Gateway encerrou inesperadamente. Exception: {ex.GetType()}. Message: {ex.Message}.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
