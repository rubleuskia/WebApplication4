using System.Threading.Tasks;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.Repositories.Common;

namespace DatabaseAccess.Infrastructure.Repositories.Accounts
{
    public interface IAccountsRepository : IGenericRepository<Account>
    {
        Task<Account[]> GetAll(string userId);
    }
}
