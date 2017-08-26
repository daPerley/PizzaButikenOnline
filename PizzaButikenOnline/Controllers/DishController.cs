using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.DishViewModels;
using System.Linq;

namespace PizzaButikenOnline.Controllers
{
    [Authorize]
    public class DishController : Controller
    {
        private ApplicationDbContext _context;

        public DishController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var dishes = _context.Dishes
                .Include(d => d.IngredientDish)
                .ThenInclude(id => id.Ingredient)
                .Include(d => d.Category)
                .ToList();

            return View(dishes);
        }

        public ActionResult Details(int id)
        {

            // TODO: make a repository for dish to replace the _context calls
            return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
        }

        public ActionResult Create()
        {
            var viewModel = new DishViewModel
            {
                Categories = _context.Categories.ToList(),
                Ingredients = _context.Ingredients.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DishViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Categories = _context.Categories.ToList();
                    viewModel.Ingredients = _context.Ingredients.ToList();
                    return View("Create", viewModel);
                }

                var dish = new Dish
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    CategoryId = _context.Categories.First(c => c.Id == viewModel.CategoryId).Id
                };

                _context.Dishes.Add(dish);
                _context.SaveChanges();

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

                return RedirectToAction("Index", "Dish");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dish/Edit/5
        public ActionResult Edit(int id)
        {
            // TODO: Send a create dish viewmodel with the view instead of id

            var dish = _context.Dishes
                .Include(d => d.IngredientDish)
                .ThenInclude(ind => ind.Ingredient)
                .Include(d => d.Category)
                .FirstOrDefault(d => d.Id == id);

            var viewModel = new DishViewModel
            {
                Name = dish.Name,
                Price = dish.Price,
                Description = dish.Description,
                Ingredients = _context.Ingredients.ToList(), // TODO: Make sure that this adds both all ingredients as well as the dishs ingredients
                UsedIngredientIds = dish.IngredientDish.Select(i => i.IngredientId).ToList(),
                CategoryId = dish.CategoryId,
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        // POST: Dish/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DishViewModel viewModel)
        {
            // TODO: Add validation, no exceptions!

            try
            {
                var dish = _context.Dishes
                .Include(d => d.IngredientDish)
                .ThenInclude(ind => ind.Ingredient)
                .Include(d => d.Category)
                .FirstOrDefault(d => d.Id == id);

                dish.Name = viewModel.Name;
                dish.Price = viewModel.Price;
                dish.Description = viewModel.Description;
                dish.Category = _context.Categories.First(c => c.Id == viewModel.CategoryId);

                _context.Dishes.Update(dish);
                _context.SaveChanges();

                if (dish.IngredientDish.Select(ind => ind.IngredientId) != viewModel.UsedIngredientIds)
                {
                    var oldRel = _context.IngredientDishes.Where(ind => ind.DishId == dish.Id).ToList();

                    _context.IngredientDishes.RemoveRange(oldRel);
                    _context.SaveChanges();

                    foreach (var i in viewModel.UsedIngredientIds)
                    {
                        var newRel = new IngredientDish
                        {
                            DishId = dish.Id,
                            IngredientId = i
                        };

                        _context.IngredientDishes.Add(newRel);
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
            }
        }

        public ActionResult Delete(int id)
        {
            return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // TODO: Add validation, no exceptions!

            try
            {
                var dish = _context.Dishes.FirstOrDefault(d => d.Id == id);

                _context.Dishes.Remove(dish);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}