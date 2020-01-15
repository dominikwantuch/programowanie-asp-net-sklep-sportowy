using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportShop.Models;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ProductsApiMockHelper
    {
        public readonly Mock<IProductRepository> Mock = new Mock<IProductRepository>();

        public readonly List<Product> Products= new List<Product>()
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
        #region CreateProductModels

        public readonly CreateProductModel CreateProductModel = new CreateProductModel
        {
            ManufacturerId = 1,
            Name = "Okulary do pływania",
            Description = "Okulary zbudowane z karbonu.",
            Price = 44,
            Category = "Sporty Wodne"
        };

        public readonly Product ReturnCreateProductEntity = new Product()
        {
            ProductId = 5,
            ManufacturerId = 1,
            Name = "Okulary do pływania",
            Description = "Okulary zbudowane z karbonu.",
            Price = 44,
            Category = "Sporty Wodne"
        };

        public readonly CreateProductModel CreateExistingProductModel = new CreateProductModel()
        {
            ManufacturerId = 2,
            Name = "ExistingProduct",
            Description = "Okulary zbudowane z karbonu.",
            Price = 44,
            Category = "Sporty Wodne"
        };

        public readonly CreateProductModel ThrowErrorProductModel = new CreateProductModel()
        {
            ManufacturerId = 2,
            Name = "Throw",
            Description = "Okulary zbudowane z karbonu.",
            Price = 44,
            Category = "Sporty Wodne"
        };

        #endregion

        #region UpdateProductModels

        public readonly UpdateProductModel UpdateProductModel = new UpdateProductModel()
        {
            ProductId = 10,
            ManufacturerId = 1,
            Name = "UpdateProductName-Updated",
            Description = "UpdateProductDescription",
            Price = 10,
            Category = "UpdateProductCategory"
        };

        public readonly Product ToUpdateProductEntity = new Product()
        {
            ProductId = 10,
            ManufacturerId = 1,
            Name = "UpdateProductName",
            Description = "UpdateProductDescription",
            Price = 10,
            Category = "UpdateProductCategory"
        };

        public readonly Product UpdatedProductEntity = new Product()
        {
            ProductId = 10,
            ManufacturerId = 1,
            Name = "UpdateProductNameUpdated-Updated",
            Description = "UpdateProductDescription",
            Price = 10,
            Category = "UpdateProductCategory"
        };

        public readonly UpdateProductModel NotFoundUpdateProductModel = new UpdateProductModel()
        {
            ProductId = 15
        };

        public readonly UpdateProductModel BadRequestUpdateProductModel = new UpdateProductModel()
        {
            ProductId = 20
        };

        public readonly Product BadRequestUpdateProduct = new Product()
        {
            ProductId = 20
        };

        public readonly UpdateProductModel ThrowErrorUpdateProductModel = new UpdateProductModel()
        {
            ProductId = 25
        };

        #endregion

        public ProductsApiMockHelper()
        {
        }
    }
}