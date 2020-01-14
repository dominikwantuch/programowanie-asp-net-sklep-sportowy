using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShop.Persistence.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [Required]
        [Display(Name = "ManufacturerId")]
        [ForeignKey("Manufacturer")]
        public int? ManufacturerId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be grater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category must be given")]
        [Display(Name = "Category")]
        public string Category { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
    }
}