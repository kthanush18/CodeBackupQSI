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
    public class LogoutTests : TestBase
    {
        protected static WebPage _page;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _logout = new Logout(_browser);
        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_WaitTillThePageGetsLoaded()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_logout.IsMainContainerVisible());
        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_VerfiyTopRightLinksLoaded()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_logout.IsSigninLinkVisible());
            Assert.IsTrue(_logout.IsSignupLinkVisible());
            Assert.IsTrue(_logout.IsTopRightSubscribeLinkVisible());
        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_VerfiyHeaderLinksLoaded()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_logout.IsHomeLinkVisible());
            Assert.IsTrue(_logout.IsHeaderSubscribeLinkVisible());
            Assert.IsTrue(_logout.IsContactLinkVisible());
            Assert.IsTrue(_logout.IsHelpLinkVisible());
            Assert.IsTrue(_logout.IsFacebookLogoVisible());
            Assert.IsTrue(_logout.IsTwitterlogoVisible());
        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_VerfiyLeftRibbonLinksLoaded()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_logout.IsWebsiteLogoVisible());
            Assert.IsTrue(_logout.IsProductsVisible());
            Assert.IsTrue(_logout.IsGiftSubscriptionsVisible());
            Assert.IsTrue(_logout.IsRedeemGiftVisible());
            Assert.IsTrue(_logout.IsDownloadsVisible());
            Assert.IsTrue(_logout.IsTutorialsVisible());
            Assert.IsTrue(_logout.IsSellYourPhrasesVisible());
        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_VerfiySlidesViewLoaded()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_logout.IsSlidesContainerVisible());
            Assert.IsTrue(_logout.IsFreeTrailLogoVisible());
            Assert.IsTrue(_logout.IsSpiceLogoVisible());
            Assert.IsTrue(_logout.IsSlidesVisible());
        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_VerfiyFooterLinksLoaded()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Assert
            Assert.IsTrue(_logout.IsProductActivationLinkVisible());
            Assert.IsTrue(_logout.IsTermsOfUseLinkVisible());
            Assert.IsTrue(_logout.IsPrivacyPolicyLinkVisible());

            //Act
            _logout.SelectProductActivationLink();
            Assert.IsTrue(_logout.IsProductActivationContentVisible());
            _logout.SelectTermsOfUseLink();
            Assert.IsTrue(_logout.IsTermsOfUseContentVisible());
            _logout.SelectPrivacyPolicyLink();
            Assert.IsTrue(_logout.IsPrivacyPolicyContentVisible());

        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_VerifyWatchOurVideoLink()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Assert
            Assert.IsTrue(_logout.IsSeeSpiceProductsLinkVisible());
            Assert.IsTrue(_logout.IsWatchOurProductLinkVisible());

            //Act
            _logout.SelectSeeSpiceProductsLink();
            Assert.IsTrue(_logout.IsSpiceProductsContentVisible());
            _logout.NavigateToHomePage();
            _logout.SelectWatchOurVideoLink();
            Assert.IsTrue(_logout.IsSpiceVideoVisible());
        }

        [TestMethod]
        public void TC_LaunchWritersMusePortal_VerfiyProductPrices()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act
            ProductPrices productPricesFromDB = _logout.GetProductpricesFromDB();
            ProductPrices productPricesFromUI = _logout.GetProductpricesFromUI();

            //Assert
            Assert.IsTrue(productPricesFromDB.SpiceMobilePerMonthDB.SequenceEqual(productPricesFromUI.SpiceMobilePerMonthUI));
            Assert.IsTrue(productPricesFromDB.SpiceMobilePerYearDB.SequenceEqual(productPricesFromUI.SpiceMobilePerYearUI));
        }

        [TestMethod]
        public void TC_CreateAnUserAccount_VerifyCreatedAccount()
        {
            //Arrange
            _logout.WaitForPageToLoad();
            string accountCreationMessage = "Account created successfully!";
            string usernameOfCreatedAccout = ConfigurationManager.AppSettings["NewUsername"].ToString();

            //Act
            _logout.ClickSignUPLinkAndWaitForFieldsToLoad();
            _logout.CheckForUsernameAvailabilityAndDeleteExistingAccount();
            _logout.FillAllTheRequiredFieldsAndCreateAccount();
            string accountCreationMessageFromUI = _logout.GetAccountCreationHeaderText();
            _logout.ActivateCreatedAccount();
            _logout.LoginToWritersMuse();
            string UsernameFromUI = _logout.GetUsernameAfterLogin();

            //Assert
            Assert.IsTrue(accountCreationMessage.SequenceEqual(accountCreationMessageFromUI));
            Assert.IsTrue(usernameOfCreatedAccout.SequenceEqual(UsernameFromUI));
        }

        [TestMethod]
        public void TC_OpenCreateAccountFromMemberSignIn_VerifyCreateAccountFieldsLoaded()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act
            _logout.NavigateToMemberSignIn();
            _logout.NavigateToCreateAccountFromMemberSignIn();

            //Assert
            Assert.IsTrue(_logout.IsCreateAccountFieldsVisible());
        }

        [TestMethod]
        public void TC_OpenForgotMyPassword_VerifyPasswordIsReset()
        {
            //Arrange
            _logout.WaitForPageToLoad();
            string forgotPasswordText = "We have sent your login information.\r\nPlease check your email shortly.";

            //Act
            _logout.NavigateToMemberSignIn();
            _logout.NavigateToForgotMyPassword();
            ResetPasswordFields passwordFields = _logout.GetResetpasswordFieldsDataFromDB();
            string forgotPasswordMessageFromUI = _logout.GetForgotPasswordMessageAfterSubmitOfRequiredFields(passwordFields);
            _logout.ResetPasswordToOldPassword();

            //Assert
            Assert.IsTrue(forgotPasswordText.SequenceEqual(forgotPasswordMessageFromUI));
        }

        [TestMethod]
        public void TC_OpenMyPasswordDoesNotWork_VerifyPasswordIsReset()
        {
            //Arrange
            _logout.WaitForPageToLoad();
            string forgotPasswordText = "We have sent your login information.\r\nPlease check your email shortly.";

            //Act
            _logout.NavigateToMemberSignIn();
            _logout.NavigateToMyPasswordDoesNotWork();
            ResetPasswordFields passwordFields = _logout.GetResetpasswordFieldsDataFromDB();
            string forgotPasswordMessageFromUI = _logout.GetForgotPasswordMessageAfterSubmitOfRequiredFields(passwordFields);
            _logout.ResetPasswordToOldPassword();

            //Assert
            Assert.IsTrue(forgotPasswordText.SequenceEqual(forgotPasswordMessageFromUI));
        }

        [TestMethod]
        public void TC_OpenForgotMyUsername_VerifyMessageAsLoginInformationSent()
        {
            //Arrange
            _logout.WaitForPageToLoad();
            string loginInformationText = "We have sent your login information.\r\nPlease check your email shortly.";

            //Act
            _logout.NavigateToMemberSignIn();
            _logout.NavigateToForgotMyUserName();
            ResetPasswordFields passwordFields = _logout.GetResetpasswordFieldsDataFromDB();
            string loginInformationTextFromUI = _logout.GetForgotPasswordMessageAfterSubmitOfRequiredFields(passwordFields);

            //Assert
            Assert.IsTrue(loginInformationText.SequenceEqual(loginInformationTextFromUI));
        }

        [TestMethod]
        public void TC_OpenSubscribeAndCheckoutPage_VerifyAccountSignInAndPaypalLoginPage()
        {
            //Arrange
            _logout.WaitForPageToLoad();
            string accountCreationMessage = "Account created successfully!";
            string usernameOfCreatedAccout = ConfigurationManager.AppSettings["NewUsername"].ToString();
            string signInMessage = $"You have successfully signed in as {usernameOfCreatedAccout}.";

            //Act
            _logout.NavigateToSubscribePage();
            _logout.CheckHowPayPalWorksImageLink();
            _logout.NavigateToCheckOutPage();
            _logout.CheckForUsernameAvailabilityAndDeleteExistingAccount();
            _logout.CreateNewUserAccountFromCheckoutPage();
            _logout.ProceedToCreateAccount();
            string accountCreationMessageFromUI = _logout.GetAccountCreationHeaderText();
            _logout.ActivateCreatedAccount();
            _logout.LoginToWritersMuseFromCheckoutPage();
            string signInMessageFromUI = _logout.GetSuccessfulSignInMessage();
            _logout.SelectRandomSubscriptionTypeAndDuration();
            _logout.NavigateToPayPalLogInPage();
            _logout.DeletePayPalInvoiceFromDB();

            //Assert
            Assert.IsTrue(accountCreationMessage.SequenceEqual(accountCreationMessageFromUI));
            Assert.IsTrue(signInMessage.SequenceEqual(signInMessageFromUI));
            Assert.IsTrue(_logout.IsPayPalCheckoutPageVisible());
        }

        [TestMethod]
        public void TC_OpenGiftSubscriptionsPage_VerifyGiftSubscriptions()
        {
            //Arrange
            _logout.WaitForPageToLoad();

            //Act
            _logout.NavigateToGiftSubscriptionPage();
            _logout.CheckHowPayPalWorksImageLink();
            _logout.NavigateToCheckOutPage();
            ProductPrices spiceProductPricesFromDB = _logout.GetAllProductPricesGiftAndNormalFromDB();
            ProductPrices spiceProductPricesFromUI = _logout.GetAllProductPricesGiftAndNormalFromUI();
            int numberOfProductsSelected = _logout.SelectRandomSubscriptionTypeAndNumberOfProducts();
            int numberOfRecipientFieldsDisplayed = _logout.GetNumberOfRecipientFieldsDisplayed();
            _logout.EnterUserAndRecipientDetails(numberOfRecipientFieldsDisplayed);
            _logout.NavigateToPayPalLogInPage_GiftSubscriptions();

            //Assert
            Assert.IsTrue(spiceProductPricesFromDB.SpiceMobilePerYearDB.SequenceEqual(spiceProductPricesFromUI.SpiceMobilePerYearUI));
            Assert.IsTrue(spiceProductPricesFromDB.SpiceMobilePerYearDB_Gift.SequenceEqual(spiceProductPricesFromUI.SpiceMobilePerYearUI_Gift));
            Assert.IsTrue(spiceProductPricesFromDB.SpiceProfessionalPerYearDB.SequenceEqual(spiceProductPricesFromUI.SpiceProfessionalPerYearUI));
            Assert.IsTrue(spiceProductPricesFromDB.SpiceProfessionalPerYearDB_Gift.SequenceEqual(spiceProductPricesFromUI.SpiceProfessionalPerYearUI_Gift));
            Assert.IsTrue(numberOfProductsSelected.Equals(numberOfRecipientFieldsDisplayed));
            Assert.IsTrue(_logout.IsPayPalCheckoutPageVisible());
        }

        [TestMethod]
        public void TC_OpenRedeemGiftPage_VerifyActivationOfGiftSubscription()
        {
            //Arrange
            _logout.WaitForPageToLoad();
            string giftRedeemMessage = "Account and Gift Subscription created successfully!";

            //Act
            _logout.NavigateToRedeemGiftPage();
            string subscriptionType = _logout.GetRandomSubscriptionTypeSelected();
            bool isSignInButtonVisible = _logout.IsSignInButtonVisible_Checkout();
            _logout.SelectSignInButton_CheckOut();
            bool isMemberSignInVisible_SignInButton = _logout.CheckMemberSignInBox();
            bool isSignInLinkVisible = _logout.IsSignInButtonVisible_Checkout();
            _logout.SelectSignInLink_CheckOut();
            bool isMemberSignInVisible_SignInLink = _logout.CheckMemberSignInBox();
            _logout.CheckForUsernameAvailabilityAndDeleteExistingAccount();
            _logout.CreateNewUserAccountFromCheckoutPage();
            string giftCode = _logout.GetValidGiftCodeForSubscriptionType(subscriptionType);
            string giftRedeemMessageFromUI = _logout.EnterGiftCodeAndRedeem(giftCode);
            _logout.DeleteRedeemedSubscription(giftCode);

            //Assert
            Assert.IsTrue(giftRedeemMessage.SequenceEqual(giftRedeemMessageFromUI));
            Assert.IsTrue(isSignInButtonVisible);
            Assert.IsTrue(isMemberSignInVisible_SignInButton);
            Assert.IsTrue(isSignInLinkVisible);
            Assert.IsTrue(isMemberSignInVisible_SignInLink);
        }

        [TestMethod]
        public void TC_EnterDetailsInContactUsPageAndSubmit_VerfiySubmittedContactsDetails()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            _logout.NavigateToContactUsPage();
            List<string> contactUsInfoFromUI = _logout.SelectContactPreferenceAndEnterAllDetails();
            List<string> contactUsInfoFromDB = _logout.GetContactUSDetailsFromDB();
            _logout.DeleteRecentlyAddedContactUsEntry();

            //Assert
            Assert.IsTrue(contactUsInfoFromDB.SequenceEqual(contactUsInfoFromUI));
        }

        [TestMethod]
        public void TC_EnterYourThoughtsAndSubmit_VerfiySubmittedThoughts()
        {

            //Arrange

            //Act
            _logout.NavigateToContactUsPage();
            List<string> thoughtsFromUI = _logout.SelectAllThoughtsAndSubmit();
            List<string> thoughtsFromDB = _logout.GetThoughtsNewlySubmittedFromDB();
            _logout.DeleteRecentlyAddedThoughtsEntry();

            //Assert
            Assert.IsTrue(thoughtsFromUI.SequenceEqual(thoughtsFromDB));
        }

        [TestMethod]
        public void TC_NavigateToPhraseSubmissionsInfoPage_VerfiyHeadersAndTextLoaded()
        {
            //Arrange

            //Act
            _logout.NavigateToPhraseSubmissionsPage();

            //Assert
            Assert.IsTrue(_logout.IsPhraseSubmissionsHeaderVisible());
            Assert.IsTrue(_logout.IsStandardsOfSubmissionsHeaderVisible());
            Assert.IsTrue(_logout.IsFAQForSubmissionsHeaderVisible());
            Assert.IsTrue(_logout.IsPhraseSubmissionsContainerVisible());
            Assert.IsTrue(_logout.IsStandardsOfSubmissionsContainerVisible());
            Assert.IsTrue(_logout.IsFAQForSubmissionsContainerVisible());
            Assert.IsTrue(_logout.IsProceedButtonVisible_PhraseSubmissions());
        }

        [TestMethod]
        public void TC_NavigateToPhraseSubmissionsStepsPage_VerfiyHeadersAndTextLoaded()
        {
            //Arrange

            //Act
            _logout.NavigateToPhraseSubmissionsPage();
            _logout.NavigateToPhraseSubmissionsStepsPage();
            string currentPhraseRateFromUI = _logout.GetCurrentRateForPhraseUI();
            string currentPhraseRateFromDB = _logout.GetCurrentPhraseRateFromDB();

            //Assert
            Assert.IsTrue(_logout.IsPhraseSubmissionsHeaderVisible());
            Assert.IsTrue(_logout.IsPhraseSubmissionsContentVisible());
            Assert.IsTrue(currentPhraseRateFromUI.SequenceEqual(currentPhraseRateFromDB));
            Assert.IsTrue(_logout.IsSigninButtonVisible_PhraseSubmissions());
            Assert.IsTrue(_logout.IsCreateAccountButtonVisible_PhraseSubmissions());
        }

        [TestMethod]
        public void TC_NavigateToMemberSigninFromPhraseSubmissions_VerfiySigninWindowIsLoaded()
        {
            //Arrange

            //Act
            _logout.NavigateToPhraseSubmissionsPage();
            _logout.NavigateToPhraseSubmissionsStepsPage();
            _logout.NavigateToMemberSiginFromPhraseSubmissions();

            //Assert
            Assert.IsTrue(_logout.IsUsernameTextBoxVisible());
            Assert.IsTrue(_logout.IsPasswordTextBoxVisible());
        }

        [TestMethod]
        public void TC_NavigateToCreateAccountFromPhraseSubmissions_VerfiySigninWindowIsLoaded()
        {
            //Arrange

            //Act
            _logout.NavigateToPhraseSubmissionsPage();
            _logout.NavigateToPhraseSubmissionsStepsPage();
            _logout.NavigateToCreateAccountFromPhraseSubmissions();

            //Assert
            Assert.IsTrue(_logout.IsCreateAccountFieldsVisible());
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'

            base.TestCleanup();
        }
    }
}
