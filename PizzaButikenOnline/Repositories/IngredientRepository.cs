using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using System.Linq;

namespace PizzaButikenOnline.Repositories
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Ingredient item)
        {
            try
            {
                _context.Ingredients.Add(item);

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
            // TODO: Remove relations as well

            try
            {
                _context.Ingredients.Remove(Get(id));

                SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }

        public Ingredient Get(int id)
        {
            return _context.Ingredients.FirstOrDefault(d => d.Id == id);
        }

        public IQueryable<Ingredient> List()
        {
            return _context.Ingredients.AsQueryable();
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
