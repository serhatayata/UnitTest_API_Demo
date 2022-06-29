using UnitTest_API.Models;
using UnitTest_API.Services.Abstract;

namespace UnitTest_API.Services.Concrete
{
    public class ShoppingCartService : IShoppingCartService
    {
        public ShoppingItem Add(ShoppingItem newItem)
        {
            ShoppingList.shoppingItems.Add(newItem);
            return newItem;
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return ShoppingList.shoppingItems;
        }

        public ShoppingItem? GetById(Guid id)
        {
            return ShoppingList.shoppingItems.FirstOrDefault(x=> x.Id == id);
        }

        public void Remove(Guid id)
        {
            var value = ShoppingList.shoppingItems.FirstOrDefault(x => x.Id == id);
            if (value != null)
            {
                ShoppingList.shoppingItems.Remove(value);
            }
        }
    }
}
