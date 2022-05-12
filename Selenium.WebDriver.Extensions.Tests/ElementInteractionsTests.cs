using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Tests.Helpers;
using Selenium.WebDriver.Extensions.Tests.PageObjects;

namespace Selenium.WebDriver.Extensions.Tests
{
    [TestFixture]
    [Parallelizable]
    public class ElementInteractionsTests
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


        [TestCase(null, "chocolate", "chocolate")]
        [TestCase("vanilla", "chocolate", "vanillachocolate")]
        public void SendKeys(string initialValue, string inputValue, string expectedValue)
        {
            // Stage
            if (initialValue != null) _basicFirstFormDemoPage.UserMessageTxt.SendKeys(initialValue);

            // Act
            _basicFirstFormDemoPage.UserMessageTxt.SendKeys(inputValue);

            // Assert
            _basicFirstFormDemoPage.GetUserMessageInputValue.Should().Be(expectedValue);
        }

        [TestCase(null, "", false, "")]
        [TestCase(null, "", true, "")]
        [TestCase("vanilla", "", false, "vanilla")]
        [TestCase("vanilla", "", true, "")]
        [TestCase(null, "chocolate", false, "chocolate")]
        [TestCase(null, "chocolate", true, "chocolate")]
        [TestCase("vanilla", "chocolate", false, "vanillachocolate")]
        [TestCase("vanilla", "chocolate", true, "chocolate")]
        public void SendKeysWithClearFirst(string initialValue, string inputValue, bool clearFirst, string expectedValue)
        {
            // Stage
            if (initialValue != null) _basicFirstFormDemoPage.UserMessageTxt.SendKeys(initialValue);

            // Act
            _basicFirstFormDemoPage.UserMessageTxt.SendKeys(inputValue, clearFirst);

            // Assert
            _basicFirstFormDemoPage.GetUserMessageInputValue.Should().Be(expectedValue);
        }

        [TestCase(null, "", false, false, "")]
        [TestCase(null, "vanilla", false, false, "vanilla")]
        [TestCase(null, "vanilla", true, false, "vanilla")]
        [TestCase("vanilla", "chocolate", false, false, "vanillachocolate")]
        [TestCase("vanilla", "chocolate", true, false, "chocolate")]
        [TestCase("vanilla", "chocolate", true, true, "chocolate")]
        [TestCase("vanilla", null, false, true, "vanilla")]
        [TestCase("vanilla", null, true, true, "vanilla")]
        [TestCase(null, null, true, true, "")]
        public void SendKeysWithClearFirstAndSkipNull(string initialValue, string inputValue, bool clearFirst, bool skipNull, string expectedValue)
        {
            // Stage
            if (initialValue != null) _basicFirstFormDemoPage.UserMessageTxt.SendKeys(initialValue);

            // Act
            _basicFirstFormDemoPage.UserMessageTxt.SendKeys(inputValue, clearFirst, skipNull);

            // Assert
            _basicFirstFormDemoPage.GetUserMessageInputValue.Should().Be(expectedValue);
        }
    }
}