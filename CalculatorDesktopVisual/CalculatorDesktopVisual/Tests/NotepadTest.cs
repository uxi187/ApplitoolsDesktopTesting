using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using Applitools.Images;

namespace CalculatorDesktopVisual.Tests
{
    [TestFixture]
    public class NotepadTests
    {
        private const string WinAppDriverUrl = "http://127.0.0.1:4723";
        private const string AppId = "C:\\Windows\\System32\\notepad.exe";
        private WindowsDriver<WindowsElement>? driver;
        public Eyes? eyes;

        [SetUp]
        public void Setup()
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppId);
            options.AddAdditionalCapability("platformName", "Windows"); // Added to ensure compatibility

            driver = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            // Applitools setup
            eyes = new Eyes();
            eyes.ApiKey = "RiI1MdPyCfWBuK5hfeJWMd104q3NFJDZtzK54yHH97DUI110"; // Replace with your Applitools API key
        }

        [Test]
        public void NotepadVisualTest()
        {
            try
            {
                eyes.Open("Notepad App", "Notepad Visual Test");
                var screenshot = driver.GetScreenshot();

                // Send the screenshot to Applitools for visual validation
                eyes.CheckImage(screenshot.AsByteArray, "Initial State");

                var textBox = driver.FindElementByName("Text editor");
                textBox.SendKeys("Hello, this is a test with Applitools 1!");

                // Capture another screenshot after entering text
                var updatedScreenshot = driver.GetScreenshot();
                eyes.CheckImage(updatedScreenshot.AsByteArray, "After Entering Text");
            }
            finally
            {
                if (eyes != null)
                {
                    eyes.CloseAsync();
                }
            }
        }
        

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            if (eyes != null)
            {
                eyes.AbortIfNotClosed();
            }
        }
    }
}


