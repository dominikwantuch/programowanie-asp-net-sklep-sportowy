using System.ComponentModel.DataAnnotations;
using SportShop.Persistence.Entities;

namespace SportShop.Models
{
    /// <summary>
    /// Create product model.
    /// </summary>
    public class CreateProductModel
    {
        /// <summary>
        /// Manufacturer ID
        /// </summary>
        [Required]
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        [Required]
        public decimal Price { get; set; }


        /// <summary>
        /// Product category.
        /// </summary>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Maps <see cref="CreateProductModel"/> to <see cref="Product"/>
        /// </summary>
        /// <param name="id"></param>
        public Product ToEntity(int id = 0)
        {
            return new Product()
            {
                ProductId = id,
                ManufacturerId = this.ManufacturerId,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Category = this.Category
            };
        }
    }
}