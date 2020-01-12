using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportShop.Models;

namespace SportShop.Persistence.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Country { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}