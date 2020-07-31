using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Tests.Helpers;
using Selenium.WebDriver.Extensions.Tests.PageObjects;

namespace Selenium.WebDriver.Extensions.Tests
{
    [TestFixture]
    [Parallelizable]
    public class ElementLookupsTests
    {
        private IWebDriver _webDriver;

        private BasicFirstFormDemoPage _basicFirstFormDemoPage;

        [SetUp]
        public void Setup()
        {
            _webDriver = new WebDriverHelper().StartWebDriver();
            _webDriver.Url = "https://www.seleniumeasy.com/test/basic-first-form-demo.html";

            _basicFirstFormDemoPage = new BasicFirstFormDemoPage(_webDriver);
        }

        [TearDown]
        public void Teardown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void HasElementForAnExistentElement()
        {
            _webDriver.HasElement(_basicFirstFormDemoPage.UserMessageTxtBy).Should().BeTrue();
        }

        [Test]
        public void HasElementForANonExistentElement()
        {
            _webDriver.HasElement(By.Id("non-existent")).Should().BeFalse();
        }


        [Test]
        public void HasElementAndDisplayedForAnExistentElement()
        {
            _webDriver.HasElementAndDisplayed(_basicFirstFormDemoPage.UserMessageTxtBy).Should().BeTrue();
        }

        [Test]
        public void HasElementAndDisplayedForANonExistentElement()
        {
            _webDriver.HasElementAndDisplayed(By.Id("non-existent")).Should().BeFalse();
        }
    }
}