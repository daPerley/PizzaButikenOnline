using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IDishService _dishService;
        private IRepository<Dish> _dishRepository;
        private IRepository<Ingredient> _ingredientRepository;
        private IRepository<Category> _categoryRepository;

        public DishController(IDishService dishService, IRepository<Dish> dishRepository, IRepository<Ingredient> ingredientRepository, IRepository<Category> categoryRepository)
        {
            _dishService = dishService;
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            return View(_dishRepository.List());
        }

        public ActionResult Create()
        {
            var viewModel = new DishViewModel
            {
                Categories = _categoryRepository.List(),
                Ingredients = _ingredientRepository.List().OrderBy(i => i.Name)
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
                    viewModel.Categories = _categoryRepository.List();
                    viewModel.Ingredients = _ingredientRepository.List().OrderBy(i => i.Name);
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

        public ActionResult Edit(int id)
        {
            var dish = _dishRepository.Get(id);

            var viewModel = new DishViewModel
            {
                Name = dish.Name,
                Price = dish.Price,
                Description = dish.Description,
                Ingredients = _ingredientRepository.List(),
                UsedIngredientIds = dish.IngredientDish.Select(i => i.IngredientId).ToList(),
                CategoryId = dish.CategoryId,
                Categories = _categoryRepository.List()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DishViewModel viewModel)
        {
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
            return View(_dishRepository.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
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