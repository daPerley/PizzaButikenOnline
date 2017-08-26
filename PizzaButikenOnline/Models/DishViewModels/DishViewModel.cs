using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models.DishViewModels
{
    public class DishViewModel
    {
        [Required, Display(Name = "Rätt")]
        public string Name { get; set; }

        [Required, Display(Name = "Pris")]
        public int Price { get; set; }

        [Required, Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required, Display(Name = "Ingredienser")]
        public IEnumerable<int> UsedIngredientIds { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        [Required, Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
