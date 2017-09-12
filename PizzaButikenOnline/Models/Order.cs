using System;
using System.Collections.Generic;

namespace PizzaButikenOnline.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int AnonymousAddressId { get; set; }
        public AnonymousAddress AnonymousAddress { get; set; }

        public ICollection<OrderDish> OrderDishes { get; set; }
    }
}
