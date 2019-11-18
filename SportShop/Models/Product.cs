using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SportShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [ForeignKey("Manufacturer")]
        public int? ManufacturerId { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string Category { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
    }
}