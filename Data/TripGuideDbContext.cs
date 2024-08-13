using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripGuide.Models;

namespace TripGuide.Data
{
    public class TripGuideDbContext : IdentityDbContext<User>
    {
        public TripGuideDbContext(DbContextOptions<TripGuideDbContext> options) 
            : base(options)
        {
        }

        public DbSet<TouristObject> TouristObjects { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
