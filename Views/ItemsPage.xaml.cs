using ShopNow.ViewModels;
using ShopNow.Views;

namespace ShopNow.Views
{

    public partial class ItemsPage : BasePage
    {
        public ItemsPage(int categoryId)
        {
            InitializeComponent();
            BindingContext = new ItemsViewModel(categoryId);
        }

        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection.FirstOrDefault() as Item;

            if (item != null)
            {
                await Navigation.PushAsync(new ItemDetailsPage(item));

                // Deselect the item to allow re-selection when returning
                if (sender is CollectionView collectionView)
                {
                    collectionView.SelectedItem = null;
                }
            }

        }
    }
}