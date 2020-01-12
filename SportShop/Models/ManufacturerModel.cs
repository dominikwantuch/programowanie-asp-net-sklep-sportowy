using SportShop.Persistence.Entities;

namespace SportShop.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Country { get; set; }

        public static ManufacturerModel ToModel(Manufacturer entity)
        {
            return new ManufacturerModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Country = entity.Country
            };
        }
    }
}