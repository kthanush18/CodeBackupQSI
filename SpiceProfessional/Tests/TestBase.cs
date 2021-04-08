using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Test.UI.Common.WindowsUI;
using Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.Tests
{
    [TestClass]
    public class TestBase : TestRoot
    {
        protected Home _home;
        protected SourceStatistics _sourceStatistics;
        protected SearchOptions _searchOptions;
        protected Timeline _timeline;
        protected CumulativeUsageGraph _cumulativeUsageGraph;
        protected Settings _settings;
        protected Login _login;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            try
            {
                _windowUIDriver = new WindowUIDriver();
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex, "Test Initialization failed.");
            }
            Login login = new Login(_windowUIDriver);
            //As splash screen having no controls for using explicit wait time. Implicit wait time is used after launching application.
            Thread.Sleep(login._waitTimeForLoginWindow);
            login.OpenWindowAndLoginIntoSpice();
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            try
            {
                RemoveDevice();

                //Unable to identify the login window only in the case of remove device, so killing the application.
                _windowUIDriver.StopWinAppDriver();
                _windowUIDriver.KillApplication();
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex, "Exception occurred in Removed Device.");
            }
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            
        }
        public WindowsElement GetAccountTab()
        {
            return _windowUIDriver.GetElement("picbxAccount", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetRemoveDeviceButton()
        {
            return _windowUIDriver.GetElement("picbxRemoveDeviceButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetYesButton()
        {
            return _windowUIDriver.GetElement("picbxYesButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForConfirmation()
        {
            _windowUIDriver.WaitForWindowsElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetOkButton()
        {
            return _windowUIDriver.GetElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public static void RemoveDevice()
        {
            TestBase testBase = new TestBase();
            testBase.GetAccountTab().Click();
            testBase.GetRemoveDeviceButton().Click();
            testBase.GetYesButton().Click();
            testBase.WaitForConfirmation();
            testBase.GetOkButton().Click();
        }
    }
}
