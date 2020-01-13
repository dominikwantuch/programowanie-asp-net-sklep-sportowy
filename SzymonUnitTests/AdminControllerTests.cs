using Microsoft.AspNetCore.Mvc;
using Moq;
using SportShop.Controllers;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SzymonUnitTests
{
    public class AdminControllerTests
    {
        private readonly Mock<IProductRepository> _mock = new Mock<IProductRepository>();
        private readonly AdminController _controller;
        private readonly Product _product = new Product
        {
            ProductId = 1,
            Name = "Prod1",
            Category = "Cat1"
        };

        public AdminControllerTests()
        {
            _mock.Setup(c => c.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "Prod1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "Prod2", Category = "Cat1"},
                new Product {ProductId = 3, Name = "Prod3", Category = "Cat3"}
            }.AsQueryable<Product>());

            _controller = new AdminController(_mock.Object);
        }

        [Fact]
        public void IndexActionResult_ProductCategoryGiven_ShouldReturnViewWithTwoProducts()
        {
            var result = (ViewResult)_controller.Index("Cat1");
            var data = (IEnumerable<Product>)result.ViewData.Model;
            
            Assert.NotNull(result);
            Assert.Equal(2, data.Count());
        }

        [Fact]
        public void IndexActionResult_ProductCategoryIsNullOrWhiteSpace_ShouldReturnViewWithThreeProducts()
        {
            var result = (ViewResult)_controller.Index(null);
            var data = (IEnumerable<Product>)result.ViewData.Model;

            Assert.NotNull(result);
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public void EditActionResult_ProductIsFound_ShouldReturnViewWithOneProduct()
        {
            var result = (ViewResult)_controller.Edit(1);
            var data = (Product)result.ViewData.Model;

            Assert.NotNull(result);
            Assert.Equal("Prod1", data.Name);
        }

        [Fact]
        public void EditActionResult_ProductIsNotFound_ShouldReturnIndex()
        {
            var result = (ViewResult)_controller.Edit(4);
            var data = (Product)result.ViewData.Model;

            Assert.Null(data);
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void CreateActionResult_NewProduct_ShouldReturnEditViewWithModel()
        {
            var result = (ViewResult)_controller.Create();
            var data = (Product)result.ViewData.Model;

            Assert.NotNull(result);
            Assert.Equal("Edit", result.ViewName);
            Assert.NotNull(data);
        }

        [Fact]
        public void SaveActionResult_ProductIsNotGiven_ShouldReturnViewIndexWithAllProducts()
        {
            var result = (ViewResult)_controller.Save(null);
            var data = (IEnumerable<Product>)result.ViewData.Model;

            Assert.NotNull(result);
            Assert.Equal(3, data.Count());
            Assert.Equal("Index", result.ViewName);
            Assert.Equal("Given data is not valid!", result.ViewData["Message"]);
        }
        [Fact]
        public void SaveActionResult_ModelStateIsInvalid_ShouldReturnViewIndexWithAllProducts()
        {
            _controller.ModelState.AddModelError("key", "error");
            var result = (ViewResult)_controller.Save(_product);
            var data = (IEnumerable<Product>)result.ViewData.Model;

            Assert.NotNull(result);
            Assert.Equal(3, data.Count());
            Assert.Equal("Index", result.ViewName);
            Assert.Equal("Given data is not valid!", result.ViewData["Message"]);
        }

        [Fact]
        public void SaveActionResult_AccessingRepositoryException_SholudReturnErrorMessage()
        {
            _mock.Setup(m => m.SaveProduct(It.IsAny<Product>())).Throws(new Exception());
            var result = (ViewResult)_controller.Save(_product);

            Assert.Equal("Product could not be added to the database.", result.ViewData["Message"]);
        }


        [Fact]
        public void SaveActionResult_ModelIsValid_ShouldReturnViewIndexWithAllProducts()
        {
            var result = (ViewResult)_controller.Save(_product);
            var data = (IEnumerable<Product>)result.ViewData.Model;


            Assert.NotNull(result);
            Assert.Equal(3, data.Count());
            Assert.Equal("Index", result.ViewName);
        }
    

    }
}
