using TripGuide.Data;
using TripGuide.Models;

namespace TripGuide.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TripGuideDbContext _dbContext;

        public TagRepository(TripGuideDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _dbContext.Tags.ToList();
        }
    }
}