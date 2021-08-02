using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.Infrastructure.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UsersRepository(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Task<IdentityRole[]> GetRoles()
        {
            return _roleManager.Roles.ToArrayAsync();
        }

        public Task<IdentityResult> CreateRole(string roleName)
        {
            return _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public Task<IdentityRole> FindRoleById(string roleId)
        {
            return _roleManager.FindByIdAsync(roleId);
        }

        public Task DeleteRole(IdentityRole role)
        {
            return _roleManager.DeleteAsync(role);
        }

        public Task<User[]> Get()
        {
            return _userManager.Users.ToArrayAsync();
        }

        public Task<User> FindUserById(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<IList<string>> GetUserRoleIds(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task AddToRolesAsync(User user, IEnumerable<string> addedRoles)
        {
            return _userManager.AddToRolesAsync(user, addedRoles);
        }

        public Task RemoveFromRolesAsync(User user, IEnumerable<string> removedRoles)
        {
            return _userManager.AddToRolesAsync(user, removedRoles);
        }

        public Task Update(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public Task<IdentityResult> Create(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }
    }
}