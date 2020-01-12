using System;
using SportShop.Persistence.Entities;

namespace SportShop.Models
{
    public class UpdateProductModel
    {
        public int ProductId { get; set; }
        
        public int? ManufacturerId { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string Category { get; set; }

       public Product ToEntity(Product entity)
       {
           if (entity == null)
               throw new ArgumentNullException(nameof(entity));

           entity.ManufacturerId = ManufacturerId;
           entity.Name = Name;
           entity.Description = Description;
           entity.Price = Price;
           entity.Category = Category;

           return entity;
       }
    }
    
}