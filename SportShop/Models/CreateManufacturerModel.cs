using System.ComponentModel.DataAnnotations;
using SportShop.Persistence.Entities;

namespace SportShop.Models
{
    /// <summary>
    /// Model for creating Manufacturers
    /// </summary>
    public class CreateManufacturerModel
    {
        /// <summary>
        /// Name of the manufacturer
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Country in which manufacturer is located
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Maps <see cref="CreateManufacturerModel"/> to <see cref="Manufacturer"/>
        /// </summary>
        public Manufacturer ToEntity(int id = 0)
        {
            return new Manufacturer()
            {
                Id = id,
                Name = this.Name,
                Country = this.Country
            };
        }
    }
}