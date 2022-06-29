namespace UnitTest_API.Models
{
    public class ShoppingItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
    }

    public static class ShoppingList
    {
        public static List<ShoppingItem> shoppingItems = new List<ShoppingItem>()
        {
            new ShoppingItem() { Id = Guid.NewGuid(), Name = "Item_1", Manufacturer = "Man_1", Price = 500 },
            new ShoppingItem() { Id = Guid.NewGuid(), Name = "Item_2", Manufacturer = "Man_2", Price = 520 },
            new ShoppingItem() { Id = Guid.NewGuid(), Name = "Item_3", Manufacturer = "Man_3", Price = 350 },
        };
    }
}
