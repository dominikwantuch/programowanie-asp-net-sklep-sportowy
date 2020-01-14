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

        [Fact]
        public void EditProductName_FromKajakToKajakEdit_ShouldReturnProductWithKajakEditName()
        {
            Driver.Navigate().GoToUrl(_url);
            var helper = new LoginHelper(Driver);
            helper.LoginToSystem();

            var firstEditButton =
                Driver.FindElements(By.CssSelector("a button")).First();
            firstEditButton.Click();

            var editName = "KajakEdit";

            var editPage = new EditProductPage(Driver);
            editPage.FillNameTextField(editName);
            editPage.ClickSubmitButton();

            var firstProductName =
                Driver.FindElements(By.TagName("h4")).First();
            Assert.Contains(editName, firstProductName.Text);
            Assert.Equal(_url + "/admin/products/save", Driver.Url);
        }

        [Fact]
        public void EditProductDescription_DescriptionTest_ShouldReturnProductWithChangedDescription()
        {
            Driver.Navigate().GoToUrl(_url);
            var helper = new LoginHelper(Driver);
            helper.LoginToSystem();

            var firstEditButton =
                Driver.FindElements(By.CssSelector("a button")).First();
            firstEditButton.Click();

            var editName = "DescriptionTest";

            var editPage = new EditProductPage(Driver);
            editPage.FillDescriptionTextField(editName);
            editPage.ClickSubmitButton();

            var firstProductDescription =
                Driver.FindElements(By.TagName("h6")).First();
            Assert.Contains(editName, firstProductDescription.Text);
            Assert.Equal(_url + "/admin/products/save", Driver.Url);
        }

        [Fact]
        public void EditProductPrice_Price300_ShouldReturnProductWithChangedPrice()
        {
            Driver.Navigate().GoToUrl(_url);
            var helper = new LoginHelper(Driver);
            helper.LoginToSystem();

            var firstEditButton =
                Driver.FindElements(By.CssSelector("a button")).First();
            firstEditButton.Click();

            var editName = "300";

            var editPage = new EditProductPage(Driver);
            editPage.FillPriceTextField(editName);
            editPage.ClickSubmitButton();

            var firstProductPrice =
                Driver.FindElements(By.TagName("h5")).First();
            Assert.Contains(editName, firstProductPrice.Text);
            Assert.Equal(_url + "/admin/products/save", Driver.Url);
        }

        [Fact]
        public void EditProductPrice_PriceIsText_ShouldReturnSamePriceAsBefore()
        {
            Driver.Navigate().GoToUrl(_url);
            var helper = new LoginHelper(Driver);
            helper.LoginToSystem();

            var firstEditButton =
                Driver.FindElements(By.CssSelector("a button")).First();
            firstEditButton.Click();
            
            var editName = "abcds";

            var editPage = new EditProductPage(Driver);
            var actualPrice = editPage.PriceTextField.Text;
            editPage.FillPriceTextField(editName);
            editPage.ClickSubmitButton();

            var firstProductPrice =
                Driver.FindElements(By.TagName("h5")).First();
            Assert.Contains(actualPrice, firstProductPrice.Text);
            Assert.Equal(_url + "/admin/products/save", Driver.Url);
        }
    }
}