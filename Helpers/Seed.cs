using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using MovieAppAPI.Models;

namespace MovieAppAPI.Helpers
{
    public class Seed
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public void SeedUsers()
        {
            if (!userManager.Users.Any())
            {
                var roles = new List<Role>
            {
                new Role{Name = "Admin"},
                new Role{Name = "Moderator"},
                new Role{Name = "User"}
            };
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }
                var admin = new User { UserName = "Admin" };
                var moderator = new User { UserName = "Moderator" };
                var result = userManager.CreateAsync(admin, "Admin@123").Result;
                if (result.Succeeded)
                {
                    var adminFromRepo = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(adminFromRepo, new[] { "Admin", "Moderator" }).Wait();
                }
                result = userManager.CreateAsync(moderator, "Moderator@123").Result;
                if (result.Succeeded)
                {
                    var modFromRepo = userManager.FindByNameAsync("Moderator").Result;
                    userManager.AddToRolesAsync(modFromRepo, new[] { "Moderator" }).Wait();
                }
            }
        }
    }
}