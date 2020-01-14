using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumTests.PageModels
{
    public class LoginPage
    {
        private IWebDriver _driver;
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

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
    }
}