

using Microsoft.AspNetCore.Identity;
using PizzaButikenOnline.Data;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.CheckOutViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Cart _cart;

        public CheckoutService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, Cart cart)
        {
            _context = context;
            _userManager = userManager;
            _cart = cart;
        }

        public Order CompleteCheckout(CheckoutViewModel checkoutViewModel)
        {
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

            return order;
        }
    }
}
