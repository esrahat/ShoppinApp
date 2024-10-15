using ShopNow.Models;
using ShopNow.ViewModels;
using ShopNow.Views;
using ShopNow.Services;
using ShopNow;

namespace ShopNow.Views
{

    public partial class ReviewPage : ContentPage
    {

        private int _rating;  // Current rating value

        private Item _item;

        // Declare the ViewModel as a property of the page

        public ReviewPage(Item item)
        {
            InitializeComponent();

            _item = item;  // Store the passed item
            _rating = 0;  // Default rating is 0 (no stars selected)

            //var databaseService = (DatabaseService)MauiApp.Current.Services.GetService(typeof(DatabaseService));
            // Pass the item to the ReviewViewModel, so the review is linked to the correct product

            var viewModel = new ReviewViewModel(_item);
            BindingContext = viewModel;
            // Manually trigger the SetRatingCommand with a value
            //viewModel.SetRatingCommand.Execute(5);  // Trigger command with rating of 3

        }


        // Event handler for the ImageButton clicks
        private void OnStarClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton clickedStar)
            {
                // Determine the rating based on which button was clicked
                if (clickedStar == Star1Button)
                    _rating = 1;
                else if (clickedStar == Star2Button)
                    _rating = 2;
                else if (clickedStar == Star3Button)
                    _rating = 3;
                else if (clickedStar == Star4Button)
                    _rating = 4;
                else if (clickedStar == Star5Button)
                    _rating = 5;

                // Update the star images based on the current rating
                UpdateStarImages();
            }
        }

        // Method to update the star images based on the rating
        private void UpdateStarImages()
        {
            Star1Button.Source = _rating >= 1 ? "star_filled.png" : "star_empty.png";
            Star2Button.Source = _rating >= 2 ? "star_filled.png" : "star_empty.png";
            Star3Button.Source = _rating >= 3 ? "star_filled.png" : "star_empty.png";
            Star4Button.Source = _rating >= 4 ? "star_filled.png" : "star_empty.png";
            Star5Button.Source = _rating >= 5 ? "star_filled.png" : "star_empty.png";


        }
    }

}
