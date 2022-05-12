using System;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Telerik.KendoUi;
using Selenium.WebDriver.Extensions.Tests.Helpers;
using Selenium.WebDriver.Extensions.Tests.PageObjects;

namespace Selenium.WebDriver.Extensions.Tests.Telerik.KendoUi
{
    [TestFixture]
    [Parallelizable]
    public class KendoTagListElementTests
    {
        private IWebDriver _webDriver;

        private TelerikKendoMultiSelectPage _telerikKendoMultiSelectPage;

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
            _webDriver.Url = "https://web.archive.org/web/20201026073830/https://demos.telerik.com/kendo-ui/multiselect/tag-mode";
            _webDriver.WaitForPageToLoad();
            Thread.Sleep(1000);

            _telerikKendoMultiSelectPage = new TelerikKendoMultiSelectPage(_webDriver);
        }


        [Test]
        public void KendoTagListElementForNullElement()
        {
            Action act = () => new KendoTagListElement(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Element cannot be null (Parameter 'element')");
        }

        [Test]
        public void KendoTagListElementForUnexpectedTagName()
        {
            Action act = () => new KendoTagListElement(_telerikKendoMultiSelectPage.ExampleWrapElement);
            act.Should()
                .Throw<UnexpectedTagNameException>()
                .WithMessage("Element tag name should have been 'ul' but was 'div'");
        }

        [Test]
        public void KendoTagListElement()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            var tagListElement = kendoMultiSelect.WrappedElement.FindElement(By.XPath(".//ul"));
            var kendoTagListElement = new KendoTagListElement(tagListElement);

            kendoTagListElement.Should().NotBeNull();
        }


        [Test]
        public void KendoTagListElementTagListItemElements()
        {
            var kendoMultiSelectElement = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelectElement.SelectByText("Nancy King");
            kendoMultiSelectElement.SelectByText("Steven White");
            var kendoTagListElement = kendoMultiSelectElement.KendoTagListElement;

            kendoTagListElement.TagListItemElements.Should().HaveCount(2);
        }


        [Test]
        public void KendoTagListElementKendoTagListItemElements()
        {
            var kendoMultiSelectElement = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelectElement.SelectByText("Nancy King");
            kendoMultiSelectElement.SelectByText("Steven White");
            var kendoTagListElement = kendoMultiSelectElement.KendoTagListElement;

            kendoTagListElement.KendoTagListItemElements.Should().HaveCount(2);
        }


        [Test]
        public void KendoTagListElementFindTagListItemByText()
        {
            var kendoMultiSelectElement = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelectElement.SelectByText("Nancy King");
            kendoMultiSelectElement.SelectByText("Steven White");
            var kendoTagListElement = kendoMultiSelectElement.KendoTagListElement;

            kendoTagListElement.FindTagListItemByText("Steven White").Should().NotBeNull();
        }


        [Test]
        public void KendoTagListElementDeselectTagListItemByText()
        {
            var kendoMultiSelectElement = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelectElement.SelectByText("Nancy King");
            kendoMultiSelectElement.SelectByText("Steven White");
            var kendoTagListElement = kendoMultiSelectElement.KendoTagListElement;
            kendoTagListElement.DeselectTagListItemByText("Steven White");

            kendoTagListElement.KendoTagListItemElements.Should().HaveCount(1);
        }


        [Test]
        public void KendoTagListElementDeselectAllTagListItems()
        {
            var kendoMultiSelectElement = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelectElement.SelectByText("Nancy King");
            kendoMultiSelectElement.SelectByText("Steven White");
            var kendoTagListElement = kendoMultiSelectElement.KendoTagListElement;
            kendoTagListElement.DeselectAllTagListItems();

            kendoTagListElement.KendoTagListItemElements.Should().HaveCount(0);
        }
    }
}
