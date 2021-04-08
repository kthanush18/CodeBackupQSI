using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WritersMuse.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Tests
{
    [TestClass]
    public abstract class TestBase : TestRoot
    {
        protected Logout _logout;
        protected Login _login;

        protected static string _writersMuseURL = ConfigurationManager.AppSettings["WritersMuseURL"].ToString();

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
            LaunchApplication();
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            
        }

        public static void LaunchApplication()
        {
            _browser.NavigateToUrl(_writersMuseURL);
        }

        public new TestContext TestContext { get; set; }

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

        [TestCleanup]
        public override void TestCleanup()
        {
            
        }
    }
}
