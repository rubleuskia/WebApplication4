using System.Threading.Tasks;
using Core.Currencies.Common.Conversion;
using Core.EventBus;
using Core.EventBus.Accounting;
using DatabaseAccess.Infrastructure.UnitOfWork;

namespace Core.Accounting
{
    public class AccountTransferService : IAccountTransferService
    {
        private readonly IEventBus _eventBus;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountAcquiringService _accountAcquiringService;
        private readonly ICurrencyConversionService _currencyConversionService;

        public AccountTransferService(
            IUnitOfWork unitOfWork,
            IAccountAcquiringService accountAcquiringService,
            ICurrencyConversionService currencyConversionService,
            IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _accountAcquiringService = accountAcquiringService;
            _currencyConversionService = currencyConversionService;
            _eventBus = eventBus;
        }

        public async Task Transfer(AccountTransferParameters parameters)
        {
            var fromAccount = await _unitOfWork.Accounts.GetById(parameters.FromAccount);
            var toAccount = await _unitOfWork.Accounts.GetById(parameters.ToAccount);

            // create guid
            // account1.LockTransactions(guid); | account.State = TransferInprogres
            // account2.LockTransactions(guid);

            var withdrawAmount = await _currencyConversionService.Convert(
                parameters.CurrencyCharCode,
                fromAccount.CurrencyCharCode,
                parameters.Amount);

            // Withdraw(guid)
            // await _accountAcquiringService.Withdraw(parameters.FromAccount, withdrawAmount);

            // exception?
            // TODO: transaction logic
            // 1. persist transaction history
            // 2. rollback changes if operation fails
            var acquireAmount = await _currencyConversionService.Convert(
                parameters.CurrencyCharCode,
                toAccount.CurrencyCharCode,
                parameters.Amount);

            // await _accountAcquiringService.Acquire(parameters.ToAccount, acquireAmount);
            // finally - unlock/rollback/etc

            _eventBus.Publish(new AccountTransferEvent
            {
                Amount = parameters.Amount,
                ToAccount = toAccount.Id,
                FromAccount = fromAccount.Id
            });
        }
    }
}
