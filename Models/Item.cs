using ShopNow.ViewModels;
using SQLite;

public class Item : BaseViewModel
{ 
    private int _amount=5;
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    
    public string Description { get; set; }
    public int CategoryId { get; set; }

// The amount available in stock
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));  // Notify UI when amount changes
                OnPropertyChanged(nameof(IsAvailable));  // Notify UI about availability
                OnPropertyChanged(nameof(Availability)); 
            }
        }

        // Property to check if the item is available
        public bool IsAvailable => Amount > 0;

         // Availability string that changes based on the Amount
    public string Availability
    {
        get
        {
            if (Amount > 10)
            {
                return "In Stock";  // If more than 10 items are available
            }
            else if (Amount > 0 && Amount <= 10)
            {
                return "Limited Stock";  // If items are 10 or fewer
            }
            else
            {
                return "Out of Stock";  // If no items are available
            }
        }
    }

    public string ImageUrl { get; set; }

// quantity for the Cart Added Items (to show in the cart page)
    private int _quantity;
         public int Quantity
         {
            get => _quantity;
            set
           {
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(TotalPrice));
            
           }
         }

    
private int _totalPrice;
         public int TotalPrice
         {
            get => _totalPrice;
            set
           {
            _totalPrice = value;
            OnPropertyChanged(nameof(TotalPrice));
            
           }
         }

          public Item()
    {
        Quantity = 1;  // Default quantity for new items is 1
        TotalPrice = 0;
    }
}