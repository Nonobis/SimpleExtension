using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace SimpleExtension.Selenium
{
    internal static class RemoteWebDriverExtensionHelpers
    {
        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <returns></returns>
        public static IWebElement FindElement(this RemoteWebDriver pDriver, By by, int timeoutInSeconds = 30)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(pDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return pDriver.FindElement(by);
        }
    }
}