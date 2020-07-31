using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Tests.PageObjects
{
    public class BasicCheckboxDemoPage
    {
        private readonly IWebDriver _webDriver;

        public BasicCheckboxDemoPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement Body => _webDriver.FindElement(By.TagName("body"));

        public IWebElement Checkbox => _webDriver.FindElement(By.Id("isAgeSelected"));

        public CheckboxElement CheckboxElement => new CheckboxElement(Checkbox);
    }
}
