using System;
using Core.Accounting;
using Core.Accounting.Tracking;
using Core.Common;
using Core.Currencies.Apis.Byn;
using Core.Currencies.Apis.Rub;
using Core.Currencies.Common;
using Core.Currencies.Common.Caching;
using Core.Currencies.Common.Conversion;
using Core.Currencies.Common.Infos;
using Core.Files;
using Core.TelegramBot;
using Core.Users;
using DatabaseAccess;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.BeforeCommitHandlers;
using DatabaseAccess.Infrastructure.Repositories.Accounts;
using DatabaseAccess.Infrastructure.Repositories.Common;
using DatabaseAccess.Infrastructure.Repositories.Users;
using DatabaseAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication4.Options;
using WebApplication4.Services;

namespace WebApplication4.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileService, FileService>();
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

            services.AddTransient<IStaticFilesService, StaticFilesService>();

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
