using System.Threading.Tasks;
using Core.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Users
{
    public interface IUserService
    {
        Task<UserViewModel> FindUserById(string userId);
        Task<UserViewModel[]> GetUsers();
        Task Update(UserViewModel model);
        Task<IdentityResult> Create(CreateUserViewModel model);
    }
}