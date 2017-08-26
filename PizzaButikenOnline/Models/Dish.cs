using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<IngredientDish> IngredientDish { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
