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
        private readonly ProductRepositoryMockHelper _mockHelper;
        private readonly ITestOutputHelper _testOutputHelper;

        public ProductRepositoryTests(ITestOutputHelper testOutputHelper)
        {
            _mockHelper = new ProductRepositoryMockHelper();
            _testOutputHelper = testOutputHelper;
        }

        #region Products

        [Fact]
        public void Products_ShouldReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ShouldReturnQueryableCollectionOfProducts").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
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
        public void DeleteProduct_ShouldDeleteProduct_And_ReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DeleteProductShouldReturnTrue").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
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
        public void DeleteProduct_ShouldNotFindProduct_And_ReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DeleteProductShouldReturnFalse").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.DeleteProduct(_mockHelper.NotExistingEntity.ProductId);
                Assert.False(result);
                Assert.Equal(5, productsRepository.Products.Count());
            }
        }

        #endregion

        #region SaveProduct

        [Fact]
        public void SaveProduct_ShouldAddProduct_And_ReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SaveProductShouldAddProductAndReturnTrue").Options;

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_mockHelper.CreateProductEntity);

                Assert.True(result);
                Assert.Equal(1, productsRepository.Products.Count());
            }
        }

        [Fact]
        public void SaveProduct_ShouldUpdateExistingProduct_And_ReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SaveProductShouldUpdateExistingProduct").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_mockHelper.UpdateProductEntity);
                Assert.True(result);
                Assert.Equal(5, productsRepository.Products.Count());

                var shouldBeModified =
                    productsRepository.Products.FirstOrDefault(x =>
                        x.ProductId == _mockHelper.UpdateProductEntity.ProductId);

                Assert.NotNull(shouldBeModified);
                Assert.Equal(_mockHelper.UpdateProductEntity.ManufacturerId, shouldBeModified.ManufacturerId);
                Assert.Equal(_mockHelper.UpdateProductEntity.Name, shouldBeModified.Name);
                Assert.Equal(_mockHelper.UpdateProductEntity.Description, shouldBeModified.Description);
                Assert.Equal(_mockHelper.UpdateProductEntity.Price, shouldBeModified.Price);
                Assert.Equal(_mockHelper.UpdateProductEntity.Category, shouldBeModified.Category);
            }
        }

        [Fact]
        public void SaveProduct_ShouldNotFindProduct_And_ReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SaveProductShouldNotFindProductAndReturnFalse").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_mockHelper.NotExistingEntity);

                Assert.False(result);
            }
        }

        [Fact]
        public void SaveProduct_ShouldDoNothing_And_ReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SaveProductShouldShouldReturnFalse").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.SaveProduct(_mockHelper.OutOfRangeIdEntity);

                Assert.False(result);
            }
        }

        #endregion

        #region GetById

        [Fact]
        public void GetById_ShouldReturnProduct_And_200StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("GetByIdShouldReturnProductAnd200StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
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
        public void GetById_ShouldReturnNoProduct_And_404StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("GetByIdShouldReturnNoProductAnd404StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.GetById(_mockHelper.NotExistingEntity.ProductId);

                Assert.NotNull(result);

                Assert.Equal(404, result.StatusCode);

                Assert.Null(result.Data);
            }
        }

        [Fact]
        public void GetById_ShouldThrowError_And_Return500StatusCode()
        {
            using (var productsRepository = new ProductRepository(_mockHelper.ErrorMockedDbContext))
            {
                var result = productsRepository.GetById(1);
                
                Assert.NotNull(result);
                
                Assert.Equal(500, result.StatusCode);
            }
        }
        
        #endregion

        #region GetAll

        [Fact]
        public void GetAll_ShouldReturnAllProducts_And_200StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("GetAllShouldReturnAllProductsAnd200StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
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
        public void GetAll_ShouldReturnProductFromGivenCategory_And_200StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("GetAllShouldReturnProductFromGivenCategoryAnd200StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
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
        
        [Fact]
        public void GetAll_ShouldReturn500StatusCode()
        {
            using (var productsRepository = new ProductRepository(_mockHelper.ErrorMockedDbContext))
            {
                var result = productsRepository.GetAll();
                
                Assert.NotNull(result);
                
                Assert.Equal(500, result.StatusCode);
            }
        }

        #endregion

        #region Create

        [Fact]
        public void Create_ShouldAddProduct_And_Return201StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CreateShouldAddProductAndReturn201StatusCode").Options;

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                _testOutputHelper.WriteLine(JsonConvert.SerializeObject(context.Products));
                var result = productsRepository.Create(_mockHelper.CreateProductEntity);

                Assert.NotNull(result);

                Assert.Equal(201, result.StatusCode);

                Assert.NotNull(result.Data);
            }
        }

        [Fact]
        public void Create_ShouldReturn209StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CreateShouldReturn209StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.Create(_mockHelper.AlreadyExistingEntity);

                Assert.NotNull(result);

                Assert.Equal(409, result.StatusCode);
            }
        }
        
        [Fact]
        public void Create_ShouldReturn500StatusCode()
        {
            using (var productsRepository = new ProductRepository(_mockHelper.ErrorMockedDbContext))
            {
                var result = productsRepository.Create(_mockHelper.CreateProductEntity);
                
                Assert.NotNull(result);
                
                Assert.Equal(500, result.StatusCode);
            }
        }
        
        #endregion

        #region Update

        [Fact]
        public void Update_ShouldUpdateEntity_And_Return200StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UpdateShouldUpdateEntityAndReturn200StatusCode").Options;
            
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var updateProductEntity = _mockHelper.UpdateProductEntity;
                var result = productsRepository.Update(updateProductEntity);

                Assert.NotNull(result);

                Assert.Equal(200, result.StatusCode);

                Assert.NotNull(result.Data);

                Assert.Equal(updateProductEntity.ProductId, result.Data.ProductId);
                Assert.Equal(updateProductEntity.ManufacturerId, result.Data.ProductId);
                Assert.Equal(updateProductEntity.Name, result.Data.Name);
                Assert.Equal(updateProductEntity.Description, result.Data.Description);
                Assert.Equal(updateProductEntity.Price, result.Data.Price);
                Assert.Equal(updateProductEntity.Category, result.Data.Category);
            }
        }

        [Fact]
        public void Update_ShouldReturn404StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UpdateShouldReturn404StatusCode").Options;
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.Update(_mockHelper.NotExistingEntity);

                Assert.NotNull(result);

                Assert.Equal(404, result.StatusCode);
            }
        }
        
        [Fact]
        public void Update_ShouldReturn500StatusCode()
        {
            using (var productsRepository = new ProductRepository(_mockHelper.ErrorMockedDbContext))
            {
                var result = productsRepository.Update(_mockHelper.UpdateProductEntity);
                
                Assert.NotNull(result);
                
                Assert.Equal(500, result.StatusCode);
            }
        }

        #endregion
        
        #region Delete

        [Fact]
        public void Delete_ShouldRemoveProduct_And_Return204StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DeleteShouldRemoveProductAndReturn204StatusCode").Options;
            
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_mockHelper.Products);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.Delete(1);

                Assert.NotNull(result);

                Assert.Equal(204, result.StatusCode);

                Assert.Null(result.Data);
            }            
        }
        
        [Fact]
        public void Delete_ShouldReturn404StatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DeleteShouldReturn404StatusCode").Options;
            
            using (var context = new ApplicationDbContext(options))
            using (var productsRepository = new ProductRepository(context))
            {
                var result = productsRepository.Delete(1);

                Assert.NotNull(result);

                Assert.Equal(404, result.StatusCode);

                Assert.Null(result.Data);

            }            
        }
        
        [Fact]
        public void Delete_ShouldReturn500StatusCode()
        {
            using (var productsRepository = new ProductRepository(_mockHelper.ErrorMockedDbContext))
            {
                var result = productsRepository.Delete(1);
                
                Assert.NotNull(result);
                
                Assert.Equal(500, result.StatusCode);
            }
        }
        
        #endregion
    }
}
