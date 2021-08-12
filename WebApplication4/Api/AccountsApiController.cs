using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Accounting;
using Core.Accounting.Dtos;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Api
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsApiController : ControllerBase
    {
        private readonly IAccountManagementService _managementService;

        public AccountsApiController(IAccountManagementService managementService)
        {
            _managementService = managementService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<AccountDto[]> GetAccounts()
        {
            Account[] accounts = await _managementService.GetAccounts("bdb94046-f01d-4080-a989-341c3e88ed50");
            return accounts.Select(x => new AccountDto
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    CurrencyName = GetCurrencyFullName(x.CurrencyCharCode),
                })
                .ToArray();
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