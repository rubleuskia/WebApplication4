using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WebApplication4.Services
{
    public class StaticFilesService : IStaticFilesService
    {
        private readonly IWebHostEnvironment _environment;

        public StaticFilesService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImage(IFormFile formFile)
        {
            string relativePath = Path.Combine("ProtectedStaticFiles", formFile.FileName);
            var absolutePath = Path.Combine(_environment.ContentRootPath, relativePath);
            await using var fileStream = new FileStream(absolutePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
            return relativePath;
        }
    }
}