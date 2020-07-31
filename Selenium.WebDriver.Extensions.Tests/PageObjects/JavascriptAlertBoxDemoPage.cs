using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Tests.PageObjects
{
    public class JavascriptAlertBoxDemoPage
    {
        private readonly IWebDriver _webDriver;

        public JavascriptAlertBoxDemoPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement ClickMeBtn => _webDriver.FindElement(By.CssSelector("button[onclick='myConfirmFunction()']"));

        public void ClickTheClickMeButton() => ClickMeBtn.Click();
    }
}
