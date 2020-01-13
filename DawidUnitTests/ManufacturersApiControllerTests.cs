using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Moq;
using SportShop.Controllers;
using SportShop.Models;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
using Xunit;
using Xunit.Abstractions;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;
using StatusCodeResult = Microsoft.AspNetCore.Mvc.StatusCodeResult;

namespace DawidUnitTests
{
    public class ManufacturersApiControllerTests
    {
        private readonly Mock<IManufacturerRepository> _mock = new Mock<IManufacturerRepository>();
        private ManufacturersApiController _controller;
        private ITestOutputHelper _testOutputHelper;

        private readonly Manufacturer _manufacturer = new Manufacturer
        {
            Id = 1,
            Name = "Razer",
            Country = "USA",
        };

        private readonly Manufacturer _badManufacturer = new Manufacturer
        {
            Id = 4,
            Name = "Samsung",
            Country = "South Korea"
        };

        private readonly CreateManufacturerModel _createManufacturer = new CreateManufacturerModel
        {
            Name = "Logitech",
            Country = "USA"
        };

        private readonly UpdateManufacturerModel _updateManufacturer = new UpdateManufacturerModel
        {
            Id = 2,
            Name = "Apple",
            Country = "USA"
        };


        public ManufacturersApiControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
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
            _controller = new ManufacturersApiController(_mock.Object);
        }

        [Fact]
        public void GetAllManufacturers_RepositoryHasManufacturers_ShouldReturnOkResponse()
        {
            var result = _controller.GetManufacturers();
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(200, objectResponse.StatusCode);
        }

        [Fact]
        public void GetAllManufacturers_ExceptionWhenAccessingRepo_ShouldReturnInternalServerError()
        {
            _mock.Setup(c => c.Manufacturers).Throws(new Exception());
            var result = _controller.GetManufacturers();
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(500, objectResponse.StatusCode);
        }

        [Fact]
        public void GetManufacturer_ManufacturerIsInRepo_ShouldReturnOkResponse()
        {
            _mock.Setup(c => c.GetById(It.IsAny<int>())).Returns(new ResultModel<Manufacturer>(_manufacturer, 200));
            var result = _controller.GetManufacturer(1);
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(200, objectResponse.StatusCode);
        }

        [Fact]
        public void GetManufacturer_ManufacturerIsNotInRepo_ShouldReturnNotFoundResponse()
        {
            _mock.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(new ResultModel<Manufacturer>(It.IsAny<Manufacturer>(), 404));
            var result = _controller.GetManufacturer(It.IsAny<int>());
            Assert.IsType<StatusCodeResult>(result);
            var objectResponse = (StatusCodeResult) result;
            Assert.Equal(404, objectResponse.StatusCode);
        }

        [Fact]
        public void GetManufacturer_ErrorWhenAccessingRepo_ShouldReturnInternalServerErrorResponse()
        {
            _mock.Setup(c => c.GetById(It.IsAny<int>())).Throws(new Exception());
            var result = _controller.GetManufacturer(It.IsAny<int>());
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(500, objectResponse.StatusCode);
        }

        [Fact]
        public void CreateManufacturer_RepoContainsManufacturerWithThatId_ShouldReturnConflictResponse()
        {
            _mock.Setup(c => c.Create(It.IsAny<Manufacturer>()))
                .Returns(new ResultModel<Manufacturer>(It.IsAny<Manufacturer>(), 409));
            var result = _controller.CreateManufacturer(_createManufacturer);
            Assert.IsType<StatusCodeResult>(result);
            var objectResponse = (StatusCodeResult) result;
            Assert.Equal(409, objectResponse.StatusCode);
        }

        [Fact]
        public void CreateManufacturer_SuccessfullyCreatedManufacturer_ShouldReturnCreatedResponse()
        {
            _mock.Setup(c => c.Create(It.IsAny<Manufacturer>()))
                .Returns(new ResultModel<Manufacturer>(_manufacturer, (int) HttpStatusCode.Created));
            var result = _controller.CreateManufacturer(_createManufacturer);
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(201, objectResponse.StatusCode);
        }

        [Fact]
        public void CreateManufacturer_ErrorWhenCreatingManufacturer_ShouldReturnInternalServerErrorResponse()
        {
            _mock.Setup(c => c.Manufacturers)
                .Throws(new Exception());
            var result = _controller.CreateManufacturer(_createManufacturer);
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(500, objectResponse.StatusCode);
        }

        [Fact]
        public void CreateManufacturer_ModelStateInvalid_ShouldReturnBadRequestResponse()
        {
            _controller.ModelState.AddModelError("key", "error");
            var result = _controller.CreateManufacturer(_createManufacturer);
            Assert.IsType<BadRequestResult>(result);
            var objectResponse = (BadRequestResult) result;
            Assert.Equal(400, objectResponse.StatusCode);
        }

        [Fact]
        public void UpdateManufacturer_ManufacturerNotFound_ShouldReturnNotFoundResult()
        {
            _mock.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(new ResultModel<Manufacturer>(It.IsAny<Manufacturer>(), (int) HttpStatusCode.NotFound));
            var result = _controller.UpdateManufacturer(_updateManufacturer);
            Assert.IsType<NotFoundResult>(result);
            var objectResponse = (NotFoundResult) result;
            Assert.Equal(404, objectResponse.StatusCode);
        }

        [Fact]
        public void UpdateManufacturer_ManufacturerEditedSuccessfully_ShouldReturnOkResponse()
        {
            _mock.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(new ResultModel<Manufacturer>(_manufacturer, (int) HttpStatusCode.OK));
            _mock.Setup(c => c.Update(It.IsAny<Manufacturer>()))
                .Returns(new ResultModel<Manufacturer>(_manufacturer, (int) HttpStatusCode.OK));
            var result = _controller.UpdateManufacturer(_updateManufacturer);
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(200, objectResponse.StatusCode);
        }

        [Fact]
        public void UpdateManufacturer_StatusCodeIsNotSuccessful_ShouldReturnUnauthorizedResponse()
        {
            _mock.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(new ResultModel<Manufacturer>(_manufacturer, (int) HttpStatusCode.OK));
            _mock.Setup(c => c.Update(It.IsAny<Manufacturer>()))
                .Returns(new ResultModel<Manufacturer>(_manufacturer, (int) HttpStatusCode.Unauthorized));
            var result = _controller.UpdateManufacturer(_updateManufacturer);
            Assert.IsType<StatusCodeResult>(result);
            var objectResponse = (StatusCodeResult) result;
            Assert.Equal(401, objectResponse.StatusCode);
        }

        [Fact]
        public void UpdateManufacturer_ExceptionWhenAccessingRepo_ShouldReturnInternalServerErrorResponse()
        {
            _mock.Setup(c => c.GetById(It.IsAny<int>()))
                .Throws(new Exception());
            _mock.Setup(c => c.Update(It.IsAny<Manufacturer>()))
                .Returns(new ResultModel<Manufacturer>(It.IsAny<Manufacturer>(), (int) HttpStatusCode.OK));
            var result = _controller.UpdateManufacturer(_updateManufacturer);
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(500, objectResponse.StatusCode);
        }

        [Fact]
        public void DeleteManufacturer_ManufacturerNotFound_ShouldReturnNotFoundResponse()
        {
            _mock.Setup(c => c.Delete(It.IsAny<int>()))
                .Returns(new ResultModel<Manufacturer>(It.IsAny<Manufacturer>(), (int) HttpStatusCode.NotFound));
            var result = _controller.DeleteManufacturer(It.IsAny<int>());
            Assert.IsType<StatusCodeResult>(result);
            var objectResponse = (StatusCodeResult) result;
            Assert.Equal(404, objectResponse.StatusCode);
        }

        [Fact]
        public void DeleteManufacturer_ExceptionWhenDeleting_ShouldReturnInternalServerErrorResponse()
        {
            _mock.Setup(c => c.Delete(It.IsAny<int>()))
                .Throws(new Exception());
            var result = _controller.DeleteManufacturer(It.IsAny<int>());
            Assert.IsType<ObjectResult>(result);
            var objectResponse = (ObjectResult) result;
            Assert.Equal(500, objectResponse.StatusCode);
        }
    }
}