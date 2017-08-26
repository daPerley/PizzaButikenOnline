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
            //TODO: Add some sort of lazy loading to keep the data in the memory here

            return View(_context.Dishes.Include(c => c.Category).Include(i => i.Ingredients).ToList());
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
                    Category = _context.Categories.First(c => c.Id == viewModel.CategoryId),
                    Ingredients = viewModel.Ingredients.ToList()
                };

                _context.Dishes.Add(dish);
                _context.SaveChanges();

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

            var dish = _context.Dishes.Include(c => c.Category).Include(i => i.Ingredients).FirstOrDefault(x => x.Id == id);

            var viewModel = new DishViewModel
            {
                Name = dish.Name,
                Price = dish.Price,
                Description = dish.Description,
                Ingredients = _context.Ingredients.ToList(), // TODO: Make sure that this adds both all ingredients as well as the dishs ingredients
                CategoryId = dish.Category.Id,
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
                // TODO: Add update logic here
                var dish = _context.Dishes.FirstOrDefault(d => d.Id == id);

                // TODO: Replace collection with a viewModel and replace the dish values with the ones from the model
                dish.Name = viewModel.Name;
                dish.Price = viewModel.Price;
                dish.Description = viewModel.Description;
                dish.Category = _context.Categories.First(c => c.Id == viewModel.CategoryId);
                dish.Ingredients = viewModel.Ingredients.ToList(); // TODO: Delete the old ones first!

                _context.Dishes.Update(dish);
                _context.SaveChanges();

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