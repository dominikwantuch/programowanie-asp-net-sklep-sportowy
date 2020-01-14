using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SzymonSeleniumTests
{
    public class CreateNewProductFormTests
    {
        private readonly IWebDriver _driver;

        public CreateNewProductFormTests()
        {
            _driver = new ChromeDriver();

            _driver.Navigate().GoToUrl("https://localhost:5001/admin/products/create");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("admin");

            _driver.FindElement(By.Id("Password"))
                .SendKeys("test123");

            _driver.FindElement(By.Id("submit"))
                .Click();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        
        [Fact]
        public void ProductManufacturerIdNotGiven_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys(" Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("ManufacturerId-error")).Text;

            Assert.Equal("The ManufacturerId field is required.", errorMessage);

            _driver.Dispose();

        }
        [Fact]
        public void ProductManufacturerIdIsString_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("Test");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys(" Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("ManufacturerId-error")).Text;

            Assert.Equal("The value 'Test' is not valid for ManufacturerId.", errorMessage);

            _driver.Dispose();

        }

        [Fact]
        public void ProductNameNotGiven_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("");

            _driver.FindElement(By.Id("Description"))
                .SendKeys(" Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Name-error")).Text;

            Assert.Equal("The Name field is required.", errorMessage);

            _driver.Dispose();

        }

        [Fact]
        public void ProductNameStringLengthLessThanThree_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Te");

            _driver.FindElement(By.Id("Description"))
                .SendKeys("Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Name-error")).Text;

            Assert.Equal("The field Name must be a string with a minimum length of 3 and a maximum length of 60.", errorMessage);

            _driver.Dispose();

        }

        [Fact]
        public void ProductNameStringLengthGraterThanSixty_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Testowy produkt, Testowy produkt, Testowy produkt, Testowy produkt, Testowy produkt, Testowy produkt");

            _driver.FindElement(By.Id("Description"))
                .SendKeys("Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Name-error")).Text;

            Assert.Equal("The field Name must be a string with a minimum length of 3 and a maximum length of 60.", errorMessage);

            _driver.Dispose();

        }

        [Fact]
        public void ProductDescriptionNotGiven_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys("");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Description-error")).Text;

            Assert.Equal("Description is required", errorMessage);

            _driver.Dispose();

        }
        [Fact]
        public void ProductPriceIsNotGraterThanZero_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys(" Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("0");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Price-error")).Text;

            Assert.Equal("Price must be grater than 0", errorMessage);

            _driver.Dispose();

        }
        [Fact]
        public void ProductPriceIsString_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys(" Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("Test");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Price-error")).Text;

            Assert.Equal("The value 'Test' is not valid for Price.", errorMessage);

            _driver.Dispose();

        }

        [Fact]
        public void ProductCategoryNotGiven_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys(" Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Category-error")).Text;

            Assert.Equal("Category must be given", errorMessage);

            _driver.Dispose();

        }

        [Fact]
        public void ShouldProperlyCreateProduct()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys("Test product description");

            _driver.FindElement(By.Id("Price"))
                .Clear();
            _driver.FindElement(By.Id("Price"))
                .SendKeys("40");

            _driver.FindElement(By.Id("Category"))
                .SendKeys("Test product category");

            _driver.FindElement(By.Id("submit"))
                .Click();

            var editButton = _driver.FindElement(By.Id("editButton"));
            var deleteButton = _driver.FindElement(By.Id("deleteButton"));


            Assert.True(editButton.Displayed);
            Assert.True(deleteButton.Displayed);
            Assert.Equal("https://localhost:5001/admin/products/save", _driver.Url);


            _driver.Dispose();
        }




    }
}
