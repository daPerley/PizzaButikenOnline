namespace PizzaButikenOnline.Models
{
    public class IngredientOrderDish
    {
        public int IngredientId { get; set; }
        public int OrderDishId { get; set; }

        public virtual OrderDish OrderDish { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
