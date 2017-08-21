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
            var aUser = new ApplicationUser();

            aUser.UserName = "test@user.com";
            aUser.Email = "test@user.com";

            var r = userManager.CreateAsync(aUser, "Abc12#Test");

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

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
