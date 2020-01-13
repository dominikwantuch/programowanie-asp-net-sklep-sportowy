using System.Linq;
using SportShop.Persistence.Repositories;
using Xunit;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductRepositoryTests
    {
        private readonly ApplicationDbContextMockHelper _mockHelper;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _mockHelper = new ApplicationDbContextMockHelper();
            _productRepository = new ProductRepository(_mockHelper.MockedDbContext);
        }

        [Fact]
        public void ShouldReturnQueryableCollectionOfProducts()
        {
            var result = _productRepository.Products;
            
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }
    }
}