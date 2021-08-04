using System;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.UnitOfWork;

namespace Core.Accounting
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountAcquiringService _accountAcquiringService;
        private readonly IAccountTransferService _accountTransferService;

        public AccountManagementService(
            IUnitOfWork unitOfWork,
            IAccountAcquiringService accountAcquiringService,
            IAccountTransferService accountTransferService)
        {
            _unitOfWork = unitOfWork;
            _accountAcquiringService = accountAcquiringService;
            _accountTransferService = accountTransferService;
        }

        public async Task<Account> CreateAccount(string userId, string currencyCharCode)
        {
            var accountId = Guid.NewGuid();
            await _unitOfWork.Accounts.Create(new Account
            {
                Amount = 0,
                Id = accountId,
                UserId = userId,
                CurrencyCharCode = currencyCharCode,
            });

            await _unitOfWork.Commit();
            return await _unitOfWork.Accounts.GetById(accountId);;
        }

        public async Task DeleteAccount(Guid accountId)
        {
            await _unitOfWork.Accounts.Delete(accountId);
            await _unitOfWork.Commit();
        }

        public Task Withdraw(Guid accountId, byte[] rowVersion, decimal amount)
        {
            AssertValidAmount(amount);
            return _accountAcquiringService.Withdraw(accountId, default, amount);
        }

        public async Task Acquire(Guid accountId, byte[] rowVersion, decimal amount)
        {
            AssertValidAmount(amount);
            await _accountAcquiringService.Acquire(accountId, rowVersion, amount);
            await _unitOfWork.Commit();
        }

        public Task Transfer(AccountTransferParameters parameters)
        {
            return _accountTransferService.Transfer(parameters);
        }

        public Task<Account> GetAccount(Guid accountId)
        {
            return _unitOfWork.Accounts.GetById(accountId);
        }

        public Task<Account[]> GetAccounts(string userId)
        {
            return _unitOfWork.Accounts.GetAll(userId);
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
