using SQLite;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Models{
public class Review
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ItemId { get; set; } // Foreign Key to link to the Product
    public double Rating { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string ReviewText { get; set; }  // Rating from 1 to 5 stars
}
}