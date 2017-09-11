using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.CartViewModels;
using System.Linq;

namespace PizzaButikenOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Cart _cart;

        public CartController(ApplicationDbContext context, Cart cartService)
        {
            _context = context;
            _cart = cartService;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public IActionResult AddToCart(int dishId, string returnUrl)
        {
            Dish dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);

            if (dish != null)
            {
                _cart.AddItem(dish, 1);
            }

            return PartialView("Components/CartSummary/Default", _cart);
        }

        public RedirectToActionResult RemoveFromCart(int CartLineId, string returnUrl)
        {
            var dish = _cart.Lines.FirstOrDefault(l => l.CartLineId == CartLineId);

            if (dish != null)
            {
                _cart.RemoveLine(dish.CartLineId);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}