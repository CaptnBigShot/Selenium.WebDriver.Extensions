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
            _webDriver.Url = "https://web.archive.org/web/20180926132852/http://www.seleniumeasy.com/test/basic-first-form-demo.html";

            _basicFirstFormDemoPage = new BasicFirstFormDemoPage(_webDriver);
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