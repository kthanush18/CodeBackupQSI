using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.WindowsUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms
{
    public class SearchOptions : WindowForm
    {
        protected static SearchKeywordDataAccess _dataAccess;
        readonly Random _random = new Random();
        protected static string _phrasesFilterLocation = ConfigurationManager.AppSettings["PhrasesFilterLocation"].ToString();

        public SearchOptions(WindowUIDriver window) : base(window)
        {

        }
        public void WaitForHomeWindowToLoad()
        {
            _windowUIDriver.WaitForWindowsElement("picbxPhrasesTab", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<WindowsElement> GetSearchOptionsElements()
        {
            return _windowUIDriver.GetElements("search options", WindowUIDriver.ElementSelectorType.Name);
        }
        public void OpenSearchOptionsWindow()
        {
            WaitForHomeWindowToLoad();
            List<WindowsElement> searchOptionsElements = GetSearchOptionsElements();
            WindowsElement searchOptionsLinkElement = searchOptionsElements.Last();
            searchOptionsLinkElement.Click();
            _windowUIDriver.SwitchToFirstWindow();
        }
        public List<string> ContainingWordsFromDB()
        {
            _dataAccess = new SearchKeywordDataAccess();
            List<string> containingWords = new List<string>();
            List<string> wordsList = new List<string>();
            string randomPhrase = _dataAccess.RandomPhrase();
            List<string> distinctWordsFromPhrase = randomPhrase.Split(' ').Distinct().ToList();
            List<string> noiseWords = new List<string>{"is","a","the","of","in","about","after","all","also","an","and","or","another","any","are","as","at","be","because","been","before","being","between","both","but","by","came","can","come","could","did","do",
                "$","0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","does","each","else","for","from",
                "get","got","had","has","have","he","her","here","him","himself","his","how","if","into","it","its","just","like","make","many","me","might","more","most","much","must","my","never","no","now","on","only","other",
                "our","out","over","re","said","same","see","should","since","so","some","still","such","take","than","that","their","them","then","there","these","they","this","those","through","to","too","under","up","use","very",
                "want","was","way","we","well","were","what","when","where","which","while","who","will","with","would","you","your","us"};
            List<string> containingWordsList = distinctWordsFromPhrase.Except(noiseWords).ToList();
            if (containingWordsList.Count > 2)
            {
                int firstWordIndex = _random.Next(containingWordsList.Count);
                int secondWordIndex = _random.Next(containingWordsList.Count);
                containingWords.Add(containingWordsList[firstWordIndex]);
                containingWords.Add(containingWordsList[secondWordIndex]);
            }
            else
            {
                containingWords = containingWordsList;
            }
            foreach (string word in containingWords)
            {
                string containingWord = word.Replace(",", "");
                wordsList.Add(containingWord);
            }
            return wordsList;
        }
        public List<string> PhraseResultsFromDB(List<string> containingWords)
        {
            XmlDocument containingWordsXML = new XmlDocument();
            XmlNode rootNode = containingWordsXML.CreateElement("KEYWORDS");
            containingWordsXML.AppendChild(rootNode);

            XmlNode userNode = containingWordsXML.CreateElement("KEYWORD");
            userNode.InnerText = containingWords[0];
            rootNode.AppendChild(userNode);

            userNode = containingWordsXML.CreateElement("KEYWORD");
            userNode.InnerText = containingWords[1];
            rootNode.AppendChild(userNode);

            return _dataAccess.PhraseResults(containingWordsXML);
        }
        public WindowsElement ContainingWordsTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtContaining", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement SearchButtonElement()
        {
            return _windowUIDriver.GetElement("picbxSearch", WindowUIDriver.ElementSelectorType.ID);
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
        public List<string> PhraseResultsFromUI(List<string> containingWords)
        {
            List<string> phraseresults = new List<string>();

            ContainingWordsTextBoxElement().Click();
            ContainingWordsTextBoxElement().SendKeys(containingWords[0] + " " + containingWords[1]);
            SearchButtonElement().Click();
            WaitForOnePhraseToLoad();
            List<string> getPhrasesListFromWindow = new List<string>();
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
        public string GetRandomWord()
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetRandomWord();
        }
        public WindowsElement KeywordTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtKeyword", WindowUIDriver.ElementSelectorType.ID);
        }
        public void EnterKeywordAndSearch(string randomWordFromDB)
        {
            KeywordTextBoxElement().Click();
            KeywordTextBoxElement().SendKeys(randomWordFromDB);
            SearchButtonElement().Click();
        }
        public WindowsElement GetSearchKeywordTextBox()
        {
            return _windowUIDriver.GetElement("SpiceWPFTextBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public string KeywordFromTextBox()
        {
            return GetSearchKeywordTextBox().Text;
        }
        public WindowsElement GetCreateNewLinkElement()
        {
            return _windowUIDriver.GetElement("Create New", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement GetStartRangeForWordsTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtFromWords", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetEndRangeForWordsTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtToWords", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement FilterNameTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtSaveFilterAs", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement SaveFilterButtonElement()
        {
            return _windowUIDriver.GetElement("picbxSaveFilter", WindowUIDriver.ElementSelectorType.ID);
        }
        public void WaitForConfirmation()
        {
            _windowUIDriver.WaitForWindowsElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetOkButton()
        {
            return _windowUIDriver.GetElement("picbxOkButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement ManageFiltersLinkElement()
        {
            return _windowUIDriver.GetElement("Manage Filters", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement FilterNameElement()
        {
            return _windowUIDriver.GetElement("PhrasesFilter Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public List<WindowsElement> GetLinkElementsFromManageFilters()
        {
            return _windowUIDriver.GetElements(" Row 0", WindowUIDriver.ElementSelectorType.Name);
        }
        public void WaitForDeleteFilterConfirmation()
        {
            _windowUIDriver.WaitForWindowsElement("picbxYesButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetYesButton()
        {
            return _windowUIDriver.GetElement("picbxYesButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetNoButton()
        {
            return _windowUIDriver.GetElement("picbxNoButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public void ClickOkOnConfirmationPopUp()
        {
            WaitForConfirmation();
            GetOkButton().Click();
        }
        public string CreateFilterForWordsCount(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string startRange = _random.Next(minimumValue, maximumValue).ToString();
            string endRange = _random.Next(minimumValue, maximumValue).ToString();
            if (Int32.Parse(startRange) > Int32.Parse(endRange))
            {
                string swap = startRange;
                startRange = endRange;
                endRange = swap;
            }
            GetStartRangeForWordsTextBoxElement().SendKeys(startRange);
            GetEndRangeForWordsTextBoxElement().SendKeys(endRange);
            FilterNameTextBoxElement().SendKeys(startRange + "to" + endRange + "words");
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return startRange + "to" + endRange + "words";
        }
        public string OpenManageFiltersAndGetFirstFilterName()
        {
            ManageFiltersLinkElement().Click();
            return FilterNameElement().Text;
        }
        public bool IsFiltersPresent()
        {
            return _windowUIDriver.IsElementVisible("qsgridPhrasesFilters", WindowUIDriver.ElementSelectorType.ID);
        }
        public void DeleteCreatedFilter()
        {
            OpenSearchOptionsWindow();
            ManageFiltersLinkElement().Click();
            List<WindowsElement> linkElementsFromManageFilters = GetLinkElementsFromManageFilters();
            foreach (WindowsElement Element in linkElementsFromManageFilters)
            {
                if (Element.Text == "delete")
                {
                    _windowUIDriver.ClickAtStartPointOfElement(Element);
                    WaitForDeleteFilterConfirmation();
                    GetYesButton().Click();
                }
            }
            try
            {
                if (IsFiltersPresent())
                {
                    File.Delete(_phrasesFilterLocation);
                }
            }
            catch (Exception ex)
            {
                LogInfo.LogException(ex, "One filter deleted");
            }
        }
        public WindowsElement GetUniqueUsesTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtUniqueUses", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetUniqueUsesOrLessButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnOrLess", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForUsesCountOrLess(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomUsesCount = _random.Next(minimumValue, maximumValue).ToString();
            GetUniqueUsesTextBoxElement().SendKeys(randomUsesCount);
            GetUniqueUsesOrLessButtonElement().Click();
            FilterNameTextBoxElement().SendKeys(randomUsesCount + "_unique_uses_or_less");
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return randomUsesCount + "_unique_uses_or_less";
        }
        public WindowsElement GetUniqueUsesApproximatelyButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnApproximately", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForUsesCountApproximately(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomUsesCount = _random.Next(minimumValue, maximumValue).ToString();
            GetUniqueUsesTextBoxElement().SendKeys(randomUsesCount);
            GetUniqueUsesApproximatelyButtonElement().Click();
            FilterNameTextBoxElement().SendKeys(randomUsesCount + "_unique_uses_approximately");
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return randomUsesCount + "_unique_uses_approximately";
        }
        public WindowsElement GetUniqueUsesOrMoreButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnOrMore", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForUsesCountOrMore(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomUsesCount = _random.Next(minimumValue, maximumValue).ToString();
            GetUniqueUsesTextBoxElement().SendKeys(randomUsesCount);
            GetUniqueUsesOrMoreButtonElement().Click();
            FilterNameTextBoxElement().SendKeys(randomUsesCount + "_unique_uses_or_more");
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return randomUsesCount + "_unique_uses_or_more";
        }
        public WindowsElement GetPhrasesFromYearButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnPhrasesFromYear", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetPhrasesFromYearTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtFromYear", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForPhrasesFromYear(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomYearValue = _random.Next(minimumValue, maximumValue).ToString();
            GetPhrasesFromYearButtonElement().Click();
            GetPhrasesFromYearTextBoxElement().SendKeys(randomYearValue);
            FilterNameTextBoxElement().SendKeys("phrases_from_year_" + randomYearValue);
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return "phrases_from_year_" + randomYearValue;
        }
        public WindowsElement GetRangeOfYearsButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnPhrasesRange", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetStartRangeForYearTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtRangeFrom", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetEndRangeForYearTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtRangeTo", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForPhrasesFromYearRange(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string startYearRange = _random.Next(minimumValue, maximumValue).ToString();
            string endYearRange = _random.Next(minimumValue, maximumValue).ToString();
            if (Int32.Parse(startYearRange) > Int32.Parse(endYearRange))
            {
                string swap = startYearRange;
                startYearRange = endYearRange;
                endYearRange = swap;
            }
            GetRangeOfYearsButtonElement().Click();
            GetStartRangeForYearTextBoxElement().SendKeys(startYearRange);
            GetEndRangeForYearTextBoxElement().SendKeys(endYearRange);
            FilterNameTextBoxElement().SendKeys("years_from" + startYearRange + "_to_" + endYearRange);
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return "years_from" + startYearRange + "_to_" + endYearRange;
        }
        public WindowsElement GetPhrasesBeforeYearButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnPhrasesBefore", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetPhrasesBeforeYearTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtPhrasesBefore", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForPhrasesBeforeYear(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomYearValue = _random.Next(minimumValue, maximumValue).ToString();
            GetPhrasesBeforeYearButtonElement().Click();
            GetPhrasesBeforeYearTextBoxElement().SendKeys(randomYearValue);
            FilterNameTextBoxElement().SendKeys("phrases_before_year_" + randomYearValue);
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return "phrases_before_year_" + randomYearValue;
        }
        public WindowsElement GetPhrasesAfterYearButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnPhrasesAfter", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetPhrasesAfterYearTextBoxElement()
        {
            return _windowUIDriver.GetElement("qtxtPhrasesAfter", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForPhrasesAfterYear(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomYearValue = _random.Next(minimumValue, maximumValue).ToString();
            GetPhrasesAfterYearButtonElement().Click();
            GetPhrasesAfterYearTextBoxElement().SendKeys(randomYearValue);
            FilterNameTextBoxElement().SendKeys("phrases_after_year_" + randomYearValue);
            SaveFilterButtonElement().Click();
            ClickOkOnConfirmationPopUp();
            return "phrases_after_year_" + randomYearValue;
        }
        public WindowsElement GetEnglishPhrasesButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnEnglishOnly", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement GetTranslatedPhrasesButtonElement()
        {
            return _windowUIDriver.GetElement("qrbtnTranslatedOnly", WindowUIDriver.ElementSelectorType.ID);
        }
        public string CreateFilterForEnglishOrTranslatedPhrases()
        {
            string result = "";
            List<string> englishOrTranslated = new List<string>
            {
                "english",
                "translated"
            };
            GetCreateNewLinkElement().Click();
            int randomCountForEnglishOrTranslated = _random.Next(englishOrTranslated.Count);
            if (randomCountForEnglishOrTranslated == 1)
            {
                GetEnglishPhrasesButtonElement().Click();
                FilterNameTextBoxElement().SendKeys("english_phrases_only");
                SaveFilterButtonElement().Click();
                ClickOkOnConfirmationPopUp();
                result = "english_phrases_only";
            }
            else
            {
                GetTranslatedPhrasesButtonElement().Click();
                FilterNameTextBoxElement().SendKeys("translated_phrases_only");
                SaveFilterButtonElement().Click();
                ClickOkOnConfirmationPopUp();
                result = "translated_phrases_only";
            }
            return result;
        }
        public WindowsElement GetUseExistingLinkElement()
        {
            return _windowUIDriver.GetElement("Use Existing", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement GetUseExistingDropdownLinkElement()
        {
            return _windowUIDriver.GetElement("SpiceComboBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<int> GetRangeForCreatedFilter(string filterNameFromCreateFilter)
        {
            List<int> minAndMaxRange = new List<int>();
            GetUseExistingLinkElement().Click();
            GetUseExistingDropdownLinkElement().Click();
            _windowUIDriver.ClickDownButtonAndEnter();
            minAndMaxRange.Add(Int32.Parse(GetStartRangeForWordsTextBoxElement().Text));
            minAndMaxRange.Add(Int32.Parse(GetEndRangeForWordsTextBoxElement().Text));
            return minAndMaxRange;
        }
        public XmlDocument GetLocallyStoredPhrasesXML()
        {
            XmlDocument phrasesXML = new XmlDocument();
            phrasesXML.Load(_phrasesFilterLocation);
            return phrasesXML;
        }
        public List<int> GetRangeFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            List<int> minAndMaxRange = new List<int>();

            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if(FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach(XmlNode WordsCount in FilterInfo.SelectNodes("//WordsCount"))
                    {
                        minAndMaxRange.Add(Int32.Parse(WordsCount.SelectSingleNode("Min").InnerText.ToString()));
                        minAndMaxRange.Add(Int32.Parse(WordsCount.SelectSingleNode("Max").InnerText.ToString()));
                    }
                }
            }
            return minAndMaxRange;
        }
        public int GetUniqueUsesFromCreatedFilter(string filterNameFromCreateFilter)
        {
            GetUseExistingLinkElement().Click();
            GetUseExistingDropdownLinkElement().Click();
            _windowUIDriver.ClickDownButtonAndEnter();
            return Int32.Parse(GetUniqueUsesTextBoxElement().Text);
        }
        public List<WindowsElement> GetRadioButtonElements()
        {
            return _windowUIDriver.GetElements("SpiceRadioButton", WindowUIDriver.ElementSelectorType.ID);
        }
        public bool IsOrLessOptionButtonSelectedFromUseExisting()
        {
            bool result = false;
            List<WindowsElement> radioButtonElements = GetRadioButtonElements();
            foreach(WindowsElement radioButton in radioButtonElements)
            {
                if(radioButton.Text == "or less")
                {
                    result = bool.Parse(radioButton.GetAttribute("SelectionItem.IsSelected"));
                    break;
                }
            }
            return result;
        }
        public int GetUniqueUsesFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            int uniqueUses = 0;

            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//UniqueUsesCount"))
                    {
                        uniqueUses = Int32.Parse(UniqueUses.SelectSingleNode("Count").InnerText.ToString());
                        
                    }
                }
            }
            return uniqueUses ;
        }
        public bool IsOrLessOptionButtonSelectedFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            bool result = false;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//UniqueUsesCount"))
                    {
                        int number = Int32.Parse(UniqueUses.SelectSingleNode("EnmComparison").InnerText.ToString());
                        if(number == 1)
                        {
                            result = true;
                        }

                    }
                }
            }
            return result;
        }
        public bool IsApproximatelyOptionButtonSelectedFromUseExisting()
        {
            bool result = false;
            List<WindowsElement> radioButtonElements = GetRadioButtonElements();
            foreach (WindowsElement radioButton in radioButtonElements)
            {
                if (radioButton.Text == "approximately") 
                {
                    result = bool.Parse(radioButton.GetAttribute("SelectionItem.IsSelected"));
                    break;
                }
            }
            return result;
        }
        public bool IsApproximatelyOptionButtonSelectedFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            bool result = false;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//UniqueUsesCount"))
                    {
                        int number = Int32.Parse(UniqueUses.SelectSingleNode("EnmComparison").InnerText.ToString());
                        if (number == 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            return result;
        }
        public bool IsOrMoreOptionButtonSelectedFromUseExisting()
        {
            bool result = false;
            List<WindowsElement> radioButtonElements = GetRadioButtonElements();
            foreach (WindowsElement radioButton in radioButtonElements)
            {
                if (radioButton.Text == "or more")
                {
                    result = bool.Parse(radioButton.GetAttribute("SelectionItem.IsSelected"));
                    break;
                }
            }
            return result;
        }
        public bool IsOrMoreOptionButtonSelectedFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            bool result = false;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//UniqueUsesCount"))
                    {
                        int number = Int32.Parse(UniqueUses.SelectSingleNode("EnmComparison").InnerText.ToString());
                        if (number == 2)
                        {
                            result = true;
                        }

                    }
                }
            }
            return result;
        }
        public void ApplyCreatedFilter()
        {
            GetUseExistingLinkElement().Click();
            GetUseExistingDropdownLinkElement().Click();
            _windowUIDriver.ClickDownButtonAndEnter();
        }
        public int GetPhrasesFromYear()
        {
            return Int32.Parse(GetPhrasesFromYearTextBoxElement().Text);
        }
        public int GetPhrasesFromYearFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            int phrasesFromYear = 0;

            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        phrasesFromYear = Int32.Parse(UniqueUses.SelectSingleNode("Year").InnerText.ToString());

                    }
                }
            }
            return phrasesFromYear;
        }
        public List<int> GetPhrasesYearRange()
        {
            List<int> PhrasesYearRange = new List<int>
            {
                Int32.Parse(GetStartRangeForYearTextBoxElement().Text),
                Int32.Parse(GetEndRangeForYearTextBoxElement().Text)
            };
            return PhrasesYearRange;
        }
        public List<int> GetPhrasesFromYearRangeFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            List<int> PhrasesYearRange = new List<int>();
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        PhrasesYearRange.Add(Int32.Parse(UniqueUses.SelectSingleNode("Min").InnerText.ToString()));
                        PhrasesYearRange.Add(Int32.Parse(UniqueUses.SelectSingleNode("Max").InnerText.ToString()));
                    }
                }
            }
            return PhrasesYearRange;
        }
        public int GetPhrasesBeforeYear()
        {
            return Int32.Parse(GetPhrasesBeforeYearTextBoxElement().Text);
        }
        public int GetPhrasesBeforeYearFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            int phrasesBeforeYear = 0;

            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        phrasesBeforeYear = Int32.Parse(UniqueUses.SelectSingleNode("Year").InnerText.ToString());

                    }
                }
            }
            return phrasesBeforeYear;
        }
        public int GetPhrasesAfterYear()
        {
            return Int32.Parse(GetPhrasesAfterYearTextBoxElement().Text);
        }
        public int GetPhrasesAfterYearFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            int phrasesAfterYear = 0;

            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        phrasesAfterYear = Int32.Parse(UniqueUses.SelectSingleNode("Year").InnerText.ToString());

                    }
                }
            }
            return phrasesAfterYear;
        }
        public string GetPhrasesEnglishOrTranslated()
        {
            string englishOrTranslated = "";
            bool result = false;
            List<WindowsElement> radioButtonElements = GetRadioButtonElements();
            foreach (WindowsElement radioButton in radioButtonElements)
            {
                if (radioButton.Text == "English Phrases Only"|| radioButton.Text == "Translated Phrases Only")
                {
                    result = bool.Parse(radioButton.GetAttribute("SelectionItem.IsSelected"));
                    if(result == true)
                    {
                        englishOrTranslated = radioButton.Text;
                        break;
                    }
                }
            }
            return englishOrTranslated;
        }
        public string GetPhrasesEnglishOrTranslatedFromLocalXML(string filterNameFromCreateFilter, XmlDocument phrasesXML)
        {
            string englishOrTranslated = "";
            int comaparisionNumber = 0;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhrasesFilter"))
                    {
                        comaparisionNumber = Int32.Parse(UniqueUses.SelectSingleNode("EnmLanguage").InnerText.ToString());
                        if(comaparisionNumber == 1)
                        {
                            englishOrTranslated = "English Phrases Only";
                        }
                        else
                        {
                            englishOrTranslated = "Translated Phrases Only";
                        }

                    }
                }
            }
            return englishOrTranslated;
        }
        public void SearchRandomKeywordAndOpenSearchOptions(string randomWordFromDB)
        {
            EnterKeywordAndSearch(randomWordFromDB);
            OpenSearchOptionsWindow();
        }
        public void SearchAppliedFilter()
        {
            SearchButtonElement().Click();
        }
        public WindowsElement GetNextMeaning(string nextMeaning)
        {
            return _windowUIDriver.GetElement(nextMeaning, WindowUIDriver.ElementSelectorType.Name);
        }
        public List<string> GetPhrasesListFromUI(List<string> meaningsListFromDB, int count)
        {
            List<string> getPhrasesListFromUI = new List<string>();
            WindowsElement phrasesContainer = GetPhrasesContainer();
            List<AppiumWebElement> phrasesAppiumElements = GetPhrasesList(phrasesContainer);
            foreach (AppiumWebElement phrsaeAppiumElement in phrasesAppiumElements)
            {
                getPhrasesListFromUI.Add(phrsaeAppiumElement.Text.Replace(" \r\n      ", "").Replace("\r\n      ", ""));
            }
            int lastElementIndex = getPhrasesListFromUI.Count - 1;
            if (lastElementIndex >= 0)
            {
                if (getPhrasesListFromUI[lastElementIndex] == "To view the next meaning, click here")
                {
                    getPhrasesListFromUI.RemoveAt(lastElementIndex);
                }
            }
            if (count < meaningsListFromDB.Count)
            {
                string nextMeaning = meaningsListFromDB[count];
                GetNextMeaning(nextMeaning).Click();
            }
            return getPhrasesListFromUI;
        }
        public WindowsElement GetMainContainer()
        {
            return _windowUIDriver.GetElement("frmLookupPhrases", WindowUIDriver.ElementSelectorType.ID);
        }
        public AppiumWebElement GetFilterBannerByName(WindowsElement mainContainer, string filterNameForAssertion)
        {
            return _windowUIDriver.GetAppiumElement(filterNameForAssertion, WindowUIDriver.ElementSelectorType.Name, mainContainer);
        }
        public string GetFilterBannerText(string filterNameForAssertion)
        {
            string filterBannerText = "";
            WindowsElement mainContainer = GetMainContainer();
            AppiumWebElement FilterBannerElement = GetFilterBannerByName(mainContainer,filterNameForAssertion);
            if (FilterBannerElement.Text == filterNameForAssertion)
            {
                filterBannerText = FilterBannerElement.Text;
            }
            return filterBannerText;
        }
        public WindowsElement GetFilterBannerCloseButton()
        {
            return _windowUIDriver.GetElement("picbxFilterClose", WindowUIDriver.ElementSelectorType.ID);
        }
        public void CloseBannerAndSelectFirstMeaning(List<string> meaningsListFromDB)
        {
            int count = 0;
            GetFilterBannerCloseButton().Click();
            string nextMeaning = meaningsListFromDB[count];
            GetNextMeaning(nextMeaning).Click();
        }
        public List<string> GetMeaningsListFromDB(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.MeaningsList(randomWordFromDB);
        }
        public List<XmlDocument> GetWordInfoXML(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetWordInfoXML(randomWordFromDB);
        }
        public List<string> GetWordsCountPhrasesList(List<XmlDocument> wordInfoXMLs, int count, string filterNameFromCreateFilter, XmlDocument phrasesXML)
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
            int meaningID = meanings[count - 1].ID;

            List<string> phrases = new List<string>();
            List<string> filteredPhrases = new List<string>();
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
            foreach (string phraseText in phrases)
            {
                string[] wordsInPhrase = phraseText.Replace(".", "").Split(' ');
                List<int> wordsCount = new List<int>();
                foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
                {
                    if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                    {
                        foreach (XmlNode WordsCount in FilterInfo.SelectNodes("//WordsCount"))
                        {
                            wordsCount.Add(Int32.Parse(WordsCount.SelectSingleNode("Min").InnerText.ToString()));
                            wordsCount.Add(Int32.Parse(WordsCount.SelectSingleNode("Max").InnerText.ToString()));
                        }
                    }
                }
                if (wordsInPhrase.Count() >= wordsCount[0] && wordsInPhrase.Count() <= wordsCount[1])
                {
                    filteredPhrases.Add(phraseText);
                }
            }

            return filteredPhrases;
        }
        public List<string> GetPhrasesList(List<XmlDocument> wordInfoXMLs, int count)
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
            int meaningID = meanings[count - 1].ID;

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
        public List<UniqueUsesCount> GetUniqueUsesAndPhrasesList(List<XmlDocument> wordInfoXMLs, int count)
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
            int meaningID = meanings[count - 1].ID;

            List<UniqueUsesCount> uniqueUsesList = new List<UniqueUsesCount>();
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

                foreach (XmlNode phraseNode in phraseNodes)
                {
                    UniqueUsesCount UniqueUses = new UniqueUsesCount
                    {
                        PhraseText = phraseNode.SelectSingleNode("TEXT").InnerText.ToString(),
                        UniqueUses = Int32.Parse(phraseNode.SelectSingleNode("UCOUNT").InnerText.ToString())
                    };
                    uniqueUsesList.Add(UniqueUses);
                }
                uniqueUsesList.Sort();
            }
            return uniqueUsesList;
        }
        public List<string> GetUniqueUsesOrLessPhrasesList(string filterNameFromCreateFilter, List<UniqueUsesCount> uniqueUsesAndPhrasesText, XmlDocument phrasesXML)
        {
            List<string> filteredPhrases = new List<string>();
            int uniqueUsesValueFromFilter = 0;

            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//UniqueUsesCount"))
                    {
                        uniqueUsesValueFromFilter = Int32.Parse(UniqueUses.SelectSingleNode("Count").InnerText.ToString());

                    }
                }
            }
            for (int number = 0; number < uniqueUsesAndPhrasesText.Count; number++)
            {
                if (uniqueUsesAndPhrasesText[number].UniqueUses <= uniqueUsesValueFromFilter)
                {
                    filteredPhrases.Add(uniqueUsesAndPhrasesText[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetUniqueUsesApproximatelyPhrasesList(string filterNameFromCreateFilter, List<UniqueUsesCount> uniqueUsesAndPhrasesText, XmlDocument phrasesXML)
        {
            List<string> filteredPhrases = new List<string>();
            int uniqueUsesValueFromFilter = 0;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//UniqueUsesCount"))
                    {
                        uniqueUsesValueFromFilter = Int32.Parse(UniqueUses.SelectSingleNode("Count").InnerText.ToString());

                    }
                }
            }
            for (int number = 0; number < uniqueUsesAndPhrasesText.Count; number++)
            {
                if (uniqueUsesAndPhrasesText[number].UniqueUses == uniqueUsesValueFromFilter)
                {
                    filteredPhrases.Add(uniqueUsesAndPhrasesText[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetUniqueUsesOrMorePhrasesList(string filterNameFromCreateFilter, List<UniqueUsesCount> uniqueUsesAndPhrasesText, XmlDocument phrasesXML)
        {
            List<string> filteredPhrases = new List<string>();
            int uniqueUsesValueFromFilter = 0;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//UniqueUsesCount"))
                    {
                        uniqueUsesValueFromFilter = Int32.Parse(UniqueUses.SelectSingleNode("Count").InnerText.ToString());

                    }
                }
            }
            for (int number = 0; number < uniqueUsesAndPhrasesText.Count; number++)
            {
                if (uniqueUsesAndPhrasesText[number].UniqueUses >= uniqueUsesValueFromFilter)
                {
                    filteredPhrases.Add(uniqueUsesAndPhrasesText[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<PhraseYear> GetPhraseYearAndPhrasesList(List<XmlDocument> wordInfoXMLs, int count)
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
            int meaningID = meanings[count - 1].ID;

            List<PhraseYear> phraseYearList = new List<PhraseYear>();
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

                foreach (XmlNode phraseNode in phraseNodes)
                {
                    if (phraseNode.SelectSingleNode("FIRSTYR") == null)
                    {
                        PhraseYear PhraseYears = new PhraseYear
                        {
                            PhraseText = phraseNode.SelectSingleNode("TEXT").InnerText.ToString(),
                            Year = 0
                        };
                        phraseYearList.Add(PhraseYears);
                    }
                    else
                    {
                        PhraseYear PhraseYears = new PhraseYear
                        {
                            PhraseText = phraseNode.SelectSingleNode("TEXT").InnerText.ToString(),
                            Year = Int32.Parse(phraseNode.SelectSingleNode("FIRSTYR").InnerText.ToString())
                        };
                        phraseYearList.Add(PhraseYears);
                    }

                }
                phraseYearList.Sort();
            }
            return phraseYearList;
        }
        public List<string> GetPhrasesFromYearList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList,XmlDocument phrasesXML)
        {
            List<string> filteredPhrases = new List<string>();
            int phrasesFromYear = 0;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        phrasesFromYear = Int32.Parse(UniqueUses.SelectSingleNode("Year").InnerText.ToString());

                    }
                }
            }
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year == phrasesFromYear)
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesFromYearRangeList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList,XmlDocument phrasesXML)
        {
            List<string> filteredPhrases = new List<string>();
            List<int> phrasesFromYearRange = new List<int>();
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        phrasesFromYearRange.Add(Int32.Parse(UniqueUses.SelectSingleNode("Min").InnerText.ToString()));
                        phrasesFromYearRange.Add(Int32.Parse(UniqueUses.SelectSingleNode("Max").InnerText.ToString()));
                    }
                }
            }
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year >= phrasesFromYearRange[0] && phraseYearAndPhrasesList[number].Year <= phrasesFromYearRange[1])
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesFromBeforeYearList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList,XmlDocument phrasesXML)
        {
            List<string> filteredPhrases = new List<string>();
            int phrasesBeforeYear = 0;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        phrasesBeforeYear = Int32.Parse(UniqueUses.SelectSingleNode("Year").InnerText.ToString());

                    }
                }
            }
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year < phrasesBeforeYear)
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesFromAfterYearList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList,XmlDocument phrasesXML)
        {
            List<string> filteredPhrases = new List<string>();
            int phrasesAfterYear = 0;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhraseStartedIn"))
                    {
                        phrasesAfterYear = Int32.Parse(UniqueUses.SelectSingleNode("Year").InnerText.ToString());

                    }
                }
            }
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year > phrasesAfterYear)
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesListForEnglishOrTranslated(List<XmlDocument> wordInfoXMLs, int count, string filterNameFromCreateFilter,XmlDocument phrasesXML)
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
            int meaningID = meanings[count - 1].ID;

            List<PhraseEnglishOrTranslated> englishOrTranslatedList = new List<PhraseEnglishOrTranslated>();
            List<string> filteredPhrasesList = new List<string>();
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

                foreach (XmlNode phraseNode in phraseNodes)
                {
                    PhraseEnglishOrTranslated EnglishOrtranslated = new PhraseEnglishOrTranslated
                    {
                        PhraseText = phraseNode.SelectSingleNode("TEXT").InnerText.ToString(),
                        EnglishOrTranslated = phraseNode.SelectSingleNode("TRANS").InnerText.ToString()
                    };
                    englishOrTranslatedList.Add(EnglishOrtranslated);
                }
                englishOrTranslatedList.Sort();
            }
            string englishOrTranslatedphrasesFromLocalXML = "";
            string englishOrTranslated = "";
            int comaparisionNumber = 0;
            foreach (XmlNode FilterInfo in phrasesXML.SelectNodes("//PhrasesFilters"))
            {
                if (FilterInfo["PhrasesFilter"].GetAttribute("Name") == filterNameFromCreateFilter)
                {
                    foreach (XmlNode UniqueUses in FilterInfo.SelectNodes("//PhrasesFilter"))
                    {
                        comaparisionNumber = Int32.Parse(UniqueUses.SelectSingleNode("EnmLanguage").InnerText.ToString());
                        if (comaparisionNumber == 1)
                        {
                            englishOrTranslated = "English Phrases Only";
                        }
                        else
                        {
                            englishOrTranslated = "Translated Phrases Only";
                        }

                    }
                }
            }
            if (englishOrTranslated == "English Phrases Only")
            {
                englishOrTranslatedphrasesFromLocalXML = "false";
            }
            else
            {
                englishOrTranslatedphrasesFromLocalXML = "true";
            }
            for (int number = 0; number < englishOrTranslatedList.Count; number++)
            {
                if (englishOrTranslatedList[number].EnglishOrTranslated.ToString() == englishOrTranslatedphrasesFromLocalXML)
                {
                    filteredPhrasesList.Add(englishOrTranslatedList[number].PhraseText);
                }
            }
            return filteredPhrasesList;
        }
        public WindowsElement GetRenameTextBoxElement()
        {
            return _windowUIDriver.GetElement("Editing Control", WindowUIDriver.ElementSelectorType.Name);
        }
        public string RenameFilterNameFromManageFilters(string usesCountOrLessFilterNameFromCreateFilter)
        {
            ManageFiltersLinkElement().Click();
            List<WindowsElement> linkElementsFromManageFilters = GetLinkElementsFromManageFilters();
            foreach (WindowsElement Element in linkElementsFromManageFilters)
            {
                if (Element.Text == "rename")
                {
                    _windowUIDriver.ClickAtStartPointOfElement(Element);
                }
            }
            GetRenameTextBoxElement().SendKeys(usesCountOrLessFilterNameFromCreateFilter + "_Renamed");
            ContainingWordsTextBoxElement().Click();
            return usesCountOrLessFilterNameFromCreateFilter + "_Renamed";
        }
        public WindowsElement UniqueUsesApproximatelyFilterNameElement()
        {
            return _windowUIDriver.GetElement("PhrasesFilter Row 1", WindowUIDriver.ElementSelectorType.Name);
        }
        public WindowsElement UniqueUsesOrMoreFilterNameElement()
        {
            return _windowUIDriver.GetElement("PhrasesFilter Row 2", WindowUIDriver.ElementSelectorType.Name);
        }
        public string UsesCountApproximatelyFilterNameFromManageFilters()
        {
            return UniqueUsesApproximatelyFilterNameElement().Text;
        }
        public string UsesCountOrMoreFilterNameFromManageFilters()
        {
            return UniqueUsesOrMoreFilterNameElement().Text;
        }
        public void DeleteOneFilter()
        {
            List<WindowsElement> linkElementsFromManageFilters = GetLinkElementsFromManageFilters();
            foreach (WindowsElement Element in linkElementsFromManageFilters)
            {
                if (Element.Text == "delete")
                {
                    _windowUIDriver.ClickAtStartPointOfElement(Element);
                    WaitForDeleteFilterConfirmation();
                    GetYesButton().Click();
                }
            }
        }
        public void ClickDeleteFilterAndSelectNoAtConfirmation()
        {
            ManageFiltersLinkElement().Click();
            List<WindowsElement> linkElementsFromManageFilters = GetLinkElementsFromManageFilters();
            foreach (WindowsElement Element in linkElementsFromManageFilters)
            {
                if (Element.Text == "delete")
                {
                    _windowUIDriver.ClickAtStartPointOfElement(Element);
                    WaitForDeleteFilterConfirmation();
                    GetNoButton().Click();
                }
            }
        }
        public void DeleteUsesCountOrLessFilter()
        {
            ManageFiltersLinkElement().Click();
            List<WindowsElement> linkElementsFromManageFilters = GetLinkElementsFromManageFilters();
            foreach (WindowsElement Element in linkElementsFromManageFilters)
            {
                if (Element.Text == "delete")
                {
                    _windowUIDriver.ClickAtStartPointOfElement(Element);
                    WaitForDeleteFilterConfirmation();
                    GetYesButton().Click();
                }
            }
        }
        public WindowsElement GetNoFilterMessageElement()
        {
            return _windowUIDriver.GetElement("You have no filters at present", WindowUIDriver.ElementSelectorType.Name);
        }
        public string GetNoFiltersMessageText()
        {
            WindowsElement noFiltersMessageElement = GetNoFilterMessageElement();
            return noFiltersMessageElement.Text;
        }
    }
}
