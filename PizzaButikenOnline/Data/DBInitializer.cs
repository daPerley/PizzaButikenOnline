
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
                    new Ingredient {Name="Gurka",
                    Price = 5},
                    new Ingredient {Name="Tomat",
                    Price = 5},
                    new Ingredient {Name="Vitlökssås",
                    Price = 10},
                    new Ingredient {Name="Skinka",
                    Price = 10},
                    new Ingredient {Name="Kebab",
                    Price = 15},
                    new Ingredient {Name="Tomatsås",
                    Price = 5},
                    new Ingredient {Name="Ost",
                    Price = 10},
                    new Ingredient {Name="Annanas",
                    Price = 5},
                    new Ingredient {Name="Bacon",
                    Price = 10},
                    new Ingredient {Name="Lök",
                    Price = 5},
                    new Ingredient {Name="Rödsås",
                    Price = 10},
                    new Ingredient {Name="Kyckling",
                    Price = 15},
                    new Ingredient {Name="Krutonger",
                    Price = 10},
                    new Ingredient {Name="Isbergssallad",
                    Price = 5},
                    new Ingredient {Name="Champinjoner",
                    Price = 10},
                    new Ingredient {Name="Ceasardressing",
                    Price = 10},
                    new Ingredient {Name="Fefferoni",
                    Price = 5},
                    new Ingredient {Name="Parmesan",
                    Price = 15}
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
                        Description = "A pizza",
                        CategoryId = 1
                    },
                    new Dish {
                        Name = "Margaritha",
                        Price = 65,
                        Description = "A pizza",
                        CategoryId = 1
                    },
                    new Dish {
                        Name = "Hawaii",
                        Description = "A pizza",
                        Price = 85,
                        CategoryId = 1
                    },
                    new Dish {
                        Name = "Kebabpizza",
                        Price = 90,
                        Description = "A pizza",
                        CategoryId = 1
                    },
                    new Dish {
                        Name = "Kebab i bröd",
                        Price = 60,
                        Description = "En kebab",
                        CategoryId = 3
                    },
                    new Dish {
                        Name = "Kebabrulle",
                        Price = 75,
                        Description = "En rulle",
                        CategoryId = 3
                    },
                    new Dish {
                        Name = "Kycklingrulle",
                        Price = 75,
                        Description = "En rulle",
                        CategoryId = 3
                    },
                    new Dish {
                        Name = "Ceasarsallad",
                        Price = 85,
                        Description = "En sallad",
                        CategoryId = 2
                    },
                    new Dish {
                        Name = "Kebabsallad",
                        Price = 85,
                        Description = "En sallad",
                        CategoryId = 2
                    },
                    new Dish {
                        Name = "Skinksallad",
                        Price = 85,
                        Description = "En sallad",
                        CategoryId = 2
                    }
                });
                context.SaveChanges();

                /* INGREDIENT - DISH RELATIONS */
                context.AddRange(new List<IngredientDish>
                {
                    new IngredientDish
                {
                    DishId = 1,
                    IngredientId = 1
                },
                    new IngredientDish
                {
                    DishId = 1,
                    IngredientId = 2
                },
                    new IngredientDish
                {
                    DishId = 1,
                    IngredientId = 3
                },
                    new IngredientDish
                {
                    DishId = 2,
                    IngredientId = 1
                },
                    new IngredientDish
                {
                    DishId = 2,
                    IngredientId = 5
                },
                    new IngredientDish
                {
                    DishId = 2,
                    IngredientId = 6
                },
                    new IngredientDish
                {
                    DishId = 2,
                    IngredientId = 7
                },
                    new IngredientDish
                {
                    DishId = 3,
                    IngredientId = 13
                },
                    new IngredientDish
                {
                    DishId = 3,
                    IngredientId = 7
                },
                    new IngredientDish
                {
                    DishId = 4,
                    IngredientId = 7
                },
                    new IngredientDish
                {
                    DishId = 4,
                    IngredientId = 2
                },
                    new IngredientDish
                {
                    DishId = 4,
                    IngredientId = 1
                },
                    new IngredientDish
                {
                    DishId = 5,
                    IngredientId = 10
                },
                    new IngredientDish
                {
                    DishId = 6,
                    IngredientId = 11
                },
                    new IngredientDish
                {
                    DishId = 6,
                    IngredientId = 15
                },
                    new IngredientDish
                {
                    DishId = 7,
                    IngredientId = 16
                },
                    new IngredientDish
                {
                    DishId = 7,
                    IngredientId = 12
                },
                    new IngredientDish
                {
                    DishId = 8,
                    IngredientId = 9
                },
                    new IngredientDish
                {
                    DishId = 8,
                    IngredientId = 6
                },
                    new IngredientDish
                {
                    DishId = 9,
                    IngredientId = 16
                },
                    new IngredientDish
                {
                    DishId = 10,
                    IngredientId = 9
                }
                });

                context.SaveChanges();
            }

        }
    }
}
