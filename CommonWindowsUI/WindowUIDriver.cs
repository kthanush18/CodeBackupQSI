using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using SeleniumScreenshot = OpenQA.Selenium.Screenshot;

namespace Quant.Spice.Test.UI.Common.WindowsUI
{
    public class WindowUIDriver
    {
        public enum ElementSelectorType
        {
            ID,
            Name,
            ClassName,
            TagName
        }
        public Process WinAppDriver { get; set; }
        protected static WindowsDriver<WindowsElement> _windowsDriver;
        protected static WindowsDriver<WindowsElement> _notepadDriver;
        protected static WindowsDriver<WindowsElement> _wordDriver;
        private readonly int _maxTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MaxWaitTime"] ?? "120000");
        protected static string _appiumDriverURI = ConfigurationManager.AppSettings["AppiumDriverURI"].ToString();
        protected static string _spiceAppLocation = ConfigurationManager.AppSettings["SpiceAppLocation"].ToString();
        protected static string _winAppDriverLocation = ConfigurationManager.AppSettings["WinAppDriverLocation"].ToString();
        protected static string _notePad = ConfigurationManager.AppSettings["NotePad"].ToString();
        protected static string _wordDocument = ConfigurationManager.AppSettings["WordDocument"].ToString();
        protected static string _processName = ConfigurationManager.AppSettings["ProcessName"].ToString();

        public WindowUIDriver()
        {
            //Start Windows Application Driver
            StartWinAppDriver(_winAppDriverLocation);

            //Open Spice Professional
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("platformName", "Windows");
            options.AddAdditionalCapability("app", _spiceAppLocation);
            _windowsDriver = new WindowsDriver<WindowsElement>(new Uri(_appiumDriverURI), options);
        }

        public void StartWinAppDriver(string _winAppDriverLocation)
        {
            WinAppDriver = new Process();
            WinAppDriver.StartInfo.FileName = _winAppDriverLocation;
            WinAppDriver.Start();
        }

        public void StopWinAppDriver()
        {
            WinAppDriver.CloseMainWindow();
            WinAppDriver.Close();
        }

        public WindowsDriver<WindowsElement> OpenNotepad()
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("platformName", "Windows");
            options.AddAdditionalCapability("app", _notePad);
            _notepadDriver = new WindowsDriver<WindowsElement>(new Uri(_appiumDriverURI), options);
            return _notepadDriver;
        }
        public WindowsDriver<WindowsElement> OpenWordDocument()
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("platformName", "Windows");
            options.AddAdditionalCapability("app", _wordDocument);
            _wordDriver = new WindowsDriver<WindowsElement>(new Uri(_appiumDriverURI), options);
            return _wordDriver;
        }

        #region WindowsUI Operations
        public void SwitchToGivenWindow(int indexOfLoginWindow)
        {
            List<string> allWindowHandles = _windowsDriver.WindowHandles.ToList();
            _windowsDriver.SwitchTo().Window(allWindowHandles[indexOfLoginWindow]);
        }
        public void SwitchToPreviousWindow()
        {
            if (_windowsDriver.CurrentWindowHandle != _windowsDriver.WindowHandles.Last())
            {
                _windowsDriver.SwitchTo().Window(_windowsDriver.WindowHandles.Last());
            }
        }
        public void SwitchToFirstWindow()
        {
            if (_windowsDriver.CurrentWindowHandle != _windowsDriver.WindowHandles.First())
            {
                _windowsDriver.SwitchTo().Window(_windowsDriver.WindowHandles.First());
            }
        }
        public void SwitchToWordGivenWindow(int indexOfLoginWindow)
        {
            List<string> allWindowHandles = _wordDriver.WindowHandles.ToList();
            _wordDriver.SwitchTo().Window(allWindowHandles[indexOfLoginWindow]);
        }
        public void ClickDownButtonAndEnter()
        {
            Actions action = new Actions(_windowsDriver);
            action.SendKeys(Keys.ArrowDown + Keys.Enter).Perform();
        }
        public void ClickRightArrowButtonAndEnter()
        {
            Actions action = new Actions(_wordDriver);
            action.SendKeys(Keys.ArrowRight + Keys.Enter).Perform();
        }
        public void ClickAtStartPointOfElement(WindowsElement Element)
        {
            Actions action = new Actions(_windowsDriver);
            action.MoveToElement(Element, Element.Size.Width / 5, Element.Size.Height / 5).Click().Perform();
        }
        /// <summary>
        /// WindowsDriver Quit and then Dispose to close the Application
        /// </summary>
        public void CloseApplication()
        {
            if (_windowsDriver != null)
            {
                _windowsDriver.Close();
            }
        }

        public void QuitApplication()
        {
            if (_windowsDriver != null)
            {   
                _windowsDriver.Quit();
                _windowsDriver.Dispose();
            }
        }

        public void KillApplication()
        {
            if (_windowsDriver != null)
            {
                IEnumerable<Process> spiceApplicationProcess = Process.GetProcesses().
                                 Where(pr => pr.ProcessName == _processName);

                foreach (Process spiceAppProcess in spiceApplicationProcess)
                {
                    spiceAppProcess.Kill();
                }
            }
        }
        #endregion

        #region Operations on Windows elements

        #region WaitForWindowsElement
        public void WaitForWindowsElement(string selector, ElementSelectorType selectorType, [Optional]int? maxTimeOut)
        {
            int maxWaitTime = maxTimeOut ?? _maxTimeOut;

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(_windowsDriver)
            {
                Timeout = TimeSpan.FromSeconds(maxWaitTime),
                PollingInterval = TimeSpan.FromSeconds(0.5)
            };

            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));

            wait.Until(_windowsDriver =>
            {
                int elementCount = 0;
                switch (selectorType)
                {
                    case ElementSelectorType.ID:
                        elementCount = _windowsDriver.FindElementsByAccessibilityId(selector).Count;
                        break;
                    case ElementSelectorType.Name:
                        elementCount = _windowsDriver.FindElementsByName(selector).Count;
                        break;
                    case ElementSelectorType.ClassName:
                        elementCount = _windowsDriver.FindElementsByClassName(selector).Count;
                        break;
                }
                return elementCount > 0;
            });
        }
        #endregion
        #region WaitForWordElement
        public void WaitForWordElement(string selector, ElementSelectorType selectorType, [Optional]int? maxTimeOut)
        {
            int maxWaitTime = maxTimeOut ?? _maxTimeOut;

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(_wordDriver)
            {
                Timeout = TimeSpan.FromSeconds(maxWaitTime),
                PollingInterval = TimeSpan.FromSeconds(0.5)
            };

            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));

            wait.Until(_wordDriver =>
            {
                int elementCount = 0;
                switch (selectorType)
                {
                    case ElementSelectorType.ID:
                        elementCount = _wordDriver.FindElementsByAccessibilityId(selector).Count;
                        break;
                    case ElementSelectorType.Name:
                        elementCount = _wordDriver.FindElementsByName(selector).Count;
                        break;
                    case ElementSelectorType.ClassName:
                        elementCount = _wordDriver.FindElementsByClassName(selector).Count;
                        break;
                }
                return elementCount > 0;
            });
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
                case ElementSelectorType.Name:
                    isVisible = IsElementVisibleByName(selector);
                    break;
                case ElementSelectorType.ClassName:
                    isVisible = IsElementVisibleByClassName(selector);
                    break;
            }
            return isVisible;
        }

        private bool IsElementVisibleByID(string selector)
        {
            bool result = _windowsDriver.FindElementByAccessibilityId(selector).Displayed;
            return result;
        }

        private bool IsElementVisibleByName(string selector)
        {
            bool result = _windowsDriver.FindElementByName(selector).Displayed;
            return result;
        }
        private bool IsElementVisibleByClassName(string selector)
        {
            bool result = _windowsDriver.FindElementByClassName(selector).Displayed;
            return result;
        }
        #endregion
        #region IsAppiumElementVisible
        public bool IsAppiumElementVisible(string selector, ElementSelectorType selectorType, WindowsElement element)
        {
            bool isVisible = false;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    isVisible = IsAppiumElementVisibleByID(selector, element);
                    break;
                case ElementSelectorType.Name:
                    isVisible = IsAppiumElementVisibleByName(selector, element);
                    break;
                case ElementSelectorType.ClassName:
                    isVisible = IsAppiumElementVisibleByClassName(selector, element);
                    break;
            }
            return isVisible;
        }

        private bool IsAppiumElementVisibleByID(string selector, WindowsElement element)
        {
            bool result = element.FindElementByAccessibilityId(selector).Displayed;
            return result;
        }

        private bool IsAppiumElementVisibleByName(string selector, WindowsElement element)
        {
            bool result = element.FindElementByName(selector).Displayed;
            return result;
        }
        private bool IsAppiumElementVisibleByClassName(string selector, WindowsElement element)
        {
            bool result = element.FindElementByClassName(selector).Displayed;
            return result;
        }
        #endregion

        #region GetElement
        public WindowsElement GetElement(string selector, ElementSelectorType selectorType)
        {
            WindowsElement Element = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    Element = GetElementByID(selector);
                    break;
                case ElementSelectorType.Name:
                    Element = GetElementByName(selector);
                    break;
                case ElementSelectorType.ClassName:
                    Element = GetElementByClassname(selector);
                    break;
            }
            return Element;
        }

        private WindowsElement GetElementByID(string selector)
        {
            WindowsElement Element = _windowsDriver.FindElementByAccessibilityId(selector);
            return Element;
        }

        private WindowsElement GetElementByName(string selector)
        {
            WindowsElement Element = _windowsDriver.FindElementByName(selector);
            return Element;
        }

        private WindowsElement GetElementByClassname(string selector)
        {
            WindowsElement Element = _windowsDriver.FindElementByClassName(selector);
            return Element;
        }
        #endregion
        #region GetNotepadElement
        public WindowsElement GetNotepadElement(string selector, ElementSelectorType selectorType)
        {
            WindowsElement Element = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    Element = GetNotepadElementByID(selector);
                    break;
                case ElementSelectorType.Name:
                    Element = GetNotepadElementByName(selector);
                    break;
                case ElementSelectorType.ClassName:
                    Element = GetNotepadElementByClassname(selector);
                    break;
            }
            return Element;
        }

        private WindowsElement GetNotepadElementByID(string selector)
        {
            WindowsElement Element = _notepadDriver.FindElementByAccessibilityId(selector);
            return Element;
        }

        private WindowsElement GetNotepadElementByName(string selector)
        {
            WindowsElement Element = _notepadDriver.FindElementByName(selector);
            return Element;
        }

        private WindowsElement GetNotepadElementByClassname(string selector)
        {
            WindowsElement Element = _notepadDriver.FindElementByClassName(selector);
            return Element;
        }
        #endregion
        #region GetWordElement
        public WindowsElement GetWordElement(string selector, ElementSelectorType selectorType)
        {
            WindowsElement Element = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    Element = GetWordElementByID(selector);
                    break;
                case ElementSelectorType.Name:
                    Element = GetWordElementByName(selector);
                    break;
                case ElementSelectorType.ClassName:
                    Element = GetWordElementByClassname(selector);
                    break;
            }
            return Element;
        }

        private WindowsElement GetWordElementByID(string selector)
        {
            WindowsElement Element = _wordDriver.FindElementByAccessibilityId(selector);
            return Element;
        }

        private WindowsElement GetWordElementByName(string selector)
        {
            WindowsElement Element = _wordDriver.FindElementByName(selector);
            return Element;
        }

        private WindowsElement GetWordElementByClassname(string selector)
        {
            WindowsElement Element = _wordDriver.FindElementByClassName(selector);
            return Element;
        }
        #endregion
        #region GetAppiumElements
        public List<AppiumWebElement> GetAppiumElements(string selector, ElementSelectorType selectorType, WindowsElement element)
        {
            List<AppiumWebElement> Elements = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    Elements = GetAppiumElementsByID(selector, element);
                    break;
                case ElementSelectorType.Name:
                    Elements = GetAppiumElementsByName(selector, element);
                    break;
                case ElementSelectorType.ClassName:
                    Elements = GetAppiumElementsByClassname(selector, element);
                    break;
                case ElementSelectorType.TagName:
                    Elements = GetAppiumElementsByTagname(selector, element);
                    break;
            }
            return Elements;
        }
        private List<AppiumWebElement> GetAppiumElementsByID(string selector, WindowsElement element)
        {
            List<AppiumWebElement> Elements = element.FindElementsByAccessibilityId(selector).ToList();
            return Elements;
        }

        private List<AppiumWebElement> GetAppiumElementsByName(string selector, WindowsElement element)
        {
            List<AppiumWebElement> Elements = element.FindElementsByName(selector).ToList();
            return Elements;
        }

        private List<AppiumWebElement> GetAppiumElementsByClassname(string selector, WindowsElement element)
        {
            List<AppiumWebElement> Elements = element.FindElementsByClassName(selector).ToList();
            return Elements;
        }
        private List<AppiumWebElement> GetAppiumElementsByTagname(string selector, WindowsElement element)
        {
            List<AppiumWebElement> Elements = element.FindElementsByTagName(selector).ToList();
            return Elements;
        }
        #endregion
        #region GetAppiumElement
        public AppiumWebElement GetAppiumElement(string selector, ElementSelectorType selectorType, WindowsElement element)
        {
            AppiumWebElement Element = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    Element = GetAppiumElementByID(selector, element);
                    break;
                case ElementSelectorType.Name:
                    Element = GetAppiumElementByName(selector, element);
                    break;
                case ElementSelectorType.ClassName:
                    Element = GetAppiumElementByClassname(selector, element);
                    break;
                case ElementSelectorType.TagName:
                    Element = GetAppiumElementByTagname(selector, element);
                    break;
            }
            return Element;
        }
        private AppiumWebElement GetAppiumElementByID(string selector, WindowsElement element)
        {
            AppiumWebElement Element = element.FindElementByAccessibilityId(selector);
            return Element;
        }

        private AppiumWebElement GetAppiumElementByName(string selector, WindowsElement element)
        {
            AppiumWebElement Element = element.FindElementByName(selector);
            return Element;
        }

        private AppiumWebElement GetAppiumElementByClassname(string selector, WindowsElement element)
        {
            AppiumWebElement Element = element.FindElementByClassName(selector);
            return Element;
        }
        private AppiumWebElement GetAppiumElementByTagname(string selector, WindowsElement element)
        {
            AppiumWebElement Element = element.FindElementByTagName(selector);
            return Element;
        }
        #endregion

        #region GetElements
        public List<WindowsElement> GetElements(string selector, ElementSelectorType selectorType)
        {
            List<WindowsElement> Elements = null;
            switch (selectorType)
            {
                case ElementSelectorType.ID:
                    Elements = GetElementsByID(selector);
                    break;
                case ElementSelectorType.Name:
                    Elements = GetElementsByName(selector);
                    break;
                case ElementSelectorType.ClassName:
                    Elements = GetElementsByClassName(selector);
                    break;
            }
            return Elements;
        }

        private List<WindowsElement> GetElementsByID(string selector)
        {
            List<WindowsElement> Elements = _windowsDriver.FindElementsByAccessibilityId(selector).ToList();
            return Elements;
        }

        private List<WindowsElement> GetElementsByName(string selector)
        {
            List<WindowsElement> Elements = _windowsDriver.FindElementsByName(selector).ToList();
            return Elements;
        }

        private List<WindowsElement> GetElementsByClassName(string selector)
        {
            List<WindowsElement> Elements = _windowsDriver.FindElementsByClassName(selector).ToList();
            return Elements;
        }
        #endregion
        #endregion
        public SeleniumScreenshot GetScreenshot()
        {
            SeleniumScreenshot screenshot = ((ITakesScreenshot)_windowsDriver).GetScreenshot();
            return screenshot;
        }
    }
}
