using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Accounting;
using Core.Accounting.Dtos;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Extensions;

namespace WebApplication4.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsApiController : ControllerBase
    {
        private readonly IAccountManagementService _managementService;

        public AccountsApiController(IAccountManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpGet]
        public async Task<AccountDto[]> GetAccounts()
        {
            Account[] accounts = await _managementService.GetAccounts(User.GetUserId());
            return accounts.Select(x => new AccountDto
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    CurrencyName = GetCurrencyFullName(x.CurrencyCharCode),
                })
                .ToArray();
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> Create(CreateAccountDto dto)
        {
            if (!_managementService.IsSupportCurrencyCharCode(dto.Currency))
            {
                throw new InvalidOperationException("Unknown currency code");
            }

            var account = await _managementService.CreateAccount(User.GetUserId(), dto.Currency);
            await _managementService.Acquire(account.Id, account.RowVersion, dto.Amount);
            return account.Id;
        }

        private string GetCurrencyFullName(string currencyCharCode)
        {
            switch (currencyCharCode)
            {
                case "BYN":
                    return "Belorussian ruble (BYN)";
                case "RUB":
                    return "Russian ruble (RUB)";
                case "EUR":
                    return "Euro (EUR)";
                case "USD":
                    return "US dollar (USD)";
                default:
                    throw new ArgumentOutOfRangeException(currencyCharCode);
            }
        }
    }
}