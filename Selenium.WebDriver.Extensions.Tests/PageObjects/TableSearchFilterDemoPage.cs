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

        public IWebElement Body => _webDriver.FindElement(By.TagName("body"));

        public IWebElement TaskTable => _webDriver.FindElement(By.Id("task-table"));

        public TableElement TaskTableElement => new TableElement(TaskTable);

        public IWebElement TaskTableRow => TaskTable.FindElement(By.XPath(".//tr"));
    }
}
