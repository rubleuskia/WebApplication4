using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "files", fileName);
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/force-download", fileName);
        }
    }
}