using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models.PaymentViewModels
{
    public class PayWithCardViewModel
    {
        [Required, Display(Name = "Kortnummer")]
        public string CardNumber { get; set; }
        [Required, Display(Name = "Giltighetstid")]
        public DateTime ValidTil { get; set; }
        [Required]
        public string CVC { get; set; }
    }
}
