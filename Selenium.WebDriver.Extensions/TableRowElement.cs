using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions
{
    public class TableRowElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableRowElement"/> class.
        /// </summary>
        /// <param name="element">The element to be wrapped</param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
        /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a table element.</exception>
        public TableRowElement(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Element cannot be null");
            }

            if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "tr", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new UnexpectedTagNameException("tr", element.TagName);
            }

            WrappedElement = element;
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Gets the table cells (td) for the table row element.
        /// </summary>
        public IList<IWebElement> CellElements() => 
            WrappedElement.FindElements(By.XPath("./td"));

        /// <summary>
        /// Gets the table cells (td) data for the table row element.
        /// </summary>
        public IList<string> GetRowData(HashSet<int> columnsToExclude = null)
        {
            var tableRowData = new List<string>();
            var tdElements = CellElements();

            for (var i = 0; i < tdElements.Count; i++)
            {
                var shouldExcludeColumn = columnsToExclude?.Contains(i + 1); // Uses column number instead of index
                if (shouldExcludeColumn.Equals(true)) continue;

                tableRowData.Add(tdElements[i].Text.Trim());
            }

            return tableRowData;
        }
    }
}
