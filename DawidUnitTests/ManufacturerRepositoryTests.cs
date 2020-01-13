using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            using (var context = new ApplicationDbContext(options))
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.Manufacturers;

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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.DeleteManufacturer(4);

                Assert.False(result);
                Assert.Equal(3, manufacturersRepository.Manufacturers.Count());
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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.DeleteManufacturer(3);

                Assert.True(result);
                Assert.Equal(2, manufacturersRepository.Manufacturers.Count());
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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.SaveManufacturer(manufacturer);

                Assert.True(result);
                Assert.Equal(1, manufacturersRepository.Manufacturers.Count());
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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.SaveManufacturer(manufacturer);

                Assert.True(result);
                Assert.Equal(3, manufacturersRepository.Manufacturers.Count());

                var modified = manufacturersRepository.Manufacturers
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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.SaveManufacturer(manufacturer);

                Assert.False(result);
                Assert.Equal(3, manufacturersRepository.Manufacturers.Count());
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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.SaveManufacturer(manufacturer);

                Assert.False(result);
                Assert.Equal(3, manufacturersRepository.Manufacturers.Count());
            }
        }

        [Fact]
        public void GetById_IdExistsInRepo_ShouldReturnOkStatusCodeAndManufacturer()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("25D51D2B-3A99-4A41-8D90-0EBBA7CDEF76").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.GetById(1);

                Assert.NotNull(result);
                Assert.NotNull(result.Data);
                Assert.Equal((int) HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Fact]
        public void GetById_IdDoesNotExistInRepo_ShouldReturnNotFoundStatusCodeAndNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("50CE0FB7-45C0-4656-B6A8-C61C59157DB0").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.AddRange(_manufacturers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.GetById(4);

                Assert.NotNull(result);
                Assert.Null(result.Data);
                Assert.Equal((int) HttpStatusCode.NotFound, result.StatusCode);
            }
        }

        private DbSet<T> GetQueryableMockDbSet<T>() where T : class
        {
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Throws(new Exception());
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Throws(new Exception());
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Throws(new Exception());
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Throws(new Exception());

            return dbSet.Object;
        }

        [Fact]
        public void GetById_Exception_ShouldReturnInternalServerErrorStatusCodeAndNull()
        {
            Mock<ApplicationDbContext> mock =
                new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            mock.Object.Manufacturers = GetQueryableMockDbSet<Manufacturer>();

            using (var manufacturersRepository = new ManufacturerRepository(mock.Object))
            {
                var result = manufacturersRepository.GetById(4);

                Assert.NotNull(result);
                Assert.Null(result.Data);
                Assert.Equal((int) HttpStatusCode.InternalServerError, result.StatusCode);
            }
        }

        [Fact]
        public void Create_SuccessfullyCreated_ShouldReturnCreatedStatusCodeAndManufacturer()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("756EC067-CA25-4852-A780-BA5967904A0A").Options;
            var manufacturer = new Manufacturer
            {
                Id = 3,
                Name = "Razer",
                Country = "USA",
            };

            using (var context = new ApplicationDbContext(options))
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.Create(manufacturer);

                Assert.NotNull(result);
                Assert.NotNull(result.Data);
                Assert.Equal((int) HttpStatusCode.Created, result.StatusCode);
            }
        }

        [Fact]
        public void Create_Conflict_ShouldReturnConflictStatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("E6E6BD6F-5A17-40EF-9E57-BFB76005C75E").Options;
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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.Create(manufacturer);

                Assert.NotNull(result);
                Assert.NotNull(result.Data);
                Assert.Equal((int) HttpStatusCode.Conflict, result.StatusCode);
            }
        }

        [Fact]
        public void Create_ExceptionThrownByRepo_ShouldReturnInternalServerError()
        {
            Mock<ApplicationDbContext> mock =
                new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            mock.Object.Manufacturers = GetQueryableMockDbSet<Manufacturer>();

            var manufacturer = new Manufacturer
            {
                Id = 3,
                Name = "Razer",
                Country = "USA",
            };
            using (var manufacturersRepository = new ManufacturerRepository(mock.Object))
            {
                var result = manufacturersRepository.Create(manufacturer);

                Assert.NotNull(result);
                Assert.Null(result.Data);
                Assert.Equal((int) HttpStatusCode.InternalServerError, result.StatusCode);
            }
        }

        [Fact]
        public void Update_ResponseIsNull_ShouldReturnNotFoundStatusCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("3AD50E90-3A9F-49DB-8810-978CF71D8EE9").Options;
            using (var context = new ApplicationDbContext(options))
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.Update(_manufacturer);

                Assert.NotNull(result);
                Assert.Equal((int) HttpStatusCode.NotFound, result.StatusCode);
            }
        }

        [Fact]
        public void Update_SuccessfulUpdate_ShouldReturnOkStatusCodeAndManufacturer()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("5E588408-AD61-4884-9BF7-12987F622785")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;
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
            using (var manufacturersRepository = new ManufacturerRepository(context))
            {
                var result = manufacturersRepository.Update(manufacturer);

                Assert.NotNull(result);
                Assert.NotNull(result.Data);
                Assert.Equal(3, manufacturersRepository.Manufacturers.Count());
                Assert.Equal((int) HttpStatusCode.OK, result.StatusCode);

                var modified = manufacturersRepository.Manufacturers
                    .FirstOrDefault(c => c.Id == manufacturer.Id);
                Assert.NotNull(modified);
                Assert.Equal(manufacturer.Id, modified.Id);
                Assert.Equal(manufacturer.Name, modified.Name);
                Assert.Equal(manufacturer.Country, modified.Country);
            }
        }

        [Fact]
        public void Update_ExceptionThrownByRepo_ShouldReturnInternalServerErrorStatusCode()
        {
            Mock<ApplicationDbContext> mock =
                new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            mock.Object.Manufacturers = GetQueryableMockDbSet<Manufacturer>();
            using (var manufacturersRepository = new ManufacturerRepository(mock.Object))
            {
                var result = manufacturersRepository.Update(_manufacturer);

                Assert.NotNull(result);
                Assert.Null(result.Data);
                Assert.Equal((int) HttpStatusCode.InternalServerError, result.StatusCode);
            }
        }
    }
}