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
            ProductId = 12,
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

            //    NOTE FOR TEACHER
            //    IN SOME CASES I MOCKED METHODS TO THROW EXCEPTIONS EVEN IF THEY USE TRY-CATCH BLOCKS JUST TO HIT 100% COVERAGE ON CONTROLLER!

            #region GetAll

            Mock.Setup(x => x.GetAll(null)).Returns(new ResultModel<IEnumerable<Product>>(products, 200));

            // GetAll shouldn't throw and exception because it's using try catch block but I added it for sake of testing controller.
            Mock.Setup(x => x.GetAll("throw")).Throws(new Exception());

            #endregion

            #region GetById

            Mock.Setup(x => x.GetById(1))
                .Returns(new ResultModel<Product>(products.FirstOrDefault(x => x.ProductId == 1), 200));

            Mock.Setup(x => x.GetById(5)).Returns(new ResultModel<Product>(null, 404));

            Mock.Setup(x => x.GetById(7)).Throws(new Exception());

            #endregion

            #region CreateProduct

            Mock.Setup(x => x.Create(It.Is<Product>(createProduct => createProduct.ProductId == 0)))
                .Returns(new ResultModel<Product>(ReturnCreateProductEntity, 201));

            Mock.Setup(x => x.Create(It.Is<Product>(createExistingProduct =>
                    createExistingProduct.Name == CreateExistingProductModel.Name)))
                .Returns(new ResultModel<Product>(null, 409));

            Mock.Setup(x =>
                    x.Create(It.Is<Product>(throwErrorProduct =>
                        throwErrorProduct.Name == ThrowErrorProductModel.Name)))
                .Throws(new Exception());

            #endregion

            //Mock.Setup(x => x.SaveProduct(CreateProductModel)).Returns(true);
            Mock.Setup(x => x.SaveProduct(EditBadProductModel)).Returns(false);

            Mock.Setup(x => x.DeleteProduct(1)).Returns(true);
            Mock.Setup(x => x.DeleteProduct(10)).Returns(false);
        }
    }
}