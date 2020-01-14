using OpenQA.Selenium;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using SeleniumExtras.PageObjects;

namespace DawidSeleniumTests.PageModels
{
    public class LoginHelper
    {
        private IWebDriver _driver;

        public LoginHelper(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void LoginToSystem()
        {
            NavigateToLogin();
            FillLoginTextField("admin");
            FillPasswordField("test123");
            ClickSubmitButton();
        }

        [FindsBy(How = How.Id, Using = "mainPage")]
        private IWebElement MainPage { get; set; }

        [FindsBy(How = How.Id, Using = "login")]
        private IWebElement LoginButton { get; set; }

        [FindsBy(How = How.Id, Using = "Name")]
        public IWebElement LoginTextField { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement PasswordTextField { get; set; }

        [FindsBy(How = How.Id, Using = "submit")]
        public IWebElement SubmitButton { get; set; }

        public void FillLoginTextField(string content)
        {
            LoginTextField.SendKeys(content);
        }

        public void FillPasswordField(string content)
        {
            PasswordTextField.SendKeys(content);
        }

        public void ClickSubmitButton()
        {
            SubmitButton.Click();
        }

        public void NavigateToLogin()
        {
            LoginButton.Click();
        }
    }
}