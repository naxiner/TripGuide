using TripGuide.Models;

namespace TripGuide.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
    }
}