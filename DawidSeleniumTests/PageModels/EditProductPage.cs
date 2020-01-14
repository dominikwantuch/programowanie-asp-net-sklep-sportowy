using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace DawidSeleniumTests.PageModels
{
    public class EditProductPage
    {
        private IWebDriver _driver;

        public EditProductPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "Name")]
        public IWebElement NameTextField { get; set; }

        [FindsBy(How = How.Name, Using = "Description")]
        public IWebElement DescriptionTextField { get; set; }

        [FindsBy(How = How.Name, Using = "Price")]
        public IWebElement PriceTextField { get; set; }

        [FindsBy(How = How.ClassName, Using = "btn")]
        public IWebElement SubmitButton { get; set; }

        public void FillNameTextField(string text)
        {
            NameTextField.Clear();
            NameTextField.SendKeys(text);
        }

        public void FillDescriptionTextField(string text)
        {
            DescriptionTextField.Clear();
            DescriptionTextField.SendKeys(text);
        }

        public void FillPriceTextField(string text)
        {
            PriceTextField.Clear();
            PriceTextField.SendKeys(text);
        }

        public void ClickSubmitButton()
        {
            SubmitButton.Click();
        }
    }
}