using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButikenOnline.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Ingredient> _ingredientRepository;

        public IngredientsController(ApplicationDbContext context, IRepository<Ingredient> ingredientRepository)
        {
            _context = context;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ingredientRepository.List().OrderBy(i => i.Name).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Price")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _ingredientRepository.Create(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public IActionResult Edit(int id)
        {
            var ingredient = _ingredientRepository.Get(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Price")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    _ingredientRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public IActionResult Delete(int id)
        {

            var ingredient = _ingredientRepository.Get(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ingredientRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(int id)
        {
            try
            {
                _ingredientRepository.Get(id);
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }
    }
}
