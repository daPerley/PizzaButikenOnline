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

            if (!context.Ingredients.Any())
            {
                context.AddRange(new List<Ingredient>
                {
                    new Ingredient {Name="Gurka"},
                    new Ingredient {Name="Tomat"},
                    new Ingredient {Name="Vitlökssås"},
                    new Ingredient {Name="Skinka"},
                    new Ingredient {Name="Kebab"},
                    new Ingredient {Name="Tomatsås"},
                    new Ingredient {Name="Ost"},
                    new Ingredient {Name="Annanas"},
                    new Ingredient {Name="Bacon"},
                    new Ingredient {Name="Lök"},
                    new Ingredient {Name="Rödsås"},
                    new Ingredient {Name="Kyckling"},
                    new Ingredient {Name="Krutonger"},
                    new Ingredient {Name="Isbergssallad"},
                    new Ingredient {Name="Champinjoner"},
                    new Ingredient {Name="Ceasardressing"},
                    new Ingredient {Name="Fefferoni"},
                    new Ingredient {Name="Parmesan"}
                });
                context.SaveChanges();
            }

            if (!context.Dishes.Any())
            {
                context.AddRange(new List<Dish>
                {
                    // TODO: Add ingredients to the dishes
                    new Dish { Name = "Capricciosa", Price = 79 },
                    new Dish { Name = "Margaritha", Price = 69 },
                    new Dish { Name = "Hawaii", Price = 85 },
                    new Dish { Name = "Kebabpizza", Price = 90 },
                    new Dish { Name = "Kebab i bröd", Price = 60 },
                    new Dish { Name = "Kebabrulle", Price = 75 },
                    new Dish { Name = "Kycklingrulle", Price = 75 },
                    new Dish { Name = "Ceasarsallad", Price = 85 },
                    new Dish { Name = "Kebabsallad", Price = 85 },
                    new Dish { Name = "Skinksallad", Price = 85}
                });
                context.SaveChanges();
            }
        }
    }
}
