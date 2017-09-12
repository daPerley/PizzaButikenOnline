using System.Collections.Generic;

namespace PizzaButikenOnline.Models
{
    public class OrderDish
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public ICollection<IngredientOrderDish> IngredientOrderDishes { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
