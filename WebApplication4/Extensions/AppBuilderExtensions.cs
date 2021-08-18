using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

public static class AppBuilderExtensions
{
    public static void ConfigurePublicStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStaticFiles();
        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "files")),
            RequestPath = "/MyImages",
        });
    }

    public static void ConfigureProtectedStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
    {
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
    }
}