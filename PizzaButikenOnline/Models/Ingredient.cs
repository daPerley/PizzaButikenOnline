using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(25), MinLength(3), Display(Name = "Namn")]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }

        public ICollection<IngredientDish> IngredientDish { get; set; }
    }
}
