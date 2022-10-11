#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductsAndCategories.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "is required")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "is required")]
    [Display(Name = "Description")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "is required")]
    [Display(Name = "Price")]
    public double Price { get; set; }


    public DateTime CreatedAt { get; set;} = DateTime.Now;
    public DateTime UpdatedAt { get; set;} = DateTime.Now;

    // public List<Dish> CreatedDishes { get; set; } = new List<Dish>();

    public List<Association> AllCategories { get; set; } = new List<Association>();


}
