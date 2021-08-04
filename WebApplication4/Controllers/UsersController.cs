using System.Threading.Tasks;
using Core.Users;
using Core.Users.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = WebApplicationConstants.Roles.Administrator)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStaticFilesService _staticFilesService;

        public UsersController(IUserService userService, IStaticFilesService staticFilesService)
        {
            _userService = userService;
            _staticFilesService = staticFilesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
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
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _userService.GetUserViewModel(id);
            return model != null ? View(model) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            userViewModel.PhotoPath = await _staticFilesService.SaveProtectedStaticFile(userViewModel.Photo);
            await _userService.Update(userViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.Create(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            AddIdentityErrors(result);
            return View(model);
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
