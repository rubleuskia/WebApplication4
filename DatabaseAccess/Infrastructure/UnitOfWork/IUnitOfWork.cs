using System.Threading.Tasks;
using DatabaseAccess.Entities.Files;
using DatabaseAccess.Infrastructure.Repositories.Accounts;
using DatabaseAccess.Infrastructure.Repositories.Common;
using DatabaseAccess.Infrastructure.Repositories.Users;

namespace DatabaseAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAccountsRepository Accounts { get; }
        IUsersRepository Users { get; }
        IGenericRepository<FileModel> Files { get; }

        Task Commit();
    }
}