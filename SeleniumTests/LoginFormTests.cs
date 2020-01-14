using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace SeleniumTests
{
    public class LoginFormTests : TestBase
    {
        public LoginFormTests() : base()
        {
        }

        [Fact]
        public void LoginFormTest()
        {
            Driver.Navigate().GoToUrl("http://localhost:5000");
            var homePage = new HomePage(Driver);
            homePage.NavigateToLogin();
            
            var loginPage = new LoginPage(Driver);
            loginPage.FillLoginTextField("admin");
            loginPage.FillPasswordField("test123");
            loginPage.ClickSubmitButton();
            
            Thread.Sleep(3000);
            // var text = "Selenium test";
            // Driver.Navigate().GoToUrl("https://google.pl");
            // var searchTextBox = Driver.FindElement(By.Name("q"));
            // searchTextBox.SendKeys(text + Keys.Enter);
            //
            // Assert.StartsWith("https://www.google.pl/search?", Driver.Url);
        }
    }
}