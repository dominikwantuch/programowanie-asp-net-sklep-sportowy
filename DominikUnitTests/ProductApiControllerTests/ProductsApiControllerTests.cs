using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Controllers;
using SportShop.Models;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductsApiControllerTests
    {
        private readonly ProductsRepositoryMockHelper _mockHelper;

        private readonly ProductsApiController _apiController;

        public ProductsApiControllerTests()
        {
            _mockHelper = new ProductsRepositoryMockHelper();
            _apiController = new ProductsApiController(_mockHelper.Mock.Object);
        }

        #region GetProductsTests

        [Fact]
        public void GetProducts_ShouldReturnAllProducts_And_200StatusCode()
        {
            var result = _apiController.GetProducts();

            Assert.IsType<ObjectResult>(result);

            var contentResult = (ObjectResult) result;

            Assert.Equal(200, contentResult.StatusCode);

            Assert.NotNull(contentResult.Value);

            var returnedProducts = (IEnumerable<ProductModel>) contentResult.Value;

            Assert.Equal(3, returnedProducts.Count());
        }

        [Fact]
        public void GetProducts_ShouldReturn500StatusCode()
        {
            var result = _apiController.GetProducts("throw");

            Assert.IsType<ObjectResult>(result);

            var contentResult = (ObjectResult) result;

            Assert.Equal(500, contentResult.StatusCode);
        }

        #endregion

        #region GetProductTests

        [Fact]
        public void GetProduct_ShouldReturnProperProduct_And_200StatusCode()
        {
            var result = _apiController.GetProduct(1);

            Assert.IsType<ObjectResult>(result);

            var contentResult = (ObjectResult) result;

            Assert.Equal(200, contentResult.StatusCode);
            Assert.NotNull(contentResult.Value);

            var returnedProduct = (ProductModel) contentResult.Value;

            Assert.Equal(1, returnedProduct.ProductId);
        }

        [Fact]
        public void GetProduct_ShouldReturn404StatusCode()
        {
            var result = _apiController.GetProduct(5);

            Assert.IsType<StatusCodeResult>(result);

            var contentResult = (StatusCodeResult) result;

            Assert.Equal(404, contentResult.StatusCode);
        }

        [Fact]
        public void GetProduct_ShouldReturn500StatusCode()
        {
            var result = _apiController.GetProduct(7);

            Assert.IsType<ObjectResult>(result);

            var contentResult = (ObjectResult) result;

            Assert.Equal(500, contentResult.StatusCode);
        }

        #endregion

        #region CreateProductTests

        [Fact]
        public void CreateProduct_ShouldReturnCreatedProductModel_And_201StatusCode()
        {
            var result = _apiController.CreateProduct(_mockHelper.CreateProductModel);

            Assert.IsType<ObjectResult>(result);

            var resultContent = (ObjectResult) result;

            Assert.Equal(201, resultContent.StatusCode);
            
            Assert.IsType<ProductModel>(resultContent.Value);
            
            Assert.NotNull(resultContent.Value);
        }

        [Fact]
        public void CreateProduct_ShouldReturn409StatusCode()
        {
            var result = _apiController.CreateProduct(_mockHelper.CreateExistingProductModel);

            Assert.IsType<StatusCodeResult>(result);

            var resultContent = (StatusCodeResult) result;

            Assert.Equal(409, resultContent.StatusCode);
        }

        [Fact]
        public void CreateProduct_ShouldReturn500StatusCode()
        {
            var result = _apiController.CreateProduct(_mockHelper.ThrowErrorProductModel);

            Assert.IsType<ObjectResult>(result);

            var resultContent = (ObjectResult) result;

            Assert.Equal(500, resultContent.StatusCode);
        }

        #endregion

        #region UpdateProductTests

        [Fact]
        public void UpdateProduct_ShouldUpdateEntity_And_Return200StatusCode()
        {
            var result = _apiController.UpdateProduct(_mockHelper.UpdateProductModel);
            
            Assert.IsType<ObjectResult>(result);

            var resultContent = (ObjectResult) result;

            Assert.Equal(200, resultContent.StatusCode);
            
            Assert.IsType<ProductModel>(resultContent.Value);
            
            Assert.NotNull(resultContent.Value);
        }

        [Fact]
        public void UpdateProduct_ShouldReturnNotFoundResult()
        {
            var result = _apiController.UpdateProduct(_mockHelper.NotFoundUpdateProductModel);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateProduct_ShouldReturn400StatusCode()
        {
            var result = _apiController.UpdateProduct(_mockHelper.BadRequestUpdateProductModel);
            
            Assert.IsType<StatusCodeResult>(result);

            var resultContent = (StatusCodeResult) result;

            Assert.Equal(400, resultContent.StatusCode);
        }

        [Fact]
        public void UpdateProduct_ShouldReturn500StatusCode()
        {
            var result = _apiController.UpdateProduct(_mockHelper.ThrowErrorUpdateProductModel);

            Assert.IsType<ObjectResult>(result);

            var resultContent = (ObjectResult) result;

            Assert.Equal(500, resultContent.StatusCode);                
        }
        
        #endregion

        #region DeleteProductTests

        [Fact]
        public void DeleteProduct_ShouldReturn204StatusCode()
        {
            var result = _apiController.DeleteProduct(5);
            
            Assert.IsType<StatusCodeResult>(result);

            var resultContent = (StatusCodeResult) result;
            
            Assert.Equal(204, resultContent.StatusCode);
        }

        [Fact]
        public void DeleteProduct_ShouldReturn500StatusCode()
        {
            var result = _apiController.DeleteProduct(10);
            
            Assert.IsType<ObjectResult>(result);

            var resultContent = (ObjectResult) result;
            
            Assert.Equal(500, resultContent.StatusCode);            
        }

        #endregion
    }
}