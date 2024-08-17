using Microsoft.AspNetCore.Identity;
using TripGuide.Models;

namespace TripGuide.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(string userId);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(string userId);
        Task<IList<string>> GetAllRolesAsync();
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
        Task<User> LockUnlock(string userId, string banDuration);
    }
}