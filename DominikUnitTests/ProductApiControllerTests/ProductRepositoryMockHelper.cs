using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
// ReSharper disable StringLiteralTypo

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductRepositoryMockHelper
    {
        public readonly ApplicationDbContext ErrorMockedDbContext;
        public readonly List<Product> Products = new List<Product>()
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
            },
            new Product()
            {
                ProductId = 4,
                ManufacturerId = 2,
                Name = "Władca pierścieni",
                Description = "Pionki, karty, mapy i inne takie",
                Price = 255,
                Category = "Gry planszowe",
            },
            new Product()
            {
                ProductId = 5,
                ManufacturerId = 2,
                Name = "Nemesis",
                Description = "20 dużych figuer oraz plansza",
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
            ProductId = 10
        };

        public readonly Product OutOfRangeIdEntity = new Product()
        {
            ProductId = -1
        };

        public ProductRepositoryMockHelper()
        {
            Mock<ApplicationDbContext> mock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            mock.Object.Products = GetQueryableMockDbSet<Product>();
            ErrorMockedDbContext = mock.Object;

        }
        
        private static DbSet<T> GetQueryableMockDbSet<T>() where T : class
        {
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Throws(new Exception());
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Throws(new Exception());
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Throws(new Exception());
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Throws(new Exception());
            
            return dbSet.Object;
        }
    }
}