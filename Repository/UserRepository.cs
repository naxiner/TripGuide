using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;

namespace TripGuide.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        
        public async Task<User> GetAsync(string userId)
        {
            return await _userManager.Users.FirstOrDefaultAsync(to => to.Id == userId);
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }

            return false;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);

            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.NormalizedUserName = user.NormalizedUserName;
                existingUser.Email = user.Email;
                existingUser.NormalizedEmail = user.NormalizedEmail;
                existingUser.PhoneNumber = user.PhoneNumber;

                var result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                {
                    return existingUser;
                }
            }

            return null;
        }
        
        public async Task<IList<string>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            return await _userManager.RemoveFromRolesAsync(user, roles);
        }
    }
}