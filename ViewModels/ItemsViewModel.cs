
using ShopNow.ViewModels;
using ShopNow.Services;
using System.Collections.ObjectModel;

namespace ShopNow.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {

        public ObservableCollection<Item> Items => ItemService.Instance.Items;
        private int _selectedCategoryId;




        public ItemsViewModel(int categoryId)
        {
            _selectedCategoryId = categoryId;

            // Fetch the items for the selected category from the singleton
            ItemService.Instance.LoadItems(_selectedCategoryId);


        }



    }

}