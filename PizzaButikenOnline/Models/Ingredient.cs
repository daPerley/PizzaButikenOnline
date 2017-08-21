using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButikenOnline.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(25), MinLength(3)]
        public string Name { get; set; }
    }
}
