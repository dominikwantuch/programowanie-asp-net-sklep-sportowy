using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SportShop.Persistence.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepositoryModelsHelper _modelsHelper;
        private readonly ITestOutputHelper _testOutputHelper;

        public ProductRepositoryTests(ITestOutputHelper testOutputHelper)
        {
            _modelsHelper = new ProductRepositoryModelsHelper();
            _testOutputHelper = testOutputHelper;
        }
        
        #region Products
        
        [Fact]
        public void ShouldReturnQueryableCollectionOfProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ShouldReturnQueryableCollectionOfProducts").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.Products;
            
                Assert.NotNull(result);
                Assert.Equal(5, result.Count());
            }
        }
        
        #endregion
        
        #region DeleteProduct
        
        [Fact]
        public void DeleteProductShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteProductShouldReturnTrue").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.DeleteProduct(1);
                Assert.True(result);
                Assert.Equal(4, productsRepository.Products.Count());
            }
        }
        
        [Fact]
        public void DeleteProductShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteProductShouldReturnFalse").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.DeleteProduct(_modelsHelper.NotExistingEntity.ProductId);
                Assert.False(result);
                Assert.Equal(5, productsRepository.Products.Count());
            }
        }
        
        #endregion
        
        #region SaveProduct

        [Fact]
        public void SaveProductShouldAddProductAndReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("SaveProductShouldAddProductAndReturnTrue").Options;

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_modelsHelper.CreateProductEntity);
            
                Assert.True(result);
                Assert.Equal(1, productsRepository.Products.Count());
            }
        }

        [Fact]
        public void SaveProductShouldUpdateExistingProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("SaveProductShouldUpdateExistingProduct").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_modelsHelper.UpdateProductEntity);
                Assert.True(result);
                Assert.Equal(5, productsRepository.Products.Count());

                var shouldBeModified =
                    productsRepository.Products.FirstOrDefault(x =>
                        x.ProductId == _modelsHelper.UpdateProductEntity.ProductId);
            
                Assert.NotNull(shouldBeModified);
                Assert.Equal(_modelsHelper.UpdateProductEntity.ManufacturerId, shouldBeModified.ManufacturerId);
                Assert.Equal(_modelsHelper.UpdateProductEntity.Name, shouldBeModified.Name);
                Assert.Equal(_modelsHelper.UpdateProductEntity.Description, shouldBeModified.Description);
                Assert.Equal(_modelsHelper.UpdateProductEntity.Price, shouldBeModified.Price);
                Assert.Equal(_modelsHelper.UpdateProductEntity.Category, shouldBeModified.Category);
            }

        }

        [Fact]
        public void SaveProductShouldNotFindProductAndReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("SaveProductShouldNotFindProductAndReturnFalse").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_modelsHelper.NotExistingEntity);
            
                Assert.False(result);
            }
        }

        [Fact]
        public void SaveProductShouldShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("SaveProductShouldShouldReturnFalse").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_modelsHelper.OutOfRangeIdEntity);
            
                Assert.False(result);
            }
        }
        
        #endregion

        #region GetById

        [Fact]
        public void GetByIdShouldReturnProductAnd200StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdShouldReturnProductAnd200StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.GetById(1);
            
                Assert.NotNull(result);
            
                Assert.Equal(200, result.StatusCode);
            
                Assert.NotNull(result.Data);
            }
        }

        [Fact]
        public void GetByIdShouldReturnNoProductAnd404StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdShouldReturnNoProductAnd404StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.GetById(_modelsHelper.NotExistingEntity.ProductId);
            
                Assert.NotNull(result);
            
                Assert.Equal(404, result.StatusCode);
            
                Assert.Null(result.Data);
            }
        }

        #endregion
        
        #region GetAll

        [Fact]
        public void GetAllShouldReturnAllProductsAnd200StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllShouldReturnAllProductsAnd200StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.GetAll();
            
                Assert.NotNull(result);
            
                Assert.Equal(200, result.StatusCode);
            
                Assert.NotNull(result.Data);
            
                Assert.Equal(5, result.Data.Count());
            }
        }

        [Fact]
        public void GetAllShouldReturnProductFromGivenCategoryAnd200StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllShouldReturnProductFromGivenCategoryAnd200StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var category = "Sporty Wodne";
                var result = productsRepository.GetAll(category);
            
                Assert.NotNull(result);
            
                Assert.Equal(200, result.StatusCode);
            
                Assert.NotNull(result.Data);

                foreach (var product in result.Data)
                {
                    Assert.Equal(category, product.Category);
                }
            }
        }
        
        #endregion

        #region Create

        [Fact]
        public void CreateShouldAddProductAndReturn201StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateShouldAddProductAndReturn201StatusCode").Options;

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                _testOutputHelper.WriteLine(JsonConvert.SerializeObject(context.Products));
                var result = productsRepository.Create(_modelsHelper.CreateProductEntity);
            
                Assert.NotNull(result);
            
                Assert.Equal(201, result.StatusCode);
            
                Assert.NotNull(result.Data);
            }
        }

        [Fact]
        public void CreateShouldReturn209StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateShouldReturn209StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_modelsHelper.Products);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.Create(_modelsHelper.AlreadyExistingEntity);
            
                Assert.NotNull(result);

                Assert.Equal(409, result.StatusCode);
            }
        }


        #endregion

    }
}