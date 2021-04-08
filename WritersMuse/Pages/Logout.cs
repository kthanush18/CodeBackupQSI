using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Quant.Spice.Security;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WritersMuse.DataAccess;
using Quant.Spice.Test.UI.Web.WritersMuse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Pages
{
    public class Logout : WebPage
    {
        protected static WritersMuseDataAccess _dataAccess;
        Random _random = new Random();

        public Logout(WebBrowser browser) : base(browser)
        {

        }
        public enum ProductCodes
        {
            SpiceMobile = 1,
            SpiceMobileWithAds = 2,
            SpiceProfessional =3,
            SpiceBook = 4
        }
        public enum SubscriptionDuration
        {
            Monthly = 30,
            Yearly = 365,
            FourMonths = 120,
        }
        public enum SiteContentRating
        {
            PrettyGood = 1,
            Poor = 2,
            Excellent = 3,
            Average = 4
        }
        public bool WaitForPageToLoad()
        {
            return _browser.WaitForElement("container", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsMainContainerVisible()
        {
            return _browser.IsElementVisible("container", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSigninLinkVisible()
        {
            return _browser.IsElementVisible("lnkSignIn", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSignupLinkVisible()
        {
            return _browser.IsElementVisible("lnkSignUp", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsTopRightSubscribeLinkVisible()
        {
            return _browser.IsElementVisible("lnkLoggedOutSubscribe", WebBrowser.ElementSelectorType.ID);
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
        public bool IsProductActivationLinkVisible()
        {
            return _browser.IsElementVisible("lnkProductActivation", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsTermsOfUseLinkVisible()
        {
            return _browser.IsElementVisible("lnkTermsofUse", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsPrivacyPolicyLinkVisible()
        {
            return _browser.IsElementVisible("lnkPrivacyPolicy", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetProductActivationLink()
        {
            return _browser.GetElement("lnkProductActivation", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetTermsOfUseLink()
        {
            return _browser.GetElement("lnkTermsofUse", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPrivacyPolicyLink()
        {
            return _browser.GetElement("lnkPrivacyPolicy", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsProductActivationContentVisible()
        {
            return _browser.IsElementVisible("lnkProductActivation", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsTermsOfUseContentVisible()
        {
            return _browser.IsElementVisible("lnkTermsofUse", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsPrivacyPolicyContentVisible()
        {
            return _browser.IsElementVisible("lnkPrivacyPolicy", WebBrowser.ElementSelectorType.ID);
        }
        public void SelectProductActivationLink()
        {
            GetProductActivationLink().Click();
        }
        public void SelectTermsOfUseLink()
        {
            GetTermsOfUseLink().Click();
        }
        public void SelectPrivacyPolicyLink()
        {
            GetPrivacyPolicyLink().Click();
        }
        public bool IsWatchOurProductLinkVisible()
        {
            return _browser.IsElementVisible("divTakeOurTourImageButton", WebBrowser.ElementSelectorType.ID);
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
        public ProductPrices GetProductpricesFromDB()
        {
            ProductPrices productPricesFromDB = new ProductPrices();
            int productCode = (int)ProductCodes.SpiceMobile;
            int montlyDuration = (int)SubscriptionDuration.Monthly;
            int yearlyDuration = (int)SubscriptionDuration.Yearly;
            bool isGiftProduct = false;
            _dataAccess = new WritersMuseDataAccess();
            productPricesFromDB = _dataAccess.GetProductPricesForSpiceMobile(productCode,montlyDuration,yearlyDuration,isGiftProduct);
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
        public IWebElement GetSignupLink()
        {
            return _browser.GetElement("lnkSignUp", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForUserNameTextBoxToLoad()
        {
            return _browser.WaitForElement("txtUserName", WebBrowser.ElementSelectorType.ID);
        }
        public void ClickSignUPLinkAndWaitForFieldsToLoad()
        {
            GetSignupLink().Click();
            WaitForUserNameTextBoxToLoad();
        }
        public IWebElement GetUserNameTextBox_CreateAccount()
        {
            return _browser.GetElement("txtUserName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCheckAvailabilityLink()
        {
            return _browser.GetElement("lnkCheckAvailability", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUsernameAvailabilityPopUpMessage()
        {
            return _browser.GetElement("PopupContentText", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUsernameAvailabilityCloseButton()
        {
            return _browser.GetElement("basic-modal-close", WebBrowser.ElementSelectorType.ID);
        }
        public void CheckForUsernameAvailabilityAndDeleteExistingAccount()
        {
            string newUsername = ConfigurationManager.AppSettings["NewUsername"].ToString();
            GetUserNameTextBox_CreateAccount().SendKeys(newUsername);
            GetCheckAvailabilityLink().Click();
            _browser.SwitchtoCurrentWindow();
            string UsernameAvailability = GetUsernameAvailabilityPopUpMessage().Text;
            if (UsernameAvailability != "This username is available! It\r\nis now yours!")
            {
                _dataAccess = new WritersMuseDataAccess();
                _dataAccess.DeleteUserAccountInDB(newUsername);
            }
            GetUsernameAvailabilityCloseButton().Click();
            _browser.SwitchtoPreviousWindow();
            GetUserNameTextBox_CreateAccount().Clear();
        }
        public IWebElement GetFirstNameTextBox_CreateAccount()
        {
            return _browser.GetElement("MainContent_txtFirstName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetLastNameTextBox_CreateAccount()
        {
            return _browser.GetElement("MainContent_txtLastName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPasswordTextBox_CreateAccount()
        {
            return _browser.GetElement("txtPassword", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetConfirmPasswordTextBox_CreateAccount()
        {
            return _browser.GetElement("txtConfirmPassword", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEmailTextBox_CreateAccount()
        {
            return _browser.GetElement("txtEmail", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhoneTextBox_CreateAccount()
        {
            return _browser.GetElement("MainContent_txtPhone", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecurityAnswer1TextBox_CreateAccount()
        {
            return _browser.GetElement("MainContent_txtSecurityAnswer1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecurityAnswer2TextBox_CreateAccount()
        {
            return _browser.GetElement("MainContent_txtSecurityAnswer2", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmitButton_CreateAccount()
        {
            return _browser.GetElement("MainContent_imgSubmit", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecuirtyQuestionDropDown1_CreateAccount()
        {
            return _browser.GetElement("MainContent_ddlSecurityQuestion1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecuirtyQuestionDropDown2_CreateAccount()
        {
            return _browser.GetElement("MainContent_ddlSecurityQuestion2", WebBrowser.ElementSelectorType.ID);
        }
        public void FillAllTheRequiredFieldsAndCreateAccount()
        {
            //Arrange
            string newUsername = ConfigurationManager.AppSettings["NewUsername"].ToString();
            string firstName = ConfigurationManager.AppSettings["FirstName"].ToString();
            string lastName = ConfigurationManager.AppSettings["LastName"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            string confirmPassword = ConfigurationManager.AppSettings["ConfirmPassword"].ToString();
            string eMail = ConfigurationManager.AppSettings["Email"].ToString();
            string phone = ConfigurationManager.AppSettings["Phone"].ToString();
            string securityAnswer1 = ConfigurationManager.AppSettings["SecurityAnswer#1"].ToString();
            string securityAnswer2 = ConfigurationManager.AppSettings["SecurityAnswer#2"].ToString();
            int randomNumber1 = _random.Next(1, 5);
            int randomNumber2 = _random.Next(6, 10);
            SelectElement selectQuestion1 = new SelectElement(GetSecuirtyQuestionDropDown1_CreateAccount());
            SelectElement selectQuestion2 = new SelectElement(GetSecuirtyQuestionDropDown2_CreateAccount());


            //Act
            GetUserNameTextBox_CreateAccount().SendKeys(newUsername);
            GetFirstNameTextBox_CreateAccount().SendKeys(firstName);
            GetLastNameTextBox_CreateAccount().SendKeys(lastName);
            GetPasswordTextBox_CreateAccount().SendKeys(password);
            GetConfirmPasswordTextBox_CreateAccount().SendKeys(confirmPassword);
            GetEmailTextBox_CreateAccount().SendKeys(eMail);
            GetPhoneTextBox_CreateAccount().SendKeys(phone);
            selectQuestion1.SelectByValue(randomNumber1.ToString());
            GetSecurityAnswer1TextBox_CreateAccount().SendKeys(securityAnswer1);
            selectQuestion2.SelectByValue(randomNumber2.ToString());
            GetSecurityAnswer2TextBox_CreateAccount().SendKeys(securityAnswer2);
            GetSubmitButton_CreateAccount().Click();
        }
        public IWebElement GetAccountCreatedPopUP_CreateAccount()
        {
            return _browser.GetElement("MainContent_uctrlMessageBox_spnTitle", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCloseButtonOnAccountCreatedPopUP()
        {
            return _browser.GetElement("MainContent_uctrlMessageBox_divOkButton", WebBrowser.ElementSelectorType.ID);
        }
        public string GetAccountCreationHeaderText()
        {
            int preloaderWait = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"].ToString());
            _browser.SwitchtoCurrentWindow();
            string headerMessage = GetAccountCreatedPopUP_CreateAccount().Text;
            Thread.Sleep(preloaderWait);
            GetCloseButtonOnAccountCreatedPopUP().Click();
            return headerMessage;
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
            string username = ConfigurationManager.AppSettings["Username"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            int preloaderWait = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"].ToString());
            WaitForPageToLoad();
            IsSigninLinkVisible();
            GetSigninLink().Click();
            _browser.SwitchtoCurrentWindow();
            IsUsernameTextBoxVisible();
            GetUsernameTextBox().SendKeys(username);
            IsPasswordTextBoxVisible();
            GetPasswordTextBox().SendKeys(password);
            IsSignInButtonVisible();
            GetSignInButton().Click();
            Thread.Sleep(preloaderWait);
            _browser.SwitchtoPreviousWindow();
        }
        public void ActivateCreatedAccount()
        {
            string newUsername = ConfigurationManager.AppSettings["NewUsername"].ToString();
            bool isAccountActivated = true;
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.ActivateUserAccountInDB(newUsername,isAccountActivated);
        }
        public IWebElement GetAccountLink()
        {
            return _browser.GetElement("lnkAccount", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUsernameTextBox_Account()
        {
            return _browser.GetElement("txtUserName", WebBrowser.ElementSelectorType.ID);
        }
        public string GetUsernameAfterLogin()
        {
            GetAccountLink().Click();
            return GetUsernameTextBox_Account().GetAttribute("value");
        }
        public IWebElement GetCreateAccountLink_memberSignIn()
        {
            return _browser.GetElement("memberLogin_lnkbtnCreateAccount", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForCreateAccountFieldsToLoad()
        {
            return _browser.WaitForElement("txtUserName", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToMemberSignIn()
        {
            IsSigninLinkVisible();
            GetSigninLink().Click();
            _browser.SwitchtoCurrentWindow();
        }
        public void NavigateToCreateAccountFromMemberSignIn()
        {
            GetCreateAccountLink_memberSignIn().Click();
            _browser.SwitchtoPreviousWindow();
            WaitForCreateAccountFieldsToLoad();
        }
        public bool IsCreateAccountFieldsVisible()
        {
            return _browser.IsElementVisible("txtUserName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetForgotPasswordLink()
        {
            return _browser.GetElement("lnkForgotPassword", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForEmailTextBoxToLoad()
        {
            return _browser.WaitForElement("txtMemberLoginEmail", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToForgotMyPassword()
        {
            GetForgotPasswordLink().Click();
            _browser.SwitchtoPreviousWindow();
            WaitForEmailTextBoxToLoad();
        }
        public ResetPasswordFields GetResetpasswordFieldsDataFromDB()
        {
            string username = ConfigurationManager.AppSettings["Username"].ToString();
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetResetpasswordFieldsData(username);
        }
        public IWebElement GetEmailtextBox_MemberSignIn()
        {
            return _browser.GetElement("txtMemberLoginEmail", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecurityQuestiontextBox1_MemberSignIn()
        {
            return _browser.GetElement("ddlMemberLoginSecurityQuestion1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecurityAnswertextBox1_MemberSignIn()
        {
            return _browser.GetElement("txtMemberLoginSecurityAnswer1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecurityQuestiontextBox2_MemberSignIn()
        {
            return _browser.GetElement("ddlMemberLoginSecurityQuestion2", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSecurityAnswertextBox2_MemberSignIn()
        {
            return _browser.GetElement("txtMemberLoginSecurityAnswer2", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPasswordResetMessage_MemberSignIn()
        {
            return _browser.GetElement("#divLoginCheckEmailText > span", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetSubmitButton_Forgotpassword()
        {
            return _browser.GetElement("memberLogin_btnRecovery", WebBrowser.ElementSelectorType.ID);
        }
        public string GetForgotPasswordMessageAfterSubmitOfRequiredFields(ResetPasswordFields passwordFields)
        {
            int resetPasswordWait = Int32.Parse(ConfigurationManager.AppSettings["ResetPaswordWaitTime"].ToString());
            SelectElement selectQuestion1 = new SelectElement(GetSecurityQuestiontextBox1_MemberSignIn());
            SelectElement selectQuestion2 = new SelectElement(GetSecurityQuestiontextBox2_MemberSignIn());

            GetEmailtextBox_MemberSignIn().SendKeys(passwordFields.Email);
            selectQuestion1.SelectByValue(passwordFields.QuestionID1.ToString());
            GetSecurityAnswertextBox1_MemberSignIn().SendKeys(passwordFields.Answer1);
            selectQuestion2.SelectByValue(passwordFields.QuestionID2.ToString());
            GetSecurityAnswertextBox2_MemberSignIn().SendKeys(passwordFields.Answer2);
            GetSubmitButton_Forgotpassword().Click();
            Thread.Sleep(resetPasswordWait);
            return GetPasswordResetMessage_MemberSignIn().Text;
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
        public IWebElement GetMyPasswordDoesNotWorkLink()
        {
            return _browser.GetElement("lnkPasswordNotWork", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToMyPasswordDoesNotWork()
        {
            GetMyPasswordDoesNotWorkLink().Click();
            _browser.SwitchtoPreviousWindow();
            WaitForEmailTextBoxToLoad();
        }
        public IWebElement GetForgotUserNameLink()
        {
            return _browser.GetElement("lnkForgotUserName", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToForgotMyUserName()
        {
            GetForgotUserNameLink().Click();
            _browser.SwitchtoPreviousWindow();
            WaitForEmailTextBoxToLoad();
        }
        public IWebElement GetSubscribeLinkFromTopRight()
        {
            return _browser.GetElement("lnkLoggedOutSubscribe", WebBrowser.ElementSelectorType.ID);
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

            int [] codes = new[] { (int)ProductCodes.SpiceMobile, (int)ProductCodes.SpiceProfessional };
            int randomProductCode = codes[_random.Next(codes.Length)];

            int randomDurationCode = 0;
            if(randomProductCode == 1)
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
        public IWebElement GetDateOfBirthTextBox()
        {
            return _browser.GetElement("txtDateofBirth", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCreateButton_CheckoutPage()
        {
            return _browser.GetElement("imgCreateAccount", WebBrowser.ElementSelectorType.ID);
        }
        public void CreateNewUserAccountFromCheckoutPage()
        {
            //Arrange
            string newUsername = ConfigurationManager.AppSettings["NewUsername"].ToString();
            string firstName = ConfigurationManager.AppSettings["FirstName"].ToString();
            string lastName = ConfigurationManager.AppSettings["LastName"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            string confirmPassword = ConfigurationManager.AppSettings["ConfirmPassword"].ToString();
            string eMail = ConfigurationManager.AppSettings["Email"].ToString();
            string phone = ConfigurationManager.AppSettings["Phone"].ToString();
            string securityAnswer1 = ConfigurationManager.AppSettings["SecurityAnswer#1"].ToString();
            string securityAnswer2 = ConfigurationManager.AppSettings["SecurityAnswer#2"].ToString();
            string dateOfBirth = ConfigurationManager.AppSettings["DateOfBirth"].ToString();
            int randomNumber1 = _random.Next(1, 5);
            int randomNumber2 = _random.Next(6, 10);
            SelectElement selectQuestion1 = new SelectElement(GetSecuirtyQuestionDropDown1_CreateAccount());
            SelectElement selectQuestion2 = new SelectElement(GetSecuirtyQuestionDropDown2_CreateAccount());
            _browser.ExecuteJavaScriptToRemoveAttribute("txtDateofBirth");


            //Act
            GetUserNameTextBox_CreateAccount().SendKeys(newUsername);
            GetFirstNameTextBox_CreateAccount().SendKeys(firstName);
            GetLastNameTextBox_CreateAccount().SendKeys(lastName);
            GetPasswordTextBox_CreateAccount().SendKeys(password);
            GetConfirmPasswordTextBox_CreateAccount().SendKeys(confirmPassword);
            GetEmailTextBox_CreateAccount().SendKeys(eMail);
            GetDateOfBirthTextBox().Clear();
            GetDateOfBirthTextBox().SendKeys(dateOfBirth);
            GetPhoneTextBox_CreateAccount().SendKeys(phone);
            selectQuestion1.SelectByValue(randomNumber1.ToString());
            GetSecurityAnswer1TextBox_CreateAccount().SendKeys(securityAnswer1);
            selectQuestion2.SelectByValue(randomNumber2.ToString());
            GetSecurityAnswer2TextBox_CreateAccount().SendKeys(securityAnswer2);
        }
        public void ProceedToCreateAccount()
        {
            GetCreateButton_CheckoutPage().Click();
        }
        public void NavigateToCheckOutPage()
        {
            GetProceedButtonFromSubscribePage().Click();
            WaitForSelectProductFieldsToLoad();
        }
        public IWebElement GetSigninLinkFromCheckoutPage()
        {
            return _browser.GetElement("lnkCheckOutSignIn", WebBrowser.ElementSelectorType.ID);
        }
        public void LoginToWritersMuseFromCheckoutPage()
        {
            string username = ConfigurationManager.AppSettings["Username"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            int preloaderWait = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"].ToString());
            WaitForPageToLoad();
            NavigateToSubscribePage();
            NavigateToCheckOutPage();
            IsSigninLinkVisible();
            GetSigninLinkFromCheckoutPage().Click();
            _browser.SwitchtoCurrentWindow();
            IsUsernameTextBoxVisible();
            GetUsernameTextBox().SendKeys(username);
            IsPasswordTextBoxVisible();
            GetPasswordTextBox().SendKeys(password);
            IsSignInButtonVisible();
            GetSignInButton().Click();
            Thread.Sleep(preloaderWait);
            _browser.SwitchtoPreviousWindow();
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
        public bool IsPayPalCheckoutPageVisible()
        {
            return _browser.IsElementVisible("login", WebBrowser.ElementSelectorType.ID);
        }
        public void DeletePayPalInvoiceFromDB()
        {
            string newUsername = ConfigurationManager.AppSettings["NewUsername"].ToString();
            _dataAccess = new WritersMuseDataAccess();
            float spiceUserID = _dataAccess.GetSpiceUserIdForUserName(newUsername);
            _dataAccess.DeletePayPalInvoiceInDB(spiceUserID);
        }
        public IWebElement GetGiftSubscriptionsLink_LeftBanner()
        {
            return _browser.GetElement("LeftBanner_lnkGiftSubscriptions", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToGiftSubscriptionPage()
        {
            GetGiftSubscriptionsLink_LeftBanner().Click();
        }
        public ProductPrices GetSpiceProfessionalPricesFromDB()
        {
            int productCode = (int)ProductCodes.SpiceProfessional;
            int yearlyDuration = (int)SubscriptionDuration.Yearly;
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetGiftAndRegularProductPricesForProductCode(productCode, yearlyDuration);
        }
        public ProductPrices GetSpiceMobilePricesFromDB()
        {
            int productCode = (int)ProductCodes.SpiceMobile;
            int yearlyDuration = (int)SubscriptionDuration.Yearly;
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetGiftAndRegularProductPricesForProductCode(productCode, yearlyDuration);
        }
        public ProductPrices GetAllProductPricesGiftAndNormalFromDB()
        {
            ProductPrices spiceProfessionalPricesFromDB = GetSpiceProfessionalPricesFromDB();
            ProductPrices spiceMobilePricesFromDB = GetSpiceMobilePricesFromDB();
            ProductPrices productPricesGiftAndNormalFromDB = new ProductPrices
            {
                SpiceProfessionalPerYearDB = $"(Regular Price $ {spiceProfessionalPricesFromDB.SpiceProfessionalPerYearDB})",
                SpiceProfessionalPerYearDB_Gift = $"Now ${spiceProfessionalPricesFromDB.SpiceProfessionalPerYearDB_Gift}.00",
                SpiceMobilePerYearDB = $"(Regular Price $ {spiceMobilePricesFromDB.SpiceMobilePerYearDB})",
                SpiceMobilePerYearDB_Gift = $"Now ${spiceMobilePricesFromDB.SpiceMobilePerYearDB_Gift}0",
            };
            return productPricesGiftAndNormalFromDB;
        }
        public IWebElement GetSpiceProfessionalNormalPerYearUI()
        {
            return _browser.GetElement("MainContent_spnRegularPriceForSpicePro", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSpiceProfessionalGiftPerYearUI()
        {
            return _browser.GetElement("MainContent_spnDiscountedPriceForSpicePro", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSpiceMobileNormalPerYearUI()
        {
            return _browser.GetElement("MainContent_spnRegularPriceForSpiceMobile", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSpiceMobileGiftPerYearUI()
        {
            return _browser.GetElement("MainContent_spnDiscountedPriceForSpiceMobile", WebBrowser.ElementSelectorType.ID);
        }
        public ProductPrices GetAllProductPricesGiftAndNormalFromUI()
        {
            ProductPrices productPricesGiftAndNormalFromUI = new ProductPrices
            {
                SpiceProfessionalPerYearUI = GetSpiceProfessionalNormalPerYearUI().Text,
                SpiceProfessionalPerYearUI_Gift = GetSpiceProfessionalGiftPerYearUI().Text,
                SpiceMobilePerYearUI = GetSpiceMobileNormalPerYearUI().Text,
                SpiceMobilePerYearUI_Gift = GetSpiceMobileGiftPerYearUI().Text
            };
            return productPricesGiftAndNormalFromUI;
        }
        public IWebElement GetHowManyOptionBox()
        {
            return _browser.GetElement("MainContent_ddlNoOfProducts", WebBrowser.ElementSelectorType.ID);
        }
        public int SelectRandomSubscriptionTypeAndNumberOfProducts()
        {
            int[] codes = new[] { (int)ProductCodes.SpiceMobile, (int)ProductCodes.SpiceProfessional };
            int codesLength = codes.Length;
            int randomLength = _random.Next(codesLength);
            int randomProductCode = codes[randomLength];

            int randomNumberOfProducts = _random.Next(1, 5);

            SelectElement selectProductName = new SelectElement(GetProductNameOptionBox());
            selectProductName.SelectByValue(randomProductCode.ToString());
            SelectElement selectDuration = new SelectElement(GetHowManyOptionBox());
            selectDuration.SelectByValue(randomNumberOfProducts.ToString());
            return randomNumberOfProducts;
        }
        public IWebElement GetAllRecipientFields()
        {
            return _browser.GetElement("MainContent_noOfRecipients", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForRecipientFieldsToLoad()
        {
            return _browser.WaitForElement("MainContent_upnlDropdowns", WebBrowser.ElementSelectorType.ID);
        }
        public int GetNumberOfRecipientFieldsDisplayed()
        {
            WaitForRecipientFieldsToLoad();
            return Int32.Parse(GetAllRecipientFields().GetAttribute("childElementCount"));
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
        public IWebElement GetSenderFirstName()
        {
            return _browser.GetElement("MainContent_txtGiftSenderFirstName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSenderLastName()
        {
            return _browser.GetElement("MainContent_txtGiftSenderLastName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSenderEmail()
        {
            return _browser.GetElement("MainContent_txtGiftSenderEmail", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRecipientFirstName(int recipientCount)
        {
            return _browser.GetElement($"MainContent_rptrRecipients_txtFirstName_{recipientCount}", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRecipientLastName(int recipientCount)
        {
            return _browser.GetElement($"MainContent_rptrRecipients_txtLastName_{recipientCount}", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRecipientEmail(int recipientCount)
        {
            return _browser.GetElement($"MainContent_rptrRecipients_txtRecipientEmail_{recipientCount}", WebBrowser.ElementSelectorType.ID);
        }
        public void EnterUserAndRecipientDetails(int numberOfRecipients)
        {
            string firstName = ConfigurationManager.AppSettings["FirstName"].ToString();
            string lastName = ConfigurationManager.AppSettings["LastName"].ToString();
            string eMail = ConfigurationManager.AppSettings["Email"].ToString();
            GetSenderFirstName().SendKeys(firstName);
            GetSenderLastName().SendKeys(lastName);
            GetSenderEmail().SendKeys(eMail);
            for (int i=0; i < numberOfRecipients; i++)
            {
                GetRecipientFirstName(i).SendKeys(GetRandomStringOfRequiredLength(5));
                GetRecipientLastName(i).SendKeys(GetRandomStringOfRequiredLength(5));
                GetRecipientEmail(i).SendKeys($"{GetRandomStringOfRequiredLength(10)}@gmail.com");
            }
        }
        public IWebElement GetProceedButton_GiftSubscriptions()
        {
            return _browser.GetElement("MainContent_imgProceed", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToPayPalLogInPage_GiftSubscriptions()
        {
            GetProceedButton_GiftSubscriptions().Click();
            WaitForPayPalLoginPage();
        }
        public IWebElement GetRedeemGiftLink_LeftBanner()
        {
            return _browser.GetElement("LeftBanner_lnkRedeemGift", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToRedeemGiftPage()
        {
            GetRedeemGiftLink_LeftBanner().Click();
            WaitForSelectProductFieldsToLoad();
        }

        public string GetRandomSubscriptionTypeSelected()
        {
            string subscriptionType = null;

            int[] codes = new[] { (int)ProductCodes.SpiceMobile, (int)ProductCodes.SpiceProfessional };
            int randomProductCode = codes[_random.Next(codes.Length)];

            SelectElement selectProductName = new SelectElement(GetProductNameOptionBox());
            selectProductName.SelectByValue(randomProductCode.ToString());

            if(randomProductCode == 1)
            {
                subscriptionType = "Spice Mobile";
            }
            else
            {
                subscriptionType = "Spice Professional";
            }
            return subscriptionType;
        }
        public bool IsSignInButtonVisible_Checkout()
        {
            return _browser.IsElementVisible("lnkCheckOutSignIn", WebBrowser.ElementSelectorType.ID);
        }
        public string GetValidGiftCodeForSubscriptionType(string subscriptionType)
        {
            string giftCode = null;

            _dataAccess = new WritersMuseDataAccess();
            if (subscriptionType == "Spice Mobile")
            {
               giftCode = _dataAccess.GenerateGiftCodesForProduct((int)ProductCodes.SpiceMobile); 
            }
            else
            {
                giftCode = _dataAccess.GenerateGiftCodesForProduct((int)ProductCodes.SpiceProfessional);
            }
            return giftCode;
        }
        public IWebElement GetRedeemGiftTextBox()
        {
            return _browser.GetElement("MainContent_txtRedeemGift", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCreateAccountAndActivationButton()
        {
            return _browser.GetElement("btnCreateAndActivateButton", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCreateAccountAndActivationMessage()
        {
            return _browser.GetElement("MainContent_uctrlAccountAndSubscriptionCreation_spnTitle", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetOkButton_ActivationMessage()
        {
            return _browser.GetElement("MainContent_uctrlAccountAndSubscriptionCreation_anchorOk", WebBrowser.ElementSelectorType.ID);
        }
        public string EnterGiftCodeAndRedeem(string giftCode)
        {
            int preloaderWait = Int32.Parse(ConfigurationManager.AppSettings["RedeemGiftWaitTime"].ToString());
            string activationMessage = null;
            GetRedeemGiftTextBox().SendKeys(giftCode);
            GetCreateAccountAndActivationButton().Click();
            Thread.Sleep(preloaderWait);
            _browser.SwitchtoCurrentWindow();
            activationMessage = GetCreateAccountAndActivationMessage().Text;
            GetOkButton_ActivationMessage().Click();
            return activationMessage;
        }
        public bool IsSignInLinkVisible_Checkout()
        {
            return _browser.IsElementVisible("//a[@class=\"signInLinkText\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public void SelectSignInButton_CheckOut()
        {
            GetSigninLinkFromCheckoutPage().Click();
        }
        public IWebElement GetSignInLink_CheckOut()
        {
            return _browser.GetElement("//a[@class=\"signInLinkText\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public void SelectSignInLink_CheckOut()
        {
            GetSignInLink_CheckOut().Click();
        }
        public bool IsMemberSignInVisible_Checkout()
        {
            return _browser.IsElementVisible("divMemberSignInHeader", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCloseButton_memberSignIn()
        {
            return _browser.GetElement("divCloseBtn", WebBrowser.ElementSelectorType.ID);
        }
        public bool CheckMemberSignInBox()
        {
            bool isMemberSignInVisible = false;
            _browser.SwitchtoCurrentWindow();
            isMemberSignInVisible = IsMemberSignInVisible_Checkout();
            GetCloseButton_memberSignIn().Click();
            _browser.SwitchtoPreviousWindow();
            return isMemberSignInVisible;
        }
        public void DeleteRedeemedSubscription(string giftCode)
        {            
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeleteRedeemedSubscriptionFromDB(giftCode);
        }
        public IWebElement GetContactLink()
        {
            return _browser.GetElement("lnkContact", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToContactUsPage()
        {
            GetContactLink().Click();
        }
        public IWebElement GetEmailTextBox_Contact()
        {
            return _browser.GetElement("MainContent_txtEmail", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetFirstNameTextBox_Contact()
        {
            return _browser.GetElement("MainContent_txtFname", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetLastNameTextBox_Contact()
        {
            return _browser.GetElement("MainContent_txtLName", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhoneTextBox_Contact()
        {
            return _browser.GetElement("MainContent_txtPhone", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForPhoneTextBox_Contact()
        {
            return _browser.WaitForElement("MainContent_txtPhone", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhoneOptionButton_Contact()
        {
            return _browser.GetElement("MainContent_rdophone", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetHelpDropDown_Contact()
        {
            return _browser.GetElement("MainContent_ddlHelp", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetHelpTextBox_Contact()
        {
            return _browser.GetElement("MainContent_txtHelp", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmitButton_Contact()
        {
            return _browser.GetElement("imbContact", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCloseButton_Contact()
        {
            return _browser.GetElement("basic-modal-close", WebBrowser.ElementSelectorType.ID);
        }
        public List<string> SelectContactPreferenceAndEnterAllDetails()
        {
            List<string> contactDetails = new List<string>();

            int randomSelection = _random.Next(1, 2);
            int randomHelpTopic = _random.Next(6, 15);

            string email = ConfigurationManager.AppSettings["Email"].ToString();
            string phone = ConfigurationManager.AppSettings["Phone"].ToString();
            string firstName = ConfigurationManager.AppSettings["FirstName"].ToString();
            string lastName = ConfigurationManager.AppSettings["LastName"].ToString();
            string helpText = ConfigurationManager.AppSettings["HelpText"].ToString();


            if (randomSelection == 1)
            {
                GetEmailTextBox_Contact().SendKeys(email);
                contactDetails.AddRange(new List<string>() { email, firstName, lastName, randomHelpTopic.ToString(), helpText });
            }
            else
            {
                GetPhoneOptionButton_Contact().Click();
                WaitForPhoneTextBox_Contact();
                GetPhoneTextBox_Contact().SendKeys(phone);
                contactDetails.AddRange(new List<string>() { phone, firstName, lastName, randomHelpTopic.ToString(), helpText });
            }
            FillAllRemainingFieldsOfContactPage(randomHelpTopic, firstName, lastName, helpText);
            _browser.SwitchtoCurrentWindow();
            GetCloseButton_Contact().Click();

            return contactDetails;
        }
        public void FillAllRemainingFieldsOfContactPage(int randomHelpTopic,string firstName,string lastName,string helpText)
        {
            GetFirstNameTextBox_Contact().SendKeys(firstName);
            GetLastNameTextBox_Contact().SendKeys(lastName);
            SelectElement selectHelpTopic = new SelectElement(GetHelpDropDown_Contact());
            selectHelpTopic.SelectByValue(randomHelpTopic.ToString());
            GetHelpTextBox_Contact().SendKeys(helpText);
            GetSubmitButton_Contact().Click();
        }
        public List<string> GetContactUSDetailsFromDB()
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetRecentlySubmittedContactUsInfo();
        }
        public void DeleteRecentlyAddedContactUsEntry()
        {
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeleteNewlySubmittedContactUsEntry();
        }
        public IWebElement GetNoOptionButton_Thoughts()
        {
            return _browser.GetElement("MainContent_rdono", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPrettyGoodOptionButton_Thoughts()
        {
            return _browser.GetElement("MainContent_rdoPrettygood", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetAverageOptionButton_Thoughts()
        {
            return _browser.GetElement("MainContent_rdoAvg", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPoorOptionButton_Thoughts()
        {
            return _browser.GetElement("MainContent_rdoPoor", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetNewsLetterCheckBox_Thoughts()
        {
            return _browser.GetElement("MainContent_chkJoinnews", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEmailTextBox_Thoughts()
        {
            return _browser.GetElement("MainContent_txtThotEmail", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSubmitButton_Thoughts()
        {
            return _browser.GetElement("imbThts", WebBrowser.ElementSelectorType.ID);
        }
        public void SelectSuitableRating(SiteContentRating rating)
        {
            switch (rating)
            {
                case SiteContentRating.Excellent:
                    //No action as this is selected by default
                    break;
                case SiteContentRating.PrettyGood:
                    GetPrettyGoodOptionButton_Thoughts().Click();
                    break;
                case SiteContentRating.Average:
                    GetAverageOptionButton_Thoughts().Click();
                    break;
                case SiteContentRating.Poor:
                    GetPoorOptionButton_Thoughts().Click();
                    break;
            }
        }
        public List<string> SelectAllThoughtsAndSubmit()
        {
            List<string> thoughts = new List<string>();
            string email = ConfigurationManager.AppSettings["Email"].ToString();

            int randomSelection = _random.Next(1, 2);
            if(randomSelection == 1)
            {
                GetNoOptionButton_Thoughts().Click();
                GetNewsLetterCheckBox_Thoughts().Click();
                thoughts.Add("False");  //for no option button selection condition is false
                thoughts.Add("False");  //for Unchecked newsletter condition is false
            }
            else
            {
                //No actions as newsletter is checked and yes option button is selected
                thoughts.Add("False");  //for yes option button selection condition is true
                thoughts.Add("False");  //for Checked newsletter condition is true
            }

            int randomRatingOfSiteContent = _random.Next(1, 4);

            thoughts.Add(randomRatingOfSiteContent.ToString());
            thoughts.Add(email);

            SiteContentRating rating = (SiteContentRating)randomRatingOfSiteContent;
            SelectSuitableRating(rating);
            GetEmailTextBox_Thoughts().SendKeys(email);
            GetSubmitButton_Thoughts().Click();
            
            return thoughts;
        }
        public List<string> GetThoughtsNewlySubmittedFromDB()
        {
            _dataAccess = new WritersMuseDataAccess();
            return _dataAccess.GetRecentlySubmittedThoughtsInfo();
        }
        public void DeleteRecentlyAddedThoughtsEntry()
        {
            _dataAccess = new WritersMuseDataAccess();
            _dataAccess.DeleteNewlySubmittedThoughtsEntry();
        }
        public IWebElement GetSellYourPhrasesLink()
        {
            return _browser.GetElement("LeftBanner_sellypurphrases", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToPhraseSubmissionsPage()
        {
            GetSellYourPhrasesLink().Click();
        }
        public bool IsPhraseSubmissionsHeaderVisible()
        {
            return _browser.IsElementVisible("//div[@class=\"col1\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public bool IsStandardsOfSubmissionsHeaderVisible()
        {
            return _browser.IsElementVisible("divStandardsOfSubmissionsHeading", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsFAQForSubmissionsHeaderVisible()
        {
            return _browser.IsElementVisible("divFAQForSubmissionsHeading", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsPhraseSubmissionsContainerVisible()
        {
            return _browser.IsElementVisible("divPhraseSubmissionsInfo", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsStandardsOfSubmissionsContainerVisible()
        {
            return _browser.IsElementVisible("MainContent_divStandardsOfSubmissions", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsFAQForSubmissionsContainerVisible()
        {
            return _browser.IsElementVisible("divFAQForSubmissions", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsProceedButtonVisible_PhraseSubmissions()
        {
            return _browser.IsElementVisible("divPhraseSubmissionInfoProceedButton", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetProceedLink_PhraseSubmissions()
        {
            return _browser.GetElement("divPhraseSubmissionInfoProceedButton", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToPhraseSubmissionsStepsPage()
        {
            GetProceedLink_PhraseSubmissions().Click();
        }
        public bool IsPhraseSubmissionsContentVisible()
        {
            return _browser.IsElementVisible("divPhraseSubmissionStepsContent", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsSigninButtonVisible_PhraseSubmissions()
        {
            return _browser.IsElementVisible("divSigninbtn", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsCreateAccountButtonVisible_PhraseSubmissions()
        {
            return _browser.IsElementVisible("divCreateAccountbtn", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSigninButton_PhraseSubmissions()
        {
            return _browser.GetElement("divSigninbtn", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCreateAccountButton_PhraseSubmissions()
        {
            return _browser.GetElement("divCreateAccountbtn", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhraseRate_PhraseSubmissions()
        {
            return _browser.GetElement("MainContent_spnCurrentRate", WebBrowser.ElementSelectorType.ID);
        }
        public string GetCurrentRateForPhraseUI()
        {
            return GetPhraseRate_PhraseSubmissions().Text;
        }
        public string GetCurrentPhraseRateFromDB()
        {
            _dataAccess = new WritersMuseDataAccess();
            return $"${_dataAccess.GetCurrentPhraseRate()}";
        }
        public void NavigateToMemberSiginFromPhraseSubmissions()
        {
            GetSigninButton_PhraseSubmissions().Click();
            _browser.SwitchtoCurrentWindow();
        }
        public void NavigateToCreateAccountFromPhraseSubmissions()
        {
            GetCreateAccountButton_PhraseSubmissions().Click();
        }
    }
}
