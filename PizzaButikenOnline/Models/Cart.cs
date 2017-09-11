﻿using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Dish dish, int quantity)
        {
            lineCollection.Add(new CartLine
            {
                Dish = dish,
                Quantity = quantity
            });
        }

        public virtual void RemoveLine(int cartLineId) =>
           lineCollection.FirstOrDefault(l => l.CartLineId == cartLineId);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(l => l.Dish.Price * l.Quantity);

        // TODO: Add logic that count price of extra ingredients here

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Dish Dish { get; set; }
        public int Quantity { get; set; }

        public ICollection<int> IngredientId { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
