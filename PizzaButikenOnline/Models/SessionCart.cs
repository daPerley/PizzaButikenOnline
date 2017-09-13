using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PizzaButikenOnline.Extensions;
using System;
using System.Collections.Generic;

namespace PizzaButikenOnline.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Dish dish, ICollection<int> ingredientIds)
        {
            base.AddItem(dish, ingredientIds);
            Session.SetJson("Cart", this);
        }

        public override void EditItem(int cartLineId, ICollection<int> ingredientIds)
        {
            base.EditItem(cartLineId, ingredientIds);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(int cartLineId)
        {
            base.RemoveLine(cartLineId);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
