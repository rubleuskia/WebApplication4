using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Files;
using Core.Users.Model;
using DatabaseAccess.Entities;
using DatabaseAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Core.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<UserViewModel[]> GetViewModels()
        {
            var users = await _unitOfWork.Users.Get();
            var result = new List<UserViewModel>();

            foreach (var user in users)
            {
                result.Add(await MapUserToViewModel(user));
            }

            return result.ToArray();
        }

        public async Task<UserViewModel> GetUserViewModel(string id)
        {
            var user = await _unitOfWork.Users.FindUserById(id);
            return user != null ? await MapUserToViewModel(user) : null;
        }

        private async Task<UserViewModel> MapUserToViewModel(User user)
        {
            var viewModel = _mapper.Map<UserViewModel>(user);
            viewModel.PhotoPath = MapToLocalFile(await _fileService.GetFilePath(user.PhotoId));
            return viewModel;
        }

        public async Task Update(UserViewModel model)
        {
            var userToUpdate = await _unitOfWork.Users.FindUserById(model.Id);
            if (userToUpdate == null)
            {
                return;
            }

            _mapper.Map(model, userToUpdate);
            userToUpdate.PhotoId = await _fileService.Create(model.PhotoPath, model.Photo.FileName);

            await _unitOfWork.Users.Update(userToUpdate);
            await _unitOfWork.Commit();
        }

        public Task<IdentityResult> Create(CreateUserViewModel model)
        {
            var user = _mapper.Map<User>(model);
            return _unitOfWork.Users.Create(user, model.Password);
        }

        private static string MapToLocalFile(string path) => $"~{path}";
    }
}