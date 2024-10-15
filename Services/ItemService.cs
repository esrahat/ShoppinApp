using System.Collections.ObjectModel;
using ShopNow.ViewModels;
using ShopNow.Services;


namespace ShopNow.Services
{
    public class ItemService : BaseViewModel
    {
        // Singleton instance
        private static ItemService _instance;
        public static ItemService Instance => _instance ??= new ItemService();

        // ObservableCollection for filtered items (shown in the UI)
        public ObservableCollection<Item> Items { get; private set; }

        //A list of Item for all the items added
        private List<Item> AllItems { get; set; }



        // Private constructor to prevent direct instantiation
        private ItemService()
        {
            // Initialize the collections
            AllItems = new List<Item>();
            Items = new ObservableCollection<Item>();



        }

        

        public async void LoadItems(int categoryId)
        {
            var allItems = await App.Database.GetItemsAsync(categoryId);
            
            foreach (var item in allItems)
            {
                AllItems.Add(item);


            }


            Items.Clear();

            var filteredItems = AllItems.Where(i => i.CategoryId == categoryId).ToList();

            foreach (var item in filteredItems)
            {


                // Check if the item is already in the Items collection
                if (!Items.Any(i => i.Id == item.Id))
                {
                    Items.Add(item);  // Only add the item if it doesn't exist already
                }

            }

        }



        // Fetch items by category
        public void FilterItemsForCategory(int categoryId)
        {
            Items.Clear();

            var filteredItems = AllItems.Where(i => i.CategoryId == categoryId).ToList();

            foreach (var item in filteredItems)
            {
                Items.Add(item);  // Add the filtered items to the Items collection
            }
        }

        // Add or update item
        public void AddOrUpdateItem(Item item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Amount = item.Amount;
                // Update other properties as needed
            }
            else
            {
                Items.Add(item);
            }
        }

        // Find item by ID
        public Item GetItemById(int itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            return item ?? new Item(); // Return a new Item if 'item' is null (handling null reference return warning)
        }
    }
}