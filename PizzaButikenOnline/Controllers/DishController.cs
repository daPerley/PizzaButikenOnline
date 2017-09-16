using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.DishViewModels;
using PizzaButikenOnline.Repositories;
using PizzaButikenOnline.Services;
using System.Linq;

namespace PizzaButikenOnline.Controllers
{
    [Authorize]
    public class DishController : Controller
    {
        private ApplicationDbContext _context;
        private IDishService _dishService;
        private IRepository<Dish> _dishRepository;

        public DishController(ApplicationDbContext context, IDishService dishService, IRepository<Dish> dishRepository)
        {
            _context = context;
            _dishService = dishService;
            _dishRepository = dishRepository;
        }

        public ActionResult Index()
        {
            return View(_dishRepository.List());
        }

        public ActionResult Create()
        {
            var viewModel = new DishViewModel
            {
                Categories = _context.Categories.ToList(),
                Ingredients = _context.Ingredients.OrderBy(i => i.Name).ToList()
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
                    viewModel.Ingredients = _context.Ingredients.OrderBy(i => i.Name).ToList();
                    return View("Create", viewModel);
                }

                _dishService.AddDish(viewModel);

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
            var dish = _dishRepository.Get(id);

            var viewModel = new DishViewModel
            {
                Name = dish.Name,
                Price = dish.Price,
                Description = dish.Description,
                Ingredients = _context.Ingredients.ToList(),
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
                _dishService.EditDish(id, viewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_dishRepository.Get(id));
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
                _dishRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}