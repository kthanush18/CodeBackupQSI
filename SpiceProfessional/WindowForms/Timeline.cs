using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.WindowsUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms
{
    public class Timeline : WindowForm
    {
        protected static SearchKeywordDataAccess _dataAccess;
        public int _waitForWordDocumentToLoad = Int32.Parse(ConfigurationManager.AppSettings["WaitTimeForWordDocument"].ToString());
        public string _timelineDocumentLocation = ConfigurationManager.AppSettings["TimelineDocumentLocation"].ToString();
        public Timeline(WindowUIDriver window) : base(window)
        {

        }
        public string GetRandomWord()
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetRandomWord();
        }
        public void WaitForHomeWindowToLoad()
        {
            _windowUIDriver.WaitForWindowsElement("picbxPhrasesTab", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetSearchKeywordTextBox()
        {
            return _windowUIDriver.GetElement("SpiceWPFTextBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForOneKeywordSuggestionToLoad()
        {
            _windowUIDriver.WaitForWindowsElement(" Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public void EnterLettersIntoTextBoxWaitForSurroundingWords(string RandomLetter)
        {
            WaitForHomeWindowToLoad();
            GetSearchKeywordTextBox().Clear();
            GetSearchKeywordTextBox().SendKeys(RandomLetter);
            WaitForOneKeywordSuggestionToLoad();
        }
        public WindowsElement GetSearchButton()
        {
            return _windowUIDriver.GetElement("picbxGo", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForOnePhraseToLoad()
        {
            _windowUIDriver.WaitForWindowsElement("Phrase Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public List<WindowsElement> TimelineElements()
        {
            return _windowUIDriver.GetElements("Timeline", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SearchForKeywordAndOpenTimelineWindow(string randomWordFromDB)
        {
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
            List<WindowsElement> timelineElements = TimelineElements();
            WindowsElement TimelineLinkElement = timelineElements.Last();
            TimelineLinkElement.Click();
            _windowUIDriver.SwitchToFirstWindow();
        }
        public WindowsElement BlankDocumentElement()
        {
            return _windowUIDriver.GetWordElement("AIOStartDocument", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement SaveButtonElement()
        {
            return _windowUIDriver.GetWordElement("Save", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement DocumentsElement()
        {
            return _windowUIDriver.GetWordElement("Desktop", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement FileNameElement()
        {
            return _windowUIDriver.GetWordElement("Edit", WindowUIDriver.ElementSelectorType.ClassName);
        }
        public void WaitForCloseButton()
        {
            _windowUIDriver.WaitForWordElement("Close", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement FileNameSaveButtonElement()
        {
            return _windowUIDriver.GetWordElement("1", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForWordDocumentHomeToLoad()
        {
            _windowUIDriver.WaitForWordElement("Home", WindowUIDriver.ElementSelectorType.Name);
        }
        public int SaveAnEmptyDocumentAndGetSize()
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            WindowsDriver<WindowsElement> wordDriver = _windowUIDriver.OpenWordDocument();
            //As splash screen having no controls for using explicit wait time. Implicit wait time is used after launching application.
            Thread.Sleep(_waitForWordDocumentToLoad);
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            BlankDocumentElement().Click();
            SaveButtonElement().Click();
            DocumentsElement().Click();
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            FileNameElement().SendKeys("timeline");
            FileNameSaveButtonElement().Click();
            WaitForWordDocumentHomeToLoad();
            FileInfo timelineDocument = new FileInfo(_timelineDocumentLocation);
            return Convert.ToInt32(timelineDocument.Length) / 1024;
        }
        public void WaitForInsertButton()
        {
            _windowUIDriver.WaitForWindowsElement("picbxInsert", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<WindowsElement> InsertButtonElements()
        {
            return _windowUIDriver.GetElements("picbxInsert", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForConfirmation()
        {
            _windowUIDriver.WaitForWindowsElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetOkButton()
        {
            return _windowUIDriver.GetElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetCloseButtonForWord()
        {
            return _windowUIDriver.GetWordElement("Close", WindowUIDriver.ElementSelectorType.Name);
        }
        public int SaveTimelineDocumentAfterInsertionAndGetSize()
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            _windowUIDriver.SwitchToGivenWindow(indexOfLoginWindow);
            WaitForInsertButton();
            List<WindowsElement> insertButtonElements = InsertButtonElements();
            WindowsElement ActiveInsertElement = insertButtonElements.Last();
            ActiveInsertElement.Click();
            WaitForConfirmation();
            GetOkButton().Click();
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            SaveButtonElement().Click();
            GetCloseButtonForWord().Click();
            FileInfo timelineDocument = new FileInfo(_timelineDocumentLocation);
            return Convert.ToInt32(timelineDocument.Length) / 1024;
        }
        public void DeleteCreatedDocument()
        {
            File.Delete(_timelineDocumentLocation);
        }
    }

}
