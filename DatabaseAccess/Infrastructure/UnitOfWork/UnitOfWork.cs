using System.Threading.Tasks;
using DatabaseAccess.Infrastructure.Repositories;
using DatabaseAccess.Infrastructure.Repositories.Accounts;
using DatabaseAccess.Infrastructure.Repositories.Users;

namespace DatabaseAccess.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(
            IAccountsRepository accounts,
            ApplicationContext context,
            IUsersRepository users,
            IFilesRepository files)
        {
            Accounts = accounts;
            Users = users;
            Files = files;

            _context = context;
        }

        public IAccountsRepository Accounts { get; }

        public IUsersRepository Users { get; }

        public IFilesRepository Files { get; }

        public Task Commit()
        {
            return _context.SaveChangesAsync();
        }
    }
}