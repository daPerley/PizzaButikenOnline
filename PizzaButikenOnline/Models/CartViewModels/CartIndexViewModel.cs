using System.Collections.Generic;

namespace PizzaButikenOnline.Models.CartViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
