using System;
using System.Threading.Tasks;
using DatabaseAccess.Entities;

namespace Accounting
{
    public interface IAccountManagementService
    {
        Task<Account> CreateAccount(string userId, string currencyCharCode);

        Task DeleteAccount(Guid accountId);

        Task Withdraw(Guid accountId, byte[] version, decimal amount);

        Task Acquire(Guid accountId, byte[] version, decimal amount);

        Task Transfer(AccountTransferParameters parameters);

        Task<Account> GetAccount(Guid accountId);
        Task<Account[]> GetAccounts(string userId);
        bool IsSupportCurrencyCharCode(string currencyCharCode);
    }
}
