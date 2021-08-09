using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using WebApplication4.Extensions;
using WebApplication4.HostedServices;

namespace WebApplication4
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment _webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = _webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            services.RegisterDependencies(Configuration);
            services.RegisterOptions(Configuration);
            services.RegisterEntityFramework();
            services.AddHttpContextAccessor();
            services.AddHostedService<TelegramHostedService>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/UserAccount/Login";
                options.AccessDeniedPath = "/UserAccount/Login";
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = $"{WebHostEnvironment.WebRootPath}/spa";
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // | => | => | => | =>
            //   <= | <= |
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // web application root
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "files")),
                RequestPath = "/MyImages",
            });

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "ProtectedStaticFiles")),
                RequestPath = "/ProtectedStaticFiles"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "ProtectedStaticFiles")),
                RequestPath = "/ProtectedImages",
            });

            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "file-browser",
                    pattern: "MyImages/{fileName}",
                    defaults: new { controller = "Images", action = "Download" });
            });

            app.MapWhen(IsSpaRoute, cfg =>
            {
                cfg.UseSpa(spa =>
                {
                    spa.Options.SourcePath = $"{env.WebRootPath}/spa";

                    if (env.IsDevelopment())
                    {
                        spa.UseReactDevelopmentServer(npmScript: "start");
                    }
                });
            });
        }

        private static bool IsSpaRoute(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/spa", StringComparison.OrdinalIgnoreCase) ||
                   context.Request.Path.StartsWithSegments("/static", StringComparison.OrdinalIgnoreCase);
        }
    }
}
