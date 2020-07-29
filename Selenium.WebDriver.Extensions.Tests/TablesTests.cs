using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.WebDriver.Extensions.Tests.PageObjects;

namespace Selenium.WebDriver.Extensions.Tests
{
    public class TablesTests
    {
        private IWebDriver _webDriver;

        private TableSearchFilterDemoPage _tableSearchFilterDemoPage;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions { AcceptInsecureCertificates = true };
            chromeOptions.AddArgument("no-sandbox");
            _webDriver = new ChromeDriver(chromeOptions);

            _webDriver.Manage().Window.Maximize();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _webDriver.Url = "https://www.seleniumeasy.com/test/table-search-filter-demo.html";

            _tableSearchFilterDemoPage = new TableSearchFilterDemoPage(_webDriver);
        }

        [TearDown]
        public void Teardown()
        {
            _webDriver.Quit();
        }

        [TestCase(1, 3, 3)]
        [TestCase(4, 4, 1)]
        [TestCase(2, 6, 5)]
        [TestCase(1, int.MaxValue, 7)]
        [TestCase(2, int.MaxValue, 6)]
        public void TestGetTableRowElements(int rowNumStart, int rowNumEnd, int expectedRowCount)
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableRowElements = tableElement.GetTableRowElements(rowNumStart, rowNumEnd);

            tableRowElements.Should().HaveCount(expectedRowCount);
        }

        [Test]
        public void TestGetTableRowData()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableRowElement = tableElement.GetTableRowElements(1, 1)[0];
            var tableRowData = tableRowElement.GetTableRowData();
            var tableRowDataExpected = new List<string>
            {
                "1", "Wireframes", "John Smith", "in progress"
            };

            tableRowData.Should().BeEquivalentTo(tableRowDataExpected);
        }

        [Test]
        public void TestGetTableRowDataWithExcludedColumns()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableRowElement = tableElement.GetTableRowElements(1, 1)[0];
            var tableRowData = tableRowElement.GetTableRowData(new List<int> { 1, 4 });
            var tableRowDataExpected = new List<string>
            {
                "Wireframes", "John Smith"
            };

            tableRowData.Should().BeEquivalentTo(tableRowDataExpected);
        }


        [Test]
        public void TestGetTableRowCellElements()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableRowElement = tableElement.GetTableRowElements(1, 1)[0];
            var tableRowCellElements = tableRowElement.GetTableRowCellElements();

            tableRowCellElements.Should().HaveCount(4);
        }


        [Test]
        public void TestGetTableColumnCellElements()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableColumnCellElements = tableElement.GetTableColumnCellElements(1);

            tableColumnCellElements.Should().HaveCount(7);
        }

        [Test]
        public void TestGetTableColumnCellElementsText()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableColumnCellElements = tableElement.GetTableColumnCellElements(1);

            var columnCellElementsText = tableColumnCellElements.Select(tableColumnCellElement => tableColumnCellElement.Text);
            var expectedColumnCellElementsText = new List<string>
            {
                "1", "2", "3", "4", "5", "6", "7"
            };

            columnCellElementsText.Should().BeEquivalentTo(expectedColumnCellElementsText);
        }


        [Test]
        public void TestGetTableData()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableData = tableElement.GetTableData();
            var expectedTableData = new List<List<string>>
            {
                new List<string> { "1", "Wireframes", "John Smith", "in progress" },
                new List<string> { "2", "Landing Page", "Mike Trout", "completed" },
                new List<string> { "3", "SEO tags", "Loblab Dan", "failed qa" },
                new List<string> { "4", "Bootstrap 3", "Emily John", "in progress" },
                new List<string> { "5", "jQuery library", "Holden Charles", "deployed" },
                new List<string> { "6", "Browser Issues", "Jane Doe", "failed qa" },
                new List<string> { "7", "Bug fixing", "Kilgore Trout", "in progress" },
            };

            tableData.Should().BeEquivalentTo(expectedTableData);
        }

        [Test]
        public void TestGetTableDataWithRowLimit()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableData = tableElement.GetTableData(1, 2);
            var expectedTableData = new List<List<string>>
            {
                new List<string> { "1", "Wireframes", "John Smith", "in progress" },
                new List<string> { "2", "Landing Page", "Mike Trout", "completed" },
            };

            tableData.Should().BeEquivalentTo(expectedTableData);
        }

        [Test]
        public void TestGetTableDataWithRowSkip()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableData = tableElement.GetTableData(5, int.MaxValue);
            var expectedTableData = new List<List<string>>
            {
                new List<string> { "5", "jQuery library", "Holden Charles", "deployed" },
                new List<string> { "6", "Browser Issues", "Jane Doe", "failed qa" },
                new List<string> { "7", "Bug fixing", "Kilgore Trout", "in progress" },
            };

            tableData.Should().BeEquivalentTo(expectedTableData);
        }

        [Test]
        public void TestGetTableDataWithRowLimitAndRowSkip()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableData = tableElement.GetTableData(3, 4);
            var expectedTableData = new List<List<string>>
            {
                new List<string> { "3", "SEO tags", "Loblab Dan", "failed qa" },
                new List<string> { "4", "Bootstrap 3", "Emily John", "in progress" },
            };

            tableData.Should().BeEquivalentTo(expectedTableData);
        }

        [Test]
        public void TestGetTableDataWithExcludedColumns()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTable;
            var tableData = tableElement.GetTableData(columnsToExclude: new List<int> { 1, 4 });
            var expectedTableData = new List<List<string>>
            {
                new List<string> { "Wireframes", "John Smith" },
                new List<string> { "Landing Page", "Mike Trout" },
                new List<string> { "SEO tags", "Loblab Dan" },
                new List<string> { "Bootstrap 3", "Emily John" },
                new List<string> { "jQuery library", "Holden Charles" },
                new List<string> { "Browser Issues", "Jane Doe" },
                new List<string> { "Bug fixing", "Kilgore Trout" },
            };

            tableData.Should().BeEquivalentTo(expectedTableData);
        }
    }
}