using System;
using Accounting;
using Accounting.Tracking;
using Common;
using Core.Files;
using Core.Users;
using Currencies.Apis.Byn;
using Currencies.Apis.Rub;
using Currencies.Common;
using Currencies.Common.Caching;
using Currencies.Common.Conversion;
using Currencies.Common.Infos;
using DatabaseAccess;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.BeforeCommitHandlers;
using DatabaseAccess.Infrastructure.Repositories.Accounts;
using DatabaseAccess.Infrastructure.Repositories.Common;
using DatabaseAccess.Infrastructure.Repositories.Users;
using DatabaseAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot;
using WebApplication4.Options;
using WebApplication4.Services;

namespace WebApplication4.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IBeforeCommitHandler, CreateEntityBeforeCommitHandler>();
            services.AddTransient<IBeforeCommitHandler, UpdateEntityBeforeCommitHandler>();
            services.AddTransient<IBeforeCommitHandler, VersionedEntityBeforeCommitHandler>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IAccountsRepository, AccountsRepository>();
        }

        public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TelegramBotOptions>(configuration.GetSection("TelegramBot"));
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStaticFilesService, StaticFilesService>();
            services.AddTransient<IFilesService, FilesService>();
            services.AddTransient<ICurrencyInfoService, CurrencyInfoService>();
            services.AddTransient<IAccountAcquiringService, AccountAcquiringService>();
            services.AddTransient<ICurrencyConversionService, CurrencyConversionService>();
            services.AddTransient<IAccountTransferService, AccountTransferService>();
            services.AddTransient<IAccountManagementService, AccountManagementService>();

            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<ICurrenciesApiCacheService, CurrenciesApiCacheService>();
            services.AddSingleton<IAccountOperationsTrackingService, AccountOperationsTrackingService>();
            services.AddSingleton<GetNowAtSite>(() => DateTime.UtcNow);

            RegisterCurrenciesApi(services, configuration);
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
