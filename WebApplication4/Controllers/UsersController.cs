using System.Threading.Tasks;
using Core.Users;
using Core.Users.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = WebApplicationConstants.Roles.Administrator)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStaticFilesService _staticFilesService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UsersController(IUserService userService, IStaticFilesService staticFilesService, IStringLocalizer<SharedResource> localizer)
        {
            _userService = userService;
            _staticFilesService = staticFilesService;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var localized = _localizer["shared_test"];
            return View(await _userService.GetViewModels());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = await _userService.GetUserViewModel(id);
            return model != null
                ? View("UserDetails", model)
                : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> CreateEdit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_CreateEdit", new UserViewModel());
            }

            var model = await _userService.GetUserViewModel(id);
            return model != null ? PartialView("_CreateEdit", model) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateEdit", model);
            }

            model.PhotoPath = await _staticFilesService.SaveProtectedStaticFile(model.Photo);
            if (string.IsNullOrEmpty(model.Id))
            {
                var result = await _userService.Create(model);
                if (!result.Succeeded)
                {
                    AddIdentityErrors(result);
                    return PartialView("_CreateEdit", model);
                }
            }
            else
            {
                await _userService.Update(model);
            }

            return RedirectToAction("Index");
        }

        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, $"(${error.Code}) ${error.Description})");
            }
        }
    }
}
