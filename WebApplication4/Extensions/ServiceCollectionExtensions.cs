using System;
using Accounting;
using Accounting.Tracking;
using Common;
using Currencies.Apis.Byn;
using Currencies.Apis.Rub;
using Currencies.Common;
using Currencies.Common.Caching;
using Currencies.Common.Conversion;
using Currencies.Common.Infos;
using DatabaseAccess;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot;
using WebApplication4.Options;

namespace WebApplication4.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
        }

        public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TelegramBotOptions>(configuration.GetSection("TelegramBot"));
            services.Configure<SeedDataOptions>(configuration.GetSection("Seeds"));
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<ICurrenciesApiCacheService, CurrenciesApiCacheService>();
            services.AddSingleton<IAccountOperationsTrackingService, AccountOperationsTrackingService>();
            services.AddSingleton<GetNowAtSite>(() => DateTime.UtcNow);

            RegisterCurrenciesApi(services, configuration);

            services.AddTransient<IAccountsRepository, AccountsRepository>();
            services.AddTransient<ICurrencyInfoService, CurrencyInfoService>();
            services.AddTransient<IAccountAcquiringService, AccountAcquiringService>();
            services.AddTransient<ICurrencyConversionService, CurrencyConversionService>();
            services.AddTransient<IAccountTransferService, AccountTransferService>();
            services.AddTransient<IAccountManagementService, AccountManagementService>();
        }

        private static void RegisterCurrenciesApi(IServiceCollection services, IConfiguration configuration)
        {
            switch (configuration["CurrencyApi"])
            {
                case "BYN":
                    services.AddTransient<ICurrenciesApi, BynCurrenciesApi>();
                    break;
                case "RUB":
                    services.AddTransient<ICurrenciesApi, RubCurrenciesApi>();
                    break;
                default:
                    throw new InvalidOperationException("Cannot register currencies API: settings not provided.");
            }
        }
    }
}
