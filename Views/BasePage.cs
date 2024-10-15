using Microsoft.Maui.Controls;
using ShopNow.ViewModels;


namespace ShopNow.Views
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {



            // Add the cart icon toolbar item
            var toolbarItem = new ToolbarItem
            {
                IconImageSource = "cart.png",
                Priority = 0
            };

            // Attach the Clicked event handler
            toolbarItem.Clicked += OnToolbarItemClicked!;

            // Add the toolbar item to the page
            ToolbarItems.Add(toolbarItem);

        }

        // Event handler for the toolbar item click
        private async void OnToolbarItemClicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new CartPage()) ;
            if (Application.Current?.MainPage?.Navigation != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
            }
            else
            {
                // Handle the case where Application.Current is null
                await DisplayAlert("Error", "Application context is not available.", "OK");
            }



        }
    }
}