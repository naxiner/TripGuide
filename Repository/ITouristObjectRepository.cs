using TripGuide.Models;

namespace TripGuide.Repositories
{
    public interface ITouristObjectRepository
    {
        IEnumerable<TouristObject> GetAll();
        TouristObject Get(Guid id);
        TouristObject Add(TouristObject touristObject);
        TouristObject Update(TouristObject touristObject);
        bool Delete(Guid id);
    }
}