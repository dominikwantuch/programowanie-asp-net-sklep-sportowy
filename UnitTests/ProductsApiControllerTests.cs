using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportShop.Controllers;
using SportShop.Models;
using SportShop.Repositories;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo

namespace UnitTests
{
    public class ProductsApiControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly Mock<IProductRepository> _mock = new Mock<IProductRepository>();

        private ProductsApiController _apiController;
        //ProductsApiController _controller = new ProductsApiController(_mock.Object);
        private readonly Product _createProductModel = new Product()
        {
            ProductId = 0,
            ManufacturerId = 1,
            Name = "Okulary do pływania",
            Description = "Okulary zbudowane z karbonu.",
            Price = 44,
            Category = "Sporty Wodne"
        };

        private readonly Product _editBadProductModel = new Product()
        {
            ProductId = 3,
            ManufacturerId = 1,
            Name = "Płetwy dla nurka",
            Description = "Płetwy w rozmiarze uniwersalnym.",
            Price = 165,
            Category = "Sporty Wodne",  
        };
        
        public ProductsApiControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mock.Setup(x => x.Products).Returns(new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    ManufacturerId = 1,
                    Name = "Piłka plażowa",
                    Description = "Tania piłka do sportów plażowych",
                    Price = 55,
                    Category = "Sporty Wodne",
                },
                new Product()
                {
                    ProductId = 2,
                    ManufacturerId = 1,
                    Name = "Płetwy dla nurka",
                    Description = "Płetwy w rozmiarze uniwersalnym.",
                    Price = 158,
                    Category = "Sporty Wodne",                    
                },
                new Product()
                {
                    ProductId = 3,
                    ManufacturerId = 2,
                    Name = "Gra o tron",
                    Description = "Pionki, karty, mapy",
                    Price = 255,
                    Category = "Gry planszowe",
                }
            }.AsQueryable());

            _mock.Setup(x => x.SaveProduct(_createProductModel)).Returns(true);
            _mock.Setup(x => x.SaveProduct(_editBadProductModel)).Returns(false);
            
            _mock.Setup(x => x.DeleteProduct(1)).Returns(true);
            _mock.Setup(x => x.DeleteProduct(10)).Returns(false);
            _apiController = new ProductsApiController(_mock.Object);
        }
        
        [Fact]
        public void ShouldReturnAllProductsProperResponse()
        {
            var result = _apiController.GetProducts();
            
            Assert.IsType<OkObjectResult>(result);

            var contentResult = (OkObjectResult) result;
            
            Assert.NotNull(contentResult.Value);

            var returnedProducts = (IEnumerable<Product>) contentResult.Value;
            
            Assert.Equal(3, returnedProducts.Count());
        }

        [Theory]
        [InlineData("Sporty Wodne")]
        [InlineData("Gry planszowe")]
        public void ShouldReturnProductsFromProperCategoryAndProperResponse(string category)
        {
            var result = _apiController.GetProducts(category);
            
            Assert.IsType<OkObjectResult>(result);
            
            var contentResult = (OkObjectResult) result;
            
            Assert.NotNull(contentResult.Value);

            var returnedProducts = (IEnumerable<Product>) contentResult.Value;
            
            foreach (var product in returnedProducts)
            {
                Assert.Equal(category, product.Category);
            }
        }

        [Fact]
        public void ShouldReturnEmptyOkResponse()
        {
            var category = "IDon'tExist";
            
            var result = _apiController.GetProducts(category);
            
            Assert.IsType<OkObjectResult>(result);
            
            var contentResult = (OkObjectResult) result;
            
            var returnedProducts = (IEnumerable<Product>) contentResult.Value;

            Assert.Empty(returnedProducts);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnProperProductAndResponse(int id)
        {
            var result = _apiController.GetProduct(id);
            
            Assert.IsType<OkObjectResult>(result);
            
            var contentResult = (OkObjectResult) result;
            
            Assert.NotNull(contentResult.Value);
            
            var returnedProduct = (Product) contentResult.Value;
            
            Assert.Equal(id, returnedProduct.ProductId);
        }

        [Fact]
        public void ShouldReturnNotFoundResponse()
        {
            var result = _apiController.GetProduct(10);
            
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void ShouldReturnBadRequestResponse()
        {
            var result = _apiController.EditProduct(_editBadProductModel);
            
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ShouldReturnCreatedAtActionResponse()
        {
            var result = _apiController.CreateProduct(_createProductModel);
            
            Assert.IsType<CreatedAtActionResult>(result);
        }
        
    }
}