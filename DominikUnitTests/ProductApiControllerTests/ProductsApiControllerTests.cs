using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportShop.Controllers;
using SportShop.Models;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductsApiControllerTests
    {
        private readonly ProductsRepositoryMockHelper _mockHelper;

        private readonly ProductsApiController _apiController;
        //ProductsApiController _controller = new ProductsApiController(_mock.Object);

        public ProductsApiControllerTests()
        {
            _mockHelper = new ProductsRepositoryMockHelper();
            _apiController = new ProductsApiController(_mockHelper.Mock.Object);
        }

        #region GetProductsTests

        [Fact]
        public void GetProductsShouldReturnAllProductsAnd200StatusCode()
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
        public void GetProductsShouldReturn500StatusCode()
        {
            var result = _apiController.GetProducts("throw");

            Assert.IsType<ObjectResult>(result);

            var contentResult = (ObjectResult) result;

            Assert.Equal(500, contentResult.StatusCode);
        }

        #endregion

        #region GetProductTests

        [Fact]
        public void GetProductShouldReturnProperProductAnd200StatusCode()
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
        public void GetProductShouldReturn404StatusCode()
        {
            var result = _apiController.GetProduct(5);

            Assert.IsType<StatusCodeResult>(result);

            var contentResult = (StatusCodeResult) result;

            Assert.Equal(404, contentResult.StatusCode);
        }

        [Fact]
        public void GetProductShouldReturn500StatusCode()
        {
            var result = _apiController.GetProduct(7);

            Assert.IsType<ObjectResult>(result);

            var contentResult = (ObjectResult) result;

            Assert.Equal(500, contentResult.StatusCode);
        }

        #endregion

        #region CreateProductTests

        [Fact]
        public void CreateProductShouldReturnCreatedProductModelAnd201StatusCode()
        {
            var result = _apiController.CreateProduct(_mockHelper.CreateProductModel);

            Assert.IsType<ObjectResult>(result);

            var resultContent = (ObjectResult) result;

            Assert.Equal(201, resultContent.StatusCode);
            Assert.IsType<ProductModel>(resultContent.Value);
            Assert.NotNull(resultContent.Value);
        }

        [Fact]
        public void CreateProductShouldReturn409StatusCode()
        {
            var result = _apiController.CreateProduct(_mockHelper.CreateExistingProductModel);

            Assert.IsType<StatusCodeResult>(result);

            var resultContent = (StatusCodeResult) result;

            Assert.Equal(409, resultContent.StatusCode);
        }

        [Fact]
        public void CreateProductShouldReturn500StatusCode()
        {
            var result = _apiController.CreateProduct(_mockHelper.ThrowErrorProductModel);

            Assert.IsType<ObjectResult>(result);

            var resultContent = (ObjectResult) result;

            Assert.Equal(500, resultContent.StatusCode);
        }

        #endregion

        // [Theory]
        // [InlineData("Sporty Wodne")]
        // [InlineData("Gry planszowe")]
        // public void ShouldReturnProductsFromProperCategoryAnd200StatusCode(string category)
        // {
        //     var result = _apiController.GetProducts(category);
        //     
        //     Assert.IsType<ObjectResult>(result);
        //     
        //     var contentResult = (ObjectResult) result;
        //     
        //     Assert.Equal(200, contentResult.StatusCode);
        //     
        //     Assert.NotNull(contentResult.Value);
        //
        //     var returnedProducts = (IEnumerable<ProductModel>) contentResult.Value;
        //     
        //     foreach (var product in returnedProducts)
        //     {
        //         Assert.Equal(category, product.Category);
        //     }
        // }


        // [Fact]
        // public void ShouldReturnNotFoundResponse()
        // {
        //     var result = _apiController.GetProduct(10);
        //
        //     Assert.IsType<NotFoundObjectResult>(result);
        // }

        //
        // [Fact]
        // public void ShouldReturnBadRequestResponse()
        // {
        //     var result = _apiController.UpdateProduct(_editBadProductModel);
        //     
        //     Assert.IsType<BadRequestResult>(result);
        // }
        //
        // [Fact]
        // public void ShouldReturnCreatedAtActionResponse()
        // {
        //     var result = _apiController.CreateProduct(_createProductModel);
        //     
        //     Assert.IsType<CreatedAtActionResult>(result);
        // }
    }
}