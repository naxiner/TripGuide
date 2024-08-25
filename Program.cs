using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;
using Microsoft.AspNetCore.Identity;
using TripGuide.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using TripGuide.Utility;
using TripGuide.Repository;
using System.Configuration;

namespace TripGuide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TripGuideDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TripGuideDbConnectionString")));

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TripGuideDbContext>().AddDefaultTokenProviders();
            
            builder.Services.Configure<GoogleMapsSettings>(builder.Configuration.GetSection("GoogleMaps"));
            
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITouristObjectRepository, TouristObjectRepository>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<IUserBlogPostRepository, UserBlogPostRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<ITripRouteRepository, TripRouteRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapRazorPages();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}