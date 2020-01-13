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
            Id = 0,
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

            using (var context = new ApplicationDbContext(options))
            {
                var manufacturersRepository = new Mock<ManufacturerRepository>(context);
                var result = manufacturersRepository.Object.Manufacturers;

                Assert.NotNull(result);
                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void DeleteManufacturer_EntityIsNull_ShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("407579ED-65B4-4165-8ED9-662DE394D3EA").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var manufacturersRepository = new Mock<ManufacturerRepository>(context);
                var result = manufacturersRepository.Object.DeleteManufacturer(4);

                Assert.False(result);
                Assert.Equal(3, manufacturersRepository.Object.Manufacturers.Count());
            }
        }

        [Fact]
        public void DeleteManufacturer_EntityIsFound_ShouldReturnTrueAndRemoveOneEntry()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("19D1029C-8394-4D73-B8E2-14477D81BDAC").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var manufacturersRepository = new Mock<ManufacturerRepository>(context);
                var result = manufacturersRepository.Object.DeleteManufacturer(3);

                Assert.True(result);
                Assert.Equal(2, manufacturersRepository.Object.Manufacturers.Count());
            }
        }

        [Fact]
        public void SaveManufacturer_EntityIdIs0_ShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("FFF54147-2816-4733-A98B-EC95ECC36CDD").Options;

            var manufacturer = new Manufacturer
            {
                Id = 0,
                Name = "Razer",
                Country = "USA",
            };

            using (var context = new ApplicationDbContext(options))
            {
                var manufacturersRepository = new Mock<ManufacturerRepository>(context);
                var result = manufacturersRepository.Object.SaveManufacturer(manufacturer);

                Assert.True(result);
                Assert.Equal(1, manufacturersRepository.Object.Manufacturers.Count());
            }
        }

        [Fact]
        public void SaveManufacturer_UpdateExistingEntity_ShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("74072F25-F283-4B11-AC92-AE27096F79A7").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }

            var manufacturer = new Manufacturer
            {
                Id = 3,
                Name = "Razer",
                Country = "USA",
            };

            using (var context = new ApplicationDbContext(options))
            {
                var manufacturersRepository = new Mock<ManufacturerRepository>(context);
                var result = manufacturersRepository.Object.SaveManufacturer(manufacturer);

                Assert.True(result);
                Assert.Equal(3, manufacturersRepository.Object.Manufacturers.Count());

                var modified = manufacturersRepository.Object.Manufacturers
                    .FirstOrDefault(c => c.Id == manufacturer.Id);
                Assert.NotNull(modified);
                Assert.Equal(manufacturer.Id, modified.Id);
                Assert.Equal(manufacturer.Name, modified.Name);
                Assert.Equal(manufacturer.Country, modified.Country);
            }
        }

        [Fact]
        public void SaveManufacturer_EntityIdIsGreaterThan0AndDoesNotExistInRepo_ShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("E09F073F-DF5D-48C4-964A-517A56034250").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }

            var manufacturer = new Manufacturer
            {
                Id = 5,
                Name = "Razer",
                Country = "USA",
            };

            using (var context = new ApplicationDbContext(options))
            {
                var manufacturersRepository = new Mock<ManufacturerRepository>(context);
                var result = manufacturersRepository.Object.SaveManufacturer(manufacturer);

                Assert.False(result);
                Assert.Equal(3, manufacturersRepository.Object.Manufacturers.Count());
            }
        }

        [Fact]
        public void SaveManufacturer_EntityIdIsSmallerThan0_ShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("61DCFC11-0727-47D6-8FBA-E5FB567104B9").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }

            var manufacturer = new Manufacturer
            {
                Id = -1,
                Name = "Razer",
                Country = "USA",
            };

            using (var context = new ApplicationDbContext(options))
            {
                var manufacturersRepository = new Mock<ManufacturerRepository>(context);
                var result = manufacturersRepository.Object.SaveManufacturer(manufacturer);

                Assert.False(result);
                Assert.Equal(3, manufacturersRepository.Object.Manufacturers.Count());
            }
        }
    }
}