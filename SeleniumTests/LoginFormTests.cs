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
        private string _baseUrl;
        public LoginFormTests() : base()
        {
            _baseUrl = "http://localhost:5000";
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
            Assert.Contains("The Name field is required.", nameRequiredElement.Text);
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
            Assert.Contains("The Password field is required.", passwordRequiredElement.Text);
            Assert.Equal(_baseUrl + "/account/login", Driver.Url);
            Assert.False(nameRequiredElement.Displayed);            
        }
        
        
        [Fact]
        public void ShouldShowLoginAndPasswordRequiredValidationError()
        {
            Driver.Navigate().GoToUrl(_baseUrl);
            
            var homePage = new HomePage(Driver);
            homePage.NavigateToLogin();
            
            var loginPage = new LoginPage(Driver);
            
            loginPage.ClickSubmitButton();

            var nameRequiredElement = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Name']"));
            var passwordRequiredElement = Driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));
            
            Assert.True(passwordRequiredElement.Displayed);
            Assert.True(nameRequiredElement.Displayed);
            
            Assert.Contains("The Name field is required.", nameRequiredElement.Text);
            Assert.Contains("The Password field is required.", passwordRequiredElement.Text);
            Assert.Equal(_baseUrl + "/account/login", Driver.Url);
        }

        [Fact]
        public void ShouldDisplayWrongLoginOrPasswordNotification()
        {
            Driver.Navigate().GoToUrl(_baseUrl);
            
            var homePage = new HomePage(Driver);
            homePage.NavigateToLogin();
            
            var loginPage = new LoginPage(Driver);
            loginPage.FillLoginTextField("wrongLogin");
            loginPage.FillPasswordField("wrongPassword");
            loginPage.ClickSubmitButton();

            var validationErrorNotification = Driver.FindElement(By.CssSelector("div[class='alert alert-warning']"));
            
            Assert.True(validationErrorNotification.Displayed);
            Assert.Contains("Username or password is invalid!", validationErrorNotification.Text);
            Assert.Equal(_baseUrl + "/account/login", Driver.Url);            
        }

    }
}