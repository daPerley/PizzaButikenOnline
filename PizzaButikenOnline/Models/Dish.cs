using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Rätt")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Minst tre tecken krävs, max 30 tecken.")]
        public string Name { get; set; }

        [Required, Display(Name = "Pris")]
        public int Price { get; set; }

        [Required, Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Kategori")]
        public Category Category { get; set; }

        public ICollection<IngredientDish> IngredientDish { get; set; }

        [Display(Name = "Ingredienser")]
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
