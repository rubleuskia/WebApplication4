using System.Threading.Tasks;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.Repositories.Common;

namespace DatabaseAccess.Infrastructure.Repositories.Accounts
{
    public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
    {
        public AccountsRepository(ApplicationContext context)
            : base(context)
        {
        }

        public async Task<Account[]> GetAll(string userId)
        {
            return await GetAll(x => x.UserId == userId);
        }
    }
}
