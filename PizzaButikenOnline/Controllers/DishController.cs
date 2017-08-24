using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            //TODO: Figure if you're going to have a menu model storing category/ingrediens/category or if this is enough with a partial view and repositroy/service

            return View(_context.Dishes.ToList());
        }

        public ActionResult Details(int id)
        {

            // TODO: make a repository for dish to replace the _context calls
            return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
        }

        public ActionResult Create()
        {
            var viewModel = new CreateDishViewModel
            {
                Categories = _context.Categories.ToList(),
                Ingredients = _context.Ingredients.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDishViewModel viewModel)
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
                    CategoryId = viewModel.CategoryId,
                    Ingredients = viewModel.Ingredients.ToList()
                };

                _context.Dishes.Add(dish);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dish/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
        }

        // POST: Dish/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
            }
        }

        // GET: Dish/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
        }

        // POST: Dish/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
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