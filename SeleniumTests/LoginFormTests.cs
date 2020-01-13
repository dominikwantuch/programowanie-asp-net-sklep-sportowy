using System;
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
        public void Test1()
        {
            var text = "Selenium test";
            Driver.Navigate().GoToUrl("https://google.pl");
            var searchTextBox = Driver.FindElement(By.Name("q"));
            searchTextBox.SendKeys(text + Keys.Enter);

            Assert.StartsWith("https://www.google.pl/search?", Driver.Url);
        }
    }
}