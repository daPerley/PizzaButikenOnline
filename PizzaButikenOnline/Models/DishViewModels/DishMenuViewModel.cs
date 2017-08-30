using System.Collections.Generic;

namespace PizzaButikenOnline.Models.DishViewModels
{
    public class DishMenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }

        // For the actuall store set ingredients
        public ICollection<IngredientDish> IngredientDish { get; set; }

        // For ingredients added by the customer
        public ICollection<Ingredient> Ingredents { get; set; }
    }
}
