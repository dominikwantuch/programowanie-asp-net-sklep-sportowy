using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportShop.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}