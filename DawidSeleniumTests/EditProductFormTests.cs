using System;
using System.Linq;
using DawidSeleniumTests.PageModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace DawidSeleniumTests
{
    public class EditProductFormTests : TestsFixture
    {
        private readonly string _url;

        public EditProductFormTests()
        {
            _url = "https://localhost:5001";
        }

        [Fact]
        public void ClickFirstProductToEdit_ShouldReturnUrlWithId1()
        {
            Driver.Navigate().GoToUrl(_url);
            var helper = new LoginHelper(Driver);
            helper.LoginToSystem();
            var firstEditButton =
                Driver.FindElements(By.CssSelector("a button")).First();
            firstEditButton.Click();
            
            Assert.Equal(_url + "/admin/products/edit?id=1", Driver.Url);
        }


    }
}