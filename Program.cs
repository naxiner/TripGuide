using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;
using Microsoft.AspNetCore.Identity;
using TripGuide.Repositories;

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
            
            builder.Services.AddDefaultIdentity<User>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<TripGuideDbContext>();
            
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ITouristObjectRepository, TouristObjectRepository>();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
