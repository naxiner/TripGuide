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

        public IEnumerable<Review> GetByBlogPostId(Guid blogPostId)
        {
            return tripGuideDbContext.Reviews.Where(r => r.BlogPostId == blogPostId).ToList();
        }
    }
}