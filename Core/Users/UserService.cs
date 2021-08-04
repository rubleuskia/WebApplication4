using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Files;
using Core.Users.Model;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Core.Users
{
    public class UserService : IUserService
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<UserViewModel[]> GetViewModels()
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
                    PhotoPath = MapToLocalFile(await _fileService.GetFilePath(user.PhotoId)),
                    PhotoBase64 = GetBase64String(user.DatabasePhoto),
                });
            }

            return result.ToArray();
        }

        public async Task<UserViewModel> GetUserViewModel(string id)
        {
            var user = await _unitOfWork.Users.FindUserById(id);
            if (user == null)
            {
                return null;
            }

            //  return
            return new UserViewModel
            {
                Age = user.Age,
                Email = user.Email,
                Id = user.Id,
                PhotoPath = MapToLocalFile(await _fileService.GetFilePath(user.PhotoId)),
                PhotoBase64 = GetBase64String(user.DatabasePhoto),
            };
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
            userToUpdate.DatabasePhoto = GetPhotoBinaryData(model);
            userToUpdate.PhotoId = await _fileService.Create(model.PhotoPath, model.Photo.FileName);

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

        private static byte[] GetPhotoBinaryData(UserViewModel model)
        {
            using var binaryReader = new BinaryReader(model.Photo.OpenReadStream());
            return binaryReader.ReadBytes((int) model.Photo.Length);
        }

        private static string GetBase64String(byte[] bytes)
        {
            return bytes != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}" : null;
        }

        private static string MapToLocalFile(string path) => $"~{path}";
    }
}