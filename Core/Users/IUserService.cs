using System.Threading.Tasks;
using Core.Users.Model;
using Microsoft.AspNetCore.Identity;

namespace Core.Users
{
    public interface IUserService
    {
        Task<UserViewModel[]> GetViewModels();
        Task<UserViewModel> GetUserViewModel(string id);
        Task Update(UserViewModel model);
        Task<IdentityResult> Create(CreateUserViewModel model);
    }
}