using System.Threading.Tasks;
using DatabaseAccess;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.Repositories;

namespace Accounting
{
    public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
    {
        public AccountsRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Account[]> GetAll(string userId)
        {
            return await GetAll(x => x.UserId == userId);
        }
    }
}
