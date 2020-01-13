using SportShop.Models;
using SportShop.Persistence.Entities;
using Xunit;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductModelTests
    {
        private readonly Product _entity = new Product()
        {
            ProductId = 10,
            ManufacturerId = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 100,
            Category = "ProductCategory"
        };

        [Fact]
        public void ShouldMapProductEntityToProductModel()
        {
            var model = ProductModel.ToModel(_entity);
            
            Assert.Equal(_entity.ProductId, model.ProductId);
            Assert.Equal(_entity.ManufacturerId, model.ManufacturerId);
            Assert.Equal(_entity.Name, model.Name);
            Assert.Equal(_entity.Description, model.Description);
            Assert.Equal(_entity.Price, model.Price);
            Assert.Equal(_entity.Category, model.Category);
        }
    }
}