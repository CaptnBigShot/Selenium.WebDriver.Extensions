using OpenQA.Selenium;

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
    }
}
