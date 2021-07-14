using System;
using System.Linq;
using System.Threading.Tasks;
using Accounting;
using Accounting.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountManagementService _managementService;

        public AccountsController(IAccountManagementService managementService)
        {
            _managementService = managementService;
        }

        public async Task<IActionResult> Index()
        {
            var id1 = await _managementService.CreateAccount(Guid.Empty, "BYN");
            await _managementService.Acquire(id1, 1000);

            var id2 = await _managementService.CreateAccount(Guid.Empty, "RUB");
            await _managementService.Acquire(id2, 1000);

            var accounts = await _managementService.GetAccounts();
            var viewModels = accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Amount = x.Amount,
                CurrencyName = GetCurrencyFullName(x.CurrencyCharCode),
            }).ToArray();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AccountViewModel
            {
                AvailableCharCodes = GetAvailableCharCodes()
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_managementService.IsSupportCurrencyCharCode(model.CurrencyCharCode))
            {
                ModelState.AddModelError("CurrencyCharCode", "Not supported currency code.");
                model.AvailableCharCodes = GetAvailableCharCodes();
                return View("Create", model);
            }

            var accountId = await _managementService.CreateAccount(Guid.Empty, model.CurrencyCharCode);
            await _managementService.Acquire(accountId, model.Amount);

            Log.Information($"Account with ID: {accountId} created");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Acquire(Guid id)
        {
            var account = await _managementService.GetAccount(id);
            return View(new AccountViewModel
            {
                Amount = account.Amount,
                Id = account.Id,
                InputAmount = 0,
                CurrencyCharCode = account.CurrencyCharCode
            });
        }

        [HttpPost]
        public async Task<IActionResult> PerformAcquire(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _managementService.Acquire(model.Id, model.InputAmount);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Transfer()
        {
            var accounts = await _managementService.GetAccounts();

            return View(new TransferViewModel
            {
                AvailableCharCodes = GetAvailableCharCodes(),
                Accounts = accounts
                    .Select(x => new SelectListItem($"{x.Id} ({x.CurrencyCharCode})", x.Id.ToString()))
                    .ToArray()
            });
        }

        public async Task<IActionResult> PerformTransfer(TransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (model.From == model.To)
            {
                var accounts = await _managementService.GetAccounts();

                ModelState.AddModelError("To", "Cannot transfer money to the same account.");
                // TODO into common method
                model.Accounts = accounts
                    .Select(x => new SelectListItem($"{x.Id} ({x.CurrencyCharCode})", x.Id.ToString()))
                    .ToArray();

                return View("Transfer", model);
            }

            try
            {
                await _managementService.Transfer(new AccountTransferParameters
                {
                    Amount = model.Amount,
                    FromAccount = model.From.Value,
                    ToAccount = model.To.Value,
                    CurrencyCharCode = model.CurrencyCharCode,
                });
            }
            catch (NotEnoughMoneyToWithdrawException ex)
            {
                Log.Error(ex, "Error occured during transfer operation.");
                ModelState.AddModelError("Amount", $"Not enough money on account {ex.AccountId}: current amount - {ex.OriginalAmount}.");
                return View("Transfer", model);
            }

            return RedirectToAction("Index");
        }

        private SelectListItem[] GetAvailableCharCodes()
        {
            return new []
            {
                new SelectListItem(GetCurrencyFullName("BYN"), "BYN"),
                new SelectListItem(GetCurrencyFullName("RUB"), "RUB"),
                new SelectListItem(GetCurrencyFullName("USD"), "USD"),
                new SelectListItem(GetCurrencyFullName("EUR"), "EUR"),
                new SelectListItem("INVALID", "Some invalid data"),
            };
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
