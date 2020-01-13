using SportShop.Models;
using Xunit;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class CreateProductModelTests
    {
        private readonly CreateProductModel _model = new CreateProductModel()
        {
            ManufacturerId = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 100,
            Category = "ProductCategory"
        };

        [Fact]
        public void ShouldMapCreateProductModelToEntity()
        {
            var entity = _model.ToEntity();
            
            Assert.Equal(0, entity.ProductId);
            Assert.Equal(_model.ManufacturerId, entity.ManufacturerId);
            Assert.Equal(_model.Name, entity.Name);
            Assert.Equal(_model.Description, entity.Description);
            Assert.Equal(_model.Price, entity.Price);
            Assert.Equal(_model.Category, entity.Category);

        }
    }
}