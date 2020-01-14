using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

// ReSharper disable CommentTypo

namespace DominikSeleniumTests
{
    public class TestBase : IDisposable
    {
        protected IWebDriver Driver;

        protected TestBase()
        {
            Setup();
        }
        
        /// <summary>
        /// Setup
        /// </summary>
        private void Setup()
        {
            // Needed to use GetCurrentDirectory() because selenium couldn't find geckodriver.exe by default
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Manage().Window.FullScreen();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        
        /// <summary>
        /// Teardown
        /// </summary>
        public void Dispose()
        {
            Driver.Close();
        }
    }
}