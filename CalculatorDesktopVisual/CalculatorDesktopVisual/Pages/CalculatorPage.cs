using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;


namespace CalculatorDesktopVisual.Pages
{
    public class CalculatorPage(WindowsDriver<WindowsElement> driver)
    {
        private readonly WindowsDriver<WindowsElement> driver = driver;

        public WindowsElement ResultDisplay => driver.FindElementByAccessibilityId("CalculatorResults");
        public WindowsElement NumberButton(int number) => driver.FindElementByAccessibilityId($"num{number}Button");
        public WindowsElement PlusButton => driver.FindElementByName("Plus");
        public WindowsElement EqualsButton => driver.FindElementByName("Equals");

        public string GetResultText()
        {
            return ResultDisplay.Text.Replace("Display is", "").Trim();
        }
    }
}