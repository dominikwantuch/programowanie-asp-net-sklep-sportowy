using System;
using SportShop.Persistence.Entities;

namespace SportShop.Models
{
    public class UpdateManufacturerModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Country { get; set; }

        public Manufacturer ToEntity(Manufacturer entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Name = Name;
            entity.Country = Country;

            return entity;
        }
    }
}