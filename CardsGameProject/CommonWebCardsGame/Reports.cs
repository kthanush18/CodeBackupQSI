using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using SeleniumScreenshot = OpenQA.Selenium.Screenshot;

namespace Quant.CardsGame.UITests.Common.Web
{
    public class Reports 
    {
        protected WebBrowser _browser;
        private static ExtentReports _extent;
        private static ExtentTest _test;
        private static readonly string htmlReportDirectory = ConfigurationManager.AppSettings["ReportsDirectory"];

        public Reports(WebBrowser browser)
        {
            _browser = browser;
            try
            {
                //To create report directory and add HTML report into it
                _extent = new ExtentReports();
                string dateTimeNow = DateTime.Now.ToString("yyyy'-'MM'-'dd'-'HH'-'mm'-'ss");
                ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter($"{htmlReportDirectory}\\report-{dateTimeNow}.html");
                htmlReporter.Config.DocumentTitle = "Cads Game Website Test Reports";
                htmlReporter.Config.Theme = Theme.Dark;
                _extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        
        public void CreateTestInExtentReport(string testName)
        {
            try
            {
                _test = _extent.CreateTest(testName);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public void LogTestInfo(string assertionName)
        {
            Status logstatus = Status.Pass;
            _test.Log(logstatus, $"Assertion successful for {assertionName}");
        }
        public void LogTestInfo(string assertionName, Exception Ex,string actualResult,string expectedResult)
        {
            Status logstatus = Status.Fail;
            _test.Log(logstatus,$"Assertion failed for {assertionName}–actualResult: {actualResult}–expectedResult: {expectedResult}–errorMessage: {Ex.Message}-stackTrace: {Ex.StackTrace}");
        }
        public void LogTestStatus(Status logStatus,string testName,string pageNumber,string boardNumber, string eventName, [Optional] string segmentName)
        {   
            try
            {
                switch (logStatus)
                {
                    case Status.Pass:
                        _test.Log(logStatus, $"Test ended with {logStatus}");
                        if(segmentName != null)
                        {
                            _test.Log(logStatus, $"Test details: PageNumber-{pageNumber},  EventName-{eventName},  SegmentName-{segmentName},  BoardNumber-{boardNumber}");
                        }
                        else
                        {
                            _test.Log(logStatus, $"Test details: PageNumber-{pageNumber},  EventName-{eventName},  BoardNumber-{boardNumber}");
                        }
                        break;
                    case Status.Fail:
                        if (segmentName != null)
                        {
                            _test.Log(logStatus, $"Test details: PageNumber-{pageNumber}," +
                            $"  EventName-{eventName},  SegmentName-{segmentName},  " +
                            $"BoardNumber-{boardNumber},");
                        }
                        else
                        {
                            _test.Log(logStatus, $"Test details: PageNumber-{pageNumber},  EventName-{eventName}," +
                            $"BoardNumber-{boardNumber}");
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public void PublishExtentReports()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
