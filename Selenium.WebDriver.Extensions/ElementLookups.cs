using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Selenium.WebDriver.Extensions
{
    public static class ElementLookups
    {
        /// <summary>
        /// Finds an element with a wait condition before throwing an exception if the element isn't found immediately.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver webDriver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds <= 0) return webDriver.FindElement(by);

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => drv.FindElement(by));
        }

        /// <summary>
        /// Finds elements with a wait condition before throwing an exception if the element isn't found immediately.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver webDriver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds <= 0) return webDriver.FindElements(by);

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
        }

        /// <summary>
        /// Checks if an element is found.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static bool HasElement(this IWebDriver webDriver, By by)
        {
            try
            {
                webDriver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if an element is found, displayed, and enabled.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static bool HasElementAndDisplayed(this IWebDriver webDriver, By by)
        {
            try
            {
                var element = webDriver.FindElement(by);
                return element.Displayed && element.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Scrolls the element into view.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="element"></param>
        public static void ScrollToElement(this IWebDriver webDriver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].scrollIntoView()", element);
            new Actions(webDriver).MoveToElement(element).Perform();
        }
    }
}
