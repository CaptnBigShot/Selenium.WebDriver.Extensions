using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Tests.Helpers;

namespace Selenium.WebDriver.Extensions.Tests
{
    [TestFixture]
    [Parallelizable]
    public class ScreenshotTests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new WebDriverHelper().StartWebDriver();
        }

        [TearDown]
        public void Teardown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void GetEntirePageScreenshotsForAPageWithOneScreenshot()
        {
            _webDriver.Url = "https://help.duckduckgo.com/";

            var screenshots = _webDriver.GetEntirePageScreenshots();

            screenshots.Should().HaveCount(1);
        }

        [Test]
        public void GetEntirePageScreenshotsForAPageWithMultipleScreenshots()
        {
            _webDriver.Url = "https://www.seleniumeasy.com/test/basic-checkbox-demo.html";

            var screenshots = _webDriver.GetEntirePageScreenshots();

            screenshots.Should().HaveCount(2);
        }
    }
}