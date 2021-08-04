using System;
using System.Threading.Tasks;
using DatabaseAccess.Entities.Files;
using DatabaseAccess.Infrastructure.UnitOfWork;

namespace Core.Files
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetFilePath(Guid? fileId)
        {
            if (fileId.HasValue)
            {
                var file = await _unitOfWork.Files.GetById(fileId.Value);
                return file.Path;
            }

            return null;
        }

        public async Task<Guid> Create(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(fileName))
            {
                throw new InvalidOperationException($"Cannot create file. {path}: {fileName}");
            }

            var fileId = Guid.NewGuid();
            var file = new FileModel
            {
                Id = fileId,
                Name = fileName,
                Path = path,
            };

            await _unitOfWork.Files.Create(file);
            await _unitOfWork.Commit();
            return fileId;
        }
    }
}