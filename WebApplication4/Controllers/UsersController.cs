using System.Threading.Tasks;
using Core.Users;
using Core.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = ApplicationConstants.Roles.Administrator)]
    public class UsersController : Controller
    {
        private readonly IStaticFilesService _staticFilesService;
        private readonly IUserService _userService;

        public UsersController(IStaticFilesService staticFilesService, IUserService userService)
        {
            _staticFilesService = staticFilesService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUsers());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userModel = await _userService.FindUserById(id);
            if (userModel == null)
            {
                return BadRequest();
            }

            return View("UserDetails", userModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var userModel = await _userService.FindUserById(id);
            if (userModel == null)
            {
                return BadRequest();
            }

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            userViewModel.PhotoPath = await _staticFilesService.SaveImage(userViewModel.Photo);
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

            IdentityResult result = await _userService.Create(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, $"(${error.Code}) ${error.Description})");
            }

            return View(model);
        }
    }
}
