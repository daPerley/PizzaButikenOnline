using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(25), MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<IngredientDish> IngredientDish { get; set; }
    }
}
