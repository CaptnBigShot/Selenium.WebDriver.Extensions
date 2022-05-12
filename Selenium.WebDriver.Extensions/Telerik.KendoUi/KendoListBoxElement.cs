using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Telerik.KendoUi
{
    public class KendoListBoxElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KendoListBoxElement"/> class.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
        /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a ul element.</exception>
        public KendoListBoxElement(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Element cannot be null");
            }

            if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "ul", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new UnexpectedTagNameException("ul", element.TagName);
            }

            WrappedElement = element;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KendoListBoxElement"/> class.
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="kendoId"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="ISearchContext"/> object is <see langword="null"/></exception>
        public KendoListBoxElement(ISearchContext searchContext, string kendoId)
        {
            if (searchContext == null)
            {
                throw new ArgumentNullException(nameof(searchContext), "Search context cannot be null");
            }

            if (kendoId == null)
            {
                throw new ArgumentNullException(nameof(kendoId), "kendoId cannot be null");
            }

            WrappedElement = searchContext.FindElement(By.XPath($"//ul[@id='{kendoId}'][@aria-hidden='false']"));
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Gets the Kendo list box items.
        /// </summary>
        public IList<IWebElement> ListBoxItemElements => 
            WrappedElement.FindElements(By.XPath("./li"));

        /// <summary>
        /// Finds a list box item by text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IWebElement FindListBoxItemByText(string text)
        {
            var byText = text == "" ? "./li[not(text())]" : $"./li[normalize-space()='{text}']";

            return WrappedElement.FindElement(By.XPath(byText));
        }

        /// <summary>
        /// Clicks a list box item by its text.
        /// </summary>
        /// <param name="text"></param>
        public void ClickListBoxItemByText(string text)
        {
            FindListBoxItemByText(text).Click();
            Thread.Sleep(1000);
        }
    }
}
