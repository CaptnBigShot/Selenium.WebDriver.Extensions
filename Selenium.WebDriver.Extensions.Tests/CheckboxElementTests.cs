using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Tests.Helpers;
using Selenium.WebDriver.Extensions.Tests.PageObjects;

namespace Selenium.WebDriver.Extensions.Tests
{
    [TestFixture]
    [Parallelizable]
    public class CheckboxElementTests
    {
        private IWebDriver _webDriver;

        private BasicCheckboxDemoPage _basicCheckboxDemoPage;

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
            _webDriver.Url = "https://web.archive.org/web/20180911154259/http://www.seleniumeasy.com/test/basic-checkbox-demo.html";

            _basicCheckboxDemoPage = new BasicCheckboxDemoPage(_webDriver);
        }


        [Test]
        public void CheckboxElementForNullElement()
        {
            Action act = () => new CheckboxElement(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Element cannot be null (Parameter 'element')");
        }

        [Test]
        public void CheckboxElementForUnexpectedTagName()
        {
            Action act = () => new CheckboxElement(_basicCheckboxDemoPage.Body);
            act.Should()
                .Throw<UnexpectedTagNameException>()
                .WithMessage("Element tag name should have been 'input' but was 'body'");
        }


        [Test]
        public void IsCheckedForACheckboxThatIsNotSelected()
        {
            _basicCheckboxDemoPage.CheckboxElement.IsChecked.Should().BeFalse();
        }

        [Test]
        public void IsCheckedForACheckboxThatIsSelected()
        {
            _basicCheckboxDemoPage.Checkbox.Click();
            _basicCheckboxDemoPage.CheckboxElement.IsChecked.Should().BeTrue();
        }


        [Test]
        public void SetCheckboxToBeSelectedForACheckboxThatIsSelected()
        {
            _basicCheckboxDemoPage.Checkbox.Click();
            _basicCheckboxDemoPage.CheckboxElement.SetCheckbox(true);
            _basicCheckboxDemoPage.CheckboxElement.IsChecked.Should().BeTrue();
        }

        [Test]
        public void SetCheckboxToBeSelectedForACheckboxThatIsNotSelected()
        {
            _basicCheckboxDemoPage.CheckboxElement.SetCheckbox(true);
            _basicCheckboxDemoPage.CheckboxElement.IsChecked.Should().BeTrue();
        }

        [Test]
        public void SetCheckboxToNotBeSelectedForACheckboxThatIsSelected()
        {
            _basicCheckboxDemoPage.Checkbox.Click();
            _basicCheckboxDemoPage.CheckboxElement.SetCheckbox(false);
            _basicCheckboxDemoPage.CheckboxElement.IsChecked.Should().BeFalse();
        }

        [Test]
        public void SetCheckboxToNotBeSelectedForACheckboxThatIsNotSelected()
        {
            _basicCheckboxDemoPage.CheckboxElement.SetCheckbox(false);
            _basicCheckboxDemoPage.CheckboxElement.IsChecked.Should().BeFalse();
        }
    }
}