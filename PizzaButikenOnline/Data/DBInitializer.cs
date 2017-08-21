using Microsoft.AspNetCore.Identity;
using PizzaButikenOnline.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Data
{
    public class DBInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var aUser = new ApplicationUser()
            {
                UserName = "test@user.com",
                Email = "test@user.com"
            };

            var r = userManager.CreateAsync(aUser, "Abc12#Test");

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser()
            {
                UserName = "test2@user.com",
                Email = "test2@user.com"
            };

            var r2 = userManager.CreateAsync(adminUser, "Abc12#Test");

            userManager.AddToRoleAsync(adminUser, adminRole.Name);

            if (!context.Dishes.Any())
            {
                context.AddRange(new List<Dish>
                {
                    new Dish { Name = "Capricciosa", Price = 79 },
                    new Dish { Name = "Margaritha", Price = 69 },
                    new Dish { Name = "Hawaii", Price = 85 }
                });
                context.SaveChanges();
            }
        }
    }
}
