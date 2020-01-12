﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SportShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [Required]
        [ForeignKey("Manufacturer")]
        public int? ManufacturerId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
    }
}