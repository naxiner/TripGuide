using TripGuide.Models;

namespace TripGuide.Repository
{
    public interface IReviewRepository
    {
        void Add(Review review);
        void Update(Review review);
        void Delete(Guid reviewId);
        IEnumerable<Review> GetByBlogPostId(Guid blogPostId);
        Review GetById(Guid reviewId);
    }
}