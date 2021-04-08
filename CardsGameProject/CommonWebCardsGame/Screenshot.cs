using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using SeleniumScreenshot = OpenQA.Selenium.Screenshot;

namespace Quant.CardsGame.UITests.Common.Web
{
    public class Screenshot
    {
        private static string screenShotsDirectory = ConfigurationManager.AppSettings["ScreenshotsDirectory"];
        private static string screenShotsFilePath = Path.Combine(
                                                                  screenShotsDirectory,
                                                                  DateTime.Now.ToString("yyyyMMdd"),
                                                                  Environment.UserName
                                                                );

        protected WebBrowser _browser;

        public Screenshot(WebBrowser browser)
        {
            _browser = browser;
        }

        private static Log _logInfo;
        private static Log LogInfo
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

        public void CreateScreenshotForFailedTests(TestContext testContext)
        {
            string failedTestScreenshotName = string.Empty;
            int assumedLengthOfTestMethodCausingError = 50;
            try
            {
                failedTestScreenshotName = testContext.TestName.Substring(
                                                                           0,
                                                                           Math.Min(
                                                                                     testContext.TestName.Length,
                                                                                     assumedLengthOfTestMethodCausingError
                                                                                   )
                                                                         ) + "_" + DateTime.Now.ToString("HHmmssffff");

                CreateScreenshot(failedTestScreenshotName);
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex);
            }

        }

        private void CreateScreenshot(string screenshotName)
        {
            SeleniumScreenshot screenshot = null;

            try
            {
                Directory.CreateDirectory(screenShotsFilePath);

                screenshot = _browser.GetScreenshot();
                screenshot.SaveAsFile(screenShotsFilePath + "\\" + screenshotName + "." + ImageFormat.Png);
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex, "Unable to save screen shot.");
            }
        }
    }
}
