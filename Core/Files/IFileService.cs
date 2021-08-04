using System;
using System.Threading.Tasks;

namespace Core.Files
{
    public interface IFileService
    {
        Task<string> GetFilePath(Guid? fileId);
        Task<Guid> Create(string path, string fileName);
    }
}