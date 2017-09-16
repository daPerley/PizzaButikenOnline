using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.DishViewModels;
using PizzaButikenOnline.Repositories;
using System.Linq;

namespace PizzaButikenOnline.Services
{
    public class DishService : IDishService
    {
        private IRepository<Dish> _dishRepository;
        private ApplicationDbContext _context;

        public DishService(IRepository<Dish> dishRepository, ApplicationDbContext context)
        {
            _dishRepository = dishRepository;
            _context = context;
        }

        public bool AddDish(DishViewModel viewModel)
        {
            try
            {
                var dish = new Dish
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    CategoryId = _context.Categories.First(c => c.Id == viewModel.CategoryId).Id
                };

                _dishRepository.Create(dish);
                _dishRepository.SaveChanges();

                foreach (var i in viewModel.UsedIngredientIds)
                {
                    var id = new IngredientDish
                    {
                        DishId = dish.Id,
                        IngredientId = i
                    };

                    _context.IngredientDishes.Add(id);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }

        public bool EditDish(int id, DishViewModel viewModel)
        {
            try
            {
                var dish = _dishRepository.Get(id);

                dish.Name = viewModel.Name;
                dish.Price = viewModel.Price;
                dish.Description = viewModel.Description;

                dish.Category = _context.Categories.First(c => c.Id == viewModel.CategoryId);

                _context.Dishes.Update(dish);
                _dishRepository.SaveChanges();

                if (dish.IngredientDish.Select(ind => ind.IngredientId) != viewModel.UsedIngredientIds)
                {
                    var oldRel = _context.IngredientDishes.Where(ind => ind.DishId == dish.Id).ToList();

                    _context.IngredientDishes.RemoveRange(oldRel);
                    _dishRepository.SaveChanges();

                    foreach (var i in viewModel.UsedIngredientIds)
                    {
                        var newRel = new IngredientDish
                        {
                            DishId = dish.Id,
                            IngredientId = i
                        };

                        _context.IngredientDishes.Add(newRel);
                    }

                    _dishRepository.SaveChanges();
                }
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }
    }
}
