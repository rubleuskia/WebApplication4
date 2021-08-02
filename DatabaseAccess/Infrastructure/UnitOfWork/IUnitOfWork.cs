using System.Threading.Tasks;
using DatabaseAccess.Infrastructure.Repositories;
using DatabaseAccess.Infrastructure.Repositories.Accounts;
using DatabaseAccess.Infrastructure.Repositories.Users;

namespace DatabaseAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAccountsRepository Accounts { get; }
        IUsersRepository Users { get; }
        IFilesRepository Files { get; }

        Task Commit();
    }
}