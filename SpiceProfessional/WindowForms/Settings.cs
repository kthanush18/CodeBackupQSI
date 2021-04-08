using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Core.SpiceThesaurus;
using Quant.Spice.ServiceAccess;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.WindowsUI;
using Quant.Spice.Windows.SpiceWordPrinter;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms
{
    public class Settings : WindowForm
    {
        protected static SearchKeywordDataAccess _dataAccess;
        Random _random = new Random();
        public int _waitForWordDocumentToLoad = Int32.Parse(ConfigurationManager.AppSettings["WaitTimeForWordDocument"].ToString());
        public string _settingsDocumentLocation = ConfigurationManager.AppSettings["SettingsDocumentLocation"].ToString();
        public string _settingsXmlLocation = ConfigurationManager.AppSettings["SettingsDocumentXmlLocation"].ToString();
        public Settings(WindowUIDriver window) : base(window)
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
        public WindowsElement GetPhrasesContainer()
        {
            return _windowUIDriver.GetElement("qsgridPhrases", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<AppiumWebElement> GetPhrasesList(WindowsElement SurroundingWordsContainer)
        {
            return _windowUIDriver.GetAppiumElements("DataItem", WindowUIDriver.ElementSelectorType.TagName, SurroundingWordsContainer);
        }
        public WindowsElement SettingsTabElement()
        {
            return _windowUIDriver.GetElement("picbxSettingsTab", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetRandomPhraseElementAndOpenSettingTab(string randomWordFromDB)
        {
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
            List<WindowsElement> phrasesWindowsElements = new List<WindowsElement>();
            WindowsElement phrasesContainer = GetPhrasesContainer();
            List<AppiumWebElement> phrasesAppiumElements = GetPhrasesList(phrasesContainer);
            foreach (WindowsElement element in phrasesAppiumElements)
            {
                phrasesWindowsElements.Add(element);
            }
            int number = _random.Next(1, phrasesWindowsElements.Count);
            SettingsTabElement().Click();
            return phrasesWindowsElements[number-1];
        }
        public string GetModifiedTextForAssertion(WindowsElement randomPhraseWindowsElement)
        {
            return randomPhraseWindowsElement.Text.Replace(" \r\n      ", "").Replace("\r\n      ", "");
        }
        public WindowsElement OffOptionButtonElement()
        {
            return _windowUIDriver.GetElement("off", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement SaveButtonElement()
        {
            return _windowUIDriver.GetElement("picbxSaveSettings", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForConfirmation()
        {
            _windowUIDriver.WaitForWindowsElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetOkButton()
        {
            return _windowUIDriver.GetElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void TurnFootnotingOFF()
        {
            OffOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public AppiumWebElement GetScrollBarElement(WindowsElement container)
        {
            return _windowUIDriver.GetAppiumElement("PageDown", WindowUIDriver.ElementSelectorType.ID, container);
        }
        public WindowsElement BlankDocumentElement()
        {
            return _windowUIDriver.GetWordElement("AIOStartDocument", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetPhrasesTab()
        {
            return _windowUIDriver.GetElement("picbxPhrasesTab", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForInsertButton()
        {
            _windowUIDriver.WaitForWindowsElement("picbxInsert", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement InsertButton()
        {
            return _windowUIDriver.GetElement("picbxInsert", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetWordDocument()
        {
            return _windowUIDriver.GetWordElement("Page 1 content", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement GetCloseButton()
        {
            return _windowUIDriver.GetWordElement("Close", WindowUIDriver.ElementSelectorType.Name);
        }
        public string InsertPhraseToDocumentAndGetInsertedText(WindowsElement randomPhraseWindowsElement)
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            GetPhrasesTab().Click();
            while (randomPhraseWindowsElement.Coordinates.LocationInDom.Y > 824)
            {
                WindowsElement phrasesContainer = GetPhrasesContainer();
                AppiumWebElement scrollBar = GetScrollBarElement(phrasesContainer);
                scrollBar.Click();
            }
            randomPhraseWindowsElement.Click();
            WindowsDriver<WindowsElement> wordDriver = _windowUIDriver.OpenWordDocument();
            //As splash screen having no controls for using explicit wait time. Implicit wait time is used after launching application.
            Thread.Sleep(_waitForWordDocumentToLoad);
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            BlankDocumentElement().Click();
            _windowUIDriver.SwitchToGivenWindow(indexOfLoginWindow);
            WaitForInsertButton();
            InsertButton().Click();
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            string wordText = GetWordDocument().Text.Substring(0, GetWordDocument().Text.LastIndexOf("\"") + 1).Replace(" \"", "").Replace("\"", "");
            GetCloseButton().Click();
            _windowUIDriver.ClickRightArrowButtonAndEnter();
            return wordText;
        }
        public WindowsElement InTextCheckBoxElement()
        {
            return _windowUIDriver.GetElement("In-Text", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement EndOfThePageCheckBoxElement()
        {
            return _windowUIDriver.GetElement("End of Page", WindowUIDriver.ElementSelectorType.Name);
        }
        public void UncheckInTextCheckBoxAndCheckEndOfPageBox()
        {
            InTextCheckBoxElement().Click();
            EndOfThePageCheckBoxElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public WindowsElement GetEndOfThePageText()
        {
            return _windowUIDriver.GetWordElement("_WwG", WindowUIDriver.ElementSelectorType.ClassName);
        }
        public WindowsElement GetScrollBarDownElement()
        {
            return _windowUIDriver.GetWordElement("page down", WindowUIDriver.ElementSelectorType.Name);
        }
        public void WaitForScrollBarElement()
        {
            _windowUIDriver.WaitForWordElement("page down", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement WordSaveButtonElement()
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

        public XmlDocument InsertPhraseToDocumentAndGetWordDocumentXml(WindowsElement randomPhraseWindowsElement)
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            GetPhrasesTab().Click();
            while (randomPhraseWindowsElement.Coordinates.LocationInDom.Y > 824)
            {
                WindowsElement phrasesContainer = GetPhrasesContainer();
                AppiumWebElement scrollBar = GetScrollBarElement(phrasesContainer);
                scrollBar.Click();
            }
            randomPhraseWindowsElement.Click();
            WindowsDriver<WindowsElement> wordDriver = _windowUIDriver.OpenWordDocument();
            //As splash screen having no controls for using explicit wait time. Implicit wait time is used after launching application.
            Thread.Sleep(_waitForWordDocumentToLoad);
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            BlankDocumentElement().Click();
            _windowUIDriver.SwitchToGivenWindow(indexOfLoginWindow);
            WaitForInsertButton();
            InsertButton().Click();
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            WordSaveButtonElement().Click();
            DocumentsElement().Click();
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            FileNameElement().SendKeys("settings");
            FileNameSaveButtonElement().Click();
            WaitForWordDocumentHomeToLoad();
            GetCloseButton().Click();
            Document wordDocument = new Document();
            wordDocument.LoadFromFile(_settingsDocumentLocation);
            wordDocument.SaveToFile(_settingsXmlLocation, FileFormat.WordML);
            XmlDocument WordXmlDocument = new XmlDocument();
            WordXmlDocument.Load(_settingsXmlLocation);
            return WordXmlDocument;
        }
        public XmlNamespaceManager AddXmlNamespacemanager(XmlDocument WordXmlDocument)
        {
            XmlNamespaceManager nameSpaceManager = new XmlNamespaceManager(WordXmlDocument.NameTable);
            nameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
            nameSpaceManager.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");
            return nameSpaceManager;
        }
        public string GetFootnoteFromXmlDocument(XmlDocument WordXmlDocument)
        {
            XmlNamespaceManager nameSpaceManager = AddXmlNamespacemanager(WordXmlDocument);
            XmlNode footNoteXmlNode = WordXmlDocument.SelectSingleNode("//w:wordDocument//w:body//wx:sect//w:p//w:r//w:footnote", nameSpaceManager);
            string wordText = footNoteXmlNode.InnerText.Trim();
            return wordText;
        }
        public WindowsElement GetSourcesContainer()
        {
            return _windowUIDriver.GetElement("qsgridSources", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<AppiumWebElement> GetSourcesList(WindowsElement SurroundingWordsContainer)
        {
            return _windowUIDriver.GetAppiumElements("DataItem", WindowUIDriver.ElementSelectorType.TagName, SurroundingWordsContainer);
        }
        public string GetSourceTextForAssertionFromUI()
        {
            List<string> sourcesFromWindow = new List<string>();
            WindowsElement sourcesContainer = GetSourcesContainer();
            List<AppiumWebElement> sourcesAppiumElements = GetSourcesList(sourcesContainer);
            foreach (AppiumWebElement sourceAppiumElement in sourcesAppiumElements)
            {
                sourcesFromWindow.Add(sourceAppiumElement.Text.Replace("\r\n", "").Replace("<i>", "").Replace("</i>", "").Replace("  ", " ").Replace("  ", " ").Replace(" \"", "").Replace("\"", ""));
            }
            return sourcesFromWindow[0];
        }
        public WindowsElement ItalicOptionButtonElement()
        {
            return _windowUIDriver.GetElement("Italic", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectItalicOptionButtonFromFontStyle()
        {
            ItalicOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public string GetFontStyleFromXmlDocument(XmlDocument WordXmlDocument)
        {
            string fontStyle = "";
            XmlNamespaceManager nameSpaceManager = AddXmlNamespacemanager(WordXmlDocument);
            XmlNode FontstyleXmlNode = WordXmlDocument.SelectSingleNode("//w:wordDocument//w:body//wx:sect//w:p//w:r//w:rPr//w:i", nameSpaceManager);
            if (FontstyleXmlNode != null)
            {
                string wordText = FontstyleXmlNode.LocalName;
                if (wordText == "i")
                {
                    fontStyle = "Italic";
                }
            }
            else
            {
                fontStyle = "Normal";
            }
            return fontStyle;
        }
        public WindowsElement BoldOptionButtonElement()
        {
            return _windowUIDriver.GetElement("Bold", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectBoldOptionButtonFromFontStyle()
        {
            BoldOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public string GetFontStyleBoldFromXmlDocument(XmlDocument WordXmlDocument)
        {
            string fontStyle = "";
            XmlNamespaceManager nameSpaceManager = AddXmlNamespacemanager(WordXmlDocument);
            XmlNode FontstyleXmlNode = WordXmlDocument.SelectSingleNode("//w:wordDocument//w:body//wx:sect//w:p//w:r//w:rPr//w:b", nameSpaceManager);
            if (FontstyleXmlNode != null)
            {
                string wordText = FontstyleXmlNode.LocalName;
                if (wordText == "b")
                {
                    fontStyle = "Bold";
                }
            }
            else
            {
                fontStyle = "Normal";
            }
            return fontStyle;
        }
        public WindowsElement NormalOptionButtonElement()
        {
            return _windowUIDriver.GetElement("Normal", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectNormalOptionButtonFromFontStyle()
        {
            NormalOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public string GetFontStyleNormalFromXmlDocument(XmlDocument WordXmlDocument)
        {
            string fontStyle = "";
            XmlNamespaceManager nameSpaceManager = AddXmlNamespacemanager(WordXmlDocument);
            XmlNode FontstyleItalicXmlNode = WordXmlDocument.SelectSingleNode("//w:wordDocument//w:body//wx:sect//w:p//w:r//w:rPr//w:i", nameSpaceManager);
            XmlNode FontstyleBoldXmlNode = WordXmlDocument.SelectSingleNode("//w:wordDocument//w:body//wx:sect//w:p//w:r//w:rPr//w:b", nameSpaceManager);
            if (FontstyleItalicXmlNode == null && FontstyleBoldXmlNode == null)
            {
                fontStyle = "Normal";
            }
            return fontStyle;
        }
        public WindowsElement GetFirstPhraseElement()
        {
            return _windowUIDriver.GetElement("Phrase Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement SearchForKeywordAndGetPhraseElement(string randomWordFromDB)
        {
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
            return GetFirstPhraseElement();
        }
        public int GetFontSizeForPhraseElement(WindowsElement phraseWindowsElement)
        {
            string boundingRectangleParameters = phraseWindowsElement.GetAttribute("BoundingRectangle");
            return Int32.Parse(boundingRectangleParameters.Split(':').Last());
        }
        public WindowsElement SmallOptionButtonElement()
        {
            return _windowUIDriver.GetElement("Small", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectSmallOptionButtonFromFontSize()
        {
            SettingsTabElement().Click();
            SmallOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public int GetFontSizeForAppliedSettings()
        {
            GetPhrasesTab().Click();
            string boundingRectangleParameters = GetFirstPhraseElement().GetAttribute("BoundingRectangle");
            return Int32.Parse(boundingRectangleParameters.Split(':').Last());
        }
        public WindowsElement LargeOptionButtonElement()
        {
            return _windowUIDriver.GetElement("Large", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectLargeOptionButtonFromFontSize()
        {
            SettingsTabElement().Click();
            LargeOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public WindowsElement BibliographyCheckBoxElement()
        {
            return _windowUIDriver.GetElement("Bibliography", WindowUIDriver.ElementSelectorType.Name);
        }
        public void UncheckInTextCheckBoxAndCheckBibliography()
        {
            InTextCheckBoxElement().Click();
            BibliographyCheckBoxElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public void InsertPhraseToDocument(WindowsElement randomPhraseWindowsElement)
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            GetPhrasesTab().Click();
            while (randomPhraseWindowsElement.Coordinates.LocationInDom.Y > 824)
            {
                WindowsElement phrasesContainer = GetPhrasesContainer();
                AppiumWebElement scrollBar = GetScrollBarElement(phrasesContainer);
                scrollBar.Click();
            }
            randomPhraseWindowsElement.Click();
            WindowsDriver<WindowsElement> wordDriver = _windowUIDriver.OpenWordDocument();
            //As splash screen having no controls for using explicit wait time. Implicit wait time is used after launching application.
            Thread.Sleep(_waitForWordDocumentToLoad);
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            BlankDocumentElement().Click();
            _windowUIDriver.SwitchToGivenWindow(indexOfLoginWindow);
            WaitForInsertButton();
            InsertButton().Click();
        }
        public string GetInsertedSourcetext()
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            string wordText = GetWordDocument().Text.Split('\r').Last().Replace(" \"", "").Replace("\"", "").Trim();
            GetCloseButton().Click();
            _windowUIDriver.ClickRightArrowButtonAndEnter();
            return wordText;
        }
        public WindowsElement AllOptionButtonElement()
        {
            return _windowUIDriver.GetElement("All", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectQuotationMarksAllOptionButton()
        {
            AllOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public bool IsQuotationMarkspresent()
        {
            bool isQuotationMarksPresent = false;
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            if (GetWordDocument().Text.Contains(" \""))
            {
                isQuotationMarksPresent = true;
            }
            GetCloseButton().Click();
            _windowUIDriver.ClickRightArrowButtonAndEnter();
            return isQuotationMarksPresent;
        }
        public string GetTextInsideTheQuotationMarks()
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
            _windowUIDriver.SwitchToWordGivenWindow(indexOfLoginWindow);
            string wordTextBetweenQuotationMarks = GetWordDocument().Text.Split('"', '"')[1];
            return wordTextBetweenQuotationMarks;
        }
        public int GetNumberOfWordsInPhrase(WindowsElement randomPhraseWindowsElement)
        {
            string phraseText = randomPhraseWindowsElement.Text.Replace(" \r\n      ", "").Replace("\r\n      ", "");
            string[] wordsInPhraseText = phraseText.Split(' ');
            return wordsInPhraseText.Count();
        }
        public WindowsElement MoreThanFiveWordsOptionButtonElement()
        {
            return _windowUIDriver.GetElement("5+ words", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectQuotationMarksForMoreThanFiveWordsOptionButton()
        {
            MoreThanFiveWordsOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public int GetRandomNumberOfWords(int minimumValue, int maximumValue)
        {
            return _random.Next(minimumValue, maximumValue);
        }
        public List<WindowsElement> TextBoxElements()
        {
            return _windowUIDriver.GetElements("SpiceWPFTextBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public void SelectQuotationMarksForMoreThanRandomNumberOfWords(int randomNumberOfWords)
        {
            List<WindowsElement> textBoxes = TextBoxElements();
            textBoxes.Last().SendKeys(randomNumberOfWords.ToString());
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public WindowsElement NoneOptionButtonElement()
        {
            return _windowUIDriver.GetElement("None", WindowUIDriver.ElementSelectorType.Name);
        }
        public void SelectQuotationMarksNone()
        {
            NoneOptionButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public List<XmlDocument> GetWordInfoXML(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetWordInfoXML(randomWordFromDB);
        }
        public int GetPhraseIDFromDB(List<XmlDocument> _CompleteDecryptedXML)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.PhraseID(_CompleteDecryptedXML);
        }
        public XmlDocument GetSourcesXML(int phraseIDFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetSourcesXML(phraseIDFromDB);
        }
        public List<string> GetSourcesForSelectedPhraseIn_APA_Format_DB(XmlDocument _SourcesXML)
        {
            List<string> CompareSourcesDB = new List<string>();

            WordInfoBL objSource = new WordInfoBL();
            List<SpiceSource> sources = new List<SpiceSource>();
            WordPrinter wordPrinter;
            string datasorcesXml = string.Empty;
            List<string> listsources = new List<string>();

            foreach (XmlNode nodeSource in _SourcesXML.SelectSingleNode("//SOURCES").ChildNodes)
            {
                sources.Add(objSource.LoadSource(nodeSource));
                wordPrinter = new WordPrinter();
                datasorcesXml = wordPrinter.GetXML_In_APA_Format(objSource.LoadSource(nodeSource));
                listsources.Add(datasorcesXml);
            }

            string sourceFormatFromDB = string.Empty;
            foreach (string FormatSourecesDB in listsources)
            {
                sourceFormatFromDB = FormatSourecesDB.Replace("<i>", "").Replace("</i>", "").Replace("  ", " ").Replace("<italic>","").Replace("</italic>","").Trim();
                CompareSourcesDB.Add(sourceFormatFromDB);
            }

            return CompareSourcesDB;
        }
        public WindowsElement FootnoteStyle_APA_ButtonElement()
        {
            return _windowUIDriver.GetElement("APA", WindowUIDriver.ElementSelectorType.Name);
        }
        public void OpenSettingsAndSetFootnoteStyleTo_APA_Format()
        {
            SettingsTabElement().Click();
            FootnoteStyle_APA_ButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public List<string> GetSourcesForSelectedPhrase_Windows(string randomWordFromDB)
        {
            List<string> sourcesFromWindow = new List<string>();
            GetPhrasesTab().Click();
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
            WindowsElement sourcesContainer = GetSourcesContainer();
            List<AppiumWebElement> sourcesAppiumElements = GetSourcesList(sourcesContainer);
            foreach (AppiumWebElement sourceAppiumElement in sourcesAppiumElements)
            {
                sourcesFromWindow.Add(sourceAppiumElement.Text.Replace("\r\n", "").Replace("<i>", "").Replace("</i>", "").Replace("  ", " ").Replace("  ", " "));
            }
            return sourcesFromWindow;
        }
        public List<string> GetSourcesForSelectedPhraseIn_Chicago_Format_DB(XmlDocument _SourcesXML)
        {
            List<string> CompareSourcesDB = new List<string>();

            WordInfoBL objSource = new WordInfoBL();
            List<SpiceSource> sources = new List<SpiceSource>();
            WordPrinter wordPrinter;
            string datasorcesXml = string.Empty;
            List<string> listsources = new List<string>();

            foreach (XmlNode nodeSource in _SourcesXML.SelectSingleNode("//SOURCES").ChildNodes)
            {
                sources.Add(objSource.LoadSource(nodeSource));
                wordPrinter = new WordPrinter();
                datasorcesXml = wordPrinter.GetXML_In_CHICAGO_Format(objSource.LoadSource(nodeSource));
                listsources.Add(datasorcesXml);
            }

            string sourceFormatFromDB = string.Empty;
            foreach (string FormatSourecesDB in listsources)
            {
                sourceFormatFromDB = FormatSourecesDB.Replace("<i>", "").Replace("</i>", "").Replace("  ", " ").Replace("<italic>", "").Replace("</italic>", "").Trim();
                CompareSourcesDB.Add(sourceFormatFromDB);
            }

            return CompareSourcesDB;
        }
        public WindowsElement FootnoteStyle_Chicago_ButtonElement()
        {
            return _windowUIDriver.GetElement("Chicago", WindowUIDriver.ElementSelectorType.Name);
        }
        public void OpenSettingsAndSetFootnoteStyleTo_Chicago_Format()
        {
            SettingsTabElement().Click();
            FootnoteStyle_Chicago_ButtonElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public void UncheckInTextCheckBoxAndCheckEndOfPage_Bibliography_Boxes()
        {
            SettingsTabElement().Click();
            InTextCheckBoxElement().Click();
            EndOfThePageCheckBoxElement().Click();
            BibliographyCheckBoxElement().Click();
            SaveButtonElement().Click();
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public FootnoteLocation GetFootnoteAndBibliographyFromXmlDocument(XmlDocument WordXmlDocument)
        {
            FootnoteLocation footnoteLocation = new FootnoteLocation();
            XmlNamespaceManager nameSpaceManager = AddXmlNamespacemanager(WordXmlDocument);
            footnoteLocation.Bibliography = WordXmlDocument.SelectSingleNode("//w:wordDocument//w:body//wx:sect//w:p[3]", nameSpaceManager).InnerText.Replace(" \"", "").Replace("\"", "").Trim();
            XmlNode footNoteXmlNode = WordXmlDocument.SelectSingleNode("//w:wordDocument//w:body//wx:sect//w:p//w:r//w:footnote", nameSpaceManager);
            footnoteLocation.EndOfPage = footNoteXmlNode.InnerText.Replace(" \"", "").Replace("\"", "").Trim();
            return footnoteLocation;
        }
        public void SetFootnoteStyleTo_APA_Format()
        {
            SettingsTabElement().Click();
            FootnoteStyle_APA_ButtonElement().Click();
        }
        public void SetFootnoteStyleTo_Chicago_Format()
        {
            SettingsTabElement().Click();
            FootnoteStyle_Chicago_ButtonElement().Click();
        }
        public WindowsElement GetResetButton()
        {
            return _windowUIDriver.GetElement("picbxResetSettings", WindowUIDriver.ElementSelectorType.ID);
        }
        public void ResetSettings()
        {
            SettingsTabElement().Click();
            GetResetButton().Click();
        }
        public void DeleteSettingsDocument()
        {
            try
            {
                File.Delete(_settingsDocumentLocation);
                File.Delete(_settingsXmlLocation);
            }
            catch(Exception ex)
            {
                LogInfo.LogException(ex, "Document not created");
            }
            
        }
    }
}
