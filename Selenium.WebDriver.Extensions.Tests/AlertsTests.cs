using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Extensions.Tests.Helpers;
using Selenium.WebDriver.Extensions.Tests.PageObjects;

namespace Selenium.WebDriver.Extensions.Tests
{
    [TestFixture]
    [Parallelizable]
    public class AlertsTests
    {
        private IWebDriver _webDriver;

        private JavascriptAlertBoxDemoPage _javascriptAlertBoxDemoPage;

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
            _webDriver.Url = "https://web.archive.org/web/20180920010422/http://www.seleniumeasy.com/test/javascript-alert-box-demo.html";

            _javascriptAlertBoxDemoPage = new JavascriptAlertBoxDemoPage(_webDriver);
        }


        [TestCase(false)]
        [TestCase(true)]
        public void IsAlertPresent(bool shouldAlertBePresent)
        {
            if (shouldAlertBePresent) _javascriptAlertBoxDemoPage.ClickTheClickMeButton();
            _webDriver.IsAlertPresent().Should().Be(shouldAlertBePresent);
        }

        [Test]
        public void AcceptAlert()
        {
            _javascriptAlertBoxDemoPage.ClickTheClickMeButton();
            _webDriver.AcceptAlert();
            _webDriver.IsAlertPresent().Should().BeFalse();
        }

        [Test]
        public void ClickElementAndAcceptAlert()
        {
            _webDriver.ClickElementAndAcceptAlert(_javascriptAlertBoxDemoPage.ClickMeBtn);
            _webDriver.IsAlertPresent().Should().BeFalse();
        }
    }
}