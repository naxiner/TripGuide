using TripGuide.Data;
using TripGuide.Models;

namespace TripGuide.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TripGuideDbContext tripGuideDbContext;

        public ReviewRepository(TripGuideDbContext tripGuideDbContext)
        {
            this.tripGuideDbContext = tripGuideDbContext;
        }

        public void Add(Review review)
        {
            tripGuideDbContext.Reviews.Add(review);
            tripGuideDbContext.SaveChanges();
        }

        public void Update(Review review)
        {
            tripGuideDbContext.Reviews.Update(review);
            tripGuideDbContext.SaveChanges();
        }

        public void Delete(Guid reviewId)
        {
            var review = tripGuideDbContext.Reviews.Find(reviewId);
            if (review != null)
            {
                tripGuideDbContext.Reviews.Remove(review);
                tripGuideDbContext.SaveChanges();
            }
        }

        public IEnumerable<Review> GetByBlogPostId(Guid blogPostId)
        {
            return tripGuideDbContext.Reviews.Where(r => r.BlogPostId == blogPostId).ToList();
        }

        public Review GetById(Guid reviewId)
        {
            return tripGuideDbContext.Reviews.Find(reviewId);
        }

        public IEnumerable<Review> GetAllByUserId(string userId)
        {
            return tripGuideDbContext.Reviews.Where(r => r.UserId == userId).ToList();
        }
    }
}