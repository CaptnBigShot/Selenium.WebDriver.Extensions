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
    public class KendoComboBoxElementTests
    {
        private IWebDriver _webDriver;

        private TelerikKendoUiComboBoxPage _telerikKendoUiComboBoxPage;

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
            _webDriver.Url = "https://demos.telerik.com/kendo-ui/combobox/index";
            _webDriver.WaitForPageToLoad();
            Thread.Sleep(1000);

            _telerikKendoUiComboBoxPage = new TelerikKendoUiComboBoxPage(_webDriver);

            _webDriver.ScrollToElement(_telerikKendoUiComboBoxPage.ImageElement);
        }


        [Test]
        public void KendoComboBoxElementForNullElement()
        {
            Action act = () => new KendoComboBoxElement(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Element cannot be null (Parameter 'element')");
        }

        [Test]
        public void KendoComboBoxElementForUnexpectedTagName()
        {
            Action act = () => new KendoComboBoxElement(_telerikKendoUiComboBoxPage.ImageElement);
            act.Should()
                .Throw<UnexpectedTagNameException>()
                .WithMessage("Element tag name should have been 'span' but was 'img'");
        }

        [Test]
        public void KendoComboBoxElement()
        {
            var kendoComboBox = new KendoComboBoxElement(_telerikKendoUiComboBoxPage.FabricKendoComboBox);
            kendoComboBox.Should().NotBeNull();
        }


        [Test]
        public void KendoComboBoxElementById()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.Should().NotBeNull();
        }


        [Test]
        public void KendoComboBoxInputElement()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.InputElement.Should().NotBeNull();
        }


        [Test]
        public void KendoComboBoxSelectElement()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.SelectElement.Should().NotBeNull();
        }


        [Test]
        public void KendoComboBoxKendoListBoxElementWhenCollapsed()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.Invoking(x => x.KendoListBoxElement).Should().Throw<NoSuchElementException>();
        }

        [Test]
        public void KendoComboBoxKendoListBoxElementWhenExpanded()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.ExpandListBox();
            kendoComboBox.KendoListBoxElement.Should().NotBeNull();
        }


        [Test]
        public void KendoComboBoxOptions()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.Options().Should().HaveCount(4);
        }


        [Test]
        public void KendoComboBoxGetInputText()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.GetInputText.Should().Be("Rib Knit");
        }

        [Test]
        public void KendoComboBoxSetInputText()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.SetInputText("Ron Jon");
            kendoComboBox.GetInputText.Should().Be("Ron Jon");
        }


        [Test]
        public void KendoComboBoxExpandListBox()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.ExpandListBox();
            kendoComboBox.KendoListBoxElement.WrappedElement.Displayed.Should().BeTrue();
        }


        [Test]
        public void KendoComboBoxCollapseListBox()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.ExpandListBox();
            kendoComboBox.CollapseListBox();
            kendoComboBox.Invoking(x => x.KendoListBoxElement).Should().Throw<NoSuchElementException>();
        }


        [Test]
        public void KendoComboBoxSelectListItemByText()
        {
            var kendoComboBox = new KendoComboBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoComboBox.SelectByText("Polyester");
            kendoComboBox.GetInputText.Should().Be("Polyester");
        }
    }
}
