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

        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            Dish dish = _context.Dishes.FirstOrDefault(d => d.Id == id);

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
    }
}