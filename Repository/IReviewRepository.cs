using TripGuide.Models;

namespace TripGuide.Repository
{
    public interface IReviewRepository
    {
        void Add(Review review);
        IEnumerable<Review> GetByBlogPostId(Guid blogPostId);
    }
}