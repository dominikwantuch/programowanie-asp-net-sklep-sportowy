using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageModels;
using Xunit;

namespace SeleniumTests
{
    public class LoginFormTests : TestBase
    {
        public LoginFormTests() : base()
        {
        }

        [Fact]
        public void ShouldProperlyLogin()
        {
            Driver.Navigate().GoToUrl(_baseUrl);
            
            var homePage = new HomePage(Driver);
            homePage.NavigateToLogin();
            
            var loginPage = new LoginPage(Driver);
            
            loginPage.FillLoginTextField("admin");
            loginPage.FillPasswordField("test123");
            loginPage.ClickSubmitButton();

            var logOutButton = Driver.FindElement(By.Id("logout"));
            
            Assert.True(logOutButton.Displayed);
            
            Assert.Equal(_baseUrl + "/admin/products", Driver.Url);
        }

        [Fact]
        public void ShouldShowLoginRequiredValidationError()
        {
            Driver.Navigate().GoToUrl(_baseUrl);
            
            var homePage = new HomePage(Driver);
            homePage.NavigateToLogin();
            
            var loginPage = new LoginPage(Driver);
            
            loginPage.FillPasswordField("test123");
            loginPage.ClickSubmitButton();

            var nameRequiredElement = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Name']"));
            var passwordRequiredElement = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));
            
            Assert.True(nameRequiredElement.Displayed);
            Assert.Equal("The Name field is required.", nameRequiredElement.Text);
            Assert.Equal(_baseUrl + "/account/login", Driver.Url);
            Assert.False(passwordRequiredElement.Displayed);
        }

        [Fact]
        public void ShouldShowPasswordRequiredValidationError()
        {
            Driver.Navigate().GoToUrl(_baseUrl);
            
            var homePage = new HomePage(Driver);
            homePage.NavigateToLogin();
            
            var loginPage = new LoginPage(Driver);
            
            loginPage.FillLoginTextField("admin");
            loginPage.ClickSubmitButton();

            var nameRequiredElement = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Name']"));
            var passwordRequiredElement = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));
            
            Assert.True(passwordRequiredElement.Displayed);
            Assert.Equal("The Password field is required.", passwordRequiredElement.Text);
            Assert.Equal(_baseUrl + "/account/login", Driver.Url);
            Assert.False(nameRequiredElement.Displayed);            
        }

    }
}