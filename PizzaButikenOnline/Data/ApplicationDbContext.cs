using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaButikenOnline.Models;

namespace PizzaButikenOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<IngredientDish> IngredientDishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }
        public DbSet<IngredientOrderDish> IngredientOrderDishes { get; set; }
        public DbSet<AnonymousAddress> AnonymousAddresses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<IngredientDish>()
            .HasKey(id => new { id.DishId, id.IngredientId });

            builder.Entity<IngredientDish>()
            .HasOne(id => id.Dish)
            .WithMany(d => d.IngredientDish)
            .HasForeignKey(di => di.DishId);

            builder.Entity<IngredientDish>()
            .HasOne(id => id.Ingredient)
            .WithMany(i => i.IngredientDish)
            .HasForeignKey(di => di.IngredientId);

            builder.Entity<IngredientOrderDish>()
            .HasKey(id => new { id.OrderDishId, id.IngredientId });

            builder.Entity<IngredientOrderDish>()
            .HasOne(id => id.OrderDish)
            .WithMany(d => d.IngredientOrderDishes)
            .HasForeignKey(di => di.OrderDishId);

            builder.Entity<IngredientOrderDish>()
            .HasOne(id => id.Ingredient)
            .WithMany(i => i.IngredientOrderDish)
            .HasForeignKey(di => di.IngredientId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
