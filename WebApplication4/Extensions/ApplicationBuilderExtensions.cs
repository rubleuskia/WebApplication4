using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace WebApplication4.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSpa(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                builder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = $"{env.WebRootPath}/spa";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                });
            }
            else
            {
                builder.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/spa"),
                    builder =>
                    {
                        builder.UseSpa(spa => { spa.Options.SourcePath = $"{env.WebRootPath}/spa/build"; });
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
