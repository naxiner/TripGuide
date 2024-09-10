using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;

namespace TripGuide.Repository
{
    public class TripRouteRepository : ITripRouteRepository
    {
        private readonly TripGuideDbContext _dbContext;

        public TripRouteRepository(TripGuideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TripRoute Add(TripRoute tripRoute)
        {
            _dbContext.TripRoutes.Add(tripRoute);
            _dbContext.SaveChanges();
            return tripRoute;
        }

        public bool Delete(Guid id)
        {
            var existingTripRoute = _dbContext.TripRoutes
                .Include(tr => tr.TripRouteTouristObjects)
                .FirstOrDefault(tr => tr.Id == id);

            if (existingTripRoute != null)
            {
                _dbContext.TripRouteTouristObjects.RemoveRange(existingTripRoute.TripRouteTouristObjects);

                _dbContext.TripRoutes.Remove(existingTripRoute);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public TripRoute Get(Guid id)
        {
            return _dbContext.TripRoutes
                .Include(tr => tr.TripRouteTouristObjects)
                .ThenInclude(trto => trto.TouristObject)
                .FirstOrDefault(tr => tr.Id == id);
        }

        public IEnumerable<TripRoute> GetAll()
        {
            return _dbContext.TripRoutes
                .Include(tr => tr.TripRouteTouristObjects)
                .ThenInclude(trto => trto.TouristObject)
                .ToList();
        }

        public TripRoute Update(TripRoute tripRoute, List<Guid> touristObjectIds)
        {
            var existingTripRoute = _dbContext.TripRoutes
                .Include(tr => tr.TripRouteTouristObjects)
                .FirstOrDefault(tr => tr.Id == tripRoute.Id);

            if (existingTripRoute != null)
            {
                existingTripRoute.Name = tripRoute.Name;

                _dbContext.TripRouteTouristObjects.RemoveRange(existingTripRoute.TripRouteTouristObjects);

                foreach (var objectId in touristObjectIds)
                {
                    var tripRouteTouristObject = new TripRouteTouristObject
                    {
                        TripRouteId = tripRoute.Id,
                        TouristObjectId = objectId
                    };
                    _dbContext.TripRouteTouristObjects.Add(tripRouteTouristObject);
                }

                _dbContext.SaveChanges();
            }

            return existingTripRoute;
        }
    }
}