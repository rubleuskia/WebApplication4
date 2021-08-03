using System;
using System.Threading.Tasks;
using DatabaseAccess.Entities.Files;
using DatabaseAccess.Infrastructure.UnitOfWork;

namespace Core.Files
{
    public class FilesService : IFilesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetFilePath(Guid fileId)
        {
            var file = await _unitOfWork.Files.GetById(fileId);
            return $"~/{file.Path}";
        }

        public async Task<Guid> SaveFile(string fileName, string filePath)
        {
            var fileId = Guid.NewGuid();
            var file = new FileModel
            {
                Id = fileId,
                Name = fileName,
                Path = filePath,
            };

            await _unitOfWork.Files.Create(file);
            await _unitOfWork.Commit();
            return fileId;
        }
    }
}