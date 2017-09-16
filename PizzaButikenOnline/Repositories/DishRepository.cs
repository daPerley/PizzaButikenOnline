using Microsoft.EntityFrameworkCore;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using System.Linq;

namespace PizzaButikenOnline.Repositories
{
    public class DishRepository : IRepository<Dish>
    {
        private ApplicationDbContext _context;

        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Dish item)
        {
            try
            {
                _context.Dishes.Add(item);

                SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var ingredientInDish = _context.IngredientDishes.Where(i => i.DishId == id).ToList();

                foreach (var item in ingredientInDish)
                {
                    _context.IngredientDishes.Remove(item);
                }

                SaveChanges();

                _context.Dishes.Remove(Get(id));

                SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }

        public Dish Get(int id)
        {
            return _context.Dishes
                                .Include(d => d.IngredientDish)
                                .ThenInclude(ind => ind.Ingredient)
                                .Include(d => d.Category)
                                .FirstOrDefault(d => d.Id == id);
        }

        public IQueryable<Dish> List()
        {
            return _context.Dishes
                .Include(d => d.IngredientDish)
                .ThenInclude(id => id.Ingredient)
                .Include(d => d.Category)
                .AsQueryable();
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }
    }
}
