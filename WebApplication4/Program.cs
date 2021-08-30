using System;
using System.Threading.Tasks;
using DatabaseAccess;
using DatabaseAccess.Infrastructure.Initializers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebApplication4
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var host = CreateWebHostBuilder(args).Build();

            await SeedDatabase(host);

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    IHostEnvironment env = context.HostingEnvironment;

                    builder
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                        .AddJsonFile("appsettings.Personal.json", true, true);
                })
                .UseStartup<Startup>();

        private static async Task SeedDatabase(IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationContext>();
                foreach (var initializer in services.GetServices<IDatabaseInitializer>())
                {
                    await initializer.Execute(context);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot initialize database.");
            }
        }
    }
}
