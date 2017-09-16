using System.ComponentModel.DataAnnotations;

namespace PizzaButikenOnline.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, Display(Name = "Namn"),]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Minst tre tecken krävs, max 30 tecken.")]
        public string Name { get; set; }
    }
}
