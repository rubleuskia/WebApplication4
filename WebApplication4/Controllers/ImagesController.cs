using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ImagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            var protectedStaticFilesPath = Path.Combine(_environment.ContentRootPath, "ProtectedStaticFiles", fileName);
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(protectedStaticFilesPath);
            return File(fileBytes, "application/force-download", fileName);
        }
    }
}