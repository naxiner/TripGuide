using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;

namespace TripGuide.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TripGuideDbContext _dbContext;

        public UserRepository(TripGuideDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }
        
        public User Get(string userId)
        {
            return _dbContext.Users.FirstOrDefault(to => to.Id == userId);
        }

        public User Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public bool Delete(string userId)
        {
            var existingUser = _dbContext.Users.Find(userId);

            if (existingUser != null)
            {
                _dbContext.Users.Remove(existingUser);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public User Update(User user)
        {
            var existingUser = _dbContext.Users.Find(user.Id);

            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.NormalizedUserName = user.NormalizedUserName;
                existingUser.Email = user.Email;
                existingUser.NormalizedEmail = user.NormalizedEmail;
                existingUser.PhoneNumber = user.PhoneNumber;

                _dbContext.SaveChanges();
            }

            return existingUser;
        }
    }
}