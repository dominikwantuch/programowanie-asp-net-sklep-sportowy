using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
// ReSharper disable StringLiteralTypo

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ApplicationDbContextMockHelper
    {
        public readonly ApplicationDbContext MockedDbContext;
        
        public List<Product> _products = new List<Product>()
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
        };

        public readonly Product CreateProductEntity = new Product()
        {
            ProductId = 0,
            ManufacturerId = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 10,
            Category = "ProductCategory"
        };

        public readonly Product UpdateProductEntity = new Product()
        {
            ProductId = 1,
            ManufacturerId = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 10,
            Category = "ProductCategory"
        };

        public readonly Product AlreadyExistingEntity = new Product()
        {
            ProductId = 1
        };
            
        public readonly Product NotExistingEntity = new Product()
        {
            ProductId = 5
        };

        public readonly Product OutOfRangeIdEntity = new Product()
        {
            ProductId = -1
        };
        
        
        public ApplicationDbContextMockHelper()
        {
            var mock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            mock.Object.Products = GetQueryableMockDbSet(_products);
            MockedDbContext = mock.Object;
        }
        
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            dbSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(s => sourceList.Remove(s));

            return dbSet.Object;
        }
    }
}