using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.CartViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                Ingredients = _context.Ingredients.ToList()
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            // TODO: Figure why this get "self referencing" error when including ingredients when non of the other controllers do...
            var dish = _context.Dishes.FirstOrDefault(d => d.Id == id);

            if (dish != null)
            {
                _cart.AddItem(dish);
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

        public RedirectToActionResult ChangeIngredientsInCart(int cartLineId, ICollection<int> usedIngredientIds)
        {
            var dish = _cart.Lines.FirstOrDefault(l => l.CartLineId == cartLineId);

            if (dish != null)
            {
                _cart.EditItem(dish.CartLineId, usedIngredientIds);
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}