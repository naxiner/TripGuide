﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TripGuide.Data;
using TripGuide.Models;
using TripGuide.Utility;

namespace TripGuide.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TripGuideDbContext _dbContext;

        public DbInitializer(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            TripGuideDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            if (!_roleManager.RoleExistsAsync(StaticDetail.Role_User).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_Moderator)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_User)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new User
                {
                    UserName = "admin",
                    Email = "admin@tripguide.com",
                    AccountVerified = true,
                    AvatarImageUrl = "https://res.cloudinary.com/dyhivdtqr/image/upload/v1725485807/heklagrj9ao8br6ln41o.png",

                }, "Admin123@").GetAwaiter().GetResult();

                User user = _dbContext.Users.FirstOrDefault(
                    u => u.Email == "admin@tripguide.com");
                _userManager.AddToRoleAsync(user, StaticDetail.Role_User).GetAwaiter().GetResult();
            }

            return;
        }
    }

}
