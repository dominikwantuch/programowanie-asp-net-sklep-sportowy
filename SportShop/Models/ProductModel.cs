using SportShop.Persistence.Entities;

namespace SportShop.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public int? ManufacturerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        /// <summary>
        /// Maps <see cref="entity"/> to <see cref="ProductModel"/>
        /// </summary>
        public static ProductModel ToModel(Product entity)
        {
            return new ProductModel
            {
                ProductId = entity.ProductId,
                ManufacturerId = entity.ManufacturerId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Category = entity.Category
            };
        }
    }
}