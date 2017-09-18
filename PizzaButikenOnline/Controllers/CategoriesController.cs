using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Repositories;
using System;

namespace PizzaButikenOnline.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoriesController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(_categoryRepository.List());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Create(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Category model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var category = _categoryRepository.Get(model.Id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                _categoryRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: Log ex
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            try
            {
                _categoryRepository.Get(id);
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }
    }
}
