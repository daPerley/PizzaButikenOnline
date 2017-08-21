using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButikenOnline.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string Name { get; set; }
        [Required, MaxLength(499), MinLength(29)]
        public int Price { get; set; }
        [Required, MaxLength(150), MinLength(20)]
        public string Description { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
