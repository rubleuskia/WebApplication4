using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "wwwroot/spa"; });
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

            app.UseCors(WebApplicationConstants.Cors.PolicyName);

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "file-browser",
                    pattern: "MyImages/{fileName}",
                    defaults: new { controller = "Images", action = "Download" });
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = $"{env.WebRootPath}/spa";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
