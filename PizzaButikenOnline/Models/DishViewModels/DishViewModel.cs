using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models.DishViewModels
{
    public class DishViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IEnumerable<int> UsedIngredientIds { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
