using PizzaButikenOnline.Models;
using PizzaButikenOnline.Models.CheckOutViewModel;

namespace PizzaButikenOnline.Services
{
    public interface ICheckoutService
    {
        Order CompleteCheckout(CheckoutViewModel checkoutViewModel);
    }
}
