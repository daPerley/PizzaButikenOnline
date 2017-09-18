using PizzaButikenOnline.Models.PaymentViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models.CheckOutViewModel
{
    public class CheckoutViewModel
    {
        [Required, Display(Name = "För- & efternamn")]
        public string CustomerName { get; set; }
        [Required, Display(Name = "Postkod")]
        public int PostalCode { get; set; }
        [Required, Display(Name = "Gata")]
        public string Street { get; set; }
        [Required, Display(Name = "Postort")]
        public string City { get; set; }
        [Required, Display(Name = "Betalningssätt")]
        public int PaymentId { get; set; }

        public IEnumerable<PaymentViewModel> PaymentOptions { get; set; }
    }
}
