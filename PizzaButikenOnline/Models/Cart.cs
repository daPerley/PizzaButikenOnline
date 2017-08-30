using System.Collections.Generic;

namespace PizzaButikenOnline.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
