using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Telerik.KendoUi;

namespace Selenium.WebDriver.Extensions.Tests.PageObjects
{
    public class TelerikKendoMultiSelectPage
    {
        private readonly IWebDriver _webDriver;

        public TelerikKendoMultiSelectPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement ExampleWrapElement => _webDriver.FindElement(By.Id("exampleWrap"));
       
        public string OptionalMultiSelectKendoId => "optional_taglist";

        public KendoMultiSelectElement OptionalKendoMultiSelectElement => new KendoMultiSelectElement(_webDriver, OptionalMultiSelectKendoId);
    }
}
