using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Data;
using Microsoft.AspNetCore.Authorization;

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
        // GET: Dish
        public ActionResult Index()
        {
            return View(_context.Dishes.ToList());
        }

        // GET: Dish/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Dishes.FirstOrDefault(x => x.Id == id));
        }

        // GET: Dish/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dish/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
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
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}