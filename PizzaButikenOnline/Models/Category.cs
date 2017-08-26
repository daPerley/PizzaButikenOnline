using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, Display(Name = "Namn")]
        public string Name { get; set; }
    }
}
