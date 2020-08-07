using System;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Telerik.KendoUi
{
    public class KendoTagListItemElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KendoTagListItemElement"/> class.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
        /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a li element.</exception>
        public KendoTagListItemElement(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Element cannot be null");
            }

            if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "li", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new UnexpectedTagNameException("li", element.TagName);
            }

            WrappedElement = element;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KendoTagListItemElement"/> class.
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="ISearchContext"/> object is <see langword="null"/></exception>
        public KendoTagListItemElement(ISearchContext searchContext, string text)
        {
            if (searchContext == null)
            {
                throw new ArgumentNullException(nameof(searchContext), "Search context cannot be null");
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), "Text cannot be null");
            }

            WrappedElement = searchContext.FindElement(By.XPath($"./li[./span='{text}']"));
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Gets the text of the item.
        /// </summary>
        public string Text => 
            WrappedElement.FindElement(By.XPath("./span[1]")).Text;

        /// <summary>
        /// Gets the Deselect element.
        /// </summary>
        public IWebElement DeselectElement =>
            WrappedElement.FindElement(By.XPath("./span[2]"));

        /// <summary>
        /// Deselects the item.
        /// </summary>
        public void Deselect()
        {
            DeselectElement.Click();
            Thread.Sleep(500);
        }
    }
}