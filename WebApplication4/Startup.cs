using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication4.Extensions;
using WebApplication4.HostedServices;

namespace WebApplication4
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDependencies(Configuration);
            services.RegisterOptions(Configuration);
            services.RegisterEntityFramework(Configuration);
            services.AddHttpContextAccessor();
            services.AddHostedService<TelegramHostedService>();
            services.RegisterAutoMapper();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/UserAccount/Login";
                options.AccessDeniedPath = "/UserAccount/Login";
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: WebApplicationConstants.Cors.PolicyName,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddSpaStaticFiles(configuration =>
            {
                var publishPath = WebHostEnvironment.IsProduction() ? "/build" : string.Empty;
                configuration.RootPath = $"{WebHostEnvironment.WebRootPath}/react/{publishPath}";
            });

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDirectoryBrowser();
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            // app.ConfigurePublicStaticFiles(env);
            app.UseRouting();
            app.UseCors(WebApplicationConstants.Cors.PolicyName);
            app.UseAuthentication();
            app.UseAuthorization();
            // app.ConfigureProtectedStaticFiles(env);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "file-browser",
                    pattern: "MyImages/{fileName}",
                    defaults: new { controller = "Images", action = "Download" });
            });

            if (env.IsDevelopment())
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = $"{env.WebRootPath}/react";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                });
            }
            else
            {
                app.MapWhen(IsSpaRoute, builder =>
                {
                    builder.UseSpa(spa =>
                    {
                        spa.Options.SourcePath = $"{env.WebRootPath}/react/build";
                    });
                });
            }
        }

        private static bool IsSpaRoute(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/react");
        }
    }
}
