using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess;
using DatabaseAccess.Entities;
using DatabaseAccess.Entities.Files;
using DatabaseAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = ApplicationConstants.Roles.Administrator)]
    public class UsersController : Controller
    {
        // private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public UsersController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ApplicationContext context)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _unitOfWork.Users.Get();
            var result = new List<UserViewModel>();
            foreach (var user in users)
            {
                result.Add(new UserViewModel
                {
                    Age = user.Age,
                    Id = user.Id,
                    Email = user.Email,
                    PhotoPath = await GetPhotoPath(user),
                });
            }

            return View(result.ToArray());
        }

        private async Task<string> GetPhotoPath(User x)
        {
            var path = x.PhotoId.HasValue ? (await _unitOfWork.Files.GetById(x.PhotoId.Value)).Path : null;
            return $"~/{path}";
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _unitOfWork.Users.FindUserById(id);
            if (user == null)
            {
                return BadRequest();
            }

            return View("UserDetails", new UserViewModel
            {
                Age = user.Age,
                Email = user.Email,
                Id = user.Id,
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _unitOfWork.Users.FindUserById(id);
            if (user == null)
            {
                return BadRequest();
            }

            return View(new UserViewModel
            {
                Age = user.Age,
                Email = user.Email,
                Id = user.Id,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userToUpdate = await _unitOfWork.Users.FindUserById(userViewModel.Id);
            if (userToUpdate == null)
            {
                return BadRequest();
            }

            var uploadedFile = userViewModel.Photo;
            string path = "/files/" + uploadedFile.FileName;
            await using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            var fileId = Guid.NewGuid();
            var file = new FileModel
            {
                Id = fileId,
                Name = uploadedFile.FileName,
                Path = path,
            };

            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

            userToUpdate.Age = userViewModel.Age.Value;
            userToUpdate.Email = userViewModel.Email;
            userToUpdate.UserName = userViewModel.Email;
            userToUpdate.PhotoId = fileId;

            await _unitOfWork.Users.Update(userToUpdate);
            await _unitOfWork.Commit();

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

            var user = new User
            {
                Age = model.Age.Value,
                Email = model.Email,
                UserName = model.Email
            };

            IdentityResult result = await _unitOfWork.Users.Create(user, model.Password);
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
