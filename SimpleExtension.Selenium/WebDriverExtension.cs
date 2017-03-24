using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace CryptoBots.Tools
{
    public static class WebDriverExtension
    {
        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="pDriver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver pDriver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(pDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return pDriver.FindElement(by);
        }

        /// <summary>
        /// Finds the elements.
        /// </summary>
        /// <param name="pDriver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver pDriver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(pDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
            }
            return pDriver.FindElements(by);
        }

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="pDriver">The p driver.</param>
        /// <param name="pQuery">The by.</param>
        /// <param name="pTimeoutInSeconds">The p timeout in seconds.</param>
        /// <param name="pCheckIsVisible">if set to <c>true</c> [p check is visible].</param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver pDriver, By pQuery, int pTimeoutInSeconds, bool pCheckIsVisible)
        {
            IWebElement element;
            var wait = new WebDriverWait(pDriver, TimeSpan.FromSeconds(pTimeoutInSeconds));
            if (pCheckIsVisible)
            {
                element = wait.Until(ExpectedConditions.ElementIsVisible(pQuery));
            }
            else
            {
                element = wait.Until(ExpectedConditions.ElementExists(pQuery));
            }
            return element;
        }
    }
}
