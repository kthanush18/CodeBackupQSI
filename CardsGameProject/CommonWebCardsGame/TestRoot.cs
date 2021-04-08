using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Quant.CardsGame.UITests.Common.Web
{
    [TestClass]
    public abstract class TestRoot
    {
        protected static WebBrowser _browser;
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
                _browser = new WebBrowser();
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

                _screenshot = new Screenshot(_browser);
                _screenshot.CreateScreenshotForFailedTests(TestContext);
            }
            try
            {
                DeleteSeleniumTempFolders();
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex, "Exception occurred in TestCleanup.");
            }
            finally
            {
                _browser.QuitBrowser();
                LogInfo.WriteLine($"Name of the test: {TestContext.TestName}");
            }
        }

        protected void TakeScreenShotsForFailedTests()
        {
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed && TestContext.CurrentTestOutcome != UnitTestOutcome.Aborted)
            {
                LogInfo.WriteLine($"Test was unsuccessful. Outcome: {TestContext.CurrentTestOutcome.ToString()}");

                _screenshot = new Screenshot(_browser);
                _screenshot.CreateScreenshotForFailedTests(TestContext);
            }
        }
        private void DeleteSeleniumTempFolders()
        {
            // *********************************************************************************//
            // Selenium is not cleaning up the "scoped_dir" folders that is getting created for
            // each test run. This will delete those folders programatically after each test
            // ********************************************************************************//

            string tempFolder = Path.GetTempPath();
            string[] scopedDirectories = Directory.GetDirectories(tempFolder, "scoped_dir*", SearchOption.AllDirectories);
            foreach (string scopedDirectory in scopedDirectories)
            {
                try
                {
                    Directory.Delete(scopedDirectory, true);
                }
                catch (Exception ex)
                {
                    LogInfo.LogException(ex, $"File {scopedDirectory} could not be deleted:");
                }
            }
        }
    }
}
