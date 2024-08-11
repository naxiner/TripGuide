using TripGuide.Models;

namespace TripGuide.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(string userId);
        Task<User> Update(User user);
        Task<bool> Delete(string userId);
        Task<IList<string>> GetUserRolesAsync(User user);
    }
}