using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Quant.Spice.Security;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WritersMuse.DataAccess;
using Quant.Spice.Test.UI.Web.WritersMuse.Models;
using Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Quant.Spice.Test.UI.Web.WritersMuse.Pages.Logout;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Pages
{
    public class Login : WebPage 
    {
        protected static WritersMuseDataAccess _dataAccess;
        private readonly string _username = ConfigurationManager.AppSettings["Username"].ToString();
        private string _password = ConfigurationManager.AppSettings["Password"].ToString();
        readonly int _preloaderWait = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"].ToString());
        Random _random = new Random();

        public Login(WebBrowser browser) : base(browser)
        {

        }
        public enum PhraseStatusCodes
        {
            UnassignedPendingPhrases = 1,
            AssignedPendingPhrases = 2,
            AcceptedPhrases = 3,
            RejectedPhrases = 4
        }
        public enum SourceCategory
        {
            Book = 1,
            Periodical = 2,
            Other = 3,
            Unknown = 4
        }
        public enum BookSubCategory
        {
            Fiction = 1,
            NonFiction = 2,
            WorkInAnthology = 3,
            ReferencedQuotation = 4
        }
        public enum PeriodicalSubCategory
        {
            Magazine = 1,
            Newspaper = 2,
            Journal = 3
        }
        public enum OtherSubCategory
        {
            Television = 1,
            Film = 2,
            Play = 3,
            Lyric = 4,
            Speech = 5,
            Poetry = 6
        }
        public bool WaitForPageToLoad()
        {
            return _browser.WaitForElement("container", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSigninLinkVisible()
        {
            return _browser.IsElementVisible("lnkSignIn", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSigninLink()
        {
            return _browser.GetElement("lnkSignIn", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsUsernameTextBoxVisible()
        {
            return _browser.IsElementVisible("txtLoginUserName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUsernameTextBox()
        {
            return _browser.GetElement("txtLoginUserName", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsPasswordTextBoxVisible()
        {
            return _browser.IsElementVisible("txtLoginPassword", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPasswordTextBox()
        {
            return _browser.GetElement("txtLoginPassword", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSignInButtonVisible()
        {
            return _browser.IsElementVisible("memberLogin_LoginButton", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSignInButton()
        {
            return _browser.GetElement("memberLogin_LoginButton", WebBrowser.ElementSelectorType.ID);
        }
        public void LoginToWritersMuse()
        {
            
            WaitForPageToLoad();
            IsSigninLinkVisible();
            GetSigninLink().Click();
            _browser.SwitchtoCurrentWindow();
            IsUsernameTextBoxVisible();
            GetUsernameTextBox().SendKeys(_username);
            IsPasswordTextBoxVisible();
            GetPasswordTextBox().SendKeys(_password);
            IsSignInButtonVisible();
            GetSignInButton().Click();
            Thread.Sleep(_preloaderWait);
            _browser.SwitchtoPreviousWindow();
        }
        public void LoginToWritersMuseForUserAccountTesting()
        {
            string userName = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            
            WaitForPageToLoad();
            IsSigninLinkVisible();
            GetSigninLink().Click();
            _browser.SwitchtoCurrentWindow();
            IsUsernameTextBoxVisible();
            GetUsernameTextBox().SendKeys(userName);
            IsPasswordTextBoxVisible();
            GetPasswordTextBox().SendKeys(_password);
            IsSignInButtonVisible();
            GetSignInButton().Click();
            Thread.Sleep(_preloaderWait);
            _browser.SwitchtoPreviousWindow();
        }
        public bool IsSignOutLinkVisible()
        {
            return _browser.IsElementVisible("lnkSignOut", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsAccountLinkVisible()
        {
            return _browser.IsElementVisible("lnkAccount", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsChangePasswordLinkVisible()
        {
            return _browser.IsElementVisible("lnkChangePassword", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSubscribeLinkVisible()
        {
            return _browser.IsElementVisible("lnkLoggedInSubscribe", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSignOutLink()
        {
            return _browser.GetElement("lnkSignOut", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForSigninLink()
        {
            return _browser.WaitForElement("lnkSignIn", WebBrowser.ElementSelectorType.ID);
        }
        public void SignOutFromWritersMuse()
        {
            GetSignOutLink().Click();
            WaitForSigninLink();
        }
        public bool IsHomeLinkVisible()
        {
            return _browser.IsElementVisible("lnkHome", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsHeaderSubscribeLinkVisible()
        {
            return _browser.IsElementVisible("lnkSubscribe", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsContactLinkVisible()
        {
            return _browser.IsElementVisible("lnkContact", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsHelpLinkVisible()
        {
            return _browser.IsElementVisible("lnkHelp", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsFacebookLogoVisible()
        {
            return _browser.IsElementVisible("imgFacebook", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsTwitterlogoVisible()
        {
            return _browser.IsElementVisible("imgTwitter", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsWebsiteLogoVisible()
        {
            return _browser.IsElementVisible("LeftBanner_imgBookmarkTop", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsProductsVisible()
        {
            return _browser.IsElementVisible("LeftBanner_lnkProducts", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsGiftSubscriptionsVisible()
        {
            return _browser.IsElementVisible("LeftBanner_lnkProducts", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsRedeemGiftVisible()
        {
            return _browser.IsElementVisible("LeftBanner_lnkRedeemGift", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsDownloadsVisible()
        {
            return _browser.IsElementVisible("LeftBanner_lnkDownloads", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsTutorialsVisible()
        {
            return _browser.IsElementVisible("LeftBanner_lnkTutorials", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSellYourPhrasesVisible()
        {
            return _browser.IsElementVisible("LeftBanner_sellypurphrases", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSlidesContainerVisible()
        {
            return _browser.IsElementVisible("JSTop", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsFreeTrailLogoVisible()
        {
            return _browser.IsElementVisible("imgFreeTrial", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSpiceLogoVisible()
        {
            return _browser.IsElementVisible("SpiceLogo", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSlidesVisible()
        {
            return _browser.IsElementVisible("Sliders", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSeeSpiceProductsLinkVisible()
        {
            return _browser.IsElementVisible("divSubscribeNowButtonImage", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSpiceProductsContentVisible()
        {
            return _browser.IsElementVisible("tblProductInfo", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSpiceVideoVisible()
        {
            return _browser.IsElementVisible("sb-container", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSeeSpiceProductsLink()
        {
            return _browser.GetElement("divSubscribeNowButtonImage", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetWatchOurVideoLink()
        {
            return _browser.GetElement("divTakeOurTourImageButton", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForWatchOurVideoLinkToLoad()
        {
            return _browser.WaitForElement("divTakeOurTourImageButton", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForSpiceVideoToLoad()
        {
            return _browser.WaitForElement("sb-container", WebBrowser.ElementSelectorType.ID);
        }
        public void SelectSeeSpiceProductsLink()
        {
            GetSeeSpiceProductsLink().Click();
        }
        public void SelectWatchOurVideoLink()
        {
            WaitForWatchOurVideoLinkToLoad();
            GetWatchOurVideoLink().Click();
            _browser.SwitchtoCurrentWindow();
            WaitForSpiceVideoToLoad();
        }
        public IWebElement GetHomeButton()
        {
            return _browser.GetElement("lnkHome", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToHomePage()
        {
            GetHomeButton().Click();
        }
        public bool IsWatchOurProductLinkVisible()
        {
            return _browser.IsElementVisible("divTakeOurTourImageButton", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSpiceVideoCloseButton()
        {
            return _browser.GetElement("sb-nav-close", WebBrowser.ElementSelectorType.ID);
        }
        public void CloseSpiceVideo()
        {
            
            Thread.Sleep(_preloaderWait);
            GetSpiceVideoCloseButton().Click();
            Thread.Sleep(_preloaderWait);
        }
        public ProductPrices GetProductpricesFromDB()
        {
            ProductPrices productPricesFromDB = new ProductPrices();
            int productCode = (int)ProductCodes.SpiceMobile;
            int montlyDuration = (int)SubscriptionDuration.Monthly;
            int yearlyDuration = (int)SubscriptionDuration.Yearly;
            bool isGiftProduct = false;
            _dataAccess = new WritersMuseDataAccess();
            productPricesFromDB = _dataAccess.GetProductPricesForSpiceMobile(productCode, montlyDuration, yearlyDuration, isGiftProduct);
            productPricesFromDB.SpiceMobilePerMonthDB = $"only ${productPricesFromDB.SpiceMobilePerMonthDB}/ month";
            productPricesFromDB.SpiceMobilePerYearDB = $"or ${productPricesFromDB.SpiceMobilePerYearDB}* / year";
            return productPricesFromDB;
        }
        public IWebElement GetProductPricePerMonthUI()
        {
            return _browser.GetElement("MainContent_spnProductPriceMonth", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetProductPricePerYearUI()
        {
            return _browser.GetElement("MainContent_spnProductPriceYear", WebBrowser.ElementSelectorType.ID);
        }
        public ProductPrices GetProductpricesFromUI()
        {
            ProductPrices productPricesFromUI = new ProductPrices
            {
                SpiceMobilePerMonthUI = GetProductPricePerMonthUI().Text,
                SpiceMobilePerYearUI = GetProductPricePerYearUI().Text
            };
            return productPricesFromUI;
        }
        public IWebElement GetChangePasswordLink()
        {
            return _browser.GetElement("lnkChangePassword", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCurrentPasswordTextBox()
        {
            return _browser.GetElement("MainContent_txtOldPassword", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetNewPasswordTextBox()
        {
            return _browser.GetElement("MainContent_txtNewPassword", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetConfirmNewPasswordTextBox()
        {
            return _browser.GetElement("MainContent_txtChangeConfirmPassword", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmitButton_ChangePassword()
        {
            return _browser.GetElement("MainContent_imgChangePasswordSubmit", WebBrowser.ElementSelectorType.ID);
        }
        public string ChangeUserPasswordAndGetNewHashedPassword()
        {
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            string newPassword = ConfigurationManager.AppSettings["NewPassword"].ToString();
            string newConfirmPassword = ConfigurationManager.AppSettings["NewConfirmPassword"].ToString();
            GetChangePasswordLink().Click();
            GetCurrentPasswordTextBox().SendKeys(password);
            GetNewPasswordTextBox().SendKeys(newPassword);
            GetConfirmNewPasswordTextBox().SendKeys(newConfirmPassword);
            GetSubmitButton_ChangePassword().Click();
            GetSignOutLink().Click();
            Encryption encryption = new Encryption();
            return encryption.GetSHA1EncryptedString(newPassword);
        }
        public string GetPasswordFromDB()
        {
            string userName = ConfigurationManager.AppSettings["Username"].ToString();
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetCurrentPasswordOfUser(userName);
        }
        public void ResetPasswordToOldPassword()
        {
            string userName = ConfigurationManager.AppSettings["Username"].ToString();
            string oldPassword = ConfigurationManager.AppSettings["Password"].ToString();
            Encryption encryption = new Encryption();
            string hashedOldPassword = encryption.GetSHA1EncryptedString(oldPassword);
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.ChangePasswordInDB(hashedOldPassword, userName);
        }
        public bool CheckSignInUsingNewPassword()
        {
            string newPassword = ConfigurationManager.AppSettings["NewPassword"].ToString();
            _password = newPassword;
            LoginToWritersMuse();
            return IsSignOutLinkVisible();
        }
        public IWebElement GetAccountLinkFromHeader()
        {
            return _browser.GetElement("lnkAccount", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToAccountPage()
        {
            GetAccountLinkFromHeader().Click();
        }
        public bool IsUserImageVisible()
        {
            return _browser.IsElementVisible("MainContent_imgPhoto", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUsernameFromLeftPane()
        {
            return _browser.GetElement("lblFirstname", WebBrowser.ElementSelectorType.ID);
        }
        public string GetUsernameFromLeftPaneUI()
        {
            return GetUsernameFromLeftPane().GetAttribute("title");
        }
        public string GetAboutYourSelfTextFromDB()
        {
            string username = ConfigurationManager.AppSettings["UsernameForAccountTesting"].ToString();
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetAboutYourselfTextOfUser(username);
        }
        public IWebElement GetAboutYourSelfTextLeftPane()
        {
            return _browser.GetElement("lblAboutUser", WebBrowser.ElementSelectorType.ID);
        }
        public string GetAboutYourSelfTextLeftPaneFromUI()
        {
            return GetAboutYourSelfTextLeftPane().Text;
        }
        public PhraseSummaryDB GetPhrasesSummaryFromDB(string username)
        {
            int unassignedPendingPhrases = (int)PhraseStatusCodes.UnassignedPendingPhrases;
            int assignedPendingPhrases = (int)PhraseStatusCodes.AssignedPendingPhrases;
            int acceptedPhrases = (int)PhraseStatusCodes.AcceptedPhrases;
            int rejectedPhrases = (int)PhraseStatusCodes.RejectedPhrases;

            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetPhrasesSummaryFromDB(username,unassignedPendingPhrases,assignedPendingPhrases,acceptedPhrases,rejectedPhrases);
        }
        public IWebElement GetPendingPhrasesSummary()
        {
            return _browser.GetElement("MainContent_lblPrasesPending", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAcceptedPhrasesSummary()
        {
            return _browser.GetElement("MainContent_lblPrasesAccepted", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmittedPhrasesSummary()
        {
            return _browser.GetElement("MainContent_lblPrasesSubmitted", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetTotalEarned_LeftPane()
        {
            return _browser.GetElement("MainContent_lblTotalEarned", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetBalance_LeftPane()
        {
            return _browser.GetElement("MainContent_lblBalance", WebBrowser.ElementSelectorType.ID);
        }
        public PhraseSummaryUI GetPhrasesSummaryFromUI()
        {
            PhraseSummaryUI phraseSummary = new PhraseSummaryUI
            {
                PendingPhrases = GetPendingPhrasesSummary().Text,
                AcceptedPhrases = GetAcceptedPhrasesSummary().Text,
                SubmittedPhrases = GetSubmittedPhrasesSummary().Text,
                TotalEarned = GetTotalEarned_LeftPane().Text,
                Balance = GetBalance_LeftPane().Text
            };
            return phraseSummary;
        }
        public List<string> GetPendingPhrasesFromDB(string username)
        {
            int unassignedPendingPhrases = (int)PhraseStatusCodes.UnassignedPendingPhrases;
            int assignedPendingPhrases = (int)PhraseStatusCodes.AssignedPendingPhrases;

            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetPendingPhrasesFromDB(username, unassignedPendingPhrases, assignedPendingPhrases);
        }
        public List<IWebElement> GetPendingPhrasesList()
        {
            return _browser.GetElements("#MainContent_gvPendingSubmissions > tbody > tr", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<string> GetPendingPhrasesFromUI()
        {
            List<string> pendingPhrases = new List<string>();
            List<IWebElement> pendingphrasesWebElements = GetPendingPhrasesList();
            foreach (IWebElement pendingPhraseElement in pendingphrasesWebElements)
            {
                pendingPhrases.Add(pendingPhraseElement.Text);
            }
            return pendingPhrases;
        }
        public List<string> GetAcceptedPhrasesFromDB(string username)
        {
            int acceptedPhrasesStatusCode = (int)PhraseStatusCodes.AcceptedPhrases;

            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetAcceptedPhrasesFromDB(username, acceptedPhrasesStatusCode);
        }
        public List<IWebElement> GetAcceptedPhraseElementsList()
        {
            return _browser.GetElements("#MainContent_gvAcceptedSubmissions > tbody > tr", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<string> GetAcceptedPhrasesFromUI()
        {
            List<string> acceptedPhrases = new List<string>();
            List<IWebElement> acceptedphrasesWebElements = GetAcceptedPhraseElementsList();
            foreach (IWebElement acceptedPhraseElement in acceptedphrasesWebElements)
            {
                acceptedPhrases.Add(acceptedPhraseElement.Text);
            }
            return acceptedPhrases;
        }
        public IWebElement GetAboutYourSelfTextBoxMiddlePane()
        {
            return _browser.GetElement("txtAboutUser", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmitPhotoAndInfoButton()
        {
            return _browser.GetElement("MainContent_imbSubmitPhoto", WebBrowser.ElementSelectorType.ID);
        }
        public void EditAboutYourselfText(string editedUserInfo)
        {
            GetAboutYourSelfTextBoxMiddlePane().Clear();
            GetAboutYourSelfTextBoxMiddlePane().SendKeys(editedUserInfo);
            GetSubmitPhotoAndInfoButton().Click();
        }
        public string GetAboutYourselfTextFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetAboutYourselfTextOfUser(username);
        }
        public string GetAboutYourSelfTextMiddlePaneFromUI()
        {
            return GetAboutYourSelfTextBoxMiddlePane().Text;
        }
        public IWebElement GetUploadPhotoButton()
        {
            return _browser.GetElement("fupProfileImage", WebBrowser.ElementSelectorType.ID);
        }
        public void UploadPhotoAndSubmit()
        {
            string photoPath = ConfigurationManager.AppSettings["TestPhotoPath"].ToString();
            GetUploadPhotoButton().SendKeys(photoPath);
            GetSubmitPhotoAndInfoButton().Click();
        }
        public string GetUploadedPhotoInfoFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetUploadedPhotoInfoFromDB(username);
        }
        public IWebElement GetRemovePhotoLink()
        {
            return _browser.GetElement("MainContent_lnkdelete", WebBrowser.ElementSelectorType.ID);
        }        
        public void RemovePhotoAndConfirm()
        {
            GetRemovePhotoLink().Click();
            _browser.SwitchToAlertWindowAndAccept();
            _browser.SwitchtoPreviousWindow();
        }
        public IWebElement GetAddSubscriptionButton_Account()
        {
            return _browser.GetElement("MainContent_imbAddSCR", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForProceedButton_SubscribePage()
        {
            return _browser.WaitForElement("imgProceed", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsProceedButtonVisible()
        {
            return _browser.IsElementVisible("imgProceed", WebBrowser.ElementSelectorType.ID);
        }
        public void SelectAddSubscriptionButtonAndWaitForRedirection()
        {
            GetAddSubscriptionButton_Account().Click();
            WaitForProceedButton_SubscribePage();
        }
        public void CreateRandomSubscriptionForUser(string username)
        {
            int[] codes = new[] { (int)ProductCodes.SpiceMobile, (int)ProductCodes.SpiceProfessional };
            int randomProductCode = codes[_random.Next(codes.Length)];

            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.CreateSubscriptionForUser(username,randomProductCode);
        }
        public SubscriptionTypeAndDuration GetSubscriptionInfoFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetSubscriptionDetails(username);
        }
        public IWebElement GetSubscriptionType()
        {
            return _browser.GetElement("MainContent_rptrUserSubscriptions_spnProductTitle_0", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetExpiryOn()
        {
            return _browser.GetElement("MainContent_rptrUserSubscriptions_lblExpDate_0", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForUnsubcribeButtonToLoad()
        {
            return _browser.WaitForElement("MainContent_rptrUserSubscriptions_imgUnsubscribe_0", WebBrowser.ElementSelectorType.ID);
        }
        public SubscriptionTypeAndDuration GetSubscriptionInfoFromUI()
        {
            GetAccountLinkFromHeader().Click();
            WaitForUnsubcribeButtonToLoad();

            SubscriptionTypeAndDuration subscriptionInfo = new SubscriptionTypeAndDuration
            {
                SubscriptionTypeFromUI = GetSubscriptionType().Text,
                SubscriptionDurationFromUI = GetExpiryOn().Text
            };
            return subscriptionInfo;
        }
        public void DeleteSubscriptionForUser(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeleteSubscriptionForUser(username);
        }
        public void AddDevicesToSubscription(string username)
        {
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            Encryption encryption = new Encryption();
            string hashedPassword = encryption.GetSHA1EncryptedString(password);

            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.AddRandomDevicesToSubscription(username, hashedPassword);
        }
        public List<string> GetSubscriptionDevicesFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetSubscriptionDevices(username);
        }
        public List<IWebElement> GetActiveDevicesList()
        {
            return _browser.GetElements("//input[@type = \"submit\" and @onclick = \"return window.confirm('Are you sure you want to remove this device?');\"]", WebBrowser.ElementSelectorType.XPath);
        }
        
        public List<string> GetSubscriptionDevicesFromUI()
        {
            List<string> devicesList = new List<string>();
            GetAccountLinkFromHeader().Click();
            List<IWebElement> devicesWebElements = GetActiveDevicesList();
            foreach (IWebElement deviceWebElement in devicesWebElements)
            {
                devicesList.Add(deviceWebElement.GetAttribute("title"));
            }
            return devicesList;
        }
        public void DeleteDevicesForSubscriptionFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeleteDevicesForSubscription(username);
        }
        public IWebElement GetRemoveDeviceButton()
        {
            return _browser.GetElement("MainContent_rptrUserSubscriptions_rptrSubscribedDevices_0_btnDevice_0", WebBrowser.ElementSelectorType.ID);
        }
        public void RemoveDeviceFromActiveSubscription()
        {
            int preloaderWait = Int32.Parse(ConfigurationManager.AppSettings["RedeemGiftWaitTime"].ToString());
            GetAccountLinkFromHeader().Click();
            GetRemoveDeviceButton().Click();
            _browser.SwitchToAlertWindowAndAccept();
            Thread.Sleep(preloaderWait);
            _browser.SwitchtoPreviousWindow();
            _browser.SwitchToAlertWindowAndAccept();
        }
        public IWebElement GetEditButton_AccountPage()
        {
            return _browser.GetElement("imbEdit", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCancelButton_AccountPage()
        {
            return _browser.GetElement("imbCancel", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSaveButton_AccountPage()
        {
            return _browser.GetElement("imbSave", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetFirstNameTextBox_AccountPage()
        {
            return _browser.GetElement("txtFirstName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetLastNameTextBox_AccountPage()
        {
            return _browser.GetElement("txtLastName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEmailTextBox_AccountPage()
        {
            return _browser.GetElement("txtEmail", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhoneTextBox_AccountPage()
        {
            return _browser.GetElement("txtPhone", WebBrowser.ElementSelectorType.ID);
        }
        public void EditAccountDetails()
        {
            string editedFirstName = ConfigurationManager.AppSettings["ModifiedFirstName"].ToString();
            string editedLastName = ConfigurationManager.AppSettings["ModifiedLastName"].ToString();
            string editedEmail = ConfigurationManager.AppSettings["ModifiedEmail"].ToString();
            string editedPhone = ConfigurationManager.AppSettings["ModifiedPhone"].ToString();

            GetEditButton_AccountPage().Click();
            GetFirstNameTextBox_AccountPage().Clear();
            GetLastNameTextBox_AccountPage().Clear();
            GetEmailTextBox_AccountPage().Clear();
            GetPhoneTextBox_AccountPage().Clear();
            GetFirstNameTextBox_AccountPage().SendKeys(editedFirstName);
            GetLastNameTextBox_AccountPage().SendKeys(editedLastName);
            GetEmailTextBox_AccountPage().SendKeys(editedEmail);
            GetPhoneTextBox_AccountPage().SendKeys(editedPhone);
        }
        public void SaveDetailsAndConfirm()
        {
            
            GetSaveButton_AccountPage().Click();
            Thread.Sleep(_preloaderWait);
            _browser.SwitchToAlertWindowAndAccept();
        }
        public AccountDetailsDB GetAccountDetailsFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetAccountDetails(username);
        }
        public AccountDetailsUI GetAccountDetailsFromUI()
        {
            AccountDetailsUI accountDetails = new AccountDetailsUI
            {
                FirstName = GetFirstNameTextBox_AccountPage().GetAttribute("value"),
                LastName = GetLastNameTextBox_AccountPage().GetAttribute("value"),
                Email = GetEmailTextBox_AccountPage().GetAttribute("value"),
                Phone = GetPhoneTextBox_AccountPage().GetAttribute("value")
            };
            return accountDetails;
        }
        public void ResetAccountDetailsWithPreviousValuesDB(string username)
        {
            string firstName = ConfigurationManager.AppSettings["FirstName"].ToString();
            string lastName = ConfigurationManager.AppSettings["LastName"].ToString();
            string email = ConfigurationManager.AppSettings["EmailForAccountTesting"].ToString();
            string phone = ConfigurationManager.AppSettings["Phone"].ToString();

            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.UpdateAccountDetails(username,firstName,lastName,email,phone);
        }
        public void CancelChanges()
        {
            GetCancelButton_AccountPage().Click();
        }
        public void DeletePreviousRequestsForUser(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeletePaymentRequests(username);
        }
        public IWebElement GetTransferButton_AccountPage()
        {
            return _browser.GetElement("MainContent_btnTransfer", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPaymentConfirmationButton_AccountPage()
        {
            return _browser.GetElement("MainContent_rdListConfirmation_0", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetNewAddressButton_AccountPage()
        {
            return _browser.GetElement("rbtnNewAddress", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAddressLine1()
        {
            return _browser.GetElement("txtCheckAddressLine1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAddressLine2()
        {
            return _browser.GetElement("txtCheckAddressLine2", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCity()
        {
            return _browser.GetElement("txtCheckCity", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetZipCode()
        {
            return _browser.GetElement("txtCheckZipCode", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCountryDropdown()
        {
            return _browser.GetElement("ddlCheckAddressCountry", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetContinueButton()
        {
            return _browser.GetElement("MainContent_btnContinue", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPaymentRequestStatus()
        {
            return _browser.GetElement("PopupContentText", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCloseButtonOnPaymentRequest()
        {
            return _browser.GetElement("basic-modal-close", WebBrowser.ElementSelectorType.ID);
        }
        public void SelectNewAddress()
        {
            

            GetAccountLinkFromHeader().Click();
            GetTransferButton_AccountPage().Click();
            _browser.SwitchtoCurrentWindow();
            Thread.Sleep(_preloaderWait);
            GetPaymentConfirmationButton_AccountPage().Click();
            _browser.SwitchtoCurrentWindow();
            GetNewAddressButton_AccountPage().Click();
            
        }
        public string EnterNewAddressAndProceed()
        {
            
            string addressLine1 = ConfigurationManager.AppSettings["AddressLine1"].ToString();
            string addressLine2 = ConfigurationManager.AppSettings["AddressLine2"].ToString();
            string city = ConfigurationManager.AppSettings["City"].ToString();
            string zipCode = ConfigurationManager.AppSettings["ZipCode"].ToString();
            string country = ConfigurationManager.AppSettings["Country"].ToString();
            SelectElement selectCountry = new SelectElement(GetCountryDropdown());

            GetAddressLine1().SendKeys(addressLine1);
            GetAddressLine2().SendKeys(addressLine2);
            GetCity().SendKeys(city);
            GetZipCode().SendKeys(zipCode);
            selectCountry.SelectByText(country);
            GetContinueButton().Click();
            _browser.SwitchtoCurrentWindow();
            string status = GetPaymentRequestStatus().Text;
            GetCloseButtonOnPaymentRequest().Click();
            _browser.SwitchtoPreviousWindow();

            return status;
        }
        public void SelectOldAddress()
        {
            

            GetAccountLinkFromHeader().Click();
            GetTransferButton_AccountPage().Click();
            _browser.SwitchtoCurrentWindow();
            Thread.Sleep(_preloaderWait);
            GetPaymentConfirmationButton_AccountPage().Click();
            _browser.SwitchtoCurrentWindow();
        }
        public string ProceedWithAddressOnFile()
        {
            GetContinueButton().Click();
            _browser.SwitchtoCurrentWindow();
            string status = GetPaymentRequestStatus().Text;
            GetCloseButtonOnPaymentRequest().Click();
            _browser.SwitchtoPreviousWindow();

            return status;
        }
        public IWebElement GetPayPalPaymentTypeOptionButton()
        {
            return _browser.GetElement("MainContent_rblstPaymentOptions_1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPayPalEmailTextBox()
        {
            return _browser.GetElement("txtPaypalEmailID", WebBrowser.ElementSelectorType.ID);
        }

        public string SelectPayPalPaymentAndEnterPayPalEmailID()
        {
            string paypalEmail = ConfigurationManager.AppSettings["PayPalEmail"].ToString();
            

            GetPayPalPaymentTypeOptionButton().Click();
            Thread.Sleep(_preloaderWait);
            GetPayPalEmailTextBox().SendKeys(paypalEmail);

            return paypalEmail;
        }
        public string ConfirmPayPalTransfer()
        {
            

            GetTransferButton_AccountPage().Click();
            _browser.SwitchtoCurrentWindow();
            Thread.Sleep(_preloaderWait);
            GetPaymentConfirmationButton_AccountPage().Click();
            _browser.SwitchtoCurrentWindow();
            string status = GetPaymentRequestStatus().Text;
            GetCloseButtonOnPaymentRequest().Click();
            _browser.SwitchtoPreviousWindow();

            return status;
        }
        public string GetPayPalEmailFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetPayPalEmailFromPaymentRequest(username);
        }
        public IWebElement GetTitleTextBox_PhraseSubmissions()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtTitle", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetReferenceUrlTextBox_PhraseSubmissions()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtRefURL", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetYearTextBox_PhraseSubmissions()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtYear", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAuthorTextBox_Fiction()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFicAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPublisherTextBox_Fiction()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFicPublisher", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCityTextBox_Fiction()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFicCity", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetISBNTextBox_Fiction()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFicISBN", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSectionAuthorTextBox_Anthology()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtAnthSectionAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAnthologyTitleTextBox_Anthology()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtAnthologyTitle", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEditorTextBox_Anthology()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtAnthologyEditor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPublisherTextBox_Anthology()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtAnthologyPublisher", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCityTextBox_Anthology()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtAnthologyCity", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetISBNTextBox_Anthology()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtAnthISBN", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAuthorTextBox_ReferencedQuotation()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtRQuotAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhraseAuthorTextBox_ReferencedQuotation()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtRQuotPhrAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPublisherTextBox_ReferencedQuotation()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtRQuotPublisher", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCityTextBox_ReferencedQuotation()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtRQuotCity", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetISBNTextBox_ReferencedQuotation()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtRQuotISBN", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAuthorTextBox_Magazine()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtMagAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetTitleTextBox_Magazine()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtMagTitle", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetIssueDateSelector_Magazine()
        {
            return _browser.GetElement("txtMagIssueDt", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetVolumeTextBox_Magazine()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtMagVolume", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAuthorTextBox_Newspaper()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtNPaperAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetNewspaperTitleTextBox_Newspaper()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtNPaperTitle", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetIssueDateSelector_Newspaper()
        {
            return _browser.GetElement("txtNPaperIssueDt", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEpisodeAuthorTextBox_Television()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtTVEpisodeAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSeriesTitleTextBox_Television()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtTVSeriesTitle", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetDirectorTextBox_Television()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtTVDirector", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetNetworkTextBox_Television()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtTVNetwork", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAirDateSelector_Television()
        {
            return _browser.GetElement("txtTVAirDate", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetScreenplayAuthorTextBox_Film()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFilmAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetDirectorTextBox_Film()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFilmDirector", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetActorsTextBox_Film()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFilmActors", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetProductionCompanyTextBox_Film()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtFilmProdCompany", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAuthorTextBox_Play()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtPlayAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPublisherTextBox_Play()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtPlayPublisher", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCityTextBox_Play()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtPlayCity", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetComposerTextBox_Lyric()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtLrcComposer", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPerformerTextBox_Lyric()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtLrcPerformer", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetNameOfCDTextBox_Lyric()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtLrcNameOfCD", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPublisherTextBox_Lyric()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtLrcPublisher", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetDateRecorderSelector_Lyric()
        {
            return _browser.GetElement("txtLrcDtRec", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetWorkAuthorTextBox_Speech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtSpchWorkAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhraseAuthorTextBox_Speech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtSpchPhrAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetWorkEditorTextBox_Speech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtSpchWorkEditor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetIssueDateSelector_Speech()
        {
            return _browser.GetElement("txtSpchIssueDt", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetVolumeTextBox_Speech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtSpchVolume", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSpeechTypeOptionBox_Speech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_ddlSpchType", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAuthorTextBox_OtherSpeech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtOSpeechAuthor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPublisherTextBox_OtherSpeech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtOSpeechPublisher", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCityTextBox_OtherSpeech()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_txtOSpeechCity", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSellYourPhrasesLink_LeftBanner()
        {
            return _browser.GetElement("UserLeftBanner_lnksellyourphrases", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAgreeOptionButton_NewPhraseSubmissions()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_rdListLegalAgreement_1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCategoryOptionButton_SourceDetails(int categoryCode)
        {
            return _browser.GetElement($"MainContent_PhraseSubmissions_rbtnListSourceTypes_{categoryCode}", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubCategoryOptionButton_Book(int subCategoryCode)
        {
            return _browser.GetElement($"MainContent_PhraseSubmissions_rbtnListBook_{subCategoryCode}", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubCategoryOptionButton_Periodical(int subCategoryCode)
        {
            return _browser.GetElement($"MainContent_PhraseSubmissions_rbtnListPeriodical_{subCategoryCode}", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubCategoryOptionButton_Other(int subCategoryCode)
        {
            return _browser.GetElement($"MainContent_PhraseSubmissions_rbtnListScript_{subCategoryCode}", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmitButton_PhraseSubmissions()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_imbSubmitPhrase", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmitPhrasesLink_PhraseSubmissions()
        {
            return _browser.GetElement("MainContent_PhraseSubmissions_lbtnSubmitMultiplePhrases", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetOkButton_PhraseSubmissions()
        {
            return _browser.GetElement("btnPhraseSubmissions", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForAgreeButtonToLoad()
        {
            return _browser.WaitForElement("MainContent_PhraseSubmissions_rdListLegalAgreement_1", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToPhraseSubmissionsPage()
        {
            GetSellYourPhrasesLink_LeftBanner().Click();
            WaitForAgreeButtonToLoad();
        }
        public bool WaitForSourceDetailsCategoriesToLoad()
        {
            return _browser.WaitForElement("MainContent_PhraseSubmissions_rbtnListBook_0", WebBrowser.ElementSelectorType.ID);
        }
        public void AcceptTermsAndConditions()
        {
            GetAgreeOptionButton_NewPhraseSubmissions().Click();
            WaitForSourceDetailsCategoriesToLoad();
        }
        public IWebElement GetPhraseSubmissionTextBox_PhraseSubmissions()
        {
            return _browser.GetElement("txtMultiplePhraseSubmissions", WebBrowser.ElementSelectorType.ID);
        }
        public string GetRandomStringOfRequiredLength(int length)
        {
            StringBuilder randomString = new StringBuilder();
            char alphabet;
            for (int i = 0; i < length; i++)
            {
                alphabet = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                randomString.Append(alphabet);
            }
            return randomString.ToString().ToLower();
        }
        public string GetRandomPhraseText()
        {
            StringBuilder phraseText = new StringBuilder();
            int wordsCount = _random.Next(2, 7);
            for(int i =1; i <= wordsCount; i++)
            {
                phraseText.Append($"{GetRandomStringOfRequiredLength(5)} ");
            }
            return phraseText.ToString();
        }
        public string GetMultiplePhrasesText()
        {
            StringBuilder multiplePhraseText = new StringBuilder();
            int pageNumber = _random.Next(1, 100);
            multiplePhraseText.AppendLine($"{GetRandomPhraseText()} ");
            multiplePhraseText.AppendLine($"{GetRandomPhraseText()} ({GetRandomStringOfRequiredLength(6)},{GetRandomStringOfRequiredLength(4)})");
            multiplePhraseText.AppendLine($"{GetRandomPhraseText()} ({pageNumber})");
            multiplePhraseText.AppendLine($"{GetRandomPhraseText()} ({pageNumber})({pageNumber}-{pageNumber})");
            return multiplePhraseText.ToString();
        }
        public string GetRandomTextForSourceDetails()
        {
            return $"test_{GetRandomStringOfRequiredLength(5)}";
        }
        public string GetRefrenceUrlForSourceDetails()
        {
            return ConfigurationManager.AppSettings["TestReferenceURL"].ToString();
        }
        public string GetRandomYearForSourceDetails()
        {
            return _random.Next(1000, 2020).ToString();
        }
        public string EnterNewPhraseInPhraseSubmissionsBox()
        {
            Thread.Sleep(_preloaderWait);
            GetSubmitPhrasesLink_PhraseSubmissions().Click();
            _browser.SwitchtoCurrentWindow();
            Thread.Sleep(_preloaderWait);
            string phraseText = GetRandomPhraseText();
            GetPhraseSubmissionTextBox_PhraseSubmissions().SendKeys(phraseText);
            GetOkButton_PhraseSubmissions().Click();
            _browser.SwitchtoPreviousWindow();

            return phraseText.Trim();
        }
        #region GetSourceSubCategory
        public int GetSubCategoryCode(SourceCategory categoryType)
        {
            int subCategoryCode = 0;
            switch (categoryType)
            {
                case SourceCategory.Book:
                    subCategoryCode = GetRandomSubCategoryForBook();
                    break;
                case SourceCategory.Periodical:
                    subCategoryCode = GetRandomSubCategoryForPeriodical();
                    break;
                case SourceCategory.Other:
                    subCategoryCode = GetRandomSubCategoryForOther();
                    break;
                case SourceCategory.Unknown:
                    subCategoryCode = 0;
                    break;
            }
            return subCategoryCode;
        }
        private int GetRandomSubCategoryForBook()
        {
            return _random.Next(1, 4);
        }
        private int GetRandomSubCategoryForPeriodical()
        {
            return _random.Next(1, 3);
        }
        private int GetRandomSubCategoryForOther()
        {
            return _random.Next(1, 6);
        }
        #endregion
        public int GetRandomSourceCategoryCode()
        {
            return _random.Next(1, 4);
        }
        public int GetRandomSourceSubCategory(int sourceCategoryCode)
        {
            SourceCategory sourceCategoryType = (SourceCategory)sourceCategoryCode;
            return GetSubCategoryCode(sourceCategoryType);
        }
        public void SelectCategoryAndSubCategory(int sourceCategoryCode,int sourceSubCategoryCode)
        {
            

            if (sourceCategoryCode == 1)
            {
                GetCategoryOptionButton_SourceDetails(sourceCategoryCode - 1).Click();
                Thread.Sleep(_preloaderWait);
                GetSubCategoryOptionButton_Book(sourceSubCategoryCode - 1).Click();
            }
            else if (sourceCategoryCode == 2)
            {
                GetCategoryOptionButton_SourceDetails(sourceCategoryCode - 1).Click();
                Thread.Sleep(_preloaderWait);
                GetSubCategoryOptionButton_Periodical(sourceSubCategoryCode - 1).Click();
            }
            else if (sourceCategoryCode == 3)
            {
                GetCategoryOptionButton_SourceDetails(sourceCategoryCode - 1).Click();
                Thread.Sleep(_preloaderWait);
                GetSubCategoryOptionButton_Other(sourceSubCategoryCode - 1).Click();
            }
            else
            {
                GetCategoryOptionButton_SourceDetails(sourceCategoryCode - 1).Click();
            }
        }
        #region FillSourceDetailsForBook
        public void FillSourceDetailsForBook(BookSubCategory bookSubCategoryType)
        {
            switch (bookSubCategoryType)
            {
                case BookSubCategory.Fiction:
                    FillAllFieldsForFictionSubCategory();
                    break;
                case BookSubCategory.NonFiction:
                    FillAllFieldsForNonFictionSubCategory();
                    break;
                case BookSubCategory.WorkInAnthology:
                    FillAllFieldsForAnthologySubCategory();
                    break;
                case BookSubCategory.ReferencedQuotation:
                    FillAllFieldsForReferencedQuotationSubCategory();
                    break;
            }
        }
        private void FillAllFieldsForFictionSubCategory()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetCityTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetISBNTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
        }
        private void FillAllFieldsForNonFictionSubCategory()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetCityTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetISBNTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
        }
        private void FillAllFieldsForAnthologySubCategory()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetSectionAuthorTextBox_Anthology().SendKeys(GetRandomTextForSourceDetails());
            GetAnthologyTitleTextBox_Anthology().SendKeys(GetRandomTextForSourceDetails());
            GetEditorTextBox_Anthology().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_Anthology().SendKeys(GetRandomTextForSourceDetails());
            GetCityTextBox_Anthology().SendKeys(GetRandomTextForSourceDetails());
            GetISBNTextBox_Anthology().SendKeys(GetRandomTextForSourceDetails());
        }
        private void FillAllFieldsForReferencedQuotationSubCategory()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_ReferencedQuotation().SendKeys(GetRandomTextForSourceDetails());
            GetPhraseAuthorTextBox_ReferencedQuotation().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_ReferencedQuotation().SendKeys(GetRandomTextForSourceDetails());
            GetCityTextBox_ReferencedQuotation().SendKeys(GetRandomTextForSourceDetails());
            GetISBNTextBox_ReferencedQuotation().SendKeys(GetRandomTextForSourceDetails());
        }
        #endregion

        #region FillSourceDetailsForPeriodical
        public void FillSourceDetailsForPeriodical(PeriodicalSubCategory periodicalSubCategoryType)
        {
            switch (periodicalSubCategoryType)
            {
                case PeriodicalSubCategory.Magazine:
                    FillAllFieldsForMagazineSubCategory();
                    break;
                case PeriodicalSubCategory.Newspaper:
                    FillAllFieldsForNewspaperSubCategory();
                    break;
                case PeriodicalSubCategory.Journal:
                    FillAllFieldsForJournalSubCategory();
                    break;
            }
        }
        private void FillAllFieldsForMagazineSubCategory()
        {
            string testDate = ConfigurationManager.AppSettings["TestDate"].ToString();

            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Magazine().SendKeys(GetRandomTextForSourceDetails());
            GetTitleTextBox_Magazine().SendKeys(GetRandomTextForSourceDetails());
            GetIssueDateSelector_Magazine().SendKeys(testDate);
            GetVolumeTextBox_Magazine().SendKeys(GetRandomYearForSourceDetails());
        }
        private void FillAllFieldsForNewspaperSubCategory()
        {
            string testDate = ConfigurationManager.AppSettings["TestDate"].ToString();

            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Newspaper().SendKeys(GetRandomTextForSourceDetails());
            GetNewspaperTitleTextBox_Newspaper().SendKeys(GetRandomTextForSourceDetails());
            GetIssueDateSelector_Newspaper().SendKeys(testDate);
        }
        private void FillAllFieldsForJournalSubCategory()
        {
            string testDate = ConfigurationManager.AppSettings["TestDate"].ToString();

            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Magazine().SendKeys(GetRandomTextForSourceDetails());
            GetTitleTextBox_Magazine().SendKeys(GetRandomTextForSourceDetails());
            GetIssueDateSelector_Magazine().SendKeys(testDate);
            GetVolumeTextBox_Magazine().SendKeys(GetRandomYearForSourceDetails());
        }
        #endregion

        #region FillSourceDetailsForOther
        public void FillSourceDetailsForOther(OtherSubCategory otherSubCategoryType)
        {
            switch (otherSubCategoryType)
            {
                case OtherSubCategory.Television:
                    FillAllFieldsForTelevisionSubCategory();
                    break;
                case OtherSubCategory.Film:
                    FillAllFieldsForFilmSubCategory();
                    break;
                case OtherSubCategory.Play:
                    FillAllFieldsForPlaySubCategory();
                    break;
                case OtherSubCategory.Lyric:
                    FillAllFieldsForLyricSubCategory();
                    break;
                case OtherSubCategory.Speech:
                    FillAllFieldsForSpeechSubCategory();
                    break;
                case OtherSubCategory.Poetry:
                    FillAllFieldsForPoetrySubCategory();
                    break;
            }
        }
        private void FillAllFieldsForTelevisionSubCategory()
        {
            string testDate = ConfigurationManager.AppSettings["TestDate"].ToString();

            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetEpisodeAuthorTextBox_Television().SendKeys(GetRandomTextForSourceDetails());
            GetSeriesTitleTextBox_Television().SendKeys(GetRandomTextForSourceDetails());
            GetDirectorTextBox_Television().SendKeys(GetRandomTextForSourceDetails());
            GetNetworkTextBox_Television().SendKeys(GetRandomTextForSourceDetails());
            GetAirDateSelector_Television().SendKeys(testDate);
        }
        private void FillAllFieldsForFilmSubCategory()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetScreenplayAuthorTextBox_Film().SendKeys(GetRandomTextForSourceDetails());
            GetDirectorTextBox_Film().SendKeys(GetRandomTextForSourceDetails());
            GetActorsTextBox_Film().SendKeys(GetRandomTextForSourceDetails());
            GetProductionCompanyTextBox_Film().SendKeys(GetRandomTextForSourceDetails());
        }
        private void FillAllFieldsForPlaySubCategory()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Play().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_Play().SendKeys(GetRandomTextForSourceDetails());
            GetCityTextBox_Play().SendKeys(GetRandomTextForSourceDetails());
        }
        private void FillAllFieldsForLyricSubCategory()
        {
            string testDate = ConfigurationManager.AppSettings["TestDate"].ToString();

            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetComposerTextBox_Lyric().SendKeys(GetRandomTextForSourceDetails());
            GetPerformerTextBox_Lyric().SendKeys(GetRandomTextForSourceDetails());
            GetNameOfCDTextBox_Lyric().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_Lyric().SendKeys(GetRandomTextForSourceDetails());
            GetDateRecorderSelector_Lyric().SendKeys(testDate);
        }
        private void FillAllFieldsForSpeechSubCategory()
        {
            string testDate = ConfigurationManager.AppSettings["TestDate"].ToString();

            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetWorkAuthorTextBox_Speech().SendKeys(GetRandomTextForSourceDetails());
            GetPhraseAuthorTextBox_Speech().SendKeys(GetRandomTextForSourceDetails());
            GetWorkEditorTextBox_Speech().SendKeys(GetRandomTextForSourceDetails());
            GetIssueDateSelector_Speech().SendKeys(testDate);
            GetVolumeTextBox_Speech().SendKeys(GetRandomYearForSourceDetails());
        }
        private void FillAllFieldsForPoetrySubCategory()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Play().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_Play().SendKeys(GetRandomTextForSourceDetails());
            GetCityTextBox_Play().SendKeys(GetRandomTextForSourceDetails());
        }
        #endregion
        public void FillSourceDetailsForUnknown()
        {
            GetTitleTextBox_PhraseSubmissions().SendKeys(GetRandomTextForSourceDetails());
            GetReferenceUrlTextBox_PhraseSubmissions().SendKeys(GetRefrenceUrlForSourceDetails());
            GetYearTextBox_PhraseSubmissions().SendKeys(GetRandomYearForSourceDetails());
            GetAuthorTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetPublisherTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetCityTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
            GetISBNTextBox_Fiction().SendKeys(GetRandomTextForSourceDetails());
        }
        public void FillAllTheFieldsAndSubmit(int sourceCategoryCode, int sourceSubCategoryCode)
        {
            
            Thread.Sleep(_preloaderWait);
            if (sourceCategoryCode == 1)
            {
                BookSubCategory bookSubCategoryType = (BookSubCategory)sourceSubCategoryCode;
                FillSourceDetailsForBook(bookSubCategoryType);
            }
            else if (sourceCategoryCode == 2)
            {
                PeriodicalSubCategory periodicalSubCategoryType = (PeriodicalSubCategory)sourceSubCategoryCode;
                FillSourceDetailsForPeriodical(periodicalSubCategoryType);
            }
            else if (sourceCategoryCode == 3)
            {
                OtherSubCategory otherSubCategoryType = (OtherSubCategory)sourceSubCategoryCode;
                FillSourceDetailsForOther(otherSubCategoryType);
            }
            else
            {
                FillSourceDetailsForUnknown();
            }
            GetSubmitButton_PhraseSubmissions().Click();
        }
        #region GetSourceDetailsForBook
        public List<string> GetSourceDetailsForBook(BookSubCategory bookSubCategoryType)
        {
            List<string> bookSourceDetails = new List<string>();
            switch (bookSubCategoryType)
            {
                case BookSubCategory.Fiction:
                    bookSourceDetails = GetAllDataForFictionSubCategory();
                    break;
                case BookSubCategory.NonFiction:
                    bookSourceDetails = GetAllDataForNonFictionSubCategory();
                    break;
                case BookSubCategory.WorkInAnthology:
                    bookSourceDetails = GetAllDataForAnthologySubCategory();
                    break;
                case BookSubCategory.ReferencedQuotation:
                    bookSourceDetails = GetAllDataForReferencedQuotationSubCategory();
                    break;
            }
            return bookSourceDetails;
        }
        private List<string> GetAllDataForFictionSubCategory()
        {
            List<string> detailsFictionSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Fiction().GetAttribute("value"),
                GetPublisherTextBox_Fiction().GetAttribute("value"),
                GetCityTextBox_Fiction().GetAttribute("value"),
                GetISBNTextBox_Fiction().GetAttribute("value")
            };
            return detailsFictionSubCategory;
        }
        private List<string> GetAllDataForNonFictionSubCategory()
        {
            List<string> detailsNonFictionSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Fiction().GetAttribute("value"),
                GetPublisherTextBox_Fiction().GetAttribute("value"),
                GetCityTextBox_Fiction().GetAttribute("value"),
                GetISBNTextBox_Fiction().GetAttribute("value")
            };
            return detailsNonFictionSubCategory;
        }
        private List<string> GetAllDataForAnthologySubCategory()
        {
            List<string> detailsAnthologySubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetSectionAuthorTextBox_Anthology().GetAttribute("value"),
                GetAnthologyTitleTextBox_Anthology().GetAttribute("value"),
                GetEditorTextBox_Anthology().GetAttribute("value"),
                GetPublisherTextBox_Anthology().GetAttribute("value"),
                GetCityTextBox_Anthology().GetAttribute("value"),
                GetISBNTextBox_Anthology().GetAttribute("value")
            };
            return detailsAnthologySubCategory;
        }
        private List<string> GetAllDataForReferencedQuotationSubCategory()
        {
            List<string> detailsReferencedQuotationSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_ReferencedQuotation().GetAttribute("value"),
                GetPhraseAuthorTextBox_ReferencedQuotation().GetAttribute("value"),
                GetPublisherTextBox_ReferencedQuotation().GetAttribute("value"),
                GetCityTextBox_ReferencedQuotation().GetAttribute("value"),
                GetISBNTextBox_ReferencedQuotation().GetAttribute("value")
            };
            return detailsReferencedQuotationSubCategory;
        }
        #endregion

        #region GetSourceDetailsForPeriodical
        public List<string> GetSourceDetailsForPeriodical(PeriodicalSubCategory periodicalSubCategoryType)
        {
            List<string> periodicalSourceDetails = new List<string>();
            switch (periodicalSubCategoryType)
            {
                case PeriodicalSubCategory.Magazine:
                    periodicalSourceDetails = GetAllDataForMagazineSubCategory();
                    break;
                case PeriodicalSubCategory.Newspaper:
                    periodicalSourceDetails = GetAllDataForNewspaperSubCategory();
                    break;
                case PeriodicalSubCategory.Journal:
                    periodicalSourceDetails = GetAllDataForJournalSubCategory();
                    break;
            }
            return periodicalSourceDetails;
        }
        private List<string> GetAllDataForMagazineSubCategory()
        {
            List<string> detailsMagazineSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Magazine().GetAttribute("value"),
                GetTitleTextBox_Magazine().GetAttribute("value"),
                GetIssueDateSelector_Magazine().GetAttribute("value"),
                GetVolumeTextBox_Magazine().GetAttribute("value")
            };
            return detailsMagazineSubCategory;
        }
        private List<string> GetAllDataForNewspaperSubCategory()
        {
            List<string> detailsNewspaperSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Newspaper().GetAttribute("value"),
                GetNewspaperTitleTextBox_Newspaper().GetAttribute("value"),
                GetIssueDateSelector_Newspaper().GetAttribute("value")
            };
            return detailsNewspaperSubCategory;
        }
        private List<string> GetAllDataForJournalSubCategory()
        {
            List<string> detailsJournalSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Magazine().GetAttribute("value"),
                GetTitleTextBox_Magazine().GetAttribute("value"),
                GetIssueDateSelector_Magazine().GetAttribute("value"),
                GetVolumeTextBox_Magazine().GetAttribute("value")
            };
            return detailsJournalSubCategory;            
        }
        #endregion

        #region GetSourceDetailsForOther
        public List<string> GetSourceDetailsForOther(OtherSubCategory otherSubCategoryType)
        {
            List<string> otherSourceDetails = new List<string>();
            switch (otherSubCategoryType)
            {
                case OtherSubCategory.Television:
                    otherSourceDetails = GetAllDataForTelevisionSubCategory();
                    break;
                case OtherSubCategory.Film:
                    otherSourceDetails = GetAllDataForFilmSubCategory();
                    break;
                case OtherSubCategory.Play:
                    otherSourceDetails = GetAllDataForPlaySubCategory();
                    break;
                case OtherSubCategory.Lyric:
                    otherSourceDetails = GetAllDataForLyricSubCategory();
                    break;
                case OtherSubCategory.Speech:
                    otherSourceDetails = GetAllDataForSpeechSubCategory();
                    break;
                case OtherSubCategory.Poetry:
                    otherSourceDetails = GetAllDataForPoetrySubCategory();
                    break;
            }
            return otherSourceDetails;
        }
        private List<string> GetAllDataForTelevisionSubCategory()
        {
            List<string> detailsTelevisionSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetEpisodeAuthorTextBox_Television().GetAttribute("value"),
                GetSeriesTitleTextBox_Television().GetAttribute("value"),
                GetDirectorTextBox_Television().GetAttribute("value"),
                GetNetworkTextBox_Television().GetAttribute("value"),
                GetAirDateSelector_Television().GetAttribute("value")
            };
            return detailsTelevisionSubCategory;
        }
        private List<string> GetAllDataForFilmSubCategory()
        {
            List<string> detailsFilmSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetScreenplayAuthorTextBox_Film().GetAttribute("value"),
                GetDirectorTextBox_Film().GetAttribute("value"),
                GetActorsTextBox_Film().GetAttribute("value"),
                GetProductionCompanyTextBox_Film().GetAttribute("value")
            };
            return detailsFilmSubCategory;
        }
        private List<string> GetAllDataForPlaySubCategory()
        {
            List<string> detailsPlaySubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Play().GetAttribute("value"),
                GetPublisherTextBox_Play().GetAttribute("value"),
                GetCityTextBox_Play().GetAttribute("value")
            };
            return detailsPlaySubCategory;
        }
        private List<string> GetAllDataForLyricSubCategory()
        {
            List<string> detailsLyricSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetComposerTextBox_Lyric().GetAttribute("value"),
                GetPerformerTextBox_Lyric().GetAttribute("value"),
                GetNameOfCDTextBox_Lyric().GetAttribute("value"),
                GetPublisherTextBox_Lyric().GetAttribute("value"),
                GetDateRecorderSelector_Lyric().GetAttribute("value")
            };
            return detailsLyricSubCategory;
        }
        private List<string> GetAllDataForSpeechSubCategory()
        {
            List<string> detailsSpeechSubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetWorkAuthorTextBox_Speech().GetAttribute("value"),
                GetPhraseAuthorTextBox_Speech().GetAttribute("value"),
                GetWorkEditorTextBox_Speech().GetAttribute("value"),
                GetIssueDateSelector_Speech().GetAttribute("value"),
                GetVolumeTextBox_Speech().GetAttribute("value")
            };
            return detailsSpeechSubCategory;
        }
        private List<string> GetAllDataForPoetrySubCategory()
        {
            List<string> detailsPlaySubCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Play().GetAttribute("value"),
                GetPublisherTextBox_Play().GetAttribute("value"),
                GetCityTextBox_Play().GetAttribute("value")
            };
            return detailsPlaySubCategory;
        }
        #endregion
        public List<string> GetAllDataForUnknown()
        {
            List<string> detailsUnknownSourceCategory = new List<string>
            {
                GetTitleTextBox_PhraseSubmissions().GetAttribute("value"),
                GetReferenceUrlTextBox_PhraseSubmissions().GetAttribute("value"),
                GetYearTextBox_PhraseSubmissions().GetAttribute("value"),
                GetAuthorTextBox_Fiction().GetAttribute("value"),
                GetPublisherTextBox_Fiction().GetAttribute("value"),
                GetCityTextBox_Fiction().GetAttribute("value"),
                GetISBNTextBox_Fiction().GetAttribute("value")
            };
            return detailsUnknownSourceCategory;
        }
        public List<string> GetDataEnteredFromReviewScreen(int sourceCategoryCode, int sourceSubCategoryCode)
        {
            List<string> submittedData = new List<string>();
            
            Thread.Sleep(_preloaderWait);
            if (sourceCategoryCode == 1)
            {
                BookSubCategory bookSubCategoryType = (BookSubCategory)sourceSubCategoryCode;
                submittedData = GetSourceDetailsForBook(bookSubCategoryType);
            }
            else if (sourceCategoryCode == 2)
            {
                PeriodicalSubCategory periodicalSubCategoryType = (PeriodicalSubCategory)sourceSubCategoryCode;
                submittedData = GetSourceDetailsForPeriodical(periodicalSubCategoryType);
            }
            else if (sourceCategoryCode == 3)
            {
                OtherSubCategory otherSubCategoryType = (OtherSubCategory)sourceSubCategoryCode;
                submittedData = GetSourceDetailsForOther(otherSubCategoryType);
            }
            else
            {
                submittedData = GetAllDataForUnknown();
            }
            GetSubmitButton_PhraseSubmissions().Click();
            return submittedData;
        }
        public IWebElement GetNoOptionButton_SuccessPhraseSubmission()
        {
            return _browser.GetElement("MainContent_NewSubmissions_rdListNewSubmissions_1", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToSuccessfulSubmissionBoxAndSelectNoButton()
        {
            int successPopupWaitTime = Int32.Parse(ConfigurationManager.AppSettings["ResetPaswordWaitTime"].ToString());
            Thread.Sleep(successPopupWaitTime);
            _browser.SwitchtoCurrentWindow();
            GetNoOptionButton_SuccessPhraseSubmission().Click();
        }
        public List<string> GetSourceDetailsFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetSourceDetailsForNewlySubmittedPhrase(username);
        }
        public IWebElement GetRecentPhraseFromPendingPhrases()
        {
            return _browser.GetElement("MainContent_gvPendingSubmissions_lbtnPhrases_0", WebBrowser.ElementSelectorType.ID);
        }

        public string GetPhraseTextFromAccountPage()
        {
            Thread.Sleep(_preloaderWait);
            return GetRecentPhraseFromPendingPhrases().Text;
        }
        public void DeleteSubmittedPhraseFromDB(string username)
        {
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeleteNewlySubmittedPhrase(username);
        }
        public string EnterMultiplePhrasesOfAllFormatSpecifications()
        {
            Thread.Sleep(_preloaderWait);
            GetSubmitPhrasesLink_PhraseSubmissions().Click();
            _browser.SwitchtoCurrentWindow();
            Thread.Sleep(_preloaderWait);
            string phraseText = GetMultiplePhrasesText();
            GetPhraseSubmissionTextBox_PhraseSubmissions().SendKeys(phraseText);
            GetOkButton_PhraseSubmissions().Click();
            _browser.SwitchtoPreviousWindow();

            return phraseText.Trim();
        }
        public List<string> GetSubmittedMultiplePhrasesFromDB(string username,int phrasesCount)
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetPhrasesListForSubmittedPhrasesCount(username,phrasesCount);
        }
        public List<string> GetSubmittedMultiplePhrasesFromAccountPage(int phrasesCount)
        {
            Thread.Sleep(_preloaderWait);
            List<string> pendingPhrases = new List<string>();
            List<IWebElement> pendingphrasesWebElements = GetPendingPhrasesList();
            foreach (IWebElement pendingPhraseElement in pendingphrasesWebElements)
            {
                pendingPhrases.Add(pendingPhraseElement.Text);
                if(pendingPhrases.Count == phrasesCount)
                {
                    break;
                }
            }
            return pendingPhrases;
        }
        public void DeleteMultipleSubmittedPhrasesFromDB(string username,int phrasesCount)
        {
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeleteMultiplePhrasesSubmitted(username, phrasesCount);
        }
        public void SubmitFromReviewPage()
        {
            Thread.Sleep(_preloaderWait);
            GetSubmitButton_PhraseSubmissions().Click();
        }
        public bool IsSignOutButtonVisible_Subscriptions()
        {
            return _browser.IsElementVisible("imgSignOut", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubscribeLinkFromTopRight()
        {
            return _browser.GetElement("lnkSubscribe", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToSubscribePage()
        {
            GetSubscribeLinkFromTopRight().Click();
        }
        public IWebElement GetHowPayPalWorksImageLink()
        {
            return _browser.GetElement("div-of-paypalimage", WebBrowser.ElementSelectorType.Class);
        }
        public IWebElement IsPayPalMainMenuVisible()
        {
            return _browser.GetElement("main-menu", WebBrowser.ElementSelectorType.ID);
        }
        public void CheckHowPayPalWorksImageLink()
        {
            GetHowPayPalWorksImageLink().Click();
            _browser.SwitchtoCurrentWindow();
            IsPayPalMainMenuVisible();
            _browser.CloseBrowser();
            _browser.SwitchtoPreviousWindow();
        }
        public IWebElement GetProceedButtonFromSubscribePage()
        {
            return _browser.GetElement("divPhraseSubmissionInfoProceedButton", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForSelectProductFieldsToLoad()
        {
            return _browser.WaitForElement("MainContent_ddlProducts", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetProductNameOptionBox()
        {
            return _browser.GetElement("MainContent_ddlProducts", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetDurationOptionBox()
        {
            return _browser.GetElement("MainContent_ddlPrice", WebBrowser.ElementSelectorType.ID);
        }
        public void SelectRandomSubscriptionTypeAndDuration()
        {
            int preloaderWait = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"].ToString());

            int[] codes = new[] { (int)ProductCodes.SpiceMobile, (int)ProductCodes.SpiceProfessional };
            int randomProductCode = codes[_random.Next(codes.Length)];

            int randomDurationCode = 0;
            if (randomProductCode == 1)
            {
                int oneYear = 28;
                int fourMonths = 29;
                int oneMonth = 32;
                int[] spiceMobileDurationCodes = new[] { oneYear, fourMonths, oneMonth };
                randomDurationCode = spiceMobileDurationCodes[_random.Next(spiceMobileDurationCodes.Length)];
            }
            else
            {
                int oneYear = 34;
                int fourMonths = 36;
                int oneMonth = 37;
                int[] spiceProfessionalDurationCodes = new[] { oneYear, fourMonths, oneMonth };
                randomDurationCode = spiceProfessionalDurationCodes[_random.Next(spiceProfessionalDurationCodes.Length)];
            }

            SelectElement selectProductName = new SelectElement(GetProductNameOptionBox());
            selectProductName.SelectByValue(randomProductCode.ToString());
            Thread.Sleep(preloaderWait);
            SelectElement selectDuration = new SelectElement(GetDurationOptionBox());
            selectDuration.SelectByValue(randomDurationCode.ToString());
            Thread.Sleep(preloaderWait);
        }
        public void NavigateToCheckOutPage()
        {
            GetProceedButtonFromSubscribePage().Click();
            WaitForSelectProductFieldsToLoad();
        }
        public IWebElement GetAccountSignInInfo()
        {
            return _browser.GetElement("MainContent_PnlAccountAlreadyExists", WebBrowser.ElementSelectorType.ID);
        }
        public string GetSuccessfulSignInMessage()
        {
            return GetAccountSignInInfo().Text;
        }
        public IWebElement GetProceedButton_Checkout()
        {
            return _browser.GetElement("imgProceed", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForPayPalLoginPage()
        {
            return _browser.WaitForElement("login", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToPayPalLogInPage()
        {
            GetProceedButton_Checkout().Click();
            WaitForPayPalLoginPage();
        }
        public void DeletePayPalInvoiceFromDB()
        {
            string newUsername = ConfigurationManager.AppSettings["NewUsername"].ToString();
            _dataAccess = new WritersMuseDataAccess();
            float spiceUserID = _dataAccess.GetSpiceUserIdForUserName(newUsername);
            _dataAccess.DeletePayPalInvoiceInDB(spiceUserID);
        }
        public bool IsPayPalCheckoutPageVisible()
        {
            return _browser.IsElementVisible("login", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForSignOutButtonLoad_Subscriptions()
        {
            return _browser.WaitForElement("imgSignOut", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateBackToCheckOutPage()
        {
            _browser.Back();
            WaitForSignOutButtonLoad_Subscriptions();
        }
    }
}
