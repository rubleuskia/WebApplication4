using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Users;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = WebApplicationConstants.Roles.Administrator)]
    public class RolesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index() => View(await _unitOfWork.Users.GetRoles());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError(string.Empty, "Cannot create a role without a name.");
                return View(name);
            }

            IdentityResult result = await _unitOfWork.Users.CreateRole(name);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _unitOfWork.Users.FindRoleById(id);
            if (role != null)
            {
                await _unitOfWork.Users.DeleteRole(role);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _unitOfWork.Users.Get();
            return View(users.Select(x => new UserViewModel {Id = x.Id, Email = x.Email,}).ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _unitOfWork.Users.FindUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await  _unitOfWork.Users.GetUserRoleIds(user);
            var allRoles = (await _unitOfWork.Users.GetRoles()).ToList();
            var model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            User user = await _unitOfWork.Users.FindUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _unitOfWork.Users.GetUserRoleIds(user);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);

            await _unitOfWork.Users.AddToRolesAsync(user, addedRoles);
            await _unitOfWork.Users.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("UserList");

        }
    }
}