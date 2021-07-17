using System;
using System.Threading.Tasks;
using Accounting.DataAccess.Entities;

namespace Accounting.DataAccess
{
    public interface IAccountsRepository
    {
        Task Add(Account account);
        Task Update(Account account);
        Task Delete(Guid accountId);
        Task<Account> GetById(Guid accountId);
        Task<Account[]> GetAll();
    }
}
