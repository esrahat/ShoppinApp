using System.Collections.Generic;
using ShopNow.ViewModels;


namespace shopNow.Services
{   // this class in not used 
     public static class DataSeeder
    {
        // Method to get predefined categories
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Name = "Shoes" },
                new Category { Name = "Jeans" },
                new Category { Name = "T-Shirts" },
                new Category { Name = "Kids Clothes" }
            };
        }

        // Method to get items for Shoes category
        public static List<Item> GetShoesItems(int categoryId)
        {
            return new List<Item>
            {
                new Item { Name = "Adidas", Price = 110, Amount = 5, CategoryId = categoryId, Description = "Men's Running shoe, not waterproof" },
                new Item { Name = "Columbia", Price = 300, Amount = 5, CategoryId = categoryId, Description = "Columbia Men's Crestwood Mid Waterproof Hiking Boot" },
                new Item { Name = "Puma", Price = 200, Amount = 5, CategoryId = categoryId, Description = "Puma Mens Dazzler Sneaker, not waterproof" }
            };
        }

        // Method to get items for Jeans category
        public static List<Item> GetJeansItems(int categoryId)
        {
            return new List<Item>
            {
                new Item { Name = "Slimfit", Price = 30, Amount = 5, CategoryId = categoryId, Description = "Stretch Cotton Poly Spandex fabric" },
                new Item { Name = "Regular jeans", Price = 35, Amount = 5, CategoryId = categoryId, Description = "denim, Pocket, Washed" },
                new Item { Name = "Baggy", Price = 40, Amount = 5, CategoryId = categoryId, Description = "Straight fit - Falls Straight from the waist to the Hem" }
            };
        }

        // Method to get items for T-Shirts category
        public static List<Item> GetTShirtItems(int categoryId)
        {
            return new List<Item>
            {
                new Item { Name = "Puma", Price = 40, Amount = 5, CategoryId = categoryId, Description = "slimfit, short sleeve" },
                new Item { Name = "Reebok", Price = 60, Amount = 5, CategoryId = categoryId, Description = "slimfit, short sleeve" },
                new Item { Name = "Fila", Price = 50, Amount = 5, CategoryId = categoryId, Description = "Fila Men's Regular Fit T-Shirt" }
            };
        }

        // Method to get items for Kids Clothes category
        public static List<Item> GetKidsItems(int categoryId)
        {
            return new List<Item>
            {
                new Item { Name = "Boys Formal Set", Price = 35, Amount = 5, CategoryId = categoryId, Description = "1 Shirt, 1 Short, 1 Bow, 1 Suspender" },
                new Item { Name = "Layered dress", Price = 60, Amount = 5, CategoryId = categoryId, Description = "Sleeveless birthday dress" },
                new Item { Name = "Floral dress", Price = 110, Amount = 5, CategoryId = categoryId, Description = "Long sleeves, chic, wedding dress" }
            };
        }
    }
}