using PizzaButikenOnline.Models.PaymentViewModels;

namespace PizzaButikenOnline.Models.CheckOutViewModel
{
    public class CheckoutViewModel
    {
        public string CustomerName { get; set; }
        public int PostalCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public PaymentViewModel Payment { get; set; }
    }
}
