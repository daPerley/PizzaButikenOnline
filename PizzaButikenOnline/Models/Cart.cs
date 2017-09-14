using System.Collections.Generic;
using System.Linq;

namespace PizzaButikenOnline.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Dish dish, ICollection<int> ingredientIds)
        {
            lineCollection.Add(new CartLine
            {
                CartLineId = lineCollection.Count + 1,
                Dish = dish,
                IngredientIds = ingredientIds,
                BaseIngredientIds = ingredientIds
            });
        }

        public virtual void EditItem(int cartLineId, ICollection<int> ingredientIds, int extraCost)
        {
            var cartLine = lineCollection.FirstOrDefault(l => l.CartLineId == cartLineId);

            cartLine.IngredientIds.Clear();

            foreach (var id in ingredientIds)
            {
                cartLine.IngredientIds.Add(id);
            }

            cartLine.ExtraCost = extraCost;
        }

        public virtual void RemoveLine(int cartLineId) =>
           lineCollection.RemoveAll(l => l.CartLineId == cartLineId);

        public virtual decimal ComputeTotalValue()
        {
            var basePrice = lineCollection.Sum(l => l.Dish.Price);

            var extraPrice = lineCollection.Sum(l => l.ExtraCost);

            var totalPrice = basePrice + extraPrice;

            return totalPrice;
        }

        // TODO: Add logic that count price of extra ingredients here

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Dish Dish { get; set; }

        public ICollection<int> IngredientIds { get; set; }
        public ICollection<int> BaseIngredientIds { get; set; }
        public int ExtraCost { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
