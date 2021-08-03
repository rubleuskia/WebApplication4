using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IFilesService filesService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _filesService = filesService;
            _mapper = mapper;
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

            if (model.Photo != null && !string.IsNullOrEmpty(model.PhotoPath))
            {
                userToUpdate.PhotoId = await _filesService.SaveFile(model.Photo.FileName, model.PhotoPath);
            }

            _mapper.Map(model, userToUpdate);
            await _unitOfWork.Users.Update(userToUpdate);
            await _unitOfWork.Commit();
        }

        public Task<IdentityResult> Create(CreateUserViewModel model)
        {
            var user = _mapper.Map<User>(model);
            return _unitOfWork.Users.Create(user, model.Password);
        }

        private async Task<UserViewModel> CreateViewModel(User user)
        {
            var model = _mapper.Map<UserViewModel>(user);
            model.PhotoPath = await GetPhotoPath(user);
            return model;
        }

        private async Task<string> GetPhotoPath(User user)
        {
            return user.PhotoId.HasValue ? await _filesService.GetFilePath(user.PhotoId.Value) : null;
        }
    }
}