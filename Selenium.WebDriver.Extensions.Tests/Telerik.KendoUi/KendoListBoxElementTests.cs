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
    public class KendoListBoxElementTests
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
        }


        [Test]
        public void KendoListBoxElementForNullElement()
        {
            Action act = () => new KendoListBoxElement(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Element cannot be null (Parameter 'element')");
        }

        [Test]
        public void KendoListBoxElementForUnexpectedTagName()
        {
            Action act = () => new KendoListBoxElement(_telerikKendoUiComboBoxPage.ImageElement);
            act.Should()
                .Throw<UnexpectedTagNameException>()
                .WithMessage("Element tag name should have been 'ul' but was 'img'");
        }

        [Test]
        public void KendoListBoxElementById()
        {
            _telerikKendoUiComboBoxPage.FabricKendoComboBoxElement.ExpandListBox();
            var kendoListBox = new KendoListBoxElement(_webDriver, _telerikKendoUiComboBoxPage.FabricKendoComboBoxId);
            kendoListBox.Should().NotBeNull();
        }


        [Test]
        public void KendoListBoxElementListItemElements()
        {
            _telerikKendoUiComboBoxPage.FabricKendoComboBoxElement.ExpandListBox();
            var kendoListBox = _telerikKendoUiComboBoxPage.FabricKendoComboBoxElement.KendoListBoxElement;

            kendoListBox.ListBoxItemElements.Should().HaveCount(4);
        }


        [Test]
        public void KendoListBoxElementFindListBoxItemByText()
        {
            _telerikKendoUiComboBoxPage.FabricKendoComboBoxElement.ExpandListBox();
            var kendoListBox = _telerikKendoUiComboBoxPage.FabricKendoComboBoxElement.KendoListBoxElement;

            kendoListBox.FindListBoxItemByText("Polyester").Should().NotBeNull();
        }


        [Test]
        public void KendoListBoxElementClickListBoxItemByText()
        {
            _telerikKendoUiComboBoxPage.FabricKendoComboBoxElement.ExpandListBox();
            var kendoListBox = _telerikKendoUiComboBoxPage.FabricKendoComboBoxElement.KendoListBoxElement;

            kendoListBox.Invoking(x => x.ClickListBoxItemByText("Cotton")).Should().NotThrow();
        }
    }
}
