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

        #region Products
        
        [Fact]
        public void ShouldReturnQueryableCollectionOfProducts()
        {
            var result = _productRepository.Products;
            
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }
        
        #endregion
        
        #region DeleteProduct
        
        [Fact]
        public void DeleteProductShouldReturnTrue()
        {
            var result = _productRepository.DeleteProduct(1);
            Assert.True(result);
            Assert.Equal(2, _productRepository.Products.Count());
        }
        
        [Fact]
        public void DeleteProductShouldReturnFalse()
        {
            var result = _productRepository.DeleteProduct(5);
            Assert.False(result);
            Assert.Equal(3, _productRepository.Products.Count());
        }
        
        #endregion
        
        #region SaveProduct

        [Fact]
        public void SaveProductShouldAddProductAndReturnTrue()
        {
            var result = _productRepository.SaveProduct(_mockHelper.CreateProductEntity);
            
            Assert.True(result);
            Assert.Equal(4, _productRepository.Products.Count());
        }

        [Fact]
        public void SaveProductShouldUpdateExistingProduct()
        {
            var result = _productRepository.SaveProduct(_mockHelper.UpdateProductEntity);
            Assert.True(result);
            Assert.Equal(3, _productRepository.Products.Count());

            var shouldBeModified =
                _productRepository.Products.FirstOrDefault(x =>
                    x.ProductId == _mockHelper.UpdateProductEntity.ProductId);
            
            Assert.NotNull(shouldBeModified);
            Assert.Equal(_mockHelper.UpdateProductEntity.ManufacturerId, shouldBeModified.ManufacturerId);
            Assert.Equal(_mockHelper.UpdateProductEntity.Name, shouldBeModified.Name);
            Assert.Equal(_mockHelper.UpdateProductEntity.Description, shouldBeModified.Description);
            Assert.Equal(_mockHelper.UpdateProductEntity.Price, shouldBeModified.Price);
            Assert.Equal(_mockHelper.UpdateProductEntity.Category, shouldBeModified.Category);
        }

        [Fact]
        public void SaveProductShouldNotFindProductAndReturnFalse()
        {
            var result = _productRepository.SaveProduct(_mockHelper.NotExistingEntity);
            
            Assert.False(result);
        }

        [Fact]
        public void SaveProductShouldShouldReturnFalse()
        {
            var result = _productRepository.SaveProduct(_mockHelper.OutOfRangeIdEntity);
            
            Assert.False(result);
        }
        
        #endregion

        #region GetById

        [Fact]
        public void GetByIdShouldReturnProductAnd200StatusCode()
        {
            var result = _productRepository.GetById(1);
            
            Assert.NotNull(result);
            
            Assert.Equal(200, result.StatusCode);
            
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetByIdShouldReturnNoProductAnd404StatusCode()
        {
            var result = _productRepository.GetById(5);
            
            Assert.NotNull(result);
            
            Assert.Equal(404, result.StatusCode);
            
            Assert.Null(result.Data);
        }

        #endregion
        
        #region GetAll

        [Fact]
        public void GetAllShouldReturnAllProductsAnd200StatusCode()
        {
            var result = _productRepository.GetAll();
            
            Assert.NotNull(result);
            
            Assert.Equal(200, result.StatusCode);
            
            Assert.NotNull(result.Data);
            
            Assert.Equal(3, result.Data.Count());
        }

        [Fact]
        public void GetAllShouldReturnProductFromGivenCategoryAnd200StatusCode()
        {
            var category = "Sporty Wodne";
            var result = _productRepository.GetAll(category);
            
            Assert.NotNull(result);
            
            Assert.Equal(200, result.StatusCode);
            
            Assert.NotNull(result.Data);

            foreach (var product in result.Data)
            {
                Assert.Equal(category, product.Category);
            }
        }
        
        #endregion

        #region Create

        [Fact]
        public void CreateShouldAddProductAndReturn201StatusCode()
        {
            var result = _productRepository.Create(_mockHelper.CreateProductEntity);
            
            Assert.NotNull(result);
            
            Assert.Equal(201, result.StatusCode);
            
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void CreateShouldReturn209StatusCode()
        {
            var result = _productRepository.Create(_mockHelper.AlreadyExistingEntity);
            
            Assert.NotNull(result);

            Assert.Equal(409, result.StatusCode);
        }


        #endregion

    }
}