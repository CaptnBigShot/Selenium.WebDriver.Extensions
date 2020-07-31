using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Tests.PageObjects
{
    public class BasicFirstFormDemoPage
    {
        private readonly IWebDriver _webDriver;

        public BasicFirstFormDemoPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public By UserMessageTxtBy => By.Id("user-message");

        public IWebElement UserMessageTxt => _webDriver.FindElement(UserMessageTxtBy);

        public string GetUserMessageInputValue => UserMessageTxt.GetProperty("value");
    }
}
