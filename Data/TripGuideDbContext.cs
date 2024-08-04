using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TripGuide.Models;

namespace TripGuide.Data
{
    public class TripGuideDbContext : DbContext
    {
        public TripGuideDbContext(DbContextOptions<TripGuideDbContext> options) 
            : base(options)
        {
        }

        public DbSet<TouristObject> TouristOjbects { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
