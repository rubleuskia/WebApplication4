using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Accounting;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            return await _managementService.GetAccounts("bdb94046-f01d-4080-a989-341c3e88ed50");
        }
    }
}