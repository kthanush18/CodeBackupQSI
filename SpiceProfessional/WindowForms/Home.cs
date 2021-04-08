using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Core.SpiceThesaurus;
using Quant.Spice.ServiceAccess;
using Quant.Spice.Test.UI.Common;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.Models.UITest;
using Quant.Spice.Test.UI.Common.WindowsUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using Meaning = Quant.Spice.Test.UI.Common.Models.Meaning;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms
{
    public class Home : WindowForm
    {
        protected static SearchKeywordDataAccess _dataAccess;
        readonly AccountDetails _accountDetails = new AccountDetails();
        Random _random = new Random();
        public int _waitForWordDocumentToLoad = Int32.Parse(ConfigurationManager.AppSettings["WaitTimeForWordDocument"].ToString());
        public string _errorFileLocation = ConfigurationManager.AppSettings["OutputErrorFileLocation"].ToString();
        public string _allKeywordsLogFileLocation = ConfigurationManager.AppSettings["AllKeywordsStatusFileLocation"].ToString();

        public Home(WindowUIDriver window) : base(window)
        {

        }
        public string GetPartsOfSpeech(int SpeechValues)
        {
            string Speech = null;
            switch (SpeechValues)
            {
                case 0:
                    Speech = string.Empty;
                    break;
                case 1:
                    Speech = " (n.):";
                    break;
                case 2:
                    Speech = " (v.):";
                    break;
                case 3:
                    Speech = " (adj.):";
                    break;
                case 4:
                    Speech = " (adv.):";
                    break;
                case 5:
                    Speech = " (pron.):";
                    break;
            }
            return Speech;
        }

        public void WaitForHomeWindowToLoad()
        {
            _windowUIDriver.WaitForWindowsElement("picbxPhrasesTab", WindowUIDriver.ElementSelectorType.ID);
        }

        public bool IsPhrasesTabVisible()
        {
            return _windowUIDriver.IsElementVisible("picbxPhrasesTab", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetSearchKeywordTextBox()
        {
            return _windowUIDriver.GetElement("SpiceWPFTextBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetSurroundingWordsContainer()
        {
            return _windowUIDriver.GetElement("qsgridSurrndWords", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForOneKeywordSuggestionToLoad()
        {
            _windowUIDriver.WaitForWindowsElement(" Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public List<AppiumWebElement> GetSurroundingWordsList(WindowsElement SurroundingWordsContainer)
        {
            return _windowUIDriver.GetAppiumElements("DataItem", WindowUIDriver.ElementSelectorType.TagName, SurroundingWordsContainer);
        }
        public void EnterLettersIntoTextBoxWaitForSurroundingWords(string RandomLetter)
        {
            WaitForHomeWindowToLoad();
            WindowsElement searchKeywordElement = GetSearchKeywordTextBox();
            searchKeywordElement.Clear();
            searchKeywordElement.SendKeys(RandomLetter);
            WaitForOneKeywordSuggestionToLoad();
        }
        public string GetRandomLetter()
        {
            int number = _random.Next(0, 26);
            char singleLetter = (char)('a' + number);
            return singleLetter.ToString();
        }
        public List<string> GetRandomSingleLetterFromDB(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            List<string> keywordSuggestionsForSingleLetter = _dataAccess.KeywordSuggestionsForSingleLetter(randomWordFromDB);
            keywordSuggestionsForSingleLetter.Sort();
            return keywordSuggestionsForSingleLetter;
        }
        public List<string> GetSurroundingWordsForSingleLetter(string RandomLetter)
        {
            List<string> surroundingWordsFromWindow = new List<string>();
            EnterLettersIntoTextBoxWaitForSurroundingWords(RandomLetter);
            WindowsElement SurroundingWordsContainer = GetSurroundingWordsContainer();
            List<AppiumWebElement> surroundingWordsAppiumElements = GetSurroundingWordsList(SurroundingWordsContainer);
            foreach (AppiumWebElement surroundingWordAppiumElement in surroundingWordsAppiumElements)
            {
                surroundingWordsFromWindow.Add(surroundingWordAppiumElement.Text);
            }
            return surroundingWordsFromWindow;
        }
        public string GetRandomMultipleLetters()
        {
            _dataAccess = new SearchKeywordDataAccess();
            string randomWord = _dataAccess.GetRandomMultipleLetters();
            string MultipleLetters = "";
            //As the keyword length will be a minimum of 2 characters, randomly 2 to 5 charcters are taken for keyword suggestions
            int number = _random.Next(2, 5);
            //If the length of the keyword is less then 4 characters then only 2 characters are taken for keyword suggestions
            if (randomWord.Length > 4)
            {
                MultipleLetters = randomWord.Substring(0, number);
            }
            else
            {
                MultipleLetters = randomWord.Substring(0, 2);
            }
            return MultipleLetters;
        }
        public List<string> GetRandomMultipleLettersFromDB(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            List<string> keywordSuggestionsForMultipleLetters = _dataAccess.KeywordSuggestionsForSingleLetter(randomWordFromDB);
            keywordSuggestionsForMultipleLetters.Sort();
            return keywordSuggestionsForMultipleLetters;
        }
        public List<string> GetSurroundingWordsForMultipleLetters(string RandomLetter)
        {
            List<string> surroundingWordsFromWindow = new List<string>();
            EnterLettersIntoTextBoxWaitForSurroundingWords(RandomLetter);
            WindowsElement SurroundingWordsContainer = GetSurroundingWordsContainer();
            List<AppiumWebElement> surroundingWordsAppiumElements = GetSurroundingWordsList(SurroundingWordsContainer);
            foreach (AppiumWebElement surroundingWordAppiumElement in surroundingWordsAppiumElements)
            {
                surroundingWordsFromWindow.Add(surroundingWordAppiumElement.Text);
            }
            return surroundingWordsFromWindow;
        }
        public WindowsElement GetSearchButton()
        {
            return _windowUIDriver.GetElement("picbxGo", WindowUIDriver.ElementSelectorType.ID);
        }
        public string GetRandomWord()
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetRandomWord();
        }
        public void WaitForOnePhraseToLoad()
        {
            _windowUIDriver.WaitForWindowsElement("Phrase Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement GetMainContainer()
        {
            return _windowUIDriver.GetElement("frmLookupPhrases", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<AppiumWebElement> GetCommonList(WindowsElement SurroundingWordsContainer)
        {
            return _windowUIDriver.GetAppiumElements("Text", WindowUIDriver.ElementSelectorType.TagName, SurroundingWordsContainer);
        }
        public bool IsForwardMeaningNavigationButtonVisible()
        {
            bool IsVisible = false;
            try
            {
                WindowsElement nextNavigationButton = _windowUIDriver.GetElement("btnNextMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
                IsVisible = bool.Parse(nextNavigationButton.GetAttribute("IsEnabled"));
            }
            catch (Exception)
            {
                IsVisible = false;
            }
            return IsVisible;
        }
        public WindowsElement GetForwardMeaningNavigationButton()
        {
            return _windowUIDriver.GetElement("btnNextMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<string> GetMeaningsForEnteredKeyword(string randomWordFromDB)
        {
            List<string> meaningsFromUI = new List<string>();
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
            WindowsElement mainContainer = GetMainContainer();
            List<AppiumWebElement> commonAppiumElements = GetCommonList(mainContainer);
            foreach (AppiumWebElement meaningAppiumElement in commonAppiumElements)
            {
                if (meaningAppiumElement.Text.Contains(":"))
                {
                    break;
                }
                else
                {
                    meaningsFromUI.Add(meaningAppiumElement.Text);
                }
                meaningsFromUI.Sort();
            }
            while (IsForwardMeaningNavigationButtonVisible())
            {
                List<string> newMeaningsFromUI = new List<string>();
                List<string> addedMeaningsList = new List<string>();
                GetForwardMeaningNavigationButton().Click();
                List<AppiumWebElement> newCommonAppiumElements = GetCommonList(mainContainer);
                foreach (AppiumWebElement meaningAppiumElement in newCommonAppiumElements)
                {
                    if (meaningAppiumElement.Text.Contains(":"))
                    {
                        break;
                    }
                    else
                    {
                        newMeaningsFromUI.Add(meaningAppiumElement.Text);
                    }
                    newMeaningsFromUI.Sort();
                }
                addedMeaningsList = newMeaningsFromUI.Except(meaningsFromUI).ToList();
                foreach(string meaning in addedMeaningsList)
                {
                    meaningsFromUI.Add(meaning);
                }
                meaningsFromUI.Sort();
            }
            return meaningsFromUI;
        }        
        public WindowsElement GetPhrasesContainer()
        {
            return _windowUIDriver.GetElement("qsgridPhrases", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<AppiumWebElement> GetPhrasesList(WindowsElement SurroundingWordsContainer)
        {
            return _windowUIDriver.GetAppiumElements("DataItem", WindowUIDriver.ElementSelectorType.TagName, SurroundingWordsContainer);
        }
        public List<XmlDocument> GetWordInfoXML(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetWordInfoXML(randomWordFromDB);
        }

        public string GetMeaningOnLeft(List<AppiumWebElement> commonAppiumElements)
        {
            string meaningOnLeft = "";
            foreach (AppiumWebElement meaningAppiumElement in commonAppiumElements)
            {
                if (meaningAppiumElement.Text.Contains(":"))
                {
                    meaningOnLeft = meaningAppiumElement.Text.Replace("\r\n","");
                    if (meaningOnLeft.Contains("-"))
                    {
                        meaningOnLeft = meaningOnLeft.Replace("-", "");
                    }
                    break;
                }
            }
            return meaningOnLeft;
        }

        public string GetMeaningDisplayedOnLeftDB(string RandomWordFromDB, List<XmlDocument> wordInfoXMLs)
        {
            Meaning meaning = new Meaning();
            List<Meaning> meanings = new List<Meaning>();
            string PartsOfSpeechFromDB = "";
            string MeaningsDisplayedOnLeftFromDB = "";
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO//GRP//MNGS//MNG"))
            {
                Meaning leftmeaning = new Meaning
                {
                    Text = Meanings.SelectSingleNode("TXT").InnerText.ToString(),
                    ID = Int32.Parse(Meanings.SelectSingleNode("ID").InnerText)
                };

                meanings.Add(leftmeaning);
                meanings.Sort();
            }
            int meaningID = meanings[0].ID;
            foreach (XmlNode SpeechValue in wordInfoXMLs[0].SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']"))
            {
                PartsOfSpeechFromDB = GetPartsOfSpeech(Convert.ToInt32(SpeechValue.SelectSingleNode("SPCH").InnerText.ToString()));
            }
            if(PartsOfSpeechFromDB == "")
            {
                PartsOfSpeechFromDB = ":";
            }
            MeaningsDisplayedOnLeftFromDB = meanings[0].Text + PartsOfSpeechFromDB;
            return MeaningsDisplayedOnLeftFromDB;
        }
        public string GetMeaningDisplayedOnLeftDB(string RandomWordFromDB, List<XmlDocument> wordInfoXMLs, List<Meaning> sortedMeaningsFromDB)
        {
            string PartsOfSpeechFromDB = "";
            string MeaningsDisplayedOnLeftFromDB = "";
            int meaningID = sortedMeaningsFromDB[0].ID;
            foreach (XmlNode SpeechValue in wordInfoXMLs[0].SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']"))
            {
                PartsOfSpeechFromDB = GetPartsOfSpeech(Convert.ToInt32(SpeechValue.SelectSingleNode("SPCH").InnerText.ToString()));
            }
            if (PartsOfSpeechFromDB == "")
            {
                PartsOfSpeechFromDB = ":";
            }
            MeaningsDisplayedOnLeftFromDB = sortedMeaningsFromDB[0].Text + PartsOfSpeechFromDB;
            if (MeaningsDisplayedOnLeftFromDB.Contains("-"))
            {
               MeaningsDisplayedOnLeftFromDB = MeaningsDisplayedOnLeftFromDB.Replace("-", "");
            }
            return MeaningsDisplayedOnLeftFromDB;
        }

        public List<string> GetPhrasesForSelectedMeaning_Windows(string randomWordFromDB)
        {
            CommonMethods removeSpaces = new CommonMethods();
            List<string> getPhrasesListFromWindow = new List<string>();
            WindowsElement phrasesContainer = GetPhrasesContainer();
            List<AppiumWebElement> phrasesAppiumElements = GetPhrasesList(phrasesContainer);
            foreach (AppiumWebElement phraseAppiumElement in phrasesAppiumElements)
            {
                string phraseText = phraseAppiumElement.Text.Replace(" \r\n      ", "").Replace("\r\n      ", "").Replace("- ", "").Replace("-","");
                getPhrasesListFromWindow.Add(removeSpaces.RemoveExtraSpaces(phraseText));
            }
            int lastElementIndex = getPhrasesListFromWindow.Count - 1;
            if (getPhrasesListFromWindow[lastElementIndex] == "To view the next meaning, click here")
            {
                getPhrasesListFromWindow.RemoveAt(lastElementIndex);
            }
            return getPhrasesListFromWindow;
        }

        public List<string> GetPhrasesOfFirstMeaning(List<XmlDocument> wordInfoXMLs)
        {
            CommonMethods removeSpaces = new CommonMethods();
            List<Meaning> meanings = new List<Meaning>();
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO//GRP//MNGS//MNG"))
            {
                Meaning meaning = new Meaning
                {
                    Text = Meanings.SelectSingleNode("TXT").InnerText.ToString(),
                    ID = Int32.Parse(Meanings.SelectSingleNode("ID").InnerText)
                };

                meanings.Add(meaning);
            }

            meanings.Sort();
            int meaningID = meanings[0].ID;

            List<string> phrases = new List<string>();
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNodeList phraseNodes = null;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    phraseNodes = wordInfoXML.SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }
                else
                {
                    phraseNodes = wordInfoXML.SelectNodes("//MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }

                foreach (XmlNode node1 in phraseNodes)
                {
                    string phraseText = node1.SelectSingleNode("TEXT").InnerText.ToString().Replace("- ", "").Replace("-", "");
                    phrases.Add(phraseText);
                }
            }
            phrases.Sort();
            List<string> phrasesRemovedExtraSpaces = new List<string>();
            foreach(string phrase in phrases)
            {
                phrasesRemovedExtraSpaces.Add(removeSpaces.RemoveExtraSpaces(phrase));
            }
            return phrasesRemovedExtraSpaces;
        }
        public XmlDocument GetSourcesXML(int phraseIDFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetSourcesXML(phraseIDFromDB);
        }
        public int GetPhraseIDFromDB(List<XmlDocument> _CompleteDecryptedXML)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.PhraseID(_CompleteDecryptedXML);
        }
        public WindowsElement GetSourcesContainer()
        {
            return _windowUIDriver.GetElement("qsgridSources", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<AppiumWebElement> GetSourcesList(WindowsElement SurroundingWordsContainer)
        {
            return _windowUIDriver.GetAppiumElements("DataItem", WindowUIDriver.ElementSelectorType.TagName, SurroundingWordsContainer);
        }
        public List<string> GetSourcesForSelectedPhrase_Windows()
        {
            CommonMethods removeSpaces = new CommonMethods();
            List<string> sourcesFromWindow = new List<string>();
            WindowsElement sourcesContainer = GetSourcesContainer();
            List<AppiumWebElement> sourcesAppiumElements = GetSourcesList(sourcesContainer);
            foreach (AppiumWebElement sourceAppiumElement in sourcesAppiumElements)
            {
                string sourceText = sourceAppiumElement.Text.Replace("\r\n", "").Replace("<i>", "").Replace("</i>", "").Replace("  ", " ").Replace("  ", " ").Replace("- ", "").Replace("-", "");
                sourcesFromWindow.Add(removeSpaces.RemoveExtraSpaces(sourceText));
            }
            return sourcesFromWindow;
        }
        public string GetFrequencyOfUsesWindows(List<AppiumWebElement> commonAppiumElements)
        {
            //As frequency of use element is at number 5 from the bottom of the list
            int indexOfFrequencyOfUseElement = commonAppiumElements.Count - 5;
            string FrequencyOfUseCount = commonAppiumElements[indexOfFrequencyOfUseElement].Text;
            return FrequencyOfUseCount;
        }
        public List<string> GetSourcesForSelectedPhrase_DB(XmlDocument _SourcesXML)
        {
            CommonMethods removeSpaces = new CommonMethods();
            List<string> CompareSourcesDB = new List<string>();

            WordInfoBL objSource = new WordInfoBL();
            List<SpiceSource> sources = new List<SpiceSource>();
            SourcesFormat sourceformat;
            string datasorce = string.Empty;
            List<string> listsources = new List<string>();

            foreach (XmlNode nodeSource in _SourcesXML.SelectSingleNode("//SOURCES").ChildNodes)
            {
                sources.Add(objSource.LoadSource(nodeSource));
                sourceformat = new SourcesFormat();
                datasorce = sourceformat.GetXML_In_MLA_Format(objSource.LoadSource(nodeSource));
                listsources.Add(datasorce);
            }

            string sourceFormatFromDB = string.Empty;
            foreach (string FormatSourecesDB in listsources)
            {
                sourceFormatFromDB = FormatSourecesDB.Replace("<i>", "").Replace("</i>", "").Replace("  ", " ").Replace("- ", "").Replace("-", "");
                CompareSourcesDB.Add(removeSpaces.RemoveExtraSpaces(sourceFormatFromDB));
            }

            return CompareSourcesDB;
        }
        public string GetFrequencyOfUsesDB(List<XmlDocument> wordInfoXMLs)
        {
            string FrequencyOfUse = "";
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO/MNGS/MNG//PHRASES//PHRASE"))
            {
                FrequencyOfUse = Meanings.ChildNodes[3].InnerText;
                break;
            }
            return FrequencyOfUse;
        }
        public string GetFrequencyOfUsesDB(List<XmlDocument> wordInfoXMLs, List<Meaning> sortedMeaningsFromDB, List<Phrase> sortedPhrasesFromDB)
        {
            string FrequencyOfUse = "";
            int meaningID = sortedMeaningsFromDB[0].ID;
            int phraseID = sortedPhrasesFromDB[0].ID;
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNodeList phraseNodes = null;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    phraseNodes = wordInfoXML.SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']//PHRASES//PHRASE[ID/text()='" + phraseID + "']");
                }
                else
                {
                    phraseNodes = wordInfoXML.SelectNodes("//MNGS/MNG[ID/text()='" + meaningID + "']//PHRASES//PHRASE[ID/text()='" + phraseID + "']");
                }
                foreach (XmlNode phraseNode in phraseNodes)
                {
                    FrequencyOfUse = phraseNode.ChildNodes[3].InnerText;
                    break;
                }
            }
            return FrequencyOfUse;
        }
        public AppiumWebElement GetRandomMeaningElement(WindowsElement mainContainer, string randomMeaning)
        {
            return _windowUIDriver.GetAppiumElement(randomMeaning, WindowUIDriver.ElementSelectorType.Name, mainContainer);
        }
        public bool IsScrollBarElementVisible(WindowsElement container)
        {
            bool isScrollBarVisible = false;
            try
            {
                isScrollBarVisible = _windowUIDriver.IsAppiumElementVisible("PageDown", WindowUIDriver.ElementSelectorType.ID, container);
            }
            catch (Exception)
            {
                isScrollBarVisible = false;
            }
            return isScrollBarVisible;
        }
        public AppiumWebElement GetScrollBarElement(WindowsElement container)
        {
            return _windowUIDriver.GetAppiumElement("PageDown", WindowUIDriver.ElementSelectorType.ID, container);
        }
        public int GetRandomNumberFromMeaningsCount(List<string> meaningsListFromDatabase)
        {
            return _random.Next(1, meaningsListFromDatabase.Count);
        }
        public void SelectRandomMeaningAndClickNextMeaningButton(List<string> meaningsListFromDatabase, int randomNumberFromMeaningsCount, string nextMeaningLinkText, string randomWordFromDB)
        {
            // As the count starts from 1 and the elements in the list starts from 0; -1 is used to reduce number by one
            int randomMeaningNumberToBeClicked = randomNumberFromMeaningsCount - 1;
            string randomMeaning = meaningsListFromDatabase[randomMeaningNumberToBeClicked];
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
            WindowsElement mainContainer = GetMainContainer();
            AppiumWebElement randomMeaningElement = GetRandomMeaningElement(mainContainer, randomMeaning);
            randomMeaningElement.Click();
            WindowsElement phrasesContainer = GetPhrasesContainer();
            List<AppiumWebElement> phrasesAppiumElements = GetPhrasesList(phrasesContainer);
            // As the count starts from 1 and the elements in the list starts from 0; -1 is used to reduce number by one
            int lastAppiumElementInTheList = phrasesAppiumElements.Count - 1;
            if (phrasesAppiumElements[lastAppiumElementInTheList].Text == nextMeaningLinkText)
            {
                if (IsScrollBarElementVisible(phrasesContainer))
                {
                    AppiumWebElement scrollBar = GetScrollBarElement(phrasesContainer);
                    if (scrollBar.Coordinates.LocationInDom.X == 503)
                    {
                        phrasesAppiumElements[lastAppiumElementInTheList].Click();
                    }
                    else
                    {
                        while (scrollBar.Coordinates.LocationInDom.X > 0)
                        {
                            scrollBar.Click();
                        }
                        phrasesAppiumElements[lastAppiumElementInTheList].Click();
                    }
                }
                else
                {
                    phrasesAppiumElements[lastAppiumElementInTheList].Click();
                }
            }
        }
        public string GetSelectedMeaningFromWindows()
        {
            string meaningOnLeft = "";
            string activeMeaning = "";
            WindowsElement mainContainer = GetMainContainer();
            List<AppiumWebElement> commonAppiumElements = GetCommonList(mainContainer);
            foreach (AppiumWebElement meaningAppiumElement in commonAppiumElements)
            {
                if (meaningAppiumElement.Text.Contains(":"))
                {
                    meaningOnLeft = meaningAppiumElement.Text;
                    activeMeaning = meaningOnLeft.Substring(0, meaningOnLeft.LastIndexOf(":")).Trim();
                    break;
                }
            }
            return activeMeaning;
        }
        public WindowsElement GetSeeAlsoWordsContainer()
        {
            return _windowUIDriver.GetElement("qsgridSeeAlso", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<AppiumWebElement> GetSeeAlsoWordsList(WindowsElement seeAlsoWordsContainer)
        {
            return _windowUIDriver.GetAppiumElements("DataItem", WindowUIDriver.ElementSelectorType.TagName, seeAlsoWordsContainer);
        }
        public string GetRandomSeeAlsoWordAndSelect(string randomWordFromDB)
        {
            string randomSeeAlsoWord = "";
            List<string> seeAlsoWordsFromWindow = new List<string>();
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
            WindowsElement seeAlsoWordsContainer = GetSeeAlsoWordsContainer();
            List<AppiumWebElement> seeAlsoAppiumElements = GetSeeAlsoWordsList(seeAlsoWordsContainer);
            foreach (AppiumWebElement seeAlsoAppiumElement in seeAlsoAppiumElements)
            {
                seeAlsoWordsFromWindow.Add(seeAlsoAppiumElement.Text);
            }
            if (seeAlsoWordsFromWindow.Count == 0)
            {
                randomSeeAlsoWord = randomWordFromDB;
            }
            else
            {
                int randomCountSeeAlsonumber = _random.Next(1, seeAlsoWordsFromWindow.Count);
                // As the count starts from 1 and the elements in the list starts from 0; -1 is used to reduce number by one
                int randomElementFromSeeAlso = randomCountSeeAlsonumber - 1;
                randomSeeAlsoWord = seeAlsoAppiumElements[randomElementFromSeeAlso].Text;

                if (IsScrollBarElementVisible(seeAlsoWordsContainer))
                {
                    AppiumWebElement scrollBar = GetScrollBarElement(seeAlsoWordsContainer);
                    // The Y cordinate of the see also container ends at range 677. so the element should fall below that range to be clickable
                    if (seeAlsoAppiumElements[randomElementFromSeeAlso].Coordinates.LocationInDom.Y < 677)
                    {
                        seeAlsoAppiumElements[randomElementFromSeeAlso].Click();
                    }
                    else
                    {
                        while (seeAlsoAppiumElements[randomElementFromSeeAlso].Coordinates.LocationInDom.Y > 677)
                        {
                            scrollBar.Click();
                        }
                        seeAlsoAppiumElements[randomElementFromSeeAlso].Click();
                    }
                }
                else
                {
                    seeAlsoAppiumElements[randomElementFromSeeAlso].Click();
                }
            }
            return randomSeeAlsoWord;
        }
        public string KeywordFromTextBox()
        {
            return GetSearchKeywordTextBox().Text;
        }
        public List<AppiumWebElement> GetMainWindowElements(WindowsElement mainContainer)
        {
            return _windowUIDriver.GetAppiumElements("Welcome testqsi3!", WindowUIDriver.ElementSelectorType.Name, mainContainer);
        }
        public List<AppiumWebElement> GetCircularButtonElemnets()
        {
            List<AppiumWebElement> buttonElement = new List<AppiumWebElement>();
            WindowsElement mainContainer = GetMainContainer();
            List<AppiumWebElement> commonMainWindowElements = GetMainWindowElements(mainContainer);
            IEnumerable<AppiumWebElement> circularButtonElements = commonMainWindowElements.Skip(9);
            foreach (AppiumWebElement circleElement in circularButtonElements)
            {
                buttonElement.Add(circleElement);
            }
            return buttonElement;
        }
        public void ClickCircularButtons(int meaningsCount, List<AppiumWebElement> circularButtonElements)
        {
            circularButtonElements[meaningsCount].Click();
        }
        public WindowsElement GetNextNavigationButton()
        {
            return _windowUIDriver.GetElement("picbxNextMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetPreviousNavigationButton()
        {
            return _windowUIDriver.GetElement("picbxPrevMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
        }
        public bool IsNavigationButtonNextVisible()
        {
            bool IsVisible = false;
            try
            {
                WindowsElement nextNavigationButton = _windowUIDriver.GetElement("picbxNextMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
                IsVisible = bool.Parse(nextNavigationButton.GetAttribute("IsEnabled"));
            }
            catch (Exception)
            {
                IsVisible = false;
            }
            return IsVisible;
        }
        public bool IsNavigationButtonPreviousVisible()
        {
            WindowsElement nextNavigationButton = _windowUIDriver.GetElement("picbxPrevMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
            return bool.Parse(nextNavigationButton.GetAttribute("IsEnabled"));
        }
        public void ClickNavigationButtonNext()
        {
            GetNextNavigationButton().Click();
        }
        public void ClickNavigationButtonPrevious()
        {
            GetPreviousNavigationButton().Click();
        }
        public WindowsElement GetLastNavigationButton()
        {
            return _windowUIDriver.GetElement("picbxLastMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetFirstNavigationButton()
        {
            return _windowUIDriver.GetElement("picbxFirstMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
        }
        public bool IsNavigationButtonLastVisible()
        {
            bool IsVisible = false;
            try
            {
                WindowsElement nextNavigationButton = _windowUIDriver.GetElement("picbxLastMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
                IsVisible = bool.Parse(nextNavigationButton.GetAttribute("IsEnabled"));
            }
            catch (Exception)
            {
                IsVisible = false;
            }
            return IsVisible;
        }
        public bool IsNavigationButtonFirstVisible()
        {
            bool IsVisible = false;
            try
            {
                WindowsElement nextNavigationButton = _windowUIDriver.GetElement("picbxFirstMeaningNavigation", WindowUIDriver.ElementSelectorType.ID);
                IsVisible = bool.Parse(nextNavigationButton.GetAttribute("IsEnabled"));
            }
            catch (Exception)
            {
                IsVisible = false;
            }
            return IsVisible;
        }
        public void ClickNavigationButtonLast()
        {
            GetLastNavigationButton().Click();
        }
        public void ClickNavigationButtonFirst()
        {
            GetFirstNavigationButton().Click();
        }
        public List<string> GetRandomKeywordsAndSearch()
        {
            List<string> searchedKeywordsList = new List<string>();
            for (int RandomKeyword = 1; RandomKeyword <= 5; RandomKeyword++)
            {
                string randomWordFromDB = GetRandomWord();
                searchedKeywordsList.Add(randomWordFromDB);
                EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
                GetSearchButton().Click();
                WaitForOnePhraseToLoad();
            }
            return searchedKeywordsList;
        }
        public WindowsElement GetPreviousKeywordHistoryButton()
        {
            return _windowUIDriver.GetElement("picbxBackButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetNextKeywordHistoryButton()
        {
            return _windowUIDriver.GetElement("picbxForwardButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void ClickPreviousKeywordHistoryButton()
        {
            GetPreviousKeywordHistoryButton().Click();
        }
        public void ClickNextKeywordHistoryButton()
        {
            GetNextKeywordHistoryButton().Click();
        }
        public bool IsPreviousHistoryButtonDisabled()
        {
            WindowsElement nextNavigationButton = _windowUIDriver.GetElement("picbxBackButton", WindowUIDriver.ElementSelectorType.ID);
            return bool.Parse(nextNavigationButton.GetAttribute("IsEnabled"));
        }
        public bool IsNextHistoryButtonDisabled()
        {
            WindowsElement nextNavigationButton = _windowUIDriver.GetElement("picbxForwardButton", WindowUIDriver.ElementSelectorType.ID);
            return bool.Parse(nextNavigationButton.GetAttribute("IsEnabled"));
        }
        public XmlDocument GetAccountDetailsXMLFromDB()
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetUserAccountDetails();
        }
        public WindowsElement GetAccountTab()
        {
            return _windowUIDriver.GetElement("picbxAccount", WindowUIDriver.ElementSelectorType.ID);
        }
        public void NavigateToAccountTab()
        {
            GetAccountTab().Click();
        }
        public AccountDetails GetAccountDetailsFromDB(XmlDocument accountDetailsXML)
        {
            string firstName = "";
            string lastName = "";
            foreach (XmlNode AccountDetailsNode in accountDetailsXML.SelectNodes("//ACCTDTLS"))
            {
                _accountDetails.UserNameFromDB = AccountDetailsNode.SelectSingleNode("UNAME").InnerText.ToString();
                firstName = AccountDetailsNode.SelectSingleNode("FNAME").InnerText.ToString();
                lastName = AccountDetailsNode.SelectSingleNode("LNAME").InnerText.ToString();
                _accountDetails.NameFromDB = firstName + " " + lastName;
                _accountDetails.EmailFromDB = AccountDetailsNode.SelectSingleNode("EMAIL").InnerText.ToString();
                _accountDetails.ExpirationDateFromDB = Convert.ToDateTime(AccountDetailsNode.SelectSingleNode("EXPDT").InnerText.ToString()).ToString("M/d/yyyy").Replace("-", "/");
            }
            return _accountDetails;
        }
        public WindowsElement GetUserNameTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtUserName", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetNameTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtName", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetEmailTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtEmail", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetExpirationDateTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtExpiryDate", WindowUIDriver.ElementSelectorType.ID);
        }

        public AccountDetails GetAccountDetailsFromUI()
        {
            _accountDetails.UserNameFromUI = GetUserNameTextBoxElement().Text;
            _accountDetails.NameFromUI = GetNameTextBoxElement().Text;
            _accountDetails.EmailFromUI = GetEmailTextBoxElement().Text;
            _accountDetails.ExpirationDateFromUI = GetExpirationDateTextBoxElement().Text;

            return _accountDetails;
        }
        public WindowsElement GetRandomPhrasesWindowsElement(string randomWordFromDB)
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
            return phrasesWindowsElements[number - 1];
        }
        public WindowsElement GetCopyButton()
        {
            return _windowUIDriver.GetElement("picbxCopy", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetNotePadEditor()
        {
            return _windowUIDriver.GetNotepadElement("Edit", WindowUIDriver.ElementSelectorType.ClassName);
        }

        public string CopyRandomPhraseToNotepad(WindowsElement randomPhraseWindowsElement)
        {
            while (randomPhraseWindowsElement.Coordinates.LocationInDom.Y > 824)
            {
                WindowsElement phrasesContainer = GetPhrasesContainer();
                AppiumWebElement scrollBar = GetScrollBarElement(phrasesContainer);
                scrollBar.Click();
            }
            randomPhraseWindowsElement.Click();
            GetCopyButton().Click();
            WindowsDriver<WindowsElement> notepadDriver = _windowUIDriver.OpenNotepad();
            notepadDriver.Manage().Window.Maximize();
            GetNotePadEditor().SendKeys(randomPhraseWindowsElement.Text.Replace(" \r\n      ", "").Replace("\r\n      ", "").Replace("\r\n\r\n       ", ""));
            string textInNotepad = GetNotePadEditor().Text;
            GetNotePadEditor().Clear();
            notepadDriver.Quit();
            return textInNotepad;
        }
        public string GetModifiedTextForAssertion(WindowsElement randomPhraseWindowsElement)
        {
            return randomPhraseWindowsElement.Text.Replace(" \r\n      ", "").Replace("\r\n      ", "");
        }
        public WindowsElement BlankDocumentElement()
        {
            return _windowUIDriver.GetWordElement("AIOStartDocument", WindowUIDriver.ElementSelectorType.ID);
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
        public void WaitForInsertButton()
        {
            _windowUIDriver.WaitForWindowsElement("picbxInsert", WindowUIDriver.ElementSelectorType.ID);
        }
        public string InsertRandomPhraseToWordDocument(WindowsElement randomPhraseWindowsElement)
        {
            //Among the list of window handles current window index will be 0 
            int indexOfLoginWindow = 0;
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
        public KeywordAssertionFailure<string> StringCollectionOfFailures(Exception ex, List<string> expectedListFromDatabase, List<string> actualListFromUI, string keywordFromDB, string nameOfAssertion)
        {
            KeywordAssertionFailure<string> failure = new KeywordAssertionFailure<string>
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                ExpectedValues = expectedListFromDatabase,
                ActualValues = actualListFromUI,
                Keyword = keywordFromDB,
                NameOfAssertion = nameOfAssertion,
            };
            return failure;
        }
        public KeywordAssertionFailure<string> StringCollectionOfFailures(Exception ex, List<string> expectedListFromDatabase, List<string> actualListFromUI, string keywordFromDB, string meaningFromDB, string nameOfAssertion)
        {
            KeywordAssertionFailure<string> failure = new KeywordAssertionFailure<string>
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                ExpectedValues = expectedListFromDatabase,
                ActualValues = actualListFromUI,
                Keyword = keywordFromDB,
                Meaning = meaningFromDB,
                NameOfAssertion = nameOfAssertion,
            };
            return failure;
        }
        public KeywordAssertionFailure<string> StringCollectionOfFailures(Exception ex, List<string> expectedListFromDatabase, List<string> actualListFromUI, string keywordFromDB, string meaningFromDB, string phraseFromDB, string nameOfAssertion)
        {
            KeywordAssertionFailure<string> failure = new KeywordAssertionFailure<string>
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                ExpectedValues = expectedListFromDatabase,
                ActualValues = actualListFromUI,
                Keyword = keywordFromDB,
                Meaning = meaningFromDB,
                Phrase = phraseFromDB,
                NameOfAssertion = nameOfAssertion,
            };
            return failure;
        }
        public KeywordAssertionFailure<string> SingleValueOfFailure(Exception ex, string expectedValueFromDatabase, string actualValueFromUI, string keywordFromDB, string meaningFromDB, string nameOfAssertion)
        {
            KeywordAssertionFailure<string> failure = new KeywordAssertionFailure<string>
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                ExpectedValue = expectedValueFromDatabase,
                ActualValue = actualValueFromUI,
                Keyword = keywordFromDB,
                Meaning = meaningFromDB,
                NameOfAssertion = nameOfAssertion,
            };
            return failure;
        }
        public KeywordAssertionFailure<string> SingleValueOfFailure(Exception ex, string expectedValueFromDatabase, string actualValueFromUI, string keywordFromDB, string meaningFromDB, string phraseFromDB, string nameOfAssertion)
        {
            KeywordAssertionFailure<string> failure = new KeywordAssertionFailure<string>
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                ExpectedValue = expectedValueFromDatabase,
                ActualValue = actualValueFromUI,
                Keyword = keywordFromDB,
                Meaning = meaningFromDB,
                Phrase = phraseFromDB,
                NameOfAssertion = nameOfAssertion,
            };
            return failure;
        }
        public KeywordAssertionFailure<string> SingleActualValueInputOfFailure(Exception ex, string actualValueFromUI, string keywordFromDB, string meaningFromDB, string nameOfAssertion)
        {
            KeywordAssertionFailure<string> failure = new KeywordAssertionFailure<string>
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                ExpectedValue = "true",
                ActualValue = actualValueFromUI,
                Keyword = keywordFromDB,
                Meaning = meaningFromDB,
                NameOfAssertion = nameOfAssertion,
            };
            return failure;
        }
        public KeywordAssertionFailure<string> KeywordSearchFailure(string message, string keywordFromDB)
        {
            KeywordAssertionFailure<string> failure = new KeywordAssertionFailure<string>
            {
                ExceptionMessage = message,
                Keyword = keywordFromDB,
            };
            return failure;
        }
       
        public List<Word> GetAllKeywordsRelatedToSearchCriteriaFromDB()
        {
            string searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"].ToString();
            List<Word> keywordsList = new List<Word>();
            if(searchCriteria == "Array")
            {
                keywordsList = GetArrayKeywordsFromCSVFile();
            }
            else
            {
                _dataAccess = new SearchKeywordDataAccess();
                keywordsList = _dataAccess.GetKeywordsList();
            }
            return keywordsList;
        }

        public void EnterkeywordWaitForPhraseToLoad(string randomWordFromDB)
        {
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForOnePhraseToLoad();
        }
        public WindowsElement GetFirstRowFromPhrasesContainer()
        {
            return _windowUIDriver.GetElement("Phrase Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public string TextFromPhrasesContainer()
        {
            string phraseText = "";
            try
            {
                WaitForOnePhraseToLoad();
                phraseText = GetFirstRowFromPhrasesContainer().Text;
            }
            catch (Exception)
            {
                LogInfo.WriteLine("updated keyword");
            }
            return phraseText;
        }
        public bool IsOutDatedKeywordAlertDisplayed()
        {
            bool isElementVisible = true;
            try
            {
                Thread.Sleep(3000);
                _windowUIDriver.SwitchToGivenWindow(0);
                GetOkButton().Click();
                _windowUIDriver.SwitchToGivenWindow(0);
            }
            catch (Exception)
            {
                isElementVisible = false;
            }
            return isElementVisible;
        }
        public List<string> GetMeaningsForSelectedKeyword(List<AppiumWebElement> commonAppiumElements)
        {
            List<string> meaningsFromUI = new List<string>();
            foreach (AppiumWebElement meaningAppiumElement in commonAppiumElements)
            {
                if (meaningAppiumElement.Text.Contains(":"))
                {
                    break;
                }
                else
                {
                    string meaning = meaningAppiumElement.Text;
                    if (meaning.Contains("-"))
                    {
                       meaning = meaning.Replace("-", "");
                    }
                    meaningsFromUI.Add(meaning);
                }
                meaningsFromUI.Sort();
            }
            while (IsForwardMeaningNavigationButtonVisible())
            {
                List<string> newMeaningsFromUI = new List<string>();
                List<string> addedMeaningsList = new List<string>();
                GetForwardMeaningNavigationButton().Click();
                WindowsElement mainContainer = GetMainContainer();
                List<AppiumWebElement> newCommonAppiumElements = GetCommonList(mainContainer);
                foreach (AppiumWebElement meaningAppiumElement in newCommonAppiumElements)
                {
                    if (meaningAppiumElement.Text.Contains(":"))
                    {
                        break;
                    }
                    else
                    {
                        newMeaningsFromUI.Add(meaningAppiumElement.Text);
                    }
                    newMeaningsFromUI.Sort();
                }
                addedMeaningsList = newMeaningsFromUI.Except(meaningsFromUI).ToList();
                foreach (string meaning in addedMeaningsList)
                {
                    if (meaning.Contains("-"))
                    {
                      string newMeaning = meaning.Replace("-", "");
                      meaningsFromUI.Add(newMeaning);
                    }
                    else
                    {
                        meaningsFromUI.Add(meaning);
                    }
                }
                meaningsFromUI.Sort();
            }
            if(meaningsFromUI.Count == 0)
            {
                foreach (AppiumWebElement meaningAppiumElement in commonAppiumElements)
                {
                    if (meaningAppiumElement.Text.Contains(":"))
                    {
                        meaningsFromUI.Add(meaningAppiumElement.Text);
                        break;
                    }
                }
                int index = meaningsFromUI[0].IndexOf("(");
                meaningsFromUI[0] = meaningsFromUI[0].Substring(0, index).Trim();
                if (meaningsFromUI[0].Contains("-"))
                {
                    meaningsFromUI[0] = meaningsFromUI[0].Replace("-", "");
                }
            }
            return meaningsFromUI;
        }
        public WindowsElement GetMeaningElement(int meaningCount, List<string> meaningsFromDatabase)
        {
            List<string> meanings = new List<string>();
            List<WindowsElement> meaningElements = new List<WindowsElement>();
            WindowsElement mainContainer = GetMainContainer();
            List<AppiumWebElement> commonAppiumElements = GetCommonList(mainContainer);
            foreach (WindowsElement meaningAppiumElement in commonAppiumElements)
            {
                if (meaningAppiumElement.Text.Contains(":"))
                {
                    break;
                }
                else
                {
                    meaningElements.Add(meaningAppiumElement);
                }
            }
            WindowsElement meaningElement = null;
            foreach (WindowsElement element in meaningElements)
            {
                if (element.Text == meaningsFromDatabase[meaningCount - 1])
                {
                    meaningElement = element;
                }
            }
            return meaningElement;
        }
        public List<WindowsElement> GetAllPhraseElementsForSelectedMeaning()
        {
            List<WindowsElement> phraseElements = new List<WindowsElement>();
            WindowsElement phrasesContainer = GetPhrasesContainer();
            List<AppiumWebElement> phrasesAppiumElements = GetPhrasesList(phrasesContainer);
            foreach (WindowsElement phraseAppiumElement in phrasesAppiumElements)
            {
                phraseElements.Add(phraseAppiumElement);
            }
            return phraseElements;
        }
        public WindowsElement GetPhraseElement(List<WindowsElement> phraseElements, int phraseCount)
        {
            return phraseElements[phraseCount - 1];
        }
        public List<string> GetPhrasesForSelectedMeaning_UI(int meaningCount, List<string> meaningsFromDatabase)
        {
            List<string> getPhrasesListFromWindow = new List<string>();
            if (meaningCount != 1)
            {
                GetMeaningElement(meaningCount, meaningsFromDatabase).Click();
            }
            WindowsElement phrasesContainer = GetPhrasesContainer();
            List<AppiumWebElement> phrasesAppiumElements = GetPhrasesList(phrasesContainer);
            foreach (AppiumWebElement phrsaeAppiumElement in phrasesAppiumElements)
            {
                getPhrasesListFromWindow.Add(phrsaeAppiumElement.Text.Replace(" \r\n      ", "").Replace("\r\n      ", ""));
            }
            int lastElementIndex = getPhrasesListFromWindow.Count - 1;
            if (getPhrasesListFromWindow[lastElementIndex] == "To view the next meaning, click here")
            {
                getPhrasesListFromWindow.RemoveAt(lastElementIndex);
            }
            return getPhrasesListFromWindow;
        }
        public List<Meaning> GetSortedMeaningsForWordXML(List<XmlDocument> wordInfoXMLs)
        {
            List<Meaning> meanings = new List<Meaning>();
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO//GRP//MNGS//MNG"))
            {
                Meaning meaning = new Meaning
                {
                    Text = Meanings.SelectSingleNode("TXT").InnerText.ToString(),
                    ID = Int32.Parse(Meanings.SelectSingleNode("ID").InnerText)
                };

                meanings.Add(meaning);
            }
            meanings.Sort();
            return meanings;
        }
        public List<string> GetPhrasesOfSelectedMeaning_DB(List<XmlDocument> wordInfoXMLs, int meaningIndex, List<Meaning> meanings)
        {
            int meaningID = meanings[meaningIndex].ID;

            List<string> phrases = new List<string>();
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNodeList phraseNodes = null;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    phraseNodes = wordInfoXML.SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }
                else
                {
                    phraseNodes = wordInfoXML.SelectNodes("//MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }

                foreach (XmlNode node1 in phraseNodes)
                {
                    string phraseText = node1.SelectSingleNode("TEXT").InnerText.ToString();
                    phrases.Add(phraseText);
                }
            }
            phrases.Sort();

            return phrases;
        }
        public string GetMeaningOnLeft()
        {
            string meaningOnLeft = "";
            WindowsElement mainContainer = GetMainContainer();
            List<AppiumWebElement> commonAppiumElements = GetCommonList(mainContainer);
            foreach (AppiumWebElement meaningAppiumElement in commonAppiumElements)
            {
                if (meaningAppiumElement.Text.Contains(":"))
                {
                    meaningOnLeft = meaningAppiumElement.Text;
                    break;
                }
            }
            return meaningOnLeft;
        }
        public string GetSelectedMeaningDisplayedOnLeftDB(List<XmlDocument> wordInfoXMLs, int meaningIndex, List<Meaning> meanings)
        {
            string PartsOfSpeechFromDB = "";
            string MeaningsDisplayedOnLeftFromDB = "";
            int meaningID = meanings[meaningIndex].ID;
            XmlNode meaningNode = wordInfoXMLs[0].SelectSingleNode("//WORDINFO/GRP/MNGS/MNG[ID/text()='" + meaningID + "']");
            PartsOfSpeechFromDB = GetPartsOfSpeech(Convert.ToInt32(meaningNode.SelectSingleNode("SPCH").InnerText.ToString()));
            MeaningsDisplayedOnLeftFromDB = meanings[meaningIndex].Text + PartsOfSpeechFromDB;
            return MeaningsDisplayedOnLeftFromDB;
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForEachMeaningsData(List<XmlDocument> wordInfoXMLs, int meaningsCount, string randomWordFromDB)
        {
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            Searchedkeyword searchedKeyword = new Searchedkeyword();
            CommonCollections common = new CommonCollections();
            common.MeaningsListFromDatabase = GetMeaningsListFromDB(randomWordFromDB);

            //Phrases
            common.PhrasesListForSelectedMeaningFromUI = GetPhrasesForSelectedMeaning_UI(meaningsCount, common.MeaningsListFromDatabase);
            common.SortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            common.PhrasesListForSelectedMeaningFromDB = GetPhrasesOfSelectedMeaning_DB(wordInfoXMLs, meaningsCount - 1, common.SortedMeaningsAndMeaningIDsFromDB);

            //Meaning on left
            common.MeaningOnLeftFromUI = GetMeaningOnLeft();
            common.MeaningOnLeftFromDB = GetSelectedMeaningDisplayedOnLeftDB(wordInfoXMLs, meaningsCount - 1, common.SortedMeaningsAndMeaningIDsFromDB);

            //Assertions
            try
            {
                Assert.IsTrue(common.PhrasesListForSelectedMeaningFromUI.SequenceEqual(common.PhrasesListForSelectedMeaningFromDB));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "PhrasesList";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, common.PhrasesListForSelectedMeaningFromDB, common.PhrasesListForSelectedMeaningFromUI, randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(common.MeaningOnLeftFromDB.SequenceEqual(common.MeaningOnLeftFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "MeaningOnLeft";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, common.MeaningOnLeftFromDB, common.MeaningOnLeftFromUI, randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            return assertionFailures;
        }
        public List<string> TotalPhrasesFromDBForSelectedMeaning(List<XmlDocument> wordInfoXMLs, int meaningsCount)
        {
            List<Meaning> sortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            return GetPhrasesOfSelectedMeaning_DB(wordInfoXMLs, meaningsCount - 1, sortedMeaningsAndMeaningIDsFromDB);
        }
        public List<Phrase> GetSortedPhrasesForMeaningID(List<XmlDocument> wordInfoXMLs, List<Meaning> meanings, int meaningIndex)
        {
            List<Phrase> phrases = new List<Phrase>();
            int meaningID = meanings[meaningIndex].ID;
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNodeList phraseNodes = null;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    phraseNodes = wordInfoXML.SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }
                else
                {
                    phraseNodes = wordInfoXML.SelectNodes("//MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE");
                }

                foreach (XmlNode Phrases in phraseNodes)
                {
                    Phrase phrase = new Phrase
                    {
                        Text = Phrases.SelectSingleNode("TEXT").InnerText.ToString(),
                        ID = Int32.Parse(Phrases.SelectSingleNode("ID").InnerText)
                    };

                    phrases.Add(phrase);
                }
            }
            phrases.Sort();
            return phrases;
        }
        public List<string> GetSourcesForSelectedPhrase_UI(int meaningsCount, int phrasesCount, List<WindowsElement> phraseElements)
        {
            List<string> sourcesFromWindow = new List<string>();
            WindowsElement sourcesContainer = GetSourcesContainer();
            List<AppiumWebElement> sourcesAppiumElements = GetSourcesList(sourcesContainer);
            foreach (AppiumWebElement sourceAppiumElement in sourcesAppiumElements)
            {
                sourcesFromWindow.Add(sourceAppiumElement.Text.Replace("\r\n", "").Replace("<i>", "").Replace("</i>", "").Replace("  ", " ").Replace("  ", " "));
            }
            return sourcesFromWindow;
        }
        public void SelectRequiredPhraseElement(int meaningsCount, int phrasesCount, List<WindowsElement> phraseElements)
        {
            WindowsElement phrasesContainer = GetPhrasesContainer();
            if (IsScrollBarElementVisible(phrasesContainer))
            {
                List<WindowsElement> allPhraseElements = GetAllPhraseElementsForSelectedMeaning();
                WindowsElement phraseElementToBeSelected = allPhraseElements[phrasesCount - 1];
                if (phraseElementToBeSelected.Location.Y > 653)
                {
                    AppiumWebElement scrollBar = GetScrollBarElement(phrasesContainer);
                    scrollBar.Click();
                }
                else
                {
                    GetPhraseElement(phraseElements, phrasesCount).Click();
                }
            }
            else
            {
                GetPhraseElement(phraseElements, phrasesCount).Click();
            }
        }
        public string GetFrequencyOfUsesForSelectedPhraseDB(List<XmlDocument> wordInfoXMLs, int meaningID, int phraseID)
        {
            string FrequencyOfUse = "";
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNode frequencyNode;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    frequencyNode = wordInfoXML.SelectSingleNode("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE[ID/text()='" + phraseID + "']/RUCOUNT");
                }
                else
                {
                    frequencyNode = wordInfoXML.SelectSingleNode("//MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE[ID/text()='" + phraseID + "']/RUCOUNT");
                }
                if (frequencyNode != null)
                {
                    FrequencyOfUse = frequencyNode.InnerText.ToString();
                }
            }
            return FrequencyOfUse;
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForEachPhrasessData(List<XmlDocument> wordInfoXMLs, int phrasesCount, string randomWordFromDB, int meaningsCount, List<WindowsElement> phraseElements, List<AppiumWebElement> commonAppiumElements)
        {
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            CommonCollections common = new CommonCollections();
            common.SortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            common.SortedPhrasesAndPhraseIDsFromDB = GetSortedPhrasesForMeaningID(wordInfoXMLs, common.SortedMeaningsAndMeaningIDsFromDB, meaningsCount - 1);
            int meaningID = common.SortedMeaningsAndMeaningIDsFromDB[meaningsCount - 1].ID;
            string meaningText = common.SortedMeaningsAndMeaningIDsFromDB[meaningsCount - 1].Text;
            int phraseID = common.SortedPhrasesAndPhraseIDsFromDB[phrasesCount - 1].ID;
            string phraseText = common.SortedPhrasesAndPhraseIDsFromDB[phrasesCount - 1].Text;
            XmlDocument sourcesXML = GetSourcesXML(phraseID);
            SelectRequiredPhraseElement(meaningsCount, phrasesCount, phraseElements);

            //Sources
            common.SourcesListForSelectedMeaningFromUI = GetSourcesForSelectedPhrase_UI(meaningsCount, phrasesCount, phraseElements);
            common.SourcesListForSelectedMeaningFromDB = GetSourcesForSelectedPhrase_DB(sourcesXML);

            //FrequencyOfUse
            common.FrequencyOfUseFromUI = GetFrequencyOfUsesWindows(commonAppiumElements);
            common.FrequencyOfUseFromDB = GetFrequencyOfUsesForSelectedPhraseDB(wordInfoXMLs, meaningID, phraseID);


            //Assertions
            try
            {
                Assert.IsTrue(common.SourcesListForSelectedMeaningFromDB.SequenceEqual(common.SourcesListForSelectedMeaningFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "UniqueUseslist/SourcesList";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, common.SourcesListForSelectedMeaningFromDB, common.SourcesListForSelectedMeaningFromUI, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(common.FrequencyOfUseFromDB.SequenceEqual(common.FrequencyOfUseFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "frequencyOfuse";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, common.FrequencyOfUseFromDB, common.FrequencyOfUseFromUI, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                assertionFailures.Add(failure);
            }

            return assertionFailures;
        }
        public List<string> GetMeaningsListFromDB(string randomWordFromDB)
        {
            List<string> meaningsListFromDB = new List<string>();
            _dataAccess = new SearchKeywordDataAccess();
            List<string> meaningsList = _dataAccess.MeaningsList(randomWordFromDB);
            foreach(string meaning in meaningsList)
            {
                if (meaning.Contains("-"))
                {
                    meaningsListFromDB.Add(meaning.Replace("-", ""));
                }
                else
                {
                    meaningsListFromDB.Add(meaning);
                }
            }
            return meaningsListFromDB;
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForDefualtData(List<XmlDocument> wordInfoXMLs, string randomWordFromDB, XmlDocument sourcesXML)
        {
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            int meaningIndex = 0;
            List<Meaning> sortedMeaningsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            List<Phrase> sortedPhrasesFromDB = GetSortedPhrasesForMeaningID(wordInfoXMLs, sortedMeaningsFromDB, meaningIndex);
            List<AppiumWebElement> commonAppiumElements = GetCommonAppiumElements();
            List<string> meaningsListFromUI = GetMeaningsForSelectedKeyword(commonAppiumElements);
            List<string> meaningsListFromDatabase = GetMeaningsListFromDB(randomWordFromDB);
            string meaningOnLeftFromWindow = GetMeaningOnLeft(commonAppiumElements);
            string meaningOnLeftFromDB = GetMeaningDisplayedOnLeftDB(randomWordFromDB, wordInfoXMLs, sortedMeaningsFromDB);
            List<string> phrasesListForSelectedMeaningFromWindows = GetPhrasesForSelectedMeaning_Windows(randomWordFromDB);
            List<string> phrasesListForSelectedMeaningFromDB = GetPhrasesOfFirstMeaning(wordInfoXMLs);
            List<string> sourcesListForSelectedMeaningFromWindows = GetSourcesForSelectedPhrase_Windows();
            List<string> sourcesListForSelectedMeaningFromDB = GetSourcesForSelectedPhrase_DB(sourcesXML);
            string frequencyOfUseFromUI = GetFrequencyOfUsesWindows(commonAppiumElements);
            string frequencyOfUseFromDB = GetFrequencyOfUsesDB(wordInfoXMLs,sortedMeaningsFromDB,sortedPhrasesFromDB);


            //Assertions
            try
            {
                Assert.IsTrue(meaningsListFromDatabase.SequenceEqual(meaningsListFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Meanings List";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, meaningsListFromDatabase, meaningsListFromUI, randomWordFromDB, meaningsListFromDatabase[0], phrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(meaningOnLeftFromDB.SequenceEqual(meaningOnLeftFromWindow));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Meaning On Left";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, meaningOnLeftFromDB, meaningOnLeftFromWindow, randomWordFromDB, meaningsListFromDatabase[0], phrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(phrasesListForSelectedMeaningFromDB.SequenceEqual(phrasesListForSelectedMeaningFromWindows));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Phrases List";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, phrasesListForSelectedMeaningFromDB, phrasesListForSelectedMeaningFromWindows, randomWordFromDB, meaningsListFromDatabase[0], phrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(sourcesListForSelectedMeaningFromDB.SequenceEqual(sourcesListForSelectedMeaningFromWindows));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Sources List";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, sourcesListForSelectedMeaningFromDB, sourcesListForSelectedMeaningFromWindows, randomWordFromDB, meaningsListFromDatabase[0], phrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(frequencyOfUseFromDB.SequenceEqual(frequencyOfUseFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Frequency Of Use";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, frequencyOfUseFromDB, frequencyOfUseFromUI, randomWordFromDB, meaningsListFromDatabase[0], phrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            return assertionFailures;
        }
        public WindowsElement GetOkButton()
        {
            return _windowUIDriver.GetElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void EnterkeywordAndSearch(string randomWordFromDB)
        {
            EnterLettersIntoTextBoxWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
        }
        public void WaitForConfirmation()
        {
            _windowUIDriver.WaitForWindowsElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WriteAsertionFailuresToDocument(List<KeywordAssertionFailure<string>> assertionFailures, StringBuilder errorLogCSV)
        {
            errorLogCSV.Clear();
            foreach (KeywordAssertionFailure<string> failure in assertionFailures)
            {
                try
                {
                    failure.Keyword = $"\"{failure.Keyword}\"";
                    failure.Meaning = $"\"{failure.Meaning}\"";
                    if (failure.ExceptionMessage == "Outdated Keyword" || failure.ExceptionMessage == "No Results")
                    {
                        errorLogCSV.AppendLine(failure.ExceptionMessage + "," + failure.Keyword);
                    }
                    else if (failure.NameOfAssertion == "Meanings List" || failure.NameOfAssertion == "Phrases List" || failure.NameOfAssertion == "Sources List")
                    {
                        List<string> differentExpectedValuesList = failure.ExpectedValues.Except(failure.ActualValues).ToList();
                        List<string> differentActualValuesList = failure.ActualValues.Except(failure.ExpectedValues).ToList();
                        if (failure.Phrase.Contains("\""))
                        {
                            failure.Phrase.Replace("\"", "\"\"");
                            differentExpectedValuesList[0].Replace("\"", "\"\"");
                            differentActualValuesList[0].Replace("\"", "\"\"");
                        }
                        else
                        {
                            failure.Phrase = $"\"{failure.Phrase}\"";
                            differentExpectedValuesList[0]= $"\"{differentExpectedValuesList[0]}\"";
                            differentActualValuesList[0] = $"\"{differentActualValuesList[0]}\"";
                        }
                        if (differentExpectedValuesList.Count == differentActualValuesList.Count)
                        {
                            errorLogCSV.AppendLine(failure.NameOfAssertion + "," + failure.Keyword + "," + failure.Meaning + "," + failure.Phrase + "," + differentExpectedValuesList[0] + "," + differentActualValuesList[0] + "," + failure.ExceptionMessage);
                            if (differentActualValuesList.Count > 1)
                            {
                                for (int count = 2; count <= differentActualValuesList.Count; count++)
                                {
                                    errorLogCSV.AppendLine("" + "," + "" + "," + "" + "," + "" + "," + $"\"{differentExpectedValuesList[count - 1]}\"" + "," + $"\"{differentActualValuesList[count - 1]}\"" + "," + "");
                                }
                            }

                        }
                        else
                        {
                            if (failure.Phrase.Contains("\""))
                            {
                                failure.Phrase.Replace("\"", "\"\"");
                            }
                            else
                            {
                                failure.Phrase = $"\"{failure.Phrase}\"";
                            }
                            errorLogCSV.AppendLine(failure.NameOfAssertion + "," + failure.Keyword + "," + failure.Meaning + "," + failure.Phrase + "," + $"\"{differentExpectedValuesList[0]}\"" + "," + $"\"{differentActualValuesList[0]}\"" + "," + failure.ExceptionMessage);
                            if (differentExpectedValuesList.Count > differentActualValuesList.Count)
                            {
                                for (int count = 2; count <= differentActualValuesList.Count; count++)
                                {
                                    errorLogCSV.AppendLine("" + "," + "" + "," + "" + "," + "" + "," + "" + "," + $"\"{differentExpectedValuesList[count - 1]}\"" + "," + $"\"{differentActualValuesList[count - 1]}\"" + "," + "");
                                }
                                for (int count = differentActualValuesList.Count; count <= differentExpectedValuesList.Count; count++)
                                {
                                    errorLogCSV.AppendLine("" + "," + "" + "," + "" + "," + "" + "," + "" + "," + $"\"{differentExpectedValuesList[count - 1]}\"" + "," + "" + "," + "");
                                }
                            }
                            else
                            {
                                for (int count = 2; count <= differentExpectedValuesList.Count; count++)
                                {
                                    errorLogCSV.AppendLine("" + "," + "" + "," + "" + "," + "" + "," + "" + "," + $"\"{differentExpectedValuesList[count - 1]}\"" + "," + $"\"{differentExpectedValuesList[count - 1]}\"" + "," + "");
                                }
                                for (int count = differentExpectedValuesList.Count; count <= differentActualValuesList.Count; count++)
                                {
                                    errorLogCSV.AppendLine("" + "," + "" + "," + "" + "," + "" + "," + "" + "," + "" + "," + $"\"{differentActualValuesList[count - 1]}\"" + "," + "");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (failure.Phrase.Contains("\""))
                        {
                            failure.Phrase.Replace("\"", "\"\"");
                        }
                        else
                        {
                            failure.Phrase = $"\"{failure.Phrase}\"";
                        }
                        errorLogCSV.AppendLine(failure.NameOfAssertion + "," + failure.Keyword + "," + failure.Meaning + "," + failure.Phrase + "," + $"\"{failure.ExpectedValue}\"" + "," + $"\"{failure.ActualValue}\"" + "," + failure.ExceptionMessage);
                    }
                }
                catch (Exception)
                {
                    if (failure.Phrase.Contains("\""))
                    {
                        failure.Phrase.Replace("\"", "\"\"");
                    }
                    else
                    {
                        failure.Phrase = $"\"{failure.Phrase}\"";
                    }
                    errorLogCSV.AppendLine("Error Occured in Building CSV File"+ "," + failure.Keyword + "," + failure.Meaning + "," + failure.Phrase);
                }
            }
            File.AppendAllText(_errorFileLocation, errorLogCSV.ToString());
        }
        public StringBuilder WriteKeywordDataToCSVDocument(int IDFromDB, string randomWordFromDB, List<KeywordAssertionFailure<string>> assertionFailures, StringBuilder keywordLogCSV, string startTime)
        {
            List<XmlDocument> wordInfoXMLs = GetWordInfoXML(randomWordFromDB);
            List<string> meaningsListFromDatabase = GetMeaningsListFromDB(randomWordFromDB);
            List<string> phrasesListForSelectedMeaningFromDB = GetPhrasesOfFirstMeaning(wordInfoXMLs);
            if (phrasesListForSelectedMeaningFromDB[0].Contains("\""))
            {
                phrasesListForSelectedMeaningFromDB[0] = phrasesListForSelectedMeaningFromDB[0].Replace("\"", "");
            }
            keywordLogCSV.Clear();
            if (assertionFailures.Count == 0)
            {
                keywordLogCSV.AppendLine($"{IDFromDB},\"{randomWordFromDB}\",\"{meaningsListFromDatabase[0]}\",\"{phrasesListForSelectedMeaningFromDB[0]}\",Pass,{startTime}");
            }
            else
            {
                keywordLogCSV.AppendLine($"{IDFromDB},\"{randomWordFromDB}\",\"{meaningsListFromDatabase[0]}\",\"{phrasesListForSelectedMeaningFromDB[0]}\",Fail,{startTime}");
            }

            return keywordLogCSV;
        }
        public StringBuilder WriteKeywordDataToCSVDocument(int IDFromDB, string randomWordFromDB, StringBuilder keywordLogCSV, string startTime)
        {
            keywordLogCSV.Clear();
            keywordLogCSV.AppendLine($"{IDFromDB},\"{randomWordFromDB}\",{""},{""},Fail,{startTime}");
            return keywordLogCSV;
        }
        public StringBuilder CreateStringBuilderAndAppendTitles()
        {
            StringBuilder keywordLogCSV = new StringBuilder();
            keywordLogCSV.AppendLine("ID,Keyword,Meaning,Phrase,Result,Start time,Exception message,Exception stacktrace");
            return keywordLogCSV;
        }
        public void WriteKeywordLogCSVToFile(StringBuilder keywordLogCSV)
        {
            File.AppendAllText(_allKeywordsLogFileLocation, keywordLogCSV.ToString());
        }
        public StringBuilder CreateStringBuilderAndAppendTitlesForErrorLog()
        {
            StringBuilder errorLogCSV = new StringBuilder();
            errorLogCSV.AppendLine("Assertion name,Keyword,Meaning,Phrase,Expected,Actual,Assertion failure,Custom message");
            return errorLogCSV;
        }
        public void WriteErrorLogCSVToFile(StringBuilder errorLogCSV)
        {
            File.AppendAllText(_errorFileLocation, errorLogCSV.ToString());
        }
        public List<Word> GetArrayKeywordsFromCSVFile()
        {
            string keywordArrayFileLocation = ConfigurationManager.AppSettings["KeywordArrayInput"].ToString();
            List<Word> keywords = new List<Word>();
            Word keyword = null;
            string [] keywordsFromCSV = File.ReadAllLines(keywordArrayFileLocation);
            string[] keywordsFromCSVAfterRemovingTitles = keywordsFromCSV.Skip(1).ToArray();
            foreach (string eachKeyword in keywordsFromCSVAfterRemovingTitles)
            {
                string [] singleKeyword = eachKeyword.Split(',');
                keyword = new Word
                {
                    ID = Convert.ToInt32(singleKeyword[0]),
                    Text = Convert.ToString(singleKeyword[1])
                };
                keywords.Add(keyword);
            }
            return keywords;
        }
        public void DeleteExistingLogFiles()
        {
            try
            {
                File.Delete(_errorFileLocation);
                File.Delete(_allKeywordsLogFileLocation);
            }
            catch (Exception)
            {

            }
        }
        public List<AppiumWebElement> GetCommonAppiumElements()
        {
            WindowsElement mainContainer = GetMainContainer();
            List<AppiumWebElement> commonAppiumElements = GetCommonList(mainContainer);
            return commonAppiumElements;
        }
        public List<string> GetSeeAlsoListFromDB(XmlDocument wordInfoXML)
        {
            List<string> seeAlsoWordsList = new List<string>();
            XmlNodeList seeAlsoNodes = wordInfoXML.SelectNodes("//WORDINFO/GRP/SEEALSOS/SEEALSO");
            foreach (XmlNode seeAlsoNode in seeAlsoNodes)
            {
                seeAlsoWordsList.Add(seeAlsoNode.InnerText.ToString());
            }
            return seeAlsoWordsList;
        }
        public void SelectRequiredSeeAlsoElement(int seeAlsoCount, List<WindowsElement> seeAlsoElements)
        {
            WindowsElement seeAlsoWordsContainer = GetSeeAlsoWordsContainer();
            // As the count starts from 1 and the elements in the list starts from 0; -1 is used to reduce number by one
            int randomElementFromSeeAlso = seeAlsoCount - 1;
            string randomSeeAlsoWord = seeAlsoElements[randomElementFromSeeAlso].Text;

            if (IsScrollBarElementVisible(seeAlsoWordsContainer))
            {
                AppiumWebElement scrollBar = GetScrollBarElement(seeAlsoWordsContainer);
                // The Y cordinate of the see also container ends at range 515. so the element should fall below that range to be clickable
                while (seeAlsoElements[randomElementFromSeeAlso].Coordinates.LocationInDom.Y > 515)
                {
                    scrollBar.Click();
                }
                seeAlsoElements[randomElementFromSeeAlso].Click();
            }
            else
            {
                seeAlsoElements[randomElementFromSeeAlso].Click();
            }
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForCommonMethod(string selectedKeyword, string noResultsText)
        {
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            if (IsOutDatedKeywordAlertDisplayed())
            {
                string message = "Outdated Keyword";
                KeywordAssertionFailure<string> failure = KeywordSearchFailure(message, selectedKeyword);
                assertionFailures.Add(failure);
            }
            else if (TextFromPhrasesContainer() == noResultsText)
            {
                string message = "No Results";
                KeywordAssertionFailure<string> failure = KeywordSearchFailure(message, selectedKeyword);
                assertionFailures.Add(failure);
            }
            else
            {
                List<XmlDocument> wordInfoXMLs = GetWordInfoXML(selectedKeyword);
                int phraseIDFromDB = GetPhraseIDFromDB(wordInfoXMLs);
                XmlDocument sourcesXML = GetSourcesXML(phraseIDFromDB);
                List<KeywordAssertionFailure<string>> keywordAssertionFailures = GetAssertionFailuresForDefualtData(wordInfoXMLs, selectedKeyword, sourcesXML);
                foreach (KeywordAssertionFailure<string> failure in keywordAssertionFailures)
                {
                    assertionFailures.Add(failure);
                }
            }
            return assertionFailures;
        }
        public List<WindowsElement> GetAllSeeAlsoElementsForSelectedKeyword()
        {
            List<WindowsElement> seeAlsoElements = new List<WindowsElement>();
            WindowsElement seeAlsoWordsContainer = GetSeeAlsoWordsContainer();
            List<AppiumWebElement> seeAlsoAppiumElements = GetSeeAlsoWordsList(seeAlsoWordsContainer);
            foreach (WindowsElement seeAlsoAppiumElement in seeAlsoAppiumElements)
            {
                seeAlsoElements.Add(seeAlsoAppiumElement);
            }
            return seeAlsoElements;
        }
    }
}
