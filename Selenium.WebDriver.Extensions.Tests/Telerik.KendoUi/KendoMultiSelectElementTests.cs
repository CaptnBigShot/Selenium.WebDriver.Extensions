using System;
using System.Collections.Generic;
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
    public class KendoMultiSelectElementTests
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
        public void KendoMultiSelectElementForNullSearchContext()
        {
            Action act = () => new KendoMultiSelectElement(null, By.XPath(""));
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Search context cannot be null (Parameter 'SearchContext')");
        }

        [Test]
        public void KendoMultiSelectElementForNullBy()
        {
            By by = null;
            Action act = () => new KendoMultiSelectElement(_webDriver, by);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("by cannot be null (Parameter 'by')");
        }

        [Test]
        public void KendoMultiSelectElementForNullKendoId()
        {
            string kendoId = null;
            Action act = () => new KendoMultiSelectElement(_webDriver, kendoId);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("kendoId cannot be null (Parameter 'kendoId')");
        }


        [Test]
        public void KendoMultiSelectElementInputElement()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.InputElement.Should().NotBeNull();
        }


        [Test]
        public void KendoMultiSelectElementKendoTagListElement()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.KendoTagListElement.Should().NotBeNull();
        }


        [Test]
        public void KendoMultiSelectElementKendoListBoxElement()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.ExpandListBox();

            kendoMultiSelect.KendoListBoxElement.Should().NotBeNull();
        }


        [Test] 
        public void KendoMultiSelectElementSelectByText()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Steven White");

            kendoMultiSelect.SelectedOptions.Should().HaveCount(1);
        }


        [Test]
        public void KendoMultiSelectElementSelectedOptions()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Nancy King");
            kendoMultiSelect.SelectByText("Steven White");

            kendoMultiSelect.SelectedOptions.Should().HaveCount(2);
        }


        [Test]
        public void KendoMultiSelectElementTextOfSelectedOptions()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Nancy King");
            kendoMultiSelect.SelectByText("Steven White");
            var textExpected = new List<string> { "Nancy King", "Steven White" };

            kendoMultiSelect.TextOfSelectedOptions.Should().BeEquivalentTo(textExpected);
        }


        [Test]
        public void KendoMultiSelectElementDeselectByText()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Nancy King");
            kendoMultiSelect.SelectByText("Steven White");
            kendoMultiSelect.DeselectByText("Steven White");
            var textExpected = new List<string> { "Nancy King" };

            kendoMultiSelect.TextOfSelectedOptions.Should().BeEquivalentTo(textExpected);
        }


        [Test]
        public void KendoMultiSelectElementDeselectAllOptions()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.SelectByText("Nancy King");
            kendoMultiSelect.SelectByText("Steven White");
            kendoMultiSelect.DeselectAllOptions();

            kendoMultiSelect.SelectedOptions.Should().BeNullOrEmpty();
        }


        [Test]
        public void KendoMultiSelectElementOptions()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.Options().Should().HaveCount(19);
        }


        [Test]
        public void KendoComboBoxExpandListBox()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.ExpandListBox();
            kendoMultiSelect.KendoListBoxElement.WrappedElement.Displayed.Should().BeTrue();
        }


        [Test]
        public void KendoComboBoxCollapseListBox()
        {
            var kendoMultiSelect = _telerikKendoMultiSelectPage.OptionalKendoMultiSelectElement;
            kendoMultiSelect.ExpandListBox();
            kendoMultiSelect.CollapseListBox();
            kendoMultiSelect.Invoking(x => x.KendoListBoxElement).Should().Throw<NoSuchElementException>();
        }
    }
}
