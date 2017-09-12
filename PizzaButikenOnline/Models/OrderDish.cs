using System.Collections.Generic;

namespace PizzaButikenOnline.Models
{
    public class OrderDish
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public ICollection<IngredientOrderDish> EditedIngredients { get; set; }
    }
}
