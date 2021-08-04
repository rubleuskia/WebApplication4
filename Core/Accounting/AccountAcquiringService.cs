using System;
using System.Threading.Tasks;
using Core.Accounting.Exceptions;
using Core.Common;
using Core.Common.Accounting;
using DatabaseAccess.Infrastructure.UnitOfWork;

namespace Core.Accounting
{
    // Action => delegate void Name();
    // Action<Guid, decimal> => delegate void EventHandler(Guid accountId, decimal amount);
    public class AccountAcquiringService : IAccountAcquiringService
    {
        private readonly IEventBus _eventBus;
        private readonly IUnitOfWork _unitOfWork;

        public AccountAcquiringService(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        // transfer or direct (?)
        public async Task Withdraw(Guid accountId, byte[] rowVersion, decimal amount)
        {
            var account = await _unitOfWork.Accounts.GetById(accountId);

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
            await _unitOfWork.Accounts.Update(account);

            _eventBus.Publish(new AccountWithdrawEvent
            {
                AccountId = accountId,
                Amount = amount
            });
        }


        public async Task Acquire(Guid accountId, byte[] rowVersion, decimal amount)
        {
            var account = await _unitOfWork.Accounts.GetById(accountId);

            account.Amount += amount;
            account.RowVersion = rowVersion;

            await _unitOfWork.Accounts.Update(account);

            _eventBus.Publish(new AccountAcquiredEvent
            {
                AccountId = accountId,
                Amount = amount
            });
        }
    }
}
