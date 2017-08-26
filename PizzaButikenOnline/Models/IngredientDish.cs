namespace PizzaButikenOnline.Models
{
    public class IngredientDish
    {
        public int IngredientId { get; set; }
        public int DishId { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
