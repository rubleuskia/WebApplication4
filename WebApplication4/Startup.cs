using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/UserAccount/Login";
                options.AccessDeniedPath = "/UserAccount/Login";
            });
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
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
            var protectedStaticFilesPath = Path.Combine(env.ContentRootPath, "ProtectedStaticFiles");
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(protectedStaticFilesPath),
                RequestPath = "/MyImages",
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            Directory.CreateDirectory(protectedStaticFilesPath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(protectedStaticFilesPath),
                RequestPath = "/ProtectedStaticFiles",
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "file-browser",
                    pattern: "MyImages/{fileName}",
                    defaults: new { controller = "Images", action = "Download" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
