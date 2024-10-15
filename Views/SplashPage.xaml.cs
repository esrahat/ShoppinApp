using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ShopNow.Views
{
    public partial class SplashPage : ContentPage
    {
        private bool _isNavigated;

        public SplashPage()
        {
            InitializeComponent();

            StartSplashScreenTimer(); // Start the splash screen timer
        }

        private async void StartSplashScreenTimer()
        {
            // Wait for 4 seconds
            await Task.Delay(3000);

            // If the user hasn't clicked the button yet, navigate automatically
            if (!_isNavigated)
            {
                NavigateToCategoriesPage();
            }
        }

        private void OnShopNowClicked(object sender, EventArgs e)
        {
            // If the user clicks the button before the timer ends, navigate to the Categories Page
            if (!_isNavigated)
            {
                _isNavigated = true;  // Set to true so it doesn't trigger navigation twice
                NavigateToCategoriesPage();
            }
        }

        private async void NavigateToCategoriesPage()
        {
            // Navigate to the Categories Page
            await Navigation.PushAsync(new CategoriesPage());
        }
    }
}