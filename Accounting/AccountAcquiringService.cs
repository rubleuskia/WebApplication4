using System;
using System.Threading.Tasks;
using Accounting.Exceptions;
using Common;
using Common.Accounting;

namespace Accounting
{
    public class AccountAcquiringService : IAccountAcquiringService
    {
        private readonly IEventBus _eventBus;
        private readonly IAccountsRepository _accountsRepository;

        public AccountAcquiringService(IAccountsRepository accountsRepository, IEventBus eventBus)
        {
            _accountsRepository = accountsRepository;
            _eventBus = eventBus;
        }

        public async Task Withdraw(Guid accountId, byte[] version, decimal amount)
        {
            var account = await _accountsRepository.GetById(accountId);
            if (account.Amount < amount)
            {
                throw new NotEnoughMoneyToWithdrawException("Not enough money.")
                {
                    Amount = amount,
                    AccountId = accountId,
                    OriginalAmount = account.Amount,
                };
            }

            account.Amount -= amount;
            await _accountsRepository.Update(account, version);

            _eventBus.Publish(new AccountWithdrawEvent
            {
                AccountId = accountId,
                Amount = amount
            });
        }

        public async Task Acquire(Guid accountId, byte[] version, decimal amount)
        {
            var account = await _accountsRepository.GetById(accountId);
            account.Amount += amount;
            await _accountsRepository.Update(account, version);

            _eventBus.Publish(new AccountAcquiredEvent
            {
                AccountId = accountId,
                Amount = amount
            });
        }
    }
}
