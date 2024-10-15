using System.ComponentModel;
using System.Windows.Input;
using ShopNow.ViewModels;
using ShopNow.Views;
using ShopNow.Services;
using System.Collections.ObjectModel;


namespace ShopNow.ViewModels
{
    public class CartViewModel : BaseViewModel
    {


        // Singleton instance
        private static CartViewModel _instance;
        public static CartViewModel Instance => _instance ??= new CartViewModel();

        private int _cartItemCount;

        private double _totalPrice;

        // Loading Indicator fields
        private bool _isLoading;
        private bool _isPurchaseButtonEnabled;
        private bool _isTotalPriceVisible;

        private Item _SelectedItem;
        private Item _addedCartItem;
        public int CartItemCount
        {
            get => _cartItemCount;
            set
            {
                _cartItemCount = value;
                OnPropertyChanged(nameof(CartItemCount));  // Notifies the UI of the change
                OnPropertyChanged(nameof(CartDisplayText));  // Optionally notify the UI about display text changes
            }
        }
        public Item AddedCartItem
        {
            get => _addedCartItem;
            set
            {
                _addedCartItem = value;
                OnPropertyChanged(nameof(AddedCartItem));
            }
        }

        // Property to control the loading indicator visibility
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        // Property to control the button enabled state
        public bool IsPurchaseButtonEnabled
        {
            get => _isPurchaseButtonEnabled;
            set
            {
                _isPurchaseButtonEnabled = value;
                OnPropertyChanged(nameof(IsPurchaseButtonEnabled));
            }
        }

        // Property to control the visibility of the total price label
        public bool IsTotalPriceVisible
        {
            get => _isTotalPriceVisible;
            set
            {
                _isTotalPriceVisible = value;
                OnPropertyChanged(nameof(IsTotalPriceVisible));
            }
        }
        public double TotalPrice
        {
            get => _totalPrice;
            private set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));  // Notify the UI about the change
            }
        }

        // the cart page commands
        public ObservableCollection<Item> CartItems { get; set; }

        public ICommand PurchaseCommand { get; }

        public ICommand AddToCartCommand { get; }








        public CartViewModel()
        {

            IsLoading = false;  // Initially, loading is not active
            IsPurchaseButtonEnabled = true;  // Initially, the purchase button is enabled
            IsTotalPriceVisible = false;  // The total price is visible at first


            CartItems = new ObservableCollection<Item>();  // Initialize the cart items list

            // Subscribe to CollectionChanged event to recalculate total price when the collection changes
            CartItems.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalPrice));


            _SelectedItem = AddedCartItem;   // we need the selected item to add it to the cart, And to update Availabilty and itemsCount

            CartItemCount = 0;  // Start with 0 items in the cart


            // Initialize commands for the Cart Page buttons 
            AddToCartCommand = new Command(OnAddToCart);
            PurchaseCommand = new Command(async () => await PurchaseItemsAsync());

        }




        private async Task PurchaseItemsAsync()
        {
            // Disable the purchase button and start loading
            IsPurchaseButtonEnabled = false;
            IsLoading = true;

            // Simulate a delay for the purchase process (e.g., network request)

            if (CartItems.Count > 0)
            {
                await Task.Delay(3000);
                // Simulate the purchase process
                CartItems.Clear();  // Clear the cart after purchase
                OnPropertyChanged(nameof(TotalPrice));
                // Stop loading and hide the total price

            }
            IsLoading = false;
            IsTotalPriceVisible = false;
            // Show a thank you alert to the user
            await AppHelper.CurrentApp.MainPage.DisplayAlert("Thank You!", "Thank you for shopping with us!", "OK");


            // Re-enable the purchase button if necessary (or keep disabled)
            IsPurchaseButtonEnabled = true;
        }



        // Public method to recalculate the total price (callable from outside)
        public void RecalculateTotalPrice()
        {
            TotalPrice = CartItems.Sum(item => item.Price * item.Quantity);
        }





        public string CartDisplayText => CartItemCount > 0 ? $"Cart ({CartItemCount})" : "Cart";
        public string AvailabilityText => _SelectedItem.Amount > 0 ? $"Available" : "Unavailable";



        // A function for handling the Add To Cart Button 

        private void OnAddToCart()
        {
            CartItemCount++;  // Increment the cart item count
            IsTotalPriceVisible = true;


            if (AddedCartItem.Amount > 0 && AddedCartItem != null)
            {
                if (CartItems.Contains(AddedCartItem))
                {
                    AddedCartItem.Quantity++;


                }
                else
                {
                    CartItems.Add(AddedCartItem);
                }
                // Optionally notify the UI about property changes
                OnPropertyChanged(nameof(CartItems));
                RecalculateTotalPrice();


                AddedCartItem.Amount--;  // Decrease the item amount
            }


        }






    }
}
