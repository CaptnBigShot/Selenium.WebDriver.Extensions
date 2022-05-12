using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions
{
    public class TableElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableElement"/> class.
        /// </summary>
        /// <param name="element">The element to be wrapped</param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
        /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a table element.</exception>
        public TableElement(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Element cannot be null");
            }

            if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "table", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new UnexpectedTagNameException("table", element.TagName);
            }

            WrappedElement = element;
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Gets the table rows (tr) for the table element.
        /// </summary>
        public List<IWebElement> RowElements() => 
            WrappedElement.FindElements(By.XPath("./tbody/tr"))
                .ToList();

        /// <summary>
        /// Gets the table rows (tr) in a given range for the table element.
        /// </summary>
        public List<IWebElement> RowElements(int rowNumStart, int rowNumEnd) => 
            WrappedElement.FindElements(By.XPath($"./tbody/tr[position() >= {rowNumStart} and not(position() > {rowNumEnd})]"))
                .ToList();

        /// <summary>
        /// Gets the table row elements for the table element.
        /// </summary>
        public List<TableRowElement> TableRowElements() => 
            RowElements().Select(rowElement => new TableRowElement(rowElement))
                .ToList();

        /// <summary>
        /// Gets the table row elements in a given range for the table element.
        /// </summary>
        public List<TableRowElement> TableRowElements(int rowNumStart, int rowNumEnd) => 
            RowElements(rowNumStart, rowNumEnd)
                .Select(rowElement => new TableRowElement(rowElement))
                .ToList();

        /// <summary>
        /// Gets the table cell (td) data for the table rows (tr) for the table element.
        /// </summary>
        public List<List<string>> GetTableData(HashSet<int> columnsToExclude = null) => 
            TableRowElements()
                .Select(tableRowElement => tableRowElement.GetRowData(columnsToExclude))
                .ToList();

        /// <summary>
        /// Gets the table cell (td) data for the table rows (tr) in a given range for the table element.
        /// </summary>
        public List<List<string>> GetTableData(int rowNumStart, int rowNumEnd, HashSet<int> columnsToExclude = null) =>
            TableRowElements(rowNumStart, rowNumEnd)
                .Select(tableRowElement => tableRowElement.GetRowData(columnsToExclude))
                .ToList();

        /// <summary>
        /// Gets the table's cell (td) elements for a table column <see cref="IWebElement"/> instance.
        /// </summary>
        public List<IWebElement> GetColumnCellElements(int columnNumber) => 
            WrappedElement.FindElements(By.XPath($"./tbody/tr/td[{columnNumber}]"))
                .ToList();

        /// <summary>
        /// Gets the table header (th) elements for the table element.
        /// </summary>
        public List<IWebElement> HeaderElements() =>
            WrappedElement.FindElements(By.XPath("./thead/tr/th"))
                .ToList();

        /// <summary>
        /// Gets the table header (th) elements text for the table element.
        /// </summary>
        public List<string> Headers() =>
            HeaderElements().Select(x => x.Text)
                .ToList();
    }
}
