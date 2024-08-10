using TripGuide.Models;

namespace TripGuide.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(string userId);
        User Add(User user);
        User Update(User user);
        bool Delete(string userId);
    }
}