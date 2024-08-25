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
        public DbSet<UserBlogPost> UserBlogPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBlogPost>()
                .HasKey(ub => new { ub.UserId, ub.BlogPostId });

            modelBuilder.Entity<UserBlogPost>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBlogPosts)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserBlogPost>()
                .HasOne(ub => ub.BlogPost)
                .WithMany(b => b.UserBlogPosts)
                .HasForeignKey(ub => ub.BlogPostId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
