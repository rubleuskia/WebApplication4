using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace WebApplication4.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomRequestLocalization(this IApplicationBuilder builder)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("de-DE"),
                new CultureInfo("ru-RU"),
            };

            builder.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }

        public static void UseSpa(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                builder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = $"{env.WebRootPath}/angular";
                    spa.UseAngularCliServer(npmScript: "start");
                });
            }
            else
            {
                builder.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/angular"),
                    app =>
                    {
                        app.UseSpa(spa => { spa.Options.SourcePath = $"{env.WebRootPath}/spa/dist"; });
                    });
            }
        }

        public static void UsePublicFiles(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            builder.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "files")),
                RequestPath = "/MyImages",
            });
        }

        public static void UseProtectedFiles(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (!Directory.Exists("ProtectedStaticFiles"))
            {
                Directory.CreateDirectory("ProtectedStaticFiles");
            }

            builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "ProtectedStaticFiles")),
                RequestPath = "/ProtectedStaticFiles"
            });

            builder.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "ProtectedStaticFiles")),
                RequestPath = "/ProtectedImages",
            });
        }
    }
}
