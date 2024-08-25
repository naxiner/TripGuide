using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;

namespace TripGuide.Repositories
{
    public class UserBlogPostRepository : IUserBlogPostRepository
    {
        private readonly TripGuideDbContext _context;

        public UserBlogPostRepository(TripGuideDbContext context)
        {
            _context = context;
        }

        public UserBlogPost Add(UserBlogPost userBlogPost)
        {
            _context.UserBlogPosts.Add(userBlogPost);
            _context.SaveChanges();

            return userBlogPost;
        }

        public UserBlogPost Delete(Guid blogId, string userId)
        {
            var userBlogPost = _context.UserBlogPosts
                .FirstOrDefault(ubp => ubp.UserId == userId && ubp.BlogPostId == blogId);

            if (userBlogPost != null)
            {
                _context.UserBlogPosts.Remove(userBlogPost);
                _context.SaveChanges();
            }

            return userBlogPost;
        }

        public IEnumerable<UserBlogPost> GetAllUserBlogs(string userId)
        {
            return _context.UserBlogPosts
                .Where(ubp => ubp.UserId == userId)
                .Include(ubp => ubp.BlogPost)
                .ToList();
        }
    }
}