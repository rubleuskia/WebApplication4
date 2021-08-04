using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication4.Services
{
    public interface IStaticFilesService
    {
        Task<string> SaveProtectedStaticFile(IFormFile formFile);
        Task<string> SaveStaticFile(IFormFile formFile);
    }
}