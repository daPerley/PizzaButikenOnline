using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PizzaButikenOnline.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-PizzaButikenOnline-C09D26C7-F5B3-4E03-92B2-9315CD3C05AD;Trusted_Connection=True;MultipleActiveResultSets=true");
            var db = new ApplicationDbContext(builder.Options);
            return db;
        }
    }
}

