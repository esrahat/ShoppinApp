using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using ShopNow.Services;
using ShopNow.Models; 


namespace ShopNow.Services{
public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Item>().Wait();
        _database.CreateTableAsync<Category>().Wait();
        _database.CreateTableAsync<Review>().Wait();
    }


    

        // Method to delete all categories and associated items for testing purposes 
    public async Task ClearCategoriesAsync()
    {
        // Delete all items before deleting categories to avoid orphaned data
        await _database.DeleteAllAsync<Item>();

        // Now delete all categories
        await _database.DeleteAllAsync<Category>();
    }

    public async Task ClearReviews()
    {
        // Delete all items before deleting categories to avoid orphaned data
        await _database.DeleteAllAsync<Review>();
    }



        // amethod to update the item amount 
        public async Task UpdateItemAmountAsync(int Id, int newAmount)
        {
               // Step 1: Retrieve the category by its ID
            var item = await _database.Table<Item>().Where(c => c.Id == Id).FirstOrDefaultAsync();
    
            if (item != null)
    {
        // Step 2: Update the category's name
            item.Amount= newAmount;
        
        // Step 3: Save the updated category back to the database
           try{
             await _database.UpdateAsync(item);
           } catch (Exception ex)
           {
            Console.WriteLine($"Error updating item: {ex.Message}");

           }

    }
        }

        //Retrieve Categories
    public Task<List<Category>> GetCategoriesAsync()
    {
        return _database.Table<Category>().ToListAsync();
    }

    // Retrieve Reviews
    // public Task<List<Category>> GetReviewsAsync()
    // {
    //    // return _database.Table<Review>().ToListAsync();
    // }


    // Retrieve Items by CategoryId    
    public Task<List<Item>> GetItemsAsync(int categoryId)
    {
        return _database.Table<Item>().Where(i => i.CategoryId == categoryId).ToListAsync();
    }

    // Add Review to the Database
    public Task SaveReviewAsync(Review review)
    {
        return _database.InsertAsync(review);
    }

    // Add Items, Categories, and Reviews for testing
    public Task SaveItemAsync(Item item)
    {
        return _database.InsertAsync(item);
    }

    public Task SaveCategoryAsync(Category category)
    {
        return _database.InsertAsync(category);
    }

     //ToDo: clean it later 
    // Method to seed the database with initial categories
        public async Task SeedDatabaseAsync()
        {  

            var categories = await GetCategoriesAsync();
           
             if (categories.Count == 0)
            {

                //If no categories exist, insert some default categories and items
                var shoesCategory = new Category { Name = "Shoes" , Id = 1};
                var jeansCategory = new Category { Name = "Jeans", Id = 2  };
                var tshirtCategory = new Category { Name = "T-Shirts", Id = 3};
                var kidsCategory = new Category { Name = "Kids clothes", Id = 4}; 

                await SaveCategoryAsync(shoesCategory);
                await SaveCategoryAsync(jeansCategory);
                await SaveCategoryAsync(tshirtCategory);
                await SaveCategoryAsync(kidsCategory);
                

                //populating items for each category
                await SaveItemAsync(new Item { Name = "Adidas", Price= 110, CategoryId = shoesCategory.Id , Description = "Men's Running shoe, not waterproof", ImageUrl="item1.jpeg"});
                await SaveItemAsync(new Item { Name = "Columbia", Price= 300 , CategoryId = shoesCategory.Id , Description = "Columbia Men's Crestwood Mid Waterproof Hiking Boot",  ImageUrl="item2.jpeg"});
                await SaveItemAsync(new Item { Name = "Puma", Price= 200, CategoryId = shoesCategory.Id , Description = "Puma Mens Dazzler Sneake, not waterproofer",  ImageUrl="item3.jpeg"});


                await SaveItemAsync(new Item { Name = "Slimfit", Price= 30, CategoryId = jeansCategory.Id , Description = "Stretch Cotton Poly Spandex fabric", ImageUrl="item4.jpeg"});
                await SaveItemAsync(new Item { Name = "Regular jeans", Price= 35, CategoryId = jeansCategory.Id , Description = "denim, Pocket, Washed",  ImageUrl="item5.jpeg"});
                await SaveItemAsync(new Item { Name = "Baggy", Price= 40, CategoryId = jeansCategory.Id , Description = "Straight fit - Falls Straight from the waist to the Hem",  ImageUrl="item6.jpeg"});

                await SaveItemAsync(new Item { Name = "Puma", Price= 40, CategoryId = tshirtCategory.Id , Description = "slimfit, short sleeve",  ImageUrl="item7.jpeg"});
                await SaveItemAsync(new Item { Name = "Reebok", Price= 60, CategoryId = tshirtCategory.Id , Description = "slimfit, short sleeve", ImageUrl="item8.jpeg"});
                await SaveItemAsync(new Item { Name = "Fila", Price= 50, CategoryId = tshirtCategory.Id , Description = "Fila Men's Regular Fit T-Shirt",  ImageUrl="item9.jpeg"});

                await SaveItemAsync(new Item { Name = "Boys Formal Set", Price= 35, CategoryId = kidsCategory.Id , Description = "1 Shirt, 1 Short, 1 Bow, 1 Suspender", ImageUrl="item10.jpeg"});
                await SaveItemAsync(new Item { Name = "Layered dress", Price= 60, CategoryId = kidsCategory.Id , Description = "Sleevless birthday dress",  ImageUrl="item11.jpeg"});
                await SaveItemAsync(new Item { Name = "Floral dress", Price= 110, CategoryId = kidsCategory.Id , Description = "Long sleeves, chick, wedding dress",  ImageUrl="item12.jpeg"});
                
            }
         // await ClearCategoriesAsync();
          //await ClearReviews();
        }
        }


}
