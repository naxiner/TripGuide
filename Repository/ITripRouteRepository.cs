using TripGuide.Models;

namespace TripGuide.Repository
{
    public interface ITripRouteRepository
    {
        IEnumerable<TripRoute> GetAll();
        TripRoute Get(Guid id);
        TripRoute Add(TripRoute tripRoute);
        TripRoute Update(TripRoute tripRoute, List<Guid> touristObjectIds);
        bool Delete(Guid id);
    }
}