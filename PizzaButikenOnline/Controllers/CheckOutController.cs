using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.CheckOutViewModel;
using System.Collections.Generic;

namespace PizzaButikenOnline.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(ApplicationDbContext context, Cart cart, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _cart = cart;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var checkout = new CheckoutViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = _context.Users.Find(_userManager.GetUserId(User));

                if (user != null)
                {
                    checkout = new CheckoutViewModel
                    {
                        CustomerName = user.CustomerName,
                        Street = user.Street,
                        PostalCode = user.PostalCode,
                        City = user.City
                    };
                }
            }

            return View(checkout);
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {
            var order = new Order();

            if (User.Identity.IsAuthenticated && _userManager.GetUserId(User) != null)
            {
                order.UserId = _userManager.GetUserId(User);
            }
            else
            {
                order.AnonymousAddress = new AnonymousAddress
                {
                    CustomerName = checkoutViewModel.CustomerName,
                    Street = checkoutViewModel.Street,
                    PostalCode = checkoutViewModel.PostalCode,
                    City = checkoutViewModel.City
                };
            }

            var orderDishes = new List<OrderDish>();

            foreach (var dish in _cart.Lines)
            {
                var orderDish = new OrderDish
                {
                    Dish = dish.Dish,
                    Order = order
                };

                var ingredientOrderDishes = new List<IngredientOrderDish>();

                foreach (var ingredient in dish.Ingredients)
                {
                    var ingredientOrderDish = new IngredientOrderDish
                    {
                        Ingredient = ingredient,
                        OrderDish = orderDish
                    };

                    ingredientOrderDishes.Add(ingredientOrderDish);
                }

                orderDish.IngredientOrderDishes = ingredientOrderDishes;

                orderDishes.Add(orderDish);
            }

            order.OrderDishes = orderDishes;

            _context.Orders.Add(order);
            _context.SaveChanges();

            return View();
        }

        public IActionResult CheckoutCompleted(int id)
        {
            return View();
        }
    }
}