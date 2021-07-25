using System.Threading.Tasks;
using Common;
using Common.Accounting;
using Currencies.Common.Conversion;

namespace Accounting
{
    public class AccountTransferService : IAccountTransferService
    {
        private readonly IEventBus _eventBus;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAccountAcquiringService _accountAcquiringService;
        private readonly ICurrencyConversionService _currencyConversionService;

        public AccountTransferService(
            IAccountsRepository accountsRepository,
            IAccountAcquiringService accountAcquiringService,
            ICurrencyConversionService currencyConversionService,
            IEventBus eventBus)
        {
            _accountsRepository = accountsRepository;
            _accountAcquiringService = accountAcquiringService;
            _currencyConversionService = currencyConversionService;
            _eventBus = eventBus;
        }

        public async Task Transfer(AccountTransferParameters parameters)
        {
            var fromAccount = await _accountsRepository.GetById(parameters.FromAccount);
            var toAccount = await _accountsRepository.GetById(parameters.ToAccount);

            var withdrawAmount = await _currencyConversionService.Convert(
                parameters.CurrencyCharCode,
                fromAccount.CurrencyCharCode,
                parameters.Amount);

            await _accountAcquiringService.Withdraw(parameters.FromAccount, parameters.FromVersion, withdrawAmount);

            var acquireAmount = await _currencyConversionService.Convert(
                parameters.CurrencyCharCode,
                toAccount.CurrencyCharCode,
                parameters.Amount);

            await _accountAcquiringService.Acquire(parameters.ToAccount, parameters.ToVersion, acquireAmount);

            _eventBus.Publish(new AccountTransferEvent
            {
                Amount = parameters.Amount,
                ToAccount = toAccount.Id,
                FromAccount = fromAccount.Id
            });
        }
    }
}
