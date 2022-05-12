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


        [Test]
        public void GetEntirePageScreenshotsForAPageWithOneScreenshot()
        {
            _webDriver.Url = "https://help.duckduckgo.com/404";

            var screenshots = _webDriver.GetEntirePageScreenshots();

            screenshots.Should().HaveCount(1);
        }

        [Test]
        public void GetEntirePageScreenshotsForAPageWithMultipleScreenshots()
        {
            _webDriver.Url = "https://web.archive.org/web/20180911154259/http://www.seleniumeasy.com/test/basic-checkbox-demo.html";

            var screenshots = _webDriver.GetEntirePageScreenshots();

            screenshots.Should().HaveCount(2);
        }
    }
}