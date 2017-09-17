using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.CheckOutViewModel;
using PizzaButikenOnline.Models.PaymentViewModels;
using PizzaButikenOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ApplicationDbContext context, Cart cart, UserManager<ApplicationUser> userManager, ICheckoutService checkoutService)
        {
            _context = context;
            _cart = cart;
            _userManager = userManager;
            _checkoutService = checkoutService;
        }

        public IActionResult Checkout()
        {
            var checkout = new CheckoutViewModel
            {
                PaymentOptions = new List<PaymentViewModel>
            {
                new PaymentViewModel{
                    Id =1,
                    PaymentMethod ="Kontant"
                },
                new PaymentViewModel{
                    Id =2,
                    PaymentMethod ="Kort"
                }
            }
            };

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
            if (checkoutViewModel.Payment.Id == 2)
            {
                // Redirect to card payment here
            }

            //var order = _checkoutService.CompleteCheckout(checkoutViewModel);

            var order = new Order
            {
                OrderDishes = new List<OrderDish>(),
                DateTime = DateTime.Now
            };

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

            if (_cart.Lines != null)
            {
                foreach (var line in _cart.Lines)
                {
                    var orderDish = new OrderDish
                    {
                        DishId = line.Dish.Id,
                        OrderId = order.Id,
                        EditedIngredients = line.Dish.Ingredients.Select(i => new IngredientOrderDish
                        {
                            IngredientId = i.Id,
                            OrderDishId = line.Dish.Id
                        }).ToList()
                    };

                    order.OrderDishes.Add(orderDish);
                }
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("CheckoutCompleted", order);
        }

        public IActionResult PayWithCard()
        {
            return View();
        }

        public IActionResult CheckoutCompleted(Order order)
        {
            _cart.Clear();

            return View(order);
        }
    }
}