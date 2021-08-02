using System.Threading.Tasks;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.Repositories;

namespace Accounting
{
    public interface IAccountsRepository : IGenericRepository<Account>
    {
        Task<Account[]> GetAll(string userId);
    }
}
