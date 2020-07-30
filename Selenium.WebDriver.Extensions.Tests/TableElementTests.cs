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
    [TestFixture]
    [Parallelizable]
    public class TableElementTests
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


        [Test]
        public void TableElementForNullElement()
        {
            Action act = () => new TableElement(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Element cannot be null (Parameter 'element')");
        }

        [Test]
        public void TableElementForUnexpectedTagName()
        {
            Action act = () => new TableElement(_tableSearchFilterDemoPage.Body);
            act.Should()
                .Throw<UnexpectedTagNameException>()
                .WithMessage("Element tag name should have been 'table' but was 'body'");
        }

        [Test]
        public void TableElementForValidElement()
        {
            var tableElement = new TableElement(_tableSearchFilterDemoPage.TaskTable);
            tableElement.Should().NotBeNull();
        }


        [TestCase(1, 3, 3)]
        [TestCase(4, 4, 1)]
        [TestCase(2, 6, 5)]
        [TestCase(1, int.MaxValue, 7)]
        [TestCase(2, int.MaxValue, 6)]
        public void GetTableRowElements(int rowNumStart, int rowNumEnd, int expectedRowCount)
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableRowElements = tableElement.TableRowElements(rowNumStart, rowNumEnd);

            tableRowElements.Should().HaveCount(expectedRowCount);
        }


        [Test]
        public void GetTableColumnCellElements()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableColumnCellElements = tableElement.GetColumnCellElements(1);

            tableColumnCellElements.Should().HaveCount(7);
        }

        [Test]
        public void GetTableColumnCellElementsText()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableColumnCellElements = tableElement.GetColumnCellElements(1);

            var columnCellElementsText = tableColumnCellElements.Select(tableColumnCellElement => tableColumnCellElement.Text);
            var expectedColumnCellElementsText = new List<string>
            {
                "1", "2", "3", "4", "5", "6", "7"
            };

            columnCellElementsText.Should().BeEquivalentTo(expectedColumnCellElementsText);
        }


        [Test]
        public void GetTableData()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
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
        public void GetTableDataWithRowLimit()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableData = tableElement.GetTableData(1, 2);
            var expectedTableData = new List<List<string>>
            {
                new List<string> { "1", "Wireframes", "John Smith", "in progress" },
                new List<string> { "2", "Landing Page", "Mike Trout", "completed" },
            };

            tableData.Should().BeEquivalentTo(expectedTableData);
        }

        [Test]
        public void GetTableDataWithRowSkip()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
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
        public void GetTableDataWithRowLimitAndRowSkip()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableData = tableElement.GetTableData(3, 4);
            var expectedTableData = new List<List<string>>
            {
                new List<string> { "3", "SEO tags", "Loblab Dan", "failed qa" },
                new List<string> { "4", "Bootstrap 3", "Emily John", "in progress" },
            };

            tableData.Should().BeEquivalentTo(expectedTableData);
        }

        [Test]
        public void GetTableDataWithExcludedColumns()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableData = tableElement.GetTableData(new HashSet<int> { 1, 4 });
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