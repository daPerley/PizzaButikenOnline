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
            /* ROLES */
            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            /* ADDING USERS */
            var aUser = new ApplicationUser()
            {
                UserName = "test@user.com",
                Email = "test@user.com"
            };

            var r = userManager.CreateAsync(aUser, "Abc12#Test");

            var adminUser = new ApplicationUser()
            {
                UserName = "test2@user.com",
                Email = "test2@user.com"
            };

            var r2 = userManager.CreateAsync(adminUser, "Abc12#Test");

            userManager.AddToRoleAsync(adminUser, adminRole.Name);

            /* CATEGORIES */

            var categories = new List<Category>
                {
                    new Category {Name="Pizza"},
                    new Category {Name="Sallad"},
                    new Category {Name="Övrigt"}
                };

            if (!context.Categories.Any())
            {
                context.AddRange(categories);
                context.SaveChanges();
            }

            /* INGREDIENTS */

            var ingredients = new List<Ingredient>
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
                };

            if (!context.Ingredients.Any())
            {
                context.AddRange(ingredients);
                context.SaveChanges();
            }

            /* DISHES */

            if (!context.Dishes.Any())
            {
                context.AddRange(new List<Dish>
                {
                    // TODO: Add ingredients to the dishes
                    new Dish {
                        Name = "Capricciosa",
                        Price = 70,
                        Ingredients = ingredients.Where(x => 
                            x.Name == "Tomatsås" || 
                            x.Name == "Ost" || 
                            x.Name == "Skinka" || 
                            x.Name == "Champinjoner")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Pizza")
                    },
                    new Dish {
                        Name = "Margaritha",
                        Price = 65,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Tomatsås" ||
                            x.Name == "Ost" ||
                            x.Name == "Skinka")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Pizza")
                    },
                    new Dish {
                        Name = "Hawaii",
                        Price = 85,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Tomatsås" ||
                            x.Name == "Ost" ||
                            x.Name == "Skinka" ||
                            x.Name == "Annanas")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Pizza")
                    },
                    new Dish {
                        Name = "Kebabpizza",
                        Price = 90,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Tomatsås" ||
                            x.Name == "Ost" ||
                            x.Name == "Kebab" ||
                            x.Name == "Fefferoni")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Pizza")
                    },
                    new Dish {
                        Name = "Kebab i bröd",
                        Price = 60,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Vitlökssås" ||
                            x.Name == "Tomat" ||
                            x.Name == "Kebab" ||
                            x.Name == "Fefferoni")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Övrigt")
                    },
                    new Dish {
                        Name = "Kebabrulle",
                        Price = 75,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Vitlökssås" ||
                            x.Name == "Tomat" ||
                            x.Name == "Kebab" ||
                            x.Name == "Fefferoni")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Övrigt")
                    },
                    new Dish {
                        Name = "Kycklingrulle",
                        Price = 75,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Vitlökssås" ||
                            x.Name == "Tomat" ||
                            x.Name == "Kyckling" ||
                            x.Name == "Gurka")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Övrigt")
                    },
                    new Dish {
                        Name = "Ceasarsallad",
                        Price = 85,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Ceasardressing" ||
                            x.Name == "Parmesan" ||
                            x.Name == "Kyckling" ||
                            x.Name == "Krutonger")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Sallad")
                    },
                    new Dish {
                        Name = "Kebabsallad",
                        Price = 85,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Vitlökssås" ||
                            x.Name == "Tomat" ||
                            x.Name == "Kebab" ||
                            x.Name == "Fefferoni")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Sallad")
                    },
                    new Dish {
                        Name = "Skinksallad",
                        Price = 85,
                        Ingredients = ingredients.Where(x =>
                            x.Name == "Gurka" ||
                            x.Name == "Tomat" ||
                            x.Name == "Skinka" ||
                            x.Name == "Isbergssallad")
                            .ToList(),
                        Category = categories.FirstOrDefault(x => x.Name == "Sallad")
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
