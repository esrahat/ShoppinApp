using ShopNow.ViewModels;
using System.Collections.ObjectModel;

namespace ShopNow.ViewModels{
public class CategoriesViewModel : BaseViewModel
{

    public ObservableCollection<Category> Categories {get;}

    public CategoriesViewModel()
    {
         Categories = new ObservableCollection<Category> ();
          LoadCategories();
    }

    private async void LoadCategories()
    {
        var categories = await App.Database.GetCategoriesAsync();
        
        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    }
}
