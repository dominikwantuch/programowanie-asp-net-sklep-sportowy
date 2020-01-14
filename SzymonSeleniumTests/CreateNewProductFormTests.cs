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
        public void ProductCategoryNotGiven_ShouldReturnErrorMessage()
        {
            _driver.FindElement(By.Id("ManufacturerId"))
                .SendKeys("1");

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Test product");

            _driver.FindElement(By.Id("Description"))
                .SendKeys(" Test product description");

            _driver.FindElement(By.Id("Price"))
                .SendKeys("50");


            _driver.FindElement(By.Id("submit"))
                .Click();

            var errorMessage = _driver.FindElement(By.Id("Category-error")).Text;

            Assert.Equal("Category must be given", errorMessage);

            _driver.Dispose();

        }

    }
}
