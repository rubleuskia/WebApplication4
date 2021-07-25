using System;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Entities;

namespace Accounting
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAccountAcquiringService _accountAcquiringService;
        private readonly IAccountTransferService _accountTransferService;

        public AccountManagementService(
            IAccountsRepository accountsRepository,
            IAccountAcquiringService accountAcquiringService,
            IAccountTransferService accountTransferService)
        {
            _accountsRepository = accountsRepository;
            _accountAcquiringService = accountAcquiringService;
            _accountTransferService = accountTransferService;
        }

        public async Task<Account> CreateAccount(string userId, string currencyCharCode)
        {
            var accountId = Guid.NewGuid();

            await _accountsRepository.Add(new Account
            {
                Amount = 0,
                Id = accountId,
                UserId = userId,
                CurrencyCharCode = currencyCharCode,
            });

            return await _accountsRepository.GetById(accountId);
        }

        public Task DeleteAccount(Guid accountId)
        {
            return _accountsRepository.Delete(accountId);
        }

        public Task Withdraw(Guid accountId, byte[] version, decimal amount)
        {
            AssertValidAmount(amount);
            return _accountAcquiringService.Withdraw(accountId, version, amount);
        }

        public Task Acquire(Guid accountId, byte[] version, decimal amount)
        {
            AssertValidAmount(amount);
            return _accountAcquiringService.Acquire(accountId, version, amount);
        }

        public Task Transfer(AccountTransferParameters parameters)
        {
            return _accountTransferService.Transfer(parameters);
        }

        public Task<Account> GetAccount(Guid accountId)
        {
            return _accountsRepository.GetById(accountId);
        }

        public Task<Account[]> GetAccounts(string userId)
        {
            return _accountsRepository.GetAll(userId);
        }

        public bool IsSupportCurrencyCharCode(string currencyCharCode)
        {
            // TODO list to constant
            return new[] {"BYN", "RUB", "EUR", "USD"}.Contains(currencyCharCode);
        }

        private static void AssertValidAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException($"Invalid amount value: {amount}");
            }
        }
    }
}
