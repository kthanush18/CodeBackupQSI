using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WritersMuse.Models;
using Quant.Spice.Test.UI.Web.WritersMuse.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Tests
{
    [TestClass]
    public class LoginTests :TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            Login _login = new Login(_browser);
            _login.LoginToWritersMuse();
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _login = new Login(_browser);
        }

        [TestMethod]
        public void TC_LoginToWritersMusePortal_VerfiyTopRightLinksLoaded()
        {
            //Arrange
            _login.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_login.IsSignOutLinkVisible());
            Assert.IsTrue(_login.IsAccountLinkVisible());
            Assert.IsTrue(_login.IsChangePasswordLinkVisible());
            Assert.IsTrue(_login.IsSubscribeLinkVisible());
        }

        [TestMethod]
        public void TC_LoginToWritersMusePortal_VerfiyHeaderLinksLoaded()
        {
            //Arrange
            _login.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_login.IsHomeLinkVisible());
            Assert.IsTrue(_login.IsHeaderSubscribeLinkVisible());
            Assert.IsTrue(_login.IsContactLinkVisible());
            Assert.IsTrue(_login.IsHelpLinkVisible());
            Assert.IsTrue(_login.IsFacebookLogoVisible());
            Assert.IsTrue(_login.IsTwitterlogoVisible());
        }

        [TestMethod]
        public void TC_LoginToWritersMusePortal_VerfiyLeftRibbonLinksLoaded()
        {
            //Arrange
            _login.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_login.IsWebsiteLogoVisible());
            Assert.IsTrue(_login.IsProductsVisible());
            Assert.IsTrue(_login.IsGiftSubscriptionsVisible());
            Assert.IsTrue(_login.IsRedeemGiftVisible());
            Assert.IsTrue(_login.IsDownloadsVisible());
            Assert.IsTrue(_login.IsTutorialsVisible());
            Assert.IsTrue(_login.IsSellYourPhrasesVisible());
        }

        [TestMethod]
        public void TC_LoginToWritersMusePortal_VerfiySlidesViewLoaded()
        {
            //Arrange
            _login.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_login.IsSlidesContainerVisible());
            Assert.IsTrue(_login.IsFreeTrailLogoVisible());
            Assert.IsTrue(_login.IsSpiceLogoVisible());
            Assert.IsTrue(_login.IsSlidesVisible());
        }

        [TestMethod]
        public void TC_LoginToWritersMusePortal_VerifyWatchOurVideoLink()
        {
            //Arrange
            _login.WaitForPageToLoad();

            //Assert
            Assert.IsTrue(_login.IsSeeSpiceProductsLinkVisible());
            Assert.IsTrue(_login.IsWatchOurProductLinkVisible());

            //Act
            _login.SelectSeeSpiceProductsLink();
            Assert.IsTrue(_login.IsSpiceProductsContentVisible());
            _login.NavigateToHomePage();
            _login.SelectWatchOurVideoLink();
            Assert.IsTrue(_login.IsSpiceVideoVisible());
            _login.CloseSpiceVideo();
        }

        [TestMethod]
        public void TC_LoginToWritersMusePortal_VerfiyProductPrices()
        {
            //Arrange
            _login.WaitForPageToLoad();

            //Act
            ProductPrices productPricesFromDB = _login.GetProductpricesFromDB();
            ProductPrices productPricesFromUI = _login.GetProductpricesFromUI();

            //Assert
            Assert.IsTrue(productPricesFromDB.SpiceMobilePerMonthDB.SequenceEqual(productPricesFromUI.SpiceMobilePerMonthUI));
            Assert.IsTrue(productPricesFromDB.SpiceMobilePerYearDB.SequenceEqual(productPricesFromUI.SpiceMobilePerYearUI));
        }

        [TestMethod]
        public void TC_LoginToWritersMusePortal_VerifyChangePassword()
        {
            //Arrange
            _login.WaitForPageToLoad();

            //Act
            string newHashedPassword = _login.ChangeUserPasswordAndGetNewHashedPassword();
            string newHashedPasswordFromDB = _login.GetPasswordFromDB();
            bool isAccountSigInSuccessful = _login.CheckSignInUsingNewPassword();
            _login.ResetPasswordToOldPassword();

            //Assert
            Assert.IsTrue(newHashedPassword.SequenceEqual(newHashedPasswordFromDB));
            Assert.IsTrue(isAccountSigInSuccessful);
        }

        [TestMethod]
        public void TC_OpenSubscribeAndCheckoutPage_VerifyAccountSignInAndPaypalLoginPage()
        {
            //Arrange
            _login.WaitForPageToLoad();
            string usernameOfCreatedAccout = ConfigurationManager.AppSettings["NewUsername"].ToString();
            string signInMessage = $"You have successfully signed in as {usernameOfCreatedAccout}.";

            //Act
            _login.NavigateToSubscribePage();
            _login.CheckHowPayPalWorksImageLink();
            _login.NavigateToCheckOutPage();
            string signInMessageFromUI = _login.GetSuccessfulSignInMessage();
            bool isSignOutButtonVisible = _login.IsSignOutButtonVisible_Subscriptions();
            _login.SelectRandomSubscriptionTypeAndDuration();
            _login.NavigateToPayPalLogInPage();
            bool isPayPalLoginPageVisible = _login.IsPayPalCheckoutPageVisible();
            _login.DeletePayPalInvoiceFromDB();
            _login.NavigateBackToCheckOutPage();

            //Assert
            Assert.IsTrue(isSignOutButtonVisible);
            Assert.IsTrue(signInMessage.SequenceEqual(signInMessageFromUI));
            Assert.IsTrue(isPayPalLoginPageVisible);
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            base.TestCleanup();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Login _login = new Login(_browser);
            _login.SignOutFromWritersMuse();
        }
    }
}
