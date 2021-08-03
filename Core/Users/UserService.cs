using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Files;
using Core.Users.Models;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Core.Users
{
    public class UserService : IUserService
    {
        private readonly IFilesService _filesService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IFilesService filesService)
        {
            _unitOfWork = unitOfWork;
            _filesService = filesService;
        }

        public async Task<UserViewModel> FindUserById(string userId)
        {
            var user = await _unitOfWork.Users.FindUserById(userId);
            return await CreateViewModel(user);
        }

        public async Task<UserViewModel[]> GetUsers()
        {
            var users = await _unitOfWork.Users.Get();
            var result = new List<UserViewModel>();
            foreach (var user in users)
            {
                result.Add(await CreateViewModel(user));
            }

            return result.ToArray();
        }

        public async Task Update(UserViewModel model)
        {
            var userToUpdate = await _unitOfWork.Users.FindUserById(model.Id);
            if (userToUpdate == null)
            {
                return;
            }

            userToUpdate.Age = model.Age.Value;
            userToUpdate.Email = model.Email;
            userToUpdate.UserName = model.Email;
            userToUpdate.PhotoId = await _filesService.SaveFile(model.Photo.FileName, model.PhotoPath);

            await _unitOfWork.Users.Update(userToUpdate);
            await _unitOfWork.Commit();
        }

        public Task<IdentityResult> Create(CreateUserViewModel model)
        {
            var user = new User
            {
                Age = model.Age.Value,
                Email = model.Email,
                UserName = model.Email
            };

            return _unitOfWork.Users.Create(user, model.Password);
        }

        private async Task<UserViewModel> CreateViewModel(User user)
        {
            return new()
            {
                Age = user.Age,
                Id = user.Id,
                Email = user.Email,
                PhotoPath = await GetPhotoPath(user),
            };
        }

        private async Task<string> GetPhotoPath(User user)
        {
            return user.PhotoId.HasValue ? await _filesService.GetFilePath(user.PhotoId.Value) : null;
        }
    }
}