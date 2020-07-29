using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Tests.PageObjects
{
    public class TableSearchFilterDemoPage
    {
        private readonly IWebDriver _webDriver;

        public TableSearchFilterDemoPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement TaskTable => _webDriver.FindElement(By.Id("task-table"));
    }
}
