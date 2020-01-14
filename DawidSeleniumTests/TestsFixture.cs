using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DawidSeleniumTests
{
    public abstract class TestsFixture : IDisposable
    {
        public IWebDriver Driver;
        
        public TestsFixture()
        {
            Setup();
        }

        private void Setup()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Manage().Window.FullScreen();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        public void Dispose()
        {
            Driver.Quit();
        }
    }
}