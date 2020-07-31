using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Tests.Helpers;
using Selenium.WebDriver.Extensions.Tests.PageObjects;

namespace Selenium.WebDriver.Extensions.Tests
{
    [TestFixture]
    [Parallelizable]
    public class TableRowElementTests
    {
        private IWebDriver _webDriver;

        private TableSearchFilterDemoPage _tableSearchFilterDemoPage;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _webDriver = new WebDriverHelper().StartWebDriver();
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            _webDriver.Quit();
        }

        [SetUp]
        public void Setup()
        {
            _webDriver.Url = "https://www.seleniumeasy.com/test/table-search-filter-demo.html";

            _tableSearchFilterDemoPage = new TableSearchFilterDemoPage(_webDriver);
        }


        [Test]
        public void TableRowElementForNullElement()
        {
            Action act = () => new TableRowElement(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Element cannot be null (Parameter 'element')");
        }

        [Test]
        public void TableRowElementForUnexpectedTagName()
        {
            Action act = () => new TableRowElement(_tableSearchFilterDemoPage.Body);
            act.Should()
                .Throw<UnexpectedTagNameException>()
                .WithMessage("Element tag name should have been 'tr' but was 'body'");
        }

        [Test]
        public void TableRowElementForValidElement()
        {
            var tableRowElement = new TableRowElement(_tableSearchFilterDemoPage.TaskTableRow);
            tableRowElement.Should().NotBeNull();
        }


        [Test]
        public void GetTableRowData()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableRowElement = tableElement.TableRowElements(1, 1)[0];
            var tableRowData = tableRowElement.GetRowData();
            var tableRowDataExpected = new List<string>
            {
                "1", "Wireframes", "John Smith", "in progress"
            };

            tableRowData.Should().BeEquivalentTo(tableRowDataExpected);
        }

        [Test]
        public void GetTableRowDataWithExcludedColumns()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableRowElement = tableElement.TableRowElements(1, 1)[0];
            var tableRowData = tableRowElement.GetRowData(new HashSet<int> { 1, 4 });
            var tableRowDataExpected = new List<string>
            {
                "Wireframes", "John Smith"
            };

            tableRowData.Should().BeEquivalentTo(tableRowDataExpected);
        }


        [Test]
        public void GetTableRowCellElements()
        {
            var tableElement = _tableSearchFilterDemoPage.TaskTableElement;
            var tableRowElement = tableElement.TableRowElements(1, 1)[0];
            var tableRowCellElements = tableRowElement.CellElements();

            tableRowCellElements.Should().HaveCount(4);
        }
    }
}