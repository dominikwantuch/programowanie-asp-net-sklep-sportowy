using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
using Xunit;

namespace DawidUnitTests
{
    public class ManufacturerRepositoryTests
    {
        private readonly Manufacturer _manufacturer = new Manufacturer
        {
            Id = 1,
            Name = "Razer",
            Country = "USA",
        };

        private List<Manufacturer> _manufacturers = new List<Manufacturer>
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
            }
        };


        [Fact]
        public void ManufacturerRepository_ThreeManufacturers_ShouldReturnCollectionOfManufacturers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("1E136FCA-5B52-426C-B587-22920EAF4F89").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }
            
            using(var context = new ApplicationDbContext(options))
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.Manufacturers;
                
                Assert.NotNull(result);
                Assert.Equal(3, result.Count());
            }

        }
        
    }
}