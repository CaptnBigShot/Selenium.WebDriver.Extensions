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
    public class KendoTagListItemElementTests
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
            _webDriver.Url = "https://demos.telerik.com/kendo-ui/multiselect/tag-mode";
            _webDriver.WaitForPageToLoad();
            Thread.Sleep(1000);

            _telerikKendoMultiSelectPage = new TelerikKendoMultiSelectPage(_webDriver);
        }


        [Test]
        public void KendoTagListItemElementForNullElement()
        {
            Action act = () => new KendoTagListItemElement(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Element cannot be null (Parameter 'element')");
        }

        [Test]
        public void KendoTagListItemElementForUnexpectedTagName()
        {
            Action act = () => new KendoTagListItemElement(_telerikKendoMultiSelectPage.ExampleWrapElement);
            act.Should()
                .Throw<UnexpectedTagNameException>()
                .WithMessage("Element tag name should have been 'li' but was 'div'");
        }

        [Test]
        public void KendoTagListItemElement()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Steven White");
            var tagListItemElement = kendoMultiSelect.KendoTagListElement.TagListItemElements[0];
            var kendoTagListItemElement = new KendoTagListItemElement(tagListItemElement);

            kendoTagListItemElement.Should().NotBeNull();
        }


        [Test]
        public void KendoTagListItemElementByText()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Steven White");
            var kendoTagListItemElement = new KendoTagListItemElement(kendoMultiSelect.KendoTagListElement.WrappedElement, "Steven White");

            kendoTagListItemElement.Should().NotBeNull();
        }


        [Test]
        public void KendoTagListItemElementText()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Steven White");
            var kendoTagListItemElement = kendoMultiSelect.KendoTagListElement.KendoTagListItemElements[0];

            kendoTagListItemElement.Text.Should().Be("Steven White");
        }


        [Test]
        public void KendoTagListItemElementDeselectElement()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Steven White");
            var kendoTagListItemElement = kendoMultiSelect.KendoTagListElement.KendoTagListItemElements[0];

            kendoTagListItemElement.DeselectElement.Should().NotBeNull();
        }


        [Test]
        public void KendoTagListItemElementDeselect()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Steven White");
            var kendoTagListItemElement = kendoMultiSelect.KendoTagListElement.KendoTagListItemElements[0];
            kendoTagListItemElement.Deselect();

            kendoMultiSelect.SelectedOptions.Should().HaveCount(0);
        }
    }
}
