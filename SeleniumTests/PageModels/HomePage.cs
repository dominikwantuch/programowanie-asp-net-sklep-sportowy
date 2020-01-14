using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumTests.PageModels
{
    public class HomePage
    {
        private IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "mainPage")]
        public IWebElement MainPage { get; set; }
        
        [FindsBy(How = How.Id, Using = "login")]
        public IWebElement LoginButton { get; set; }

        public void NavigateToLogin()
        {
            LoginButton.Click();
        }
    }
}