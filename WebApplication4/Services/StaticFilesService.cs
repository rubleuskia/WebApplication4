using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static WebApplication4.WebApplicationConstants;

namespace WebApplication4.Services
{
    public class StaticFilesService : IStaticFilesService
    {
        private readonly IWebHostEnvironment _environment;

        public StaticFilesService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveProtectedStaticFile(IFormFile formFile)
        {
            var (relative, absolute) = GetPaths(_environment.ContentRootPath, Files.ProtectedStaticImagesPath, formFile.FileName);
            await SaveStaticFile(formFile, absolute);
            return relative;
        }

        public async Task<string> SaveStaticFile(IFormFile formFile)
        {
            var (relative, absolute) = GetPaths(_environment.WebRootPath, Files.PublicStaticImagesPath, formFile.FileName);
            await SaveStaticFile(formFile, absolute);
            return relative;
        }

        private async Task SaveStaticFile(IFormFile formFile, string absolutePath)
        {
            await using var fileStream = new FileStream(absolutePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
        }

        private (string, string) GetPaths(string root, string basePath, string fileName)
        {
            string relativePath = basePath + fileName;
            string absolutePath = root + relativePath;
            return (relativePath, absolutePath);
        }
    }
}