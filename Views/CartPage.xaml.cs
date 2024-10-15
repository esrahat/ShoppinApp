using ShopNow.ViewModels;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;


namespace ShopNow.Views
{
    public partial class CartPage : ContentPage
    {
        //changing method, manually binding to the cs functions
        public ObservableCollection<Item> CartItems { get; set; }

        public CartPage()
        {
            InitializeComponent();
            //ch.m
            CartItems = new ObservableCollection<Item>();
            CartItems = CartViewModel.Instance.CartItems;

            // Initialize the ViewModel and set the BindingContext
            var _viewModel = new CartViewModel();

            //the old binding : BindingContext = CartViewModel.Instance;
            // ch.m Bind the ObservableCollection to the CollectionView in XAML
            CartCollectionView.ItemsSource = CartItems;
            TotalPriceLabel.BindingContext = CartViewModel.Instance;
            PurchaseButton.BindingContext = CartViewModel.Instance;
            ActivityIndicator.BindingContext = CartViewModel.Instance;


        }
        // Handle increasing the quantity of an item
        private void OnIncreaseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.BindingContext as Item;
            if (item != null)
            {
                item.Quantity++;
                item.Amount--;

                CartViewModel.Instance.RecalculateTotalPrice();

                //CartViewModel.Instance.AddedCartItem = item;
                //CartViewModel.Instance.TotalPrice();
                //CartCollectionView.ItemsSource = null;  // Refresh the CollectionView
                CartCollectionView.ItemsSource = CartItems;

            }
        }

        // Handle decreasing the quantity of an item
        private void OnDecreaseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.BindingContext as Item;
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
                item.Amount++;
                CartViewModel.Instance.RecalculateTotalPrice();

                CartCollectionView.ItemsSource = null;  // Refresh the CollectionView
                CartCollectionView.ItemsSource = CartItems;
            }
        }

        // Handle deleting an item
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.BindingContext as Item;
            if (item != null && CartItems.Contains(item))
            {
                item.Amount = item.Amount + item.Quantity;
                //TotalPriceLabel.IsVisible = false;
                // Find all occurrences of the item by Id
                // handle the quantity number
                item.Quantity = 1;
                var itemsToRemove = CartItems.Where(i => i.Id == item.Id).ToList();

                // Remove all occurrences
                foreach (var i in itemsToRemove)
                {
                    CartItems.Remove(i);
                }

                // CartItems.Remove(item);
                CartViewModel.Instance.RecalculateTotalPrice();
                CartCollectionView.ItemsSource = null;  // Refresh the CollectionView
                CartCollectionView.ItemsSource = CartItems;
            }
        }

    }
}