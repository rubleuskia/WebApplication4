using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace DatabaseAccess.Infrastructure.Repositories.Users
{
    public interface IUsersRepository
    {
        // roles management
        Task<IdentityRole[]> GetRoles();

        Task<IdentityResult> CreateRole(string roleName);

        Task<IdentityRole> FindRoleById(string roleId);

        Task DeleteRole(IdentityRole role);

        // user management
        Task<User[]> Get();

        Task<User> FindUserById(string userId);

        Task<IList<string>> GetUserRoleIds(User user);

        Task AddToRolesAsync(User user, IEnumerable<string> addedRoles);

        Task RemoveFromRolesAsync(User user, IEnumerable<string> removedRoles);

        Task Update(User user);

        Task<IdentityResult> Create(User user, string password);
    }
}