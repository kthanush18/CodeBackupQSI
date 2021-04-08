using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class AccountTests : TestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            Login _login = new Login(_browser);
            _login.LoginToWritersMuseForUserAccountTesting();
            _login.NavigateToAccountPage();
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _login = new Login(_browser);
        }

        [TestMethod]
        public void TC_OpenAccountPage_VerfiyUserDetailsInLeftPane()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            string usernameFromUI = _login.GetUsernameFromLeftPaneUI();
            string aboutYourSelfFromDB = _login.GetAboutYourSelfTextFromDB();
            string aboutYourSelfFromUI = _login.GetAboutYourSelfTextLeftPaneFromUI();

            //Assert
            Assert.IsTrue(_login.IsUserImageVisible());
            Assert.IsTrue(username.SequenceEqual(usernameFromUI));
            Assert.IsTrue(aboutYourSelfFromDB.SequenceEqual(aboutYourSelfFromDB));
        }

        [TestMethod]
        public void TC_OpenAccountPage_VerfiyPhraseSummary()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            PhraseSummaryDB phraseSummaryFromDB = _login.GetPhrasesSummaryFromDB(username);
            PhraseSummaryUI phrasesSummaryFromUI = _login.GetPhrasesSummaryFromUI();

            //Assert
            Assert.IsTrue(phraseSummaryFromDB.PendingPhrases.SequenceEqual(phrasesSummaryFromUI.PendingPhrases));
            Assert.IsTrue(phraseSummaryFromDB.AcceptedPhrases.SequenceEqual(phrasesSummaryFromUI.AcceptedPhrases));
            Assert.IsTrue(phraseSummaryFromDB.SubmittedPhrases.SequenceEqual(phrasesSummaryFromUI.SubmittedPhrases));
            Assert.IsTrue(phraseSummaryFromDB.TotalEarned.SequenceEqual(phrasesSummaryFromUI.TotalEarned));
            Assert.IsTrue(phraseSummaryFromDB.Balance.SequenceEqual(phrasesSummaryFromUI.Balance));
        }

        [TestMethod]
        public void TC_OpenAccountPage_VerfiyPendingPhrases()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            List<string> pendingPhrasesFromDB = _login.GetPendingPhrasesFromDB(username);
            List<string> pendingPhrasesFromUI = _login.GetPendingPhrasesFromUI();

            //Assert
            Assert.IsTrue(pendingPhrasesFromDB.SequenceEqual(pendingPhrasesFromUI));
        }

        [TestMethod]
        public void TC_OpenAccountPage_VerfiyAcceptedPhrases()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            List<string> acceptedPhrasesFromDB = _login.GetAcceptedPhrasesFromDB(username);
            List<string> acceptedPhrasesFromUI = _login.GetAcceptedPhrasesFromUI();

            //Assert
            Assert.IsTrue(acceptedPhrasesFromDB.SequenceEqual(acceptedPhrasesFromUI));
        }

        [TestMethod]
        public void TC_EditAboutYourselfInformation_VerfiyUpdatedInformation()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            string editedUserInfo = ConfigurationManager.AppSettings["ModifiedUserInfo"].ToString();

            //Act
            _login.EditAboutYourselfText(editedUserInfo);
            string editedAboutYourselfTextFromDB = _login.GetAboutYourselfTextFromDB(username);
            string editedAboutYourselfTextFromUI = _login.GetAboutYourSelfTextLeftPaneFromUI();

            //Assert
            Assert.IsTrue(editedAboutYourselfTextFromDB.SequenceEqual(editedAboutYourselfTextFromUI));
        }

        [TestMethod]
        public void TC_SubmitUserPhoto_VerfiyIfPhotoUploaded()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            string photoUploadedStatus = "System.Byte[]";

            //Act
            _login.UploadPhotoAndSubmit();
            string photoUploadStatusFromDB = _login.GetUploadedPhotoInfoFromDB(username);
            _login.RemovePhotoAndConfirm();

            //Assert
            Assert.IsTrue(photoUploadedStatus.SequenceEqual(photoUploadStatusFromDB));
        }

        [TestMethod]
        public void TC_RemoveUserPhoto_VerfiyIfPhotoRemoved()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            string photoUploadedStatus = "";

            //Act
            _login.UploadPhotoAndSubmit();
            _login.RemovePhotoAndConfirm();
            string photoUploadStatusFromDB = _login.GetUploadedPhotoInfoFromDB(username);

            //Assert
            Assert.IsTrue(photoUploadedStatus.SequenceEqual(photoUploadStatusFromDB));
        }

        [TestMethod]
        public void TC_SelectAddSubscriptionButton_VerfiyRedirectionToSubscriptionsPage()
        {
            //Arrange

            //Act
            _login.SelectAddSubscriptionButtonAndWaitForRedirection();

            //Assert
            Assert.IsTrue(_login.IsProceedButtonVisible());

            //Return to account page
            _login.NavigateToAccountPage();
        }

        [TestMethod]
        public void TC_CheckManageSubscriptions_VerfiySubscriptionsAndDuration()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            _login.CreateRandomSubscriptionForUser(username);
            SubscriptionTypeAndDuration subscriptionInfoFromDB = _login.GetSubscriptionInfoFromDB(username);
            SubscriptionTypeAndDuration subscriptionInfoFromUI = _login.GetSubscriptionInfoFromUI();
            _login.DeleteSubscriptionForUser(username);

            //Assert
            Assert.IsTrue(subscriptionInfoFromDB.SubscriptionTypeFromDB.SequenceEqual(subscriptionInfoFromUI.SubscriptionTypeFromUI));
            Assert.IsTrue(subscriptionInfoFromDB.SubscriptionDurationFromDB.SequenceEqual(subscriptionInfoFromUI.SubscriptionDurationFromUI));
        }

        [TestMethod]
        public void TC_AddDevicesForSubscription_VerfiyDevicesList()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            _login.CreateRandomSubscriptionForUser(username);
            _login.AddDevicesToSubscription(username);
            _login.RemoveDeviceFromActiveSubscription();
            List<string> devicesListFromDB = _login.GetSubscriptionDevicesFromDB(username);
            List<string> devicesListFromUI = _login.GetSubscriptionDevicesFromUI();
            _login.DeleteDevicesForSubscriptionFromDB(username);
            _login.DeleteSubscriptionForUser(username);

            //Assert
            Assert.IsTrue(devicesListFromDB.SequenceEqual(devicesListFromUI));
        }

        [TestMethod]
        public void TC_EditAccountDetailsAndSubmit_VerfiyUpdatedDetails()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            _login.EditAccountDetails();
            _login.SaveDetailsAndConfirm();
            AccountDetailsDB editedAccountDetailsFromDB = _login.GetAccountDetailsFromDB(username);
            AccountDetailsUI editedAccountDetailsFromUI = _login.GetAccountDetailsFromUI();
            _login.ResetAccountDetailsWithPreviousValuesDB(username);

            //Assert
            Assert.IsTrue(editedAccountDetailsFromDB.FirstName.SequenceEqual(editedAccountDetailsFromUI.FirstName));
            Assert.IsTrue(editedAccountDetailsFromDB.LastName.SequenceEqual(editedAccountDetailsFromUI.LastName));
            Assert.IsTrue(editedAccountDetailsFromDB.Email.SequenceEqual(editedAccountDetailsFromUI.Email));
            Assert.IsTrue(editedAccountDetailsFromDB.Phone.SequenceEqual(editedAccountDetailsFromUI.Phone));
        }

        [TestMethod]
        public void TC_EditAccountDetailsAndCancel_VerfiyUpdatedDetails()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            _login.EditAccountDetails();
            _login.CancelChanges();
            AccountDetailsDB editedAccountDetailsFromDB = _login.GetAccountDetailsFromDB(username);
            AccountDetailsUI editedAccountDetailsFromUI = _login.GetAccountDetailsFromUI();

            //Assert
            Assert.IsTrue(editedAccountDetailsFromDB.FirstName.SequenceEqual(editedAccountDetailsFromUI.FirstName));
            Assert.IsTrue(editedAccountDetailsFromDB.LastName.SequenceEqual(editedAccountDetailsFromUI.LastName));
            Assert.IsTrue(editedAccountDetailsFromDB.Email.SequenceEqual(editedAccountDetailsFromUI.Email));
            Assert.IsTrue(editedAccountDetailsFromDB.Phone.SequenceEqual(editedAccountDetailsFromUI.Phone));
        }

        [TestMethod]
        public void TC_EnterNewAddressForCheckPayment_VerfiyPaymentStatus()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            string confirmationMessage = "Payment Request initiated.";

            //Act
            _login.DeletePreviousRequestsForUser(username);
            _login.SelectNewAddress();
            string confirmMessageFromUI = _login.EnterNewAddressAndProceed();
            _login.DeletePreviousRequestsForUser(username);

            //Assert
            Assert.IsTrue(confirmationMessage.SequenceEqual(confirmMessageFromUI));
        }

        [TestMethod]
        public void TC_ProceedWithOldAddressForCheckPayment_VerfiyPaymentStatus()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            string confirmationMessage = "Payment Request initiated.";

            //Act
            _login.DeletePreviousRequestsForUser(username);
            _login.SelectOldAddress();
            string confirmMessageFromUI = _login.ProceedWithAddressOnFile();
            _login.DeletePreviousRequestsForUser(username);

            //Assert
            Assert.IsTrue(confirmationMessage.SequenceEqual(confirmMessageFromUI));
        }

        [TestMethod]
        public void TC_RequestPaymentTransferViaPayPal_VerfiyPaymentStatus()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            string confirmationMessage = "Payment Request initiated.";

            //Act
            _login.DeletePreviousRequestsForUser(username);
            string payPalEmailID = _login.SelectPayPalPaymentAndEnterPayPalEmailID();
            string confirmMessageFromUI = _login.ConfirmPayPalTransfer();
            string payPalEmailFromDB = _login.GetPayPalEmailFromDB(username);
            _login.DeletePreviousRequestsForUser(username);

            //Assert
            Assert.IsTrue(confirmationMessage.SequenceEqual(confirmMessageFromUI));
            Assert.IsTrue(payPalEmailID.SequenceEqual(payPalEmailFromDB));
        }

        [TestMethod]
        public void TC_EnterRandomSourceForPhraseAndSubmit_VerfiySubmittedPhraseDetails()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();

            //Act
            _login.NavigateToPhraseSubmissionsPage();
            _login.AcceptTermsAndConditions();
            string phraseTextFromPhraseSubmisions = _login.EnterNewPhraseInPhraseSubmissionsBox();
            int sourceCategoryCode = _login.GetRandomSourceCategoryCode();
            int sourceSubCategoryCode = _login.GetRandomSourceSubCategory(sourceCategoryCode);
            _login.SelectCategoryAndSubCategory(sourceCategoryCode, sourceSubCategoryCode);
            _login.FillAllTheFieldsAndSubmit(sourceCategoryCode,sourceSubCategoryCode);
            List<string> submittedSourceDetailsUI = _login.GetDataEnteredFromReviewScreen(sourceCategoryCode, sourceSubCategoryCode);
            _login.NavigateToSuccessfulSubmissionBoxAndSelectNoButton();
            List<string> submittedSourceDetailsDB = _login.GetSourceDetailsFromDB(username);
            string phraseTextFromAccountPage = _login.GetPhraseTextFromAccountPage();
            _login.DeleteSubmittedPhraseFromDB(username);

            //Assert
            Assert.IsTrue(submittedSourceDetailsDB.SequenceEqual(submittedSourceDetailsUI));
            Assert.IsTrue(phraseTextFromAccountPage.SequenceEqual(phraseTextFromPhraseSubmisions));
        }

        [TestMethod]
        public void TC_EnterMultiplePhrasesAndSubmit_VerfiySubmittedPhraseDetails()
        {
            //Arrange
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            int phrasesCount = 4;

            //Act
            _login.NavigateToPhraseSubmissionsPage();
            _login.AcceptTermsAndConditions();
            string phraseTextFromPhraseSubmisions = _login.EnterMultiplePhrasesOfAllFormatSpecifications();
            int sourceCategoryCode = _login.GetRandomSourceCategoryCode();
            int sourceSubCategoryCode = _login.GetRandomSourceSubCategory(sourceCategoryCode);
            _login.SelectCategoryAndSubCategory(sourceCategoryCode, sourceSubCategoryCode);
            _login.FillAllTheFieldsAndSubmit(sourceCategoryCode, sourceSubCategoryCode);
            _login.SubmitFromReviewPage();
            _login.NavigateToSuccessfulSubmissionBoxAndSelectNoButton();
            List<string> multipleSubmittedPhrasesFromDB = _login.GetSubmittedMultiplePhrasesFromDB(username,phrasesCount);
            List<string> multipleSubmittedPhrasesFromUI = _login.GetSubmittedMultiplePhrasesFromAccountPage(phrasesCount);
            _login.DeleteMultipleSubmittedPhrasesFromDB(username,phrasesCount);

            //Assert
            Assert.IsTrue(multipleSubmittedPhrasesFromDB.SequenceEqual(multipleSubmittedPhrasesFromUI));
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
