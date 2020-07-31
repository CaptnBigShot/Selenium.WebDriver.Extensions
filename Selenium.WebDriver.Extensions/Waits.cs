using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.WebDriver.Extensions
{
    public static class Waits
    {
        /// <summary>
        /// Waits for javascript to finish executing.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="numOfSeconds"></param>
        public static void WaitForPageToLoad(this IWebDriver webDriver, int numOfSeconds = 30)
        {
            var timeout = TimeSpan.FromSeconds(numOfSeconds);
            var wait = new WebDriverWait(webDriver, timeout);

            if (!(webDriver is IJavaScriptExecutor javascript))
                throw new ArgumentException("Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    var readyState = javascript.ExecuteScript("if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Waits until the element is found.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="by"></param>
        /// <param name="numOfSeconds"></param>
        public static void WaitForElementToBeDisplayed(this IWebDriver webDriver, By by, int numOfSeconds)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(numOfSeconds));
            wait.Until(condition => condition.FindElement(by));
        }

        /// <summary>
        /// Waits until the element is not found.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="by"></param>
        /// <param name="numOfSeconds"></param>
        public static void WaitForElementToNotBeDisplayed(this IWebDriver webDriver, By by, int numOfSeconds)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(numOfSeconds));
            wait.Until(drv =>
            {
                var isAjaxFinished = (bool)((IJavaScriptExecutor)drv).
                    ExecuteScript("return jQuery.active == 0");
                try
                {
                    drv.FindElement(by);
                    return false;
                }
                catch
                {
                    return isAjaxFinished;
                }
            });
        }
    }
}
