using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using System.Linq;

namespace PizzaButikenOnline.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Category item)
        {
            try
            {
                _context.Categories.Add(item);

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
                var dishesInCategory = _context.Dishes.Where(i => i.CategoryId == id).ToList();

                foreach (var dish in dishesInCategory)
                {
                    _context.Dishes.Remove(dish);
                }

                SaveChanges();

                _context.Categories.Remove(Get(id));

                SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }

        public Category Get(int id)
        {
            return _context.Categories.FirstOrDefault(i => i.Id == id);
        }

        public IQueryable<Category> List()
        {
            return _context.Categories.AsQueryable();
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
