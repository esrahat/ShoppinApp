using ShopNow.ViewModels;


namespace ShopNow.Views
{

    public partial class CategoriesPage : BasePage
    {
        public CategoriesPage()
        {
            InitializeComponent();
            // Bind your view model or load categories here
            this.BindingContext = new CategoriesViewModel();
        }


        //Navigate to the items list
        private async void OnCategorySelected(object sender, SelectionChangedEventArgs e)
        {
            var category = e.CurrentSelection.FirstOrDefault() as Category;
            if (category != null)
            {
                await Navigation.PushAsync(new ItemsPage(category.Id));

                // Deselect the item to allow re-selection when returning
                if (sender is CollectionView collectionView)
                {
                    collectionView.SelectedItem = null;
                }
            }
        }
    }
}