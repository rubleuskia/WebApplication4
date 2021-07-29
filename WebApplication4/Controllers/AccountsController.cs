using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting;
using Accounting.Exceptions;
using DatabaseAccess;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAccountManagementService _managementService;

        public AccountsController(IAccountManagementService managementService, UserManager<User> userManager)
        {
            _managementService = managementService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var accounts = await _managementService.GetAccounts(userId);
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
        public async Task<IActionResult> CreateAccount(AccountViewModel model, byte[] modelRowVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_managementService.IsSupportCurrencyCharCode(model.CurrencyCharCode))
            {
                ModelState.AddModelError("CurrencyCharCode", "Not supported currency code.");
                model.AvailableCharCodes = GetAvailableCharCodes();
                return View("Create", model);
            }

            var userId = _userManager.GetUserId(User);
            var accountId = await _managementService.CreateAccount(userId, model.CurrencyCharCode);
            await _managementService.Acquire(accountId, modelRowVersion, model.Amount);

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
                CurrencyCharCode = account.CurrencyCharCode,
                RowVersion = account.RowVersion,
            });
        }

        [HttpPost]
        public async Task<IActionResult> PerformAcquire(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _managementService.Acquire(model.Id, model.RowVersion, model.InputAmount);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Log.Error(e, $"Cannot acquire amount ot account {model.Id}");
                ModelState.AddModelError(string.Empty, "Account has been modified, please refresh page and try again.");
                return View("Acquire", model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Transfer()
        {
            var userId = _userManager.GetUserId(User);
            var accounts = await _managementService.GetAccounts(userId);

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
                var userId = _userManager.GetUserId(User);
                var accounts = await _managementService.GetAccounts(userId);

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
