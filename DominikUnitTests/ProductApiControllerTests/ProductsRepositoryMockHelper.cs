using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportShop.Models;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductsRepositoryMockHelper
    {
        public readonly Mock<IProductRepository> Mock = new Mock<IProductRepository>();
        
        public readonly Product CreateProductModel = new Product()
        {
            ProductId = 0,
            ManufacturerId = 1,
            Name = "Okulary do pływania",
            Description = "Okulary zbudowane z karbonu.",
            Price = 44,
            Category = "Sporty Wodne"
        };

        public readonly Product EditBadProductModel = new Product()
        {
            ProductId = 3,
            ManufacturerId = 1,
            Name = "Płetwy dla nurka",
            Description = "Płetwy w rozmiarze uniwersalnym.",
            Price = 165,
            Category = "Sporty Wodne",  
        };

        public ProductsRepositoryMockHelper()
        {
            var products = new List<Product>()
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
            Mock.Setup(x => x.Products).Returns(products.AsQueryable());

            Mock.Setup(x=> x.GetAll(null)).Returns(new ResultModel<IEnumerable<Product>>(products, 200));
            
            Mock.Setup(x => x.GetAll("Sporty Wodne"))
                .Returns(new ResultModel<IEnumerable<Product>>(products.Where(x => x.Category == "Sporty Wodne"), 200));
            
            Mock.Setup(x => x.GetAll("Gry planszowe"))
                .Returns(new ResultModel<IEnumerable<Product>>(products.Where(x => x.Category == "Gry planszowe"), 200));

            // GetAll shouldn't throw and exception because it's using try catch block but I added it for sake of testing controller.
            Mock.Setup(x => x.GetAll("throw")).Throws(new Exception());
            
            Mock.Setup(x => x.SaveProduct(CreateProductModel)).Returns(true);
            Mock.Setup(x => x.SaveProduct(EditBadProductModel)).Returns(false);
            
            Mock.Setup(x => x.DeleteProduct(1)).Returns(true);
            Mock.Setup(x => x.DeleteProduct(10)).Returns(false);
        }
    }
}