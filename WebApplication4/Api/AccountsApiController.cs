using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Accounting;
using Core.Accounting.Dtos;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApplication4.Extensions;

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

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await _managementService.GetAccounts("bdb94046-f01d-4080-a989-341c3e88ed50");
        }

        [HttpPost]
        public async Task CreateAccount(AccountDto dto)
        {
            if (!_managementService.IsSupportCurrencyCharCode(dto.CurrencyCharCode))
            {
                throw new InvalidOperationException("Unsupported currency code");
            }

            var account = await _managementService.CreateAccount("bdb94046-f01d-4080-a989-341c3e88ed50", dto.CurrencyCharCode);
            await _managementService.Acquire(account.Id, account.RowVersion, dto.Amount);
        }
    }
}
