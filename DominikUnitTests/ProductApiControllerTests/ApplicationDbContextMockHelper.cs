using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ApplicationDbContextMockHelper
    {
        public readonly ApplicationDbContext MockedDbContext;
        
        List<Product> _products = new List<Product>()
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

        public readonly Product SaveProductEntity = new Product()
        {
            ProductId = 0,
            ManufacturerId = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 10,
            Category = "ProductCategory"
        };

        public readonly Product SaveProductUpdateEntity = new Product()
        {
            ProductId = 1,
            ManufacturerId = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 10,
            Category = "ProductCategory"
        };

        public readonly Product SaveProductNotExistingEntity = new Product()
        {
            ProductId = 5
        };

        public readonly Product SaveProductOutOfRangeIdEntity = new Product()
        {
            ProductId = -1
        };
        
        public ApplicationDbContextMockHelper()
        {
            MockedDbContext = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            var mockedDbSet = GetQueryableMockDbSet(_products);
            MockedDbContext.Products = mockedDbSet;
        }
        
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}