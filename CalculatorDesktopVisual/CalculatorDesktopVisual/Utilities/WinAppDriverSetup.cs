using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using OpenQA.Selenium.Remote;
using Applitools.Selenium;

namespace Utilities
{
    public class WinAppDriverSetup
    {
        public static WindowsDriver<WindowsElement> driver;


        [SetUp]
        public void Setup()
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}