using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Telerik.KendoUi
{
    public class KendoTagListElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KendoTagListElement"/> class.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
        /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a ul element.</exception>
        public KendoTagListElement(IWebElement element)
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
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Gets Kendo tag list items.
        /// </summary>
        public IList<KendoTagListItemElement> KendoTagListItemElements => 
            TagListItemElements.Select(element => new KendoTagListItemElement(element))
                .ToList();

        /// <summary>
        /// Finds a Kendo tag list item by text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public KendoTagListItemElement FindTagListItemByText(string text) => 
            new KendoTagListItemElement(WrappedElement, text);

        /// <summary>
        /// Finds a tag list item by text and deselects it.
        /// </summary>
        /// <param name="text"></param>
        public void DeselectTagListItemByText(string text) => 
            FindTagListItemByText(text).Deselect();

        /// <summary>
        /// Deselects all tag list items.
        /// </summary>
        public void DeselectAllTagListItems()
        {
            foreach (var kendoTagListItemElement in KendoTagListItemElements)
            {
                kendoTagListItemElement.Deselect();
            }
        }

        /// <summary>
        /// Gets tag list items.
        /// </summary>
        public IList<IWebElement> TagListItemElements =>
            WrappedElement.FindElements(By.XPath("./li"));
    }
}