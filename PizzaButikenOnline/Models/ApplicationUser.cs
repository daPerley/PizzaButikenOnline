using Microsoft.AspNetCore.Identity;

namespace PizzaButikenOnline.Models
{
    // TODO: add max-min's and other attributes
    // TODO: update the controller and views affected by this
    public class ApplicationUser : IdentityUser
    {
        public string CustomerName { get; set; }
        public int PostalCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
