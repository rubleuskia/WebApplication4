using DatabaseAccess.Entities.Files;
using DatabaseAccess.Infrastructure.Repositories.Common;

namespace DatabaseAccess.Infrastructure.Repositories
{
    public interface IFilesRepository : IGenericRepository<FileModel>
    {
    }
}