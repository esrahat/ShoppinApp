using ShopNow.ViewModels;
using System.Windows.Input;
using ShopNow;
using ShopNow.Views;
using ShopNow.Services;


namespace ShopNow.Views
{


    public partial class ItemDetailsPage : BasePage
    {


        public Item Item { get; set; }



        public ItemDetailsPage(Item item)
        {
            InitializeComponent();
            // Fetch the DatabaseService from the DI container

            //old initializing for Item
            //Item = GetItem(item);

            //New Initaializing using singleton instance
            Item = ItemService.Instance.GetItemById(item.Id);

            BindingContext = Item; // Set the binding context for easy data binding

            // Create the CartViewModel and set the AddedItem to the viewmodel's added item
            var _viewModel = new CartViewModel();

            //adding the added item to the collectionview CartItems 
            CartViewModel.Instance.AddedCartItem = Item;

            CartButton.BindingContext = CartViewModel.Instance;




        }

        private Item GetItem(Item item)
        {
            // Retrieve item from local database (or mock data)
            return item;
        }


        // This method is called when the button is clicked
        private async void OnWriteReviewClicked(object sender, EventArgs e)
        {


            // Navigate to the ReviewPage, passing the current item
            await Navigation.PushAsync(new ReviewPage(Item));
        }
    }

}