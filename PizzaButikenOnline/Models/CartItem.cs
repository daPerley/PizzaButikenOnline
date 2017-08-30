using System.Collections.Generic;

namespace PizzaButikenOnline.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int CartId { get; set; }

        public ICollection<int> IngredientId { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
