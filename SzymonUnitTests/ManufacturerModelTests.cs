using SportShop.Models;
using SportShop.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SzymonUnitTests
{
    public class ManufacturerModelTests
    {
        private readonly Manufacturer _manufacturer = new Manufacturer()
        {
            Id = 1,
            Country = "Poland",
            Name = "Cerasit"
        };

        [Fact]
        public void ShouldMap_manufacturerToManufacturerModel()
        {
            var model = ManufacturerModel.ToModel(_manufacturer);

            Assert.Equal(1, model.Id);
            Assert.Equal("Poland", model.Country);
            Assert.Equal("Cerasit", model.Name);
        }

    }
}
