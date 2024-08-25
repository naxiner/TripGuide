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
        public DbSet<TripRoute> TripRoutes { get; set; }
        public DbSet<TripRouteTouristObject> TripRouteTouristObjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TripRouteTouristObject>()
                .HasKey(trto => new { trto.TripRouteId, trto.TouristObjectId });

            modelBuilder.Entity<TripRouteTouristObject>()
                .HasOne(trto => trto.TripRoute)
                .WithMany(tr => tr.TripRouteTouristObjects)
                .HasForeignKey(trto => trto.TripRouteId);

            modelBuilder.Entity<TripRouteTouristObject>()
                .HasOne(trto => trto.TouristObject)
                .WithMany(to => to.TripRouteTouristObjects)
                .HasForeignKey(trto => trto.TouristObjectId);
        }
    }
}
