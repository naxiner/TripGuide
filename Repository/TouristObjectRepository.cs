using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;

namespace TripGuide.Repositories
{
    public class TouristObjectRepository : ITouristObjectRepository
    {
        private readonly TripGuideDbContext _dbContext;

        public TouristObjectRepository(TripGuideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TouristObject Add(TouristObject touristObject)
        {
            _dbContext.TouristObjects.Add(touristObject);
            _dbContext.SaveChanges();

            return touristObject;
        }

        public bool Delete(Guid id)
        {
            var existingTouristObject = _dbContext.TouristObjects.Find(id);

            if (existingTouristObject != null)
            {
                _dbContext.TouristObjects.Remove(existingTouristObject);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public TouristObject Get(Guid id)
        {
            return _dbContext.TouristObjects.FirstOrDefault(to => to.Id == id);
        }

        public IEnumerable<TouristObject> GetAll()
        {
            return _dbContext.TouristObjects.ToList();
        }

        public TouristObject Update(TouristObject touristObject)
        {
            var existingTouristObject = _dbContext.TouristObjects.Find(touristObject.Id);

            if (existingTouristObject != null)
            {
                existingTouristObject.Name = touristObject.Name;
                existingTouristObject.Address = touristObject.Address;
                existingTouristObject.Latitude = touristObject.Latitude;
                existingTouristObject.Longitude = touristObject.Longitude;
                existingTouristObject.OpeningTime = touristObject.OpeningTime;
                existingTouristObject.ClosingTime = touristObject.ClosingTime;

                _dbContext.SaveChanges();
            }

            return existingTouristObject;
        }
    }
}