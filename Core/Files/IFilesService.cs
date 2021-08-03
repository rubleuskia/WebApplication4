using System;
using System.Threading.Tasks;

namespace Core.Files
{
    public interface IFilesService
    {
        Task<string> GetFilePath(Guid fileId);
        Task<Guid> SaveFile(string fileName, string filePath);
    }
}