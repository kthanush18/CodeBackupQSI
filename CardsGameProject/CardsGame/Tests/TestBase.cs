using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.CardsGame.UITests.Common;
using Quant.CardsGame.UITests.Common.Web;
using Quant.CardsGame.UITests.Web.CardsGame.Pages;
using System;
using System.Configuration;
using System.IO;

namespace Quant.CardsGame.UITests.Web.CardsGame.Tests
{
    public abstract class TestBase : TestRoot
    {
        protected OnlineArchive _onlineArchive;
        protected OnlineHandViewer _onlineHandViewer;
        protected SavedHands _savedHands;
        protected CardsGameCommon _commonCardsGame;
        protected static string _cardsGameURL = ConfigurationManager.AppSettings["CardsGameURL"].ToString();

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            try
            {
                _browser = new WebBrowser();
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex, "Test Initialization failed.");
            }
            LaunchApplication(_cardsGameURL);
        }

        [TestInitialize]
        public override void TestInitialize()
        {

        }

        public static void LaunchApplication(string URL)
        {
            _browser.NavigateToUrl(URL);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
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
            }
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            TakeScreenShotsForFailedTests();
            if (_browser.GetCurrentTabCount() != 1)
            {
                _browser.CloseTab();
                _browser.SwitchToFirstTab();
            }
        }
        private static void DeleteSeleniumTempFolders()
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
        protected void SendEmailOfTestStatus(int assertionFailures)
        {
            if (ConfigurationManager.AppSettings["TestStatusNotificationEmailAddresses"] != null)
            {
                string[] toAddresses = ConfigurationManager.AppSettings["TestStatusNotificationEmailAddresses"].Split(',');
                if (toAddresses[0] != "")
                {
                    //mail subject
                    string mailSubject = "UI Automation Test Run Status";

                    //Mail body
                    string mailBody;
                    if (assertionFailures != 0)
                    {
                        mailBody = $"Test run was completed and Run failed Due to log errors. Failed cases = {assertionFailures}";
                    }
                    else
                    {
                        mailBody = "Test run Was completed and Run successful without any errors";
                    }

                    Email email = new Email();
                    email.SendEmail(mailSubject, mailBody, toAddresses);
                }
            }
        }
    }
}
