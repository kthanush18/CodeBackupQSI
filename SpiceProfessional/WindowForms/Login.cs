using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Test.UI.Common.WindowsUI;
using System;
using System.Configuration;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms
{
    public class Login : WindowForm
    {
        public Login(WindowUIDriver window) : base(window)
        {

        }

        protected static string _username = ConfigurationManager.AppSettings["Username"].ToString();
        protected static string _password = ConfigurationManager.AppSettings["Password"].ToString();
        public int _waitTimeForLoginWindow = Int32.Parse(ConfigurationManager.AppSettings["WaitTimeForLoginWindow"].ToString());

        public void OpenWindowAndLoginIntoSpice()
        {
            //Among the list of window handles current window index will be 0 which is login window
            int indexOfLoginWindow = 0;
            _windowUIDriver.SwitchToGivenWindow(indexOfLoginWindow);
            LoginIntoSpice(_username, _password);
        }
        public WindowsElement GetUsernameTextBox()
        {
            return _windowUIDriver.GetElement("SpiceWPFTextBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetPasswordTextBox()
        {
            return _windowUIDriver.GetElement("SpiceWPFPasswordBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetEnterButton()
        {
            return _windowUIDriver.GetElement("picbxEnter", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetOkButton()
        {
            return _windowUIDriver.GetElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForLoginWindow()
        {
            _windowUIDriver.WaitForWindowsElement("picbxSubscribe", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForConfirmation()
        {
            _windowUIDriver.WaitForWindowsElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void LoginIntoSpice(string _username, string _password)
        {
            WaitForLoginWindow();
            GetUsernameTextBox().SendKeys(_username);
            GetPasswordTextBox().SendKeys(_password);
            GetEnterButton().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
    }
}
