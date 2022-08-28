using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SimpleExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SimpleExtension.Selenium
{
    public static class RemoteWebDriverExtension
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

        /// <summary>
        /// Finds the elements.
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElementsBy(this RemoteWebDriver pDriver, By by, int timeoutInSeconds = 30)
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
        /// <returns></returns>
        public static IWebElement FindElement(this RemoteWebDriver pDriver, By pQuery, bool pCheckIsVisible, int pTimeoutInSeconds = 30)
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

        /// <summary>
        /// Searches the element.
        /// </summary>
        /// <returns></returns>
        public static IWebElement SearchElement(this RemoteWebDriver pDriver, By pQuery, bool pCheckIsVisible = true, int pTimeoutInSeconds = 30)
        {
            if (pDriver == null)
                return null;

            var elementSearched = pDriver.FindElement(pQuery, pCheckIsVisible, pTimeoutInSeconds);
            if (elementSearched != null)
            {
                return elementSearched;
            }
            return null;
        }

        /// <summary>
        /// Submits the form.
        /// </summary>
        /// <returns></returns>
        public static bool SubmitForm(this RemoteWebDriver pDriver, By pQuery, bool pCheckIsVisible = true, int pTimeoutInSeconds = 30)
        {
            if (pDriver == null)
                return false;

            var elementSearched = pDriver.FindElement(pQuery, pCheckIsVisible, pTimeoutInSeconds);
            if (elementSearched != null)
            {
                elementSearched.Submit();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches the text.
        /// </summary>
        /// <returns></returns>
        public static void GoTo(this RemoteWebDriver pDriver, string pUrl)
        {
            if (pDriver == null)
                return;

            pDriver.Navigate().GoToUrl(pUrl);
        }

        /// <summary>
        /// Searches the text.
        /// </summary>
        /// <returns></returns>
        public static bool SearchText(this RemoteWebDriver pDriver, string pText)
        {
            if (pDriver == null)
                return false;

            var responseHtml = pDriver.PageSource;
            if (!string.IsNullOrEmpty(responseHtml) && (responseHtml.Contains(pText)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the element.
        /// </summary>
        public static bool SetElement(this RemoteWebDriver pDriver, By pQuery, string pValue)
        {
            if (pDriver == null)
                return false;

            var elementSearched = pDriver.FindElement(pQuery, true, 15);
            if (elementSearched != null)
            {
                pDriver.ExecuteScript($"window.scrollTo(0,{elementSearched.Location.Y})");
                elementSearched.Clear();
                elementSearched.SendKeys(pValue);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all links.
        /// </summary>
        /// <returns></returns>
        public static List<IWebElement> GetAllLinks(this RemoteWebDriver pDriver, string pStartWithFilter, int pTimeoutInSeconds = 30)
        {
            if (pDriver == null)
                return null;

            var elementSearched = pDriver.FindElementsBy(By.TagName("a"), pTimeoutInSeconds);
            if (elementSearched != null && elementSearched.Any())
            {
                return elementSearched.Where(p => p.GetAttribute("href").StartsWith(pStartWithFilter, StringComparison.Ordinal)).ToList();
            }
            return null;
        }

        /// <summary>
        /// Gets the text between.
        /// </summary>
        /// <returns></returns>
        public static string GetTextBetween(this RemoteWebDriver pDriver, string pStartText, string pEndText)
        {
            if (pDriver == null)
                return string.Empty;

            var responseHtml = pDriver.PageSource;
            if (!string.IsNullOrEmpty(responseHtml))
            {
                return responseHtml.Substring(pStartText, pEndText);
            }
            return string.Empty;
        }

        /// <summary>
        /// Search element by tag name
        /// </summary>
        /// <param name="pDriver"></param>
        /// <param name="pTagName"></param>
        /// <returns></returns>
        public static IWebElement FindElementByTagName(this RemoteWebDriver pDriver, string pTagName, bool pCheckIsVisible = true, int pTimeoutInSeconds = 30)
        {
            if (pDriver == null)
                return null;

            return pDriver.FindElement(By.TagName(pTagName), pCheckIsVisible, pTimeoutInSeconds);
        }

        /// <summary>
        /// Gets the type of the captcha.
        /// </summary>
        /// <returns></returns>
        public static Image GetTagScreenshot(this RemoteWebDriver pDriver, string pTagSearched)
        {
            Image pImage = null;
            if (pDriver == null)
                return null;

            var elementCaptcha = pDriver.FindElementByTagName(pTagSearched);
            if (elementCaptcha != null)
            {
                var ba = (ITakesScreenshot)pDriver;
                using (var ss = new Bitmap(new MemoryStream(ba.GetScreenshot().AsByteArray)))
                {
                    var crop = new Rectangle(elementCaptcha.Location.X, elementCaptcha.Location.Y,
                        elementCaptcha.Size.Width,
                        elementCaptcha.Size.Height);
                    //create a new image by cropping the original screenshot
                    pImage = ss.Clone(crop, ss.PixelFormat);
                }
            }
            return pImage;
        }


        /// <summary>
        /// Closes the navigator.
        /// </summary>
        /// <returns></returns>
        public static void CloseNavigator(this RemoteWebDriver pDriver)
        {
            if (pDriver != null)
                pDriver.Quit();
        }
    }
}
