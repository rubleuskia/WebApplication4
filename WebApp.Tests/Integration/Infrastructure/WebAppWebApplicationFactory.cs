using System;
using System.Linq;
using DatabaseAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication4;

namespace WebApp.Tests.Integration.Infrastructure
{
    public class WebAppWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                services.Remove(descriptor);

                services.AddDbContext<ApplicationContext>(options =>
                {
                    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.tests.json", optional: false).Build();
                    var cs = configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(cs);
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationContext>();
                var logger = scopedServices.GetRequiredService<ILogger<WebAppWebApplicationFactory>>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                try
                {
                    Utilities.InitializeDbForTests(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                }
            });
        }
    }
}