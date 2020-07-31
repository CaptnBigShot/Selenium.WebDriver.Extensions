﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.WebDriver.Extensions.Tests.Helpers
{
    public class WebDriverHelper
    {
        public IWebDriver StartWebDriver()
        {
            var chromeOptions = new ChromeOptions { AcceptInsecureCertificates = true };
            chromeOptions.AddArgument("no-sandbox");
            var webDriver = new ChromeDriver(chromeOptions);

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            return webDriver;
        }
    }
}
