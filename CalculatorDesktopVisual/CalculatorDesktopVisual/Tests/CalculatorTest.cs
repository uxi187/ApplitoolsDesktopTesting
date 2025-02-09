using CalculatorDesktopVisual.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Applitools.Images;


namespace CalculatorDesktopVisual.Tests
{
    [TestFixture]
    public class CalculatorTests 
    {
        private const string WinAppDriverUrl = "http://127.0.0.1:4723";
        private const string CalculatorAppId = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";

        protected static WindowsDriver<WindowsElement> driver;
        public Eyes? eyes;

        [SetUp]
        public void Setup()
        {
            // Launch Calculator application if it is not yet launched
            if (driver == null)
            {
                // Create a new session to bring up an instance of the Calculator application
                // Note: Multiple calculator windows (instances) share the same process Id
                AppiumOptions options = new AppiumOptions();
                options.AddAdditionalCapability("app", CalculatorAppId);
                options.AddAdditionalCapability("platformName", "Windows"); // Added to ensure compatibility
                driver = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);
                Assert.IsNotNull(driver);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                // Applitools setup
                eyes = new Eyes();
                eyes.ApiKey = "RiI1MdPyCfWBuK5hfeJWMd104q3NFJDZtzK54yHH97DUI110"; // Replace with your Applitools API key
            }
        }

        [Test]
        public void TestCalculatorVisualValidation()
        {
            var calculatorPage = new CalculatorPage(driver);

            // Perform a simple calculation
            calculatorPage.NumberButton(2).Click();
            calculatorPage.PlusButton.Click();
            calculatorPage.NumberButton(3).Click();
            calculatorPage.EqualsButton.Click();

            Assert.AreEqual("5", calculatorPage.GetResultText());

            // Open Applitools test
            eyes.Open("Calculator App", "Calculator Visual Test");
            var screenshot = driver.GetScreenshot();

            // Send the screenshot to Applitools for visual validation
            eyes.CheckImage(screenshot.AsByteArray, "Initial design");

            // Close Applitools test
            eyes.Close();
        }

        [TearDown]
        public void ApplitoolsTearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }

           eyes.AbortIfNotClosed();
        }
    }
}