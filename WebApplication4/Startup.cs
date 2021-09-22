using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication4.Extensions;
using WebApplication4.HostedServices;

namespace WebApplication4
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDependencies(Configuration);
            services.RegisterOptions(Configuration);
            services.RegisterEntityFramework(Configuration);
            services.AddHttpContextAccessor();
            services.AddHostedService<TelegramHostedService>();
            services.RegisterAutoMapper();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
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
                var buildPath = HostEnvironment.IsProduction() ? "/dist" : string.Empty;
                configuration.RootPath = $"{HostEnvironment.WebRootPath}/angular{buildPath}";
            });

            services.AddControllersWithViews();
            if (HostEnvironment.IsDevelopment())
            {
                services.AddRazorPages().AddRazorRuntimeCompilation();
            }

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCustomRequestLocalization();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UsePublicFiles(HostEnvironment);
            app.UseRouting();
            app.UseCors(WebApplicationConstants.Cors.PolicyName);
            //------------------------
            // app.UseAuthentication();
            // app.UseAuthorization();
            //------------------------
            app.UseProtectedFiles(HostEnvironment);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "file-browser",
                    pattern: "MyImages/{fileName}",
                    defaults: new { controller = "Images", action = "Download" });
            });
            app.UseSpa(HostEnvironment);
        }
    }
}
