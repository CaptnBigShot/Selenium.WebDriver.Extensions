using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions
{
    public static class ElementInteractions
    {
        /// <summary>
        /// Send keys to the element with additional options to clear the text first
        /// or skip the sendKeys method if the input string is null.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <param name="clearFirst"></param>
        /// <param name="skipNull"></param>
        public static void SendKeys(this IWebElement element, string value, bool clearFirst, bool skipNull = false)
        {
            if (skipNull && value == null) return; // Don't do anything
            if (clearFirst) element.Clear();
            element.SendKeys(value);
        }
    }
}
