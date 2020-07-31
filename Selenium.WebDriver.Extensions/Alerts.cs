using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions
{
    public static class Alerts
    {
        /// <summary>
        /// Checks if a javascript alert is present.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public static bool IsAlertPresent(this IWebDriver webDriver)
        {
            try
            {
                webDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Switches the window to the alert and accepts it.
        /// </summary>
        /// <param name="webDriver"></param>
        public static void AcceptAlert(this IWebDriver webDriver)
        {
            webDriver.SwitchTo().Alert().Accept();
            webDriver.WaitForPageToLoad();
        }

        /// <summary>
        /// Clicks an element that triggers an alert and then accepts the alert.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="element"></param>
        public static void ClickElementAndAcceptAlert(this IWebDriver webDriver, IWebElement element)
        {
            element.Click();
            webDriver.AcceptAlert();
        }
    }
}
