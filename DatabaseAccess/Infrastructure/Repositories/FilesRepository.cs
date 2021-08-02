using DatabaseAccess.Entities.Files;
using DatabaseAccess.Infrastructure.Repositories.Common;

namespace DatabaseAccess.Infrastructure.Repositories
{
    public class FilesRepository : GenericRepository<FileModel>, IFilesRepository
    {
        public FilesRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}