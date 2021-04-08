using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.WindowsUI
{
    [TestClass]
    public abstract class TestRoot
    {
        protected static WindowUIDriver _windowUIDriver;
        protected static Screenshot _screenshot;

        private static Log _logInfo;
        protected static Log LogInfo
        {
            get
            {
                _logInfo = new Log();
                return _logInfo;
            }
            set
            {
                _logInfo = value;
            }
        }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            try
            {
                _windowUIDriver = new WindowUIDriver();
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex, "Test Initialization failed.");
            }
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed && TestContext.CurrentTestOutcome != UnitTestOutcome.Aborted)
            {
                LogInfo.WriteLine($"Test was unsuccessful. Outcome: {TestContext.CurrentTestOutcome.ToString()}");

                _screenshot = new Screenshot(_windowUIDriver);
                _screenshot.CreateScreenshotForFailedTests(TestContext);
            }
            
            _windowUIDriver.StopWinAppDriver();
            LogInfo.WriteLine($"Name of the test: {TestContext.TestName}");
        }
    }
}
