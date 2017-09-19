using System;
using System.ComponentModel.DataAnnotations;
using PizzaButikenOnline.Models.CheckOutViewModel;

namespace PizzaButikenOnline.Models.PaymentViewModels
{
    public class PayWithCardViewModel
    {
        [Required, Display(Name = "Kortnummer"), RegularExpression(@"^\d{16}$", ErrorMessage = "Du har angivit ett felaktigt kortnummer, det bör bestå av 16 siffror.")]
        public string CardNumber { get; set; }
        [Required, Display(Name = "Giltighetstid")]
        public DateTime ValidTil { get; set; }
        [Required, RegularExpression(@"^\d{3}$", ErrorMessage = "Du har angivit ett felaktigt CVC, det bör bestå av endast 3 siffror")]
        public string CVC { get; set; }
    }
}
