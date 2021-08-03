using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication4.Services
{
    public interface IStaticFilesService
    {
        Task<string> SaveImage(IFormFile file);
    }
}