using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Moq;
using SportShop.Controllers;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace DawidUnitTests
{
    public class ManufacturerControllerTests
    {
        private readonly Mock<IManufacturerRepository> _mock = new Mock<IManufacturerRepository>();
        private readonly ManufacturerController _controller;
        private readonly Manufacturer _manufacturer = new Manufacturer
        {
            Id = 1,
            Name = "Razer",
            Country = "USA",
        };

        public ManufacturerControllerTests()
        {
            _mock.Setup(c => c.Manufacturers).Returns(new List<Manufacturer>()
            {
                new Manufacturer()
                {
                    Id = 1,
                    Name = "Adidas",
                    Country = "England",
                },
                new Manufacturer()
                {
                    Id = 2,
                    Name = "Nike",
                    Country = "China",
                },
                new Manufacturer()
                {
                    Id = 3,
                    Name = "Reebok",
                    Country = "Germany",
                },
            }.AsQueryable());
            _controller = new ManufacturerController(_mock.Object);
        }
        [Fact]
        public void IndexActionResult_ListContains3_ShouldReturn3()
        {
            var result = (ViewResult)_controller.Index();
            var data = (IEnumerable<Manufacturer>) result.ViewData.Model;
            
            Assert.NotNull(result);
            Assert.Equal(3,data.Count());
        }

        [Fact]
        public void EditActionResult_ManufacturerIsNotFound_ShouldReturnIndex()
        {
            var result = (ViewResult)_controller.Edit(4);
            
            Assert.NotNull(result);
            Assert.Equal("Manufacturer with given id does not exist!",result.ViewData["Message"]);
            Assert.True(result.ViewName == "Index");
        }
        [Fact]
        public void EditActionResult_ManufacturerIsFound_ShouldReturnEditViewWithManufacturer()
        {
            var result = (ViewResult)_controller.Edit(1);
            var data = (Manufacturer) result.ViewData.Model;
            
            Assert.NotNull(result);
            Assert.NotNull(data);
            Assert.Equal("Edit",result.ViewName);
        }
        [Fact]
        public void CreateActionResult_NewManufacturer_ShouldReturnEditViewWithProperModel()
        {
            var result = (ViewResult)_controller.Create();
            var data = (Manufacturer) result.ViewData.Model;
            
            Assert.NotNull(result);
            Assert.NotNull(data);
            Assert.Equal("Edit" , result.ViewName);
        }

        [Fact]
        public void SaveActionResult_ModelStateInvalid_ShouldReturnIndexView()
        {
            _controller.ModelState.AddModelError("key", "error");
            var result = (ViewResult)_controller.Save(_manufacturer);
            
            _mock.Verify( c=>c.SaveManufacturer(It.IsAny<Manufacturer>()), Times.Never);
            Assert.Equal("Given data is not valid!",result.ViewData["Message"]);
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index");
        }

        [Fact]
        public void SaveActionResult_ModelStateValid_ShouldReturnToIndexView()
        {
            var result = (ViewResult)_controller.Save(_manufacturer);
            
            _mock.Verify( c=>c.SaveManufacturer(It.IsAny<Manufacturer>()), Times.Once);
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index");
        }

        [Fact]
        public void SaveActionResult_ExceptionWhenAccessingRepo_ShouldShowMessageInViewData()
        {
            _mock.Setup(c => c.SaveManufacturer(It.IsAny<Manufacturer>()))
                .Throws(new Exception());
            var result = (ViewResult)_controller.Save(_manufacturer);
            Assert.Equal("Manufacturer could not be added to the database.",result.ViewData["Message"]);
        }

        [Fact]
        public void DeleteActionResult_SuccessfulDelete_ShouldRedirectToIndexView()
        {
            var result = (ViewResult)_controller.Delete(It.IsAny<int>());
            _mock.Verify( c=>c.DeleteManufacturer(It.IsAny<int>()), Times.Once);
            
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index");
        }

        [Fact]
        public void DeleteActionResult_ExceptionWhenDeleting_ShouldShowMessageInViewData()
        {
            _mock.Setup(c => c.DeleteManufacturer(It.IsAny<int>()))
                .Throws(new Exception());
            var result = (ViewResult)_controller.Delete(It.IsAny<int>());
            Assert.NotNull(result);
            Assert.Equal("An unexpected error has occured while trying to delete product.", result.ViewData["Message"]);
        }
    }
}