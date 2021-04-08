using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Quant.CardsGame.UITests.Common.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using SeleniumScreenshot = OpenQA.Selenium.Screenshot;

namespace Quant.CardsGame.UITests.Common.Web
{
    public class WebBrowser
    {
        public enum ElementSelectorType
        {
            ID,
            Class,
            XPath,
            CssSelector
        }

        private readonly int _maxTimeOut;
        public readonly IWebDriver _webDriver;
        private readonly string _browserName;
        private IJavaScriptExecutor _javaScriptExecutor;
        private readonly int totalWaitTime;

        public WebBrowser()
        {
            _maxTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MaxWaitTime"] ?? "120000");
            _browserName = ConfigurationManager.AppSettings["Browser"];
            switch (_browserName)
            {
                case "Chrome":
                    _webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromMinutes(2));
                    break;
                case "Firefox":
                    _webDriver = new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), new FirefoxOptions(), TimeSpan.FromMinutes(2));
                    break;
            }
        }

        #region Browser Operations
        public void NavigateToUrl(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
            _webDriver.Manage().Window.Maximize();
        }
        public void SwitchtoCurrentWindow()
        {
            _webDriver.SwitchTo().Window(_webDriver.WindowHandles.Last());
            _webDriver.Manage().Window.Maximize();
        }
        public void SwitchtoWindowName(string windowName)
        {
            _webDriver.SwitchTo().Window(windowName);
            _webDriver.Manage().Window.Maximize();
        }
        public string GetCurrentWindowHandleName()
        {
            return _webDriver.CurrentWindowHandle;
        }
        public IReadOnlyCollection<string> GetAllWindowHandleNames()
        {
            return _webDriver.WindowHandles;
        }
        public void SwitchToAlertWindowAndAccept()
        {
            _webDriver.SwitchTo().Alert().Accept();
        }
        public void SwitchtoPreviousWindow()
        {
            _webDriver.SwitchTo().Window(_webDriver.WindowHandles.First());
        }
        public void ExecuteJavaScriptToRemoveAttribute(string selector)
        {
            _javaScriptExecutor = (IJavaScriptExecutor)_webDriver;
            _javaScriptExecutor.ExecuteScript($"document.getElementById('{selector}').removeAttribute('readonly')");
        }
        public Cookie GetCookie(string filterNameFromCreateFilter)
        {
            return _webDriver.Manage().Cookies.GetCookieNamed(filterNameFromCreateFilter);
        }
        public void MoveToWebElement(IWebElement element)
        {
            new Actions(_webDriver).MoveToElement(element).Perform();
        }
        /// <summary>
        /// WebDriver Quit and then Dispose to close the browser
        /// </summary>
        public void CloseBrowser()
        {
            if (_webDriver != null)
            {
                _webDriver.Close();
            }
        }

        public void QuitBrowser()
        {
            if (_webDriver != null)
            {
                _webDriver.Quit();
                _webDriver.Dispose();
            }
        }

        public void Back()
        {
            _webDriver.Navigate().Back();
        }

        public void Forward()
        {
            _webDriver.Navigate().Forward();
        }

        public void Refresh()
        {
            _webDriver.Navigate().Refresh();
        }
        #endregion

        #region Tabs
        public T WaitForCondition<T>(Func<IWebDriver, T> condition)
        {
            return WaitForCondition(condition, TimeSpan.FromMilliseconds(totalWaitTime));
        }

        public T WaitForCondition<T>(Func<IWebDriver, T> condition, TimeSpan timeTowait)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, timeTowait);

            T result = wait.Until(condition);

            return result;
        }
        public int GetCurrentTabCount()
        {
            int currentTabCount;
            try
            {
                currentTabCount = _webDriver.WindowHandles.Count();
            }
            catch
            {
                //if all windows are closed then it will throws an error to handle if all windows are closed.
                currentTabCount = 0;
            }

            return currentTabCount;
        }

        public string GetTabTitle()
        {
            return _webDriver.Title;
        }

        public List<string> GetAllTabTitles()
        {
            List<string> tabTitles = new List<string>();
            int windowsCount = _webDriver.WindowHandles.Count;

            for (int i = 0; i < windowsCount; i++)
            {
                _webDriver.SwitchTo().Window(_webDriver.WindowHandles.ElementAt(i));

                tabTitles.Add(_webDriver.Title);
            }
            return tabTitles;
        }

        public void OpenNewTab()
        {
            OpenNewTab(string.Empty);
        }

        public void OpenNewTab(string url)
        {
            _javaScriptExecutor = (IJavaScriptExecutor)_webDriver;
            _javaScriptExecutor.ExecuteScript("window.open('" + url + "', '_blank');");
        }

        /// <summary>
        /// Waits for GetAllTabTitles() to have a record matching tabTitle and then switched to that tab
        /// </summary>
        /// <param name="tabTitle"></param>
        public void SwitchToFirstTabWithName(string tabTitle)
        {
            SwitchToTab(tabTitle, 0);
        }

        public List<TabModel> GetTabs()
        {
            List<TabModel> tabs = new List<TabModel>();
            int windowsCount = _webDriver.WindowHandles.Count;

            for (int i = 0; i < windowsCount; i++)
            {
                _webDriver.SwitchTo().Window(_webDriver.WindowHandles.ElementAt(i));

                tabs.Add(new TabModel() { Title = _webDriver.Title, Handle = _webDriver.WindowHandles.ElementAt(i) });
            }

            return tabs;
        }

        public void SwitchToTab(string tabTitle, int index)
        {
            WaitForCondition(e => GetTabs().Any(t => t.Title == tabTitle));

            TabModel tab = GetTabs().Where(t => t.Title == tabTitle).ElementAtOrDefault(index);
            if (tab != null)
            {
                _webDriver.SwitchTo().Window(tab.Handle);
            }
            else
            {
                throw new NoSuchElementException(string.Format("There was not a tab with title {0} at index {1}", tabTitle, index));
            }

        }

        public void SwitchToFirstTab()
        {
            _webDriver.SwitchTo().Window(_webDriver.WindowHandles.First());
        }

        public void SwitchToLastTab()
        {
            _webDriver.SwitchTo().Window(_webDriver.WindowHandles.Last());
        }

        public void CloseTab(string tabTitle)
        {
            SwitchToFirstTabWithName(tabTitle);
            _webDriver.Close();
        }

        public void CloseTab()
        {
            _webDriver.Close();
        }
        #endregion Tabs

        #region Operations on HTML elements

        #region WaitForElement
        public bool WaitForElement(string selector, ElementSelectorType selectorType, [Optional]int? maxTimeOut)
        {
            bool waitResult = false;
            int maxWaitTime = maxTimeOut ?? _maxTimeOut;

            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    waitResult = WaitForElementByID(selector, maxWaitTime);
                    break;
                case ElementSelectorType.Class:
                    waitResult = WaitForElementByClass(selector, maxWaitTime);
                    break;
                case ElementSelectorType.XPath:
                    waitResult = WaitForElementByXPath(selector, maxWaitTime);
                    break;
            }
            return waitResult;
        }

        private bool WaitForElementByID(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.Id(selector)).Any());
            return result;
        }

        private bool WaitForElementByClass(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.ClassName(selector)).Any());
            return result;
        }

        private bool WaitForElementByXPath(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.XPath(selector)).Any());
            return result;
        }
        #endregion
        #region WaitForElementText
        public bool WaitForElementText(string elementText, string selector, ElementSelectorType selectorType, [Optional]int? maxTimeOut)
        {
            bool waitResult = false;
            int maxWaitTime = maxTimeOut ?? _maxTimeOut;

            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    waitResult = WaitForElementTextByID(selector, maxWaitTime, elementText);
                    break;
                case ElementSelectorType.Class:
                    waitResult = WaitForElementTextByClass(selector, maxWaitTime, elementText);
                    break;
                case ElementSelectorType.XPath:
                    waitResult = WaitForElementTextByXPath(selector, maxWaitTime, elementText);
                    break;
                case ElementSelectorType.CssSelector:
                    waitResult = WaitForElementTextByCssSelector(selector, maxWaitTime, elementText);
                    break;
            }
            return waitResult;
        }

        private bool WaitForElementTextByID(string selector, int maxTimeOut, string elementText)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElement(By.Id(selector)).Text != elementText);
            return result;
        }

        private bool WaitForElementTextByClass(string selector, int maxTimeOut, string elementText)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElement(By.ClassName(selector)).Text != elementText);
            return result;
        }

        private bool WaitForElementTextByXPath(string selector, int maxTimeOut, string elementText)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElement(By.XPath(selector)).Text != elementText);
            return result;
        }
        private bool WaitForElementTextByCssSelector(string selector, int maxTimeOut, string elementText)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElement(By.CssSelector(selector)).Text != elementText);
            return result;
        }
        #endregion
        #region IsElementVisible
        public bool IsElementVisible(string selector, ElementSelectorType selectorType)
        {
            bool isVisible = false;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    isVisible = IsElementVisibleByID(selector);
                    break;
                case ElementSelectorType.Class:
                    isVisible = IsElementVisibleByClass(selector);
                    break;
                case ElementSelectorType.XPath:
                    isVisible = IsElementVisibleByXPath(selector);
                    break;
                case ElementSelectorType.CssSelector:
                    isVisible = IsElementVisibleByCssSelector(selector);
                    break;
            }
            return isVisible;
        }

        private bool IsElementVisibleByID(string selector)
        {
            bool result = _webDriver.FindElement(By.Id(selector)).Displayed;
            return result;
        }

        private bool IsElementVisibleByClass(string selector)
        {
            bool result = _webDriver.FindElement(By.ClassName(selector)).Displayed;
            return result;
        }
        private bool IsElementVisibleByXPath(string selector)
        {
            bool result = _webDriver.FindElement(By.XPath(selector)).Displayed;
            return result;
        }
        private bool IsElementVisibleByCssSelector(string selector)
        {
            bool result = _webDriver.FindElement(By.CssSelector(selector)).Displayed;
            return result;
        }
        #endregion

        #region FindElement
        public bool FindElement(string selector, ElementSelectorType selectorType, [Optional]int? maxTimeOut)
        {
            bool waitResult = false;
            int maxWaitTime = maxTimeOut ?? _maxTimeOut;

            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    waitResult = FindElementByID(selector, maxWaitTime);
                    break;
                case ElementSelectorType.Class:
                    waitResult = FindElementByClass(selector, maxWaitTime);
                    break;
                case ElementSelectorType.XPath:
                    waitResult = FindElementByXPath(selector, maxWaitTime);
                    break;
            }
            return waitResult;
        }

        private bool FindElementByID(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.Id(selector)).Any());
            return result;
        }

        private bool FindElementByClass(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.ClassName(selector)).Any());
            return result;
        }

        private bool FindElementByXPath(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.XPath(selector)).Any());
            return result;
        }
        #endregion

        #region FindElements
        public bool FindElements(string selector, ElementSelectorType selectorType, [Optional]int? maxTimeOut)
        {
            bool waitResult = false;
            int maxWaitTime = maxTimeOut ?? _maxTimeOut;

            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    waitResult = FindElementsByID(selector, maxWaitTime);
                    break;
                case ElementSelectorType.Class:
                    waitResult = FindElementsByClass(selector, maxWaitTime);
                    break;
                case ElementSelectorType.XPath:
                    waitResult = FindElementsByXPath(selector, maxWaitTime);
                    break;
            }
            return waitResult;
        }

        private bool FindElementsByID(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.Id(selector)).Any());
            return result;
        }

        private bool FindElementsByClass(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.ClassName(selector)).Any());
            return result;
        }

        private bool FindElementsByXPath(string selector, int maxTimeOut)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(maxTimeOut));
            bool result = wait.Until(w => w.FindElements(By.XPath(selector)).Any());
            return result;
        }
        #endregion

        #region GetElement
        public IWebElement GetElement(string selector, ElementSelectorType selectorType)
        {
            IWebElement WebElement = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    WebElement = GetElementByID(selector);
                    break;
                case ElementSelectorType.Class:
                    WebElement = GetElementByClass(selector);
                    break;
                case ElementSelectorType.XPath:
                    WebElement = GetElementByXPath(selector);
                    break;
                case ElementSelectorType.CssSelector:
                    WebElement = GetElementByCssSelector(selector);
                    break;
            }
            return WebElement;
        }

        private IWebElement GetElementByID(string selector)
        {
            IWebElement WebElement = _webDriver.FindElement(By.Id(selector));
            return WebElement;
        }

        private IWebElement GetElementByClass(string selector)
        {
            IWebElement WebElement = _webDriver.FindElement(By.ClassName(selector));
            return WebElement;
        }

        private IWebElement GetElementByXPath(string selector)
        {
            IWebElement WebElement = _webDriver.FindElement(By.XPath(selector));
            return WebElement;
        }
        private IWebElement GetElementByCssSelector(string selector)
        {
            IWebElement WebElement = _webDriver.FindElement(By.CssSelector(selector));
            return WebElement;
        }
        #endregion

        #region GetElements
        public List<IWebElement> GetElements(string selector, ElementSelectorType selectorType)
        {
            List<IWebElement> WebElements = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    WebElements = GetElementsByID(selector);
                    break;
                case ElementSelectorType.Class:
                    WebElements = GetElementsByClass(selector);
                    break;
                case ElementSelectorType.XPath:
                    WebElements = GetElementsByXPath(selector);
                    break;
                case ElementSelectorType.CssSelector:
                    WebElements = GetElementsByCssSelector(selector);
                    break;
            }
            return WebElements;
        }

        private List<IWebElement> GetElementsByID(string selector)
        {
            List<IWebElement> WebElements = _webDriver.FindElements(By.Id(selector)).ToList();
            return WebElements;
        }

        private List<IWebElement> GetElementsByClass(string selector)
        {
            List<IWebElement> WebElements = _webDriver.FindElements(By.ClassName(selector)).ToList();
            return WebElements;
        }

        private List<IWebElement> GetElementsByXPath(string selector)
        {
            List<IWebElement> WebElements = _webDriver.FindElements(By.XPath(selector)).ToList();
            return WebElements;
        }
        private List<IWebElement> GetElementsByCssSelector(string selector)
        {
            List<IWebElement> WebElements = _webDriver.FindElements(By.CssSelector(selector)).ToList();
            return WebElements;
        }
        #endregion

        #endregion
        public SeleniumScreenshot GetScreenshot()
        {
            SeleniumScreenshot screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();
            return screenshot;
        }
    }
}
