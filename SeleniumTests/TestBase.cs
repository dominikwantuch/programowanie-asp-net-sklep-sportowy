using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
// ReSharper disable CommentTypo

namespace SeleniumTests
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
            Driver = new FirefoxDriver(Directory.GetCurrentDirectory());
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