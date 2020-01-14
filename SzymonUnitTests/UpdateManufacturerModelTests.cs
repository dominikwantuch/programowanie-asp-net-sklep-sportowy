using SportShop.Models;
using SportShop.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SzymonUnitTests
{
    public class UpdateManufacturerModelTests
    {
        private Manufacturer _entity = new Manufacturer()
        {
            Id = 1,
            Name = "Cerasit",
            Country = "Poland"
        };
        private readonly UpdateManufacturerModel _updateManufacturerModel = new UpdateManufacturerModel()
        {
            Id = 20,
            Name = "Avon",
            Country = "England"
        };

        [Fact]
        public void ShouldUpdateManufacturerModel()
        {
            _updateManufacturerModel.ToEntity(_entity);

            Assert.Equal(1, _entity.Id);
            Assert.Equal("Avon", _entity.Name);
            Assert.Equal("England", _entity.Country);
        }

        [Fact]
        public void ShouldUpdateManufacturerModel_ExceptionOccurs()
        {
            _entity = null; 
            Action act = () => _updateManufacturerModel.ToEntity(_entity);

            var exception = Assert.Throws<ArgumentNullException>(act);
        }
    }
}
