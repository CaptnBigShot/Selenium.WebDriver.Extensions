using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions
{
    public static class Tables
    {
        #region Rows

        public static List<IWebElement> GetTableRowElements(this IWebElement tableElement, int rowNumStart = 0, int rowNumEnd = int.MaxValue)
        {
            return tableElement.FindElements(By.XPath($"./tbody/tr[position() >= {rowNumStart} and not(position() > {rowNumEnd})]")).ToList();
        }

        public static List<string> GetTableRowData(this IWebElement tableRowElement, List<int> columnsToExclude = null)
        {
            var tableRowData = new List<string>();
            var tdElementCollection = tableRowElement.GetTableRowCellElements();
            var tdCount = tdElementCollection.Count;

            for (var i = 0; i < tdCount; i++)
            {
                var shouldExcludeColumn = columnsToExclude?.Contains(i + 1); // Uses column number instead of index
                if (shouldExcludeColumn.Equals(true)) continue;

                tableRowData.Add(tdElementCollection[i].Text.Trim());
            }

            return tableRowData;
        }

        #endregion

        #region Cells

        public static List<IWebElement> GetTableRowCellElements(this IWebElement tableRowElement)
        {
            return tableRowElement.FindElements(By.XPath("./td")).ToList();
        }

        public static List<IWebElement> GetTableColumnCellElements(this IWebElement tableElement, int columnNumber)
        {
            return tableElement.FindElements(By.XPath($"./tbody/tr/td[{columnNumber}]")).ToList();
        }

        #endregion

        #region Body

        public static List<List<string>> GetTableData(this IWebElement tableElement, int rowNumStart = 0, int rowNumEnd = int.MaxValue, List<int> columnsToExclude = null)
        {
            var tableRowElements = tableElement.GetTableRowElements(rowNumStart, rowNumEnd);

            return tableRowElements.Select(tableRowElement => tableRowElement.GetTableRowData(columnsToExclude)).ToList();
        }

        #endregion
    }
}
