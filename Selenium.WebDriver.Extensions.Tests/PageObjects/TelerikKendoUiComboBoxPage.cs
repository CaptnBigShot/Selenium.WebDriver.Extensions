using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Telerik.KendoUi;

namespace Selenium.WebDriver.Extensions.Tests.PageObjects
{
    public class TelerikKendoUiComboBoxPage
    {
        private readonly IWebDriver _webDriver;

        public TelerikKendoUiComboBoxPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement ImageElement => _webDriver.FindElement(By.Id("tshirt"));
       
        public string FabricKendoComboBoxId => "fabric_listbox";

        public IWebElement FabricKendoComboBox => _webDriver.FindElement(By.XPath($"//input[@aria-owns='{FabricKendoComboBoxId}']/parent::span/parent::span"));

        public KendoComboBoxElement FabricKendoComboBoxElement => new KendoComboBoxElement(FabricKendoComboBox);
    }
}
