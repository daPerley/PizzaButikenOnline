using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.CartViewModels;
using PizzaButikenOnline.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Cart _cart;
        private IRepository<Ingredient> _ingredientRepository;

        public CartController(ApplicationDbContext context, Cart cart, IRepository<Ingredient> ingredientRepository)
        {
            _context = context;
            _cart = cart;
            _ingredientRepository = ingredientRepository;
        }

        public IActionResult Index()
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                Ingredients = _ingredientRepository.List().ToList()
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var dish = _context.Dishes
                .FirstOrDefault(d => d.Id == id);

            var ingredientIds = _context.IngredientDishes
                .Where(i => i.DishId == id)
                .Select(i => i.IngredientId)
                .ToList();

            if (dish != null)
            {
                _cart.AddItem(dish, ingredientIds);
            }

            return PartialView("Components/CartSummary/Default", _cart);
        }

        public RedirectToActionResult RemoveFromCart(int cartLineId)
        {
            var dish = _cart.Lines.FirstOrDefault(l => l.CartLineId == cartLineId);

            if (dish != null)
            {
                _cart.RemoveLine(dish.CartLineId);
            }

            return RedirectToAction("Index", "Cart");
        }

        public RedirectToActionResult EditIngredientsInCart(int cartLineId, ICollection<int> ingredientIds)
        {
            var line = _cart.Lines.FirstOrDefault(l => l.CartLineId == cartLineId);

            var extraPrice = 0;

            foreach (var id in ingredientIds)
            {
                if (!line.BaseIngredientIds.Contains(id))
                {
                    var ingredient = _ingredientRepository.Get(id);

                    extraPrice += ingredient.Price;
                }
            }

            if (line != null)
            {
                _cart.EditItem(line.CartLineId, ingredientIds, extraPrice);
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}