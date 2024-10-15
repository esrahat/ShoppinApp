using System.ComponentModel;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ShopNow.Models;
using SQLite;
using Microsoft.Maui.Controls;
using ShopNow.Services;

namespace ShopNow.ViewModels
{
    public class ReviewViewModel : BaseViewModel
    {
        private string _name;
        private string _email;
        private string _reviewText;
        private int _rating;
        private Item _item;
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;

                OnPropertyChanged(nameof(Rating));
            }
        }


        public string Star1Image { get; set; }
        public string Star2Image { get; set; }
        public string Star3Image { get; set; }
        public string Star4Image { get; set; }
        public string Star5Image { get; set; }
         public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string ReviewText
        {
            get => _reviewText;
            set
            {
                _reviewText = value;
                OnPropertyChanged();
            }
        }


        // Command to submit the review
        public ICommand SubmitReviewCommand { get; }

        public ICommand SetRatingCommand { get; }


        public ReviewViewModel(Item item)
        {
            Rating = 1;  // Default rating
            SetRatingCommand = new Command<int>(SetRating);

            UpdateStarImages();
            
            _item = item;

            SubmitReviewCommand = new Command(async () => await OnSubmitReview());
        }


        private void SetRating(int rating)
        {
            // Add a debug DisplayAlert to check if this is triggered
            AppHelper.CurrentApp.MainPage.DisplayAlert("Rating Clicked", $"Rating set to {rating}", "OK");

            Rating = rating;
            UpdateStarImages();

        }

        public void UpdateStarImages()
        {
            Star1Image = Rating >= 1 ? "star_filled.png" : "star_empty.png";
            Star2Image = Rating >= 2 ? "star_filled.png" : "star_empty.png";
            Star3Image = Rating >= 3 ? "star_filled.png" : "star_empty.png";
            Star4Image = Rating >= 4 ? "star_filled.png" : "star_empty.png";
            Star5Image = Rating >= 5 ? "star_filled.png" : "star_empty.png";

            // Notify UI to update the images
            OnPropertyChanged(nameof(Star1Image));
            OnPropertyChanged(nameof(Star2Image));
            OnPropertyChanged(nameof(Star3Image));
            OnPropertyChanged(nameof(Star4Image));
            OnPropertyChanged(nameof(Star5Image));
        }


       
        private async Task OnSubmitReview()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await AppHelper.CurrentApp.MainPage.DisplayAlert("Error", "Name is required.", "OK");
                return;
            }

            if (!IsValidEmail(Email))
            {
                await AppHelper.CurrentApp.MainPage.DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ReviewText))
            {
                await AppHelper.CurrentApp.MainPage.DisplayAlert("Error", "Review text is required.", "OK");
                return;
            }


            var review = new Review
            {
                Name = this.Name,
                Email = this.Email,
                ReviewText = this.ReviewText,
                Rating = this.Rating,
                ItemId = _item.Id  // the actual product ID
            };

            // Save to SQLite database
            try
            {
                await App.Database.SaveReviewAsync(review);  // Save review to the database

                await AppHelper.CurrentApp.MainPage.DisplayAlert("Success", "Review submitted successfully!", "OK");

                //  clearing the form after submission
                Name = string.Empty;
                Email = string.Empty;
                ReviewText = string.Empty;
                Rating = 1;  // Reset rating
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(ReviewText));
                OnPropertyChanged(nameof(Rating));

                // Navigate back to the previous page
                await AppHelper.CurrentApp.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await AppHelper.CurrentApp.MainPage.DisplayAlert("Error", $"Failed to add review: {ex.Message}", "OK");
            }
        }

        // Validate email format using regex
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
    }
}