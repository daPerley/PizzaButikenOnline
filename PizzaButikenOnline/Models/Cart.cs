using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Dish dish, int quantity)
        {
            CartLine line = lineCollection
                .Where(d => d.Dish.Id == dish.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Dish = dish,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;

                // TODO: Add logic for diffrent ingredients on dish here.
            }
        }

        public virtual void RemoveLine(Dish dish) =>
            lineCollection.RemoveAll(l => l.Dish.Id == dish.Id);

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
