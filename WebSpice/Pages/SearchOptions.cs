using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Quant.Spice.Test.UI.Web.WebSpice.Pages
{
    public class SearchOptions : WebPage
    {
        protected static SearchKeywordDataAccess _dataAccess;
        Random _random = new Random();
        public SearchOptions(WebBrowser browser) : base(browser)
        {

        }
        public IWebElement SearchOptionsLinkElement()
        {
            return _browser.GetElement("#search-options-link", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement KeywordTextBoxElement()
        {
            return _browser.GetElement("keywords-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement ContainingWordsTextBoxElement()
        {
            return _browser.GetElement("containing-words-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement SearchButtonElement()
        {
            return _browser.GetElement("btnSearch", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForMeaningsToLoad()
        {
            return _browser.WaitForElement("meanings-list", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForPhrasesToLoad()
        {
            return _browser.WaitForElement("phrases-grid", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetKeywordFromTextbox()
        {
            return _browser.GetElement("txtSearchWord", WebBrowser.ElementSelectorType.ID);
        }
        public List<IWebElement> GetPhrasesListWebElement()
        {
            return _browser.GetElements("#phrases-container > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetCreateNewLinkElement()
        {
            return _browser.GetElement("create-filter-link", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetStartRangeForWordsTextBoxElement()
        {
            return _browser.GetElement("starting-word-range-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEndRangeForWordsTextBoxElement()
        {
            return _browser.GetElement("ending-word-range-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement FilterNameTextBoxElement()
        {
            return _browser.GetElement("save-filter-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement SaveFilterButtonElement()
        {
            return _browser.GetElement("btnSaveFilter", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement ManageFiltersLinkElement()
        {
            return _browser.GetElement("manage-filter-link", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement FilterNameElement()
        {
            return _browser.GetElement("filter-name-display", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement UniqueUsesApproximatelyFilterNameElement()
        {
            return _browser.GetElement("filterName1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement UniqueUsesOrMoreFilterNameElement()
        {
            return _browser.GetElement("filterName2", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUniqueUsesTextBoxElement()
        {
            return _browser.GetElement("unique-uses-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUniqueUsesOrLessButtonElement()
        {
            return _browser.GetElement("less-than-uses-count", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUniqueUsesApproximatelyButtonElement()
        {
            return _browser.GetElement("approximate-uses-count", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUniqueUsesOrMoreButtonElement()
        {
            return _browser.GetElement("greater-than-uses-count", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhrasesFromYearButtonElement()
        {
            return _browser.GetElement("divFromYear", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRangeOfYearsButtonElement()
        {
            return _browser.GetElement("divPhrasesRange", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhrasesBeforeYearButtonElement()
        {
            return _browser.GetElement("divBeforYear", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhrasesAfterYearButtonElement()
        {
            return _browser.GetElement("divAfterYear", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhrasesFromYearTextBoxElement()
        {
            return _browser.GetElement("phrases-from-year-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetStartRangeForYearTextBoxElement()
        {
            return _browser.GetElement("phrases-range-from-year-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEndRangeForYearTextBoxElement()
        {
            return _browser.GetElement("phrases-range-to-year-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhrasesBeforeYearTextBoxElement()
        {
            return _browser.GetElement("phrases-before-year-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhrasesAfterYearTextBoxElement()
        {
            return _browser.GetElement("phrases-after-year-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetEnglishPhrasesButtonElement()
        {
            return _browser.GetElement("lblForEnglishPhrases", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetTranslatedPhrasesButtonElement()
        {
            return _browser.GetElement("lblForTranslatedPhrases", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement EnglishPhrasesFilter()
        {
            return _browser.GetElement("filterName0", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement TranslatedPhrasesFilter()
        {
            return _browser.GetElement("filterName1", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUseExistingLinkElement()
        {
            return _browser.GetElement("existing-filter-link", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetUseExistingDropdownLinkElement()
        {
            return _browser.GetElement("existing-filters-dropdown", WebBrowser.ElementSelectorType.ID);
        }
        public List<IWebElement> GetPhrasesList()
        {
            return _browser.GetElements("#phrases-container > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement FilterBannerTextElement()
        {
            return _browser.GetElement("filter-banner-text", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetNextMeaning(int count)
        {
            return _browser.GetElement("#meanings > ul > li:nth-child(" + count + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetFilterBannerCloseButton()
        {
            return _browser.GetElement("filter-banner-close", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRenameLinkElement()
        {
            return _browser.GetElement("renameFilter", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRenameTextBoxElement()
        {
            return _browser.GetElement("filterNameTextbox0", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetFilterContainerElement()
        {
            return _browser.GetElement("manage-filter-container", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement UniqueUsesApproximatelyFilterDeleteLinkElement()
        {
            return _browser.GetElement("//*[@id='manageFiltersGrid']/tbody/tr[2]/td[3]/a", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement UniqueUsesOrLessFilterDeleteLinkElement()
        {
            return _browser.GetElement("//*[@id='manageFiltersGrid']/tbody/tr[1]/td[3]/a", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement NoButtonElementFromDeleteFilterConfirmation()
        {
            return _browser.GetElement("#delete-filter-confirm-dialog-widget > div.ui-dialog-buttonpane.ui-widget-content.ui-helper-clearfix > div > button:nth-child(2)", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement YesButtonElementFromDeleteFilterConfirmation()
        {
            return _browser.GetElement("#delete-filter-confirm-dialog-widget > div.ui-dialog-buttonpane.ui-widget-content.ui-helper-clearfix > div > button:nth-child(1)", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement NoFiltersDisplayElement()
        {
            return _browser.GetElement("noFiltersDisplay", WebBrowser.ElementSelectorType.ID);
        }
        public Cookie GetFilterCookie(string filterNameFromCreateFilter)
        {
            return _browser.GetCookie(filterNameFromCreateFilter);
        }
        public string GetRandomWord()
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetRandomWord();
        }
        public void OpenSearchOptionsWindow()
        {
            SearchOptionsLinkElement().Click();
            _browser.SwitchtoCurrentWindow();
        }
        public void EnterKeywordAndSearch(string randomWordFromDB)
        {
            KeywordTextBoxElement().SendKeys(randomWordFromDB);
            SearchButtonElement().Click();
        }
        public string KeywordFromTextBox()
        {
            _browser.SwitchtoPreviousWindow();
            WaitForMeaningsToLoad();
            return GetKeywordFromTextbox().GetAttribute("value");
        }
        public void SwitchToMainWindow()
        {
            _browser.SwitchtoPreviousWindow();
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
        public List<string> PhraseResultsFromUI(List<string> containingWords)
        {
            List<string> phraseresults = new List<string>();

            ContainingWordsTextBoxElement().SendKeys(containingWords[0] + " " + containingWords[1]);
            SearchButtonElement().Click();
            _browser.SwitchtoPreviousWindow();
            WaitForPhrasesToLoad();
            List<IWebElement> PhrasesListWebElement = GetPhrasesListWebElement();
            foreach (IWebElement phraseWebElement in PhrasesListWebElement)
            {
                phraseresults.Add(phraseWebElement.Text);
            }
            return phraseresults;
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
            _browser.SwitchToAlertWindowAndAccept();
            return startRange + "to" + endRange + "words";
        }
        public string FilterNameFromManageFilters()
        {
            ManageFiltersLinkElement().Click();
            return FilterNameElement().Text;
        }
        public string CreateFilterForUsesCountOrLess(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomUsesCount = _random.Next(minimumValue, maximumValue).ToString();
            GetUniqueUsesTextBoxElement().SendKeys(randomUsesCount);
            GetUniqueUsesOrLessButtonElement().Click();
            FilterNameTextBoxElement().SendKeys(randomUsesCount + "_unique_uses_or_less");
            SaveFilterButtonElement().Click();
            _browser.SwitchToAlertWindowAndAccept();
            return randomUsesCount + "_unique_uses_or_less";
        }
        public string CreateFilterForUsesCountApproximately(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomUsesCount = _random.Next(minimumValue, maximumValue).ToString();
            GetUniqueUsesTextBoxElement().SendKeys(randomUsesCount);
            GetUniqueUsesApproximatelyButtonElement().Click();
            FilterNameTextBoxElement().SendKeys(randomUsesCount + "_unique_uses_approximately");
            SaveFilterButtonElement().Click();
            _browser.SwitchToAlertWindowAndAccept();
            return randomUsesCount + "_unique_uses_approximately";
        }
        public string CreateFilterForUsesCountOrMore(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomUsesCount = _random.Next(minimumValue, maximumValue).ToString();
            GetUniqueUsesTextBoxElement().SendKeys(randomUsesCount);
            GetUniqueUsesOrMoreButtonElement().Click();
            FilterNameTextBoxElement().SendKeys(randomUsesCount + "_unique_uses_or_more");
            SaveFilterButtonElement().Click();
            _browser.SwitchToAlertWindowAndAccept();
            return randomUsesCount + "_unique_uses_or_more";
        }
        public string CreateFilterForPhrasesFromYear(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomYearValue = _random.Next(minimumValue, maximumValue).ToString();
            GetPhrasesFromYearButtonElement().Click();
            GetPhrasesFromYearTextBoxElement().SendKeys(randomYearValue);
            FilterNameTextBoxElement().SendKeys("phrases_from_year_" + randomYearValue);
            SaveFilterButtonElement().Click();
            _browser.SwitchToAlertWindowAndAccept();
            return "phrases_from_year_" + randomYearValue;
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
            _browser.SwitchToAlertWindowAndAccept();
            return "years_from" + startYearRange + "_to_" + endYearRange;
        }
        public string CreateFilterForPhrasesBeforeYear(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomYearValue = _random.Next(minimumValue, maximumValue).ToString();
            GetPhrasesBeforeYearButtonElement().Click();
            GetPhrasesBeforeYearTextBoxElement().SendKeys(randomYearValue);
            FilterNameTextBoxElement().SendKeys("phrases_before_year_" + randomYearValue);
            SaveFilterButtonElement().Click();
            _browser.SwitchToAlertWindowAndAccept();
            return "phrases_before_year_" + randomYearValue;
        }
        public string CreateFilterForPhrasesAfterYear(int minimumValue, int maximumValue)
        {
            GetCreateNewLinkElement().Click();
            string randomYearValue = _random.Next(minimumValue, maximumValue).ToString();
            GetPhrasesAfterYearButtonElement().Click();
            GetPhrasesAfterYearTextBoxElement().SendKeys(randomYearValue);
            FilterNameTextBoxElement().SendKeys("phrases_after_year_" + randomYearValue);
            SaveFilterButtonElement().Click();
            _browser.SwitchToAlertWindowAndAccept();
            return "phrases_after_year_" + randomYearValue;
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
                _browser.SwitchToAlertWindowAndAccept();
                result = "english_phrases_only";
            }
            else
            {
                GetTranslatedPhrasesButtonElement().Click();
                FilterNameTextBoxElement().SendKeys("translated_phrases_only");
                SaveFilterButtonElement().Click();
                _browser.SwitchToAlertWindowAndAccept();
                result = "translated_phrases_only";
            }
            return result;
        }
        public List<int> GetRangeForCreatedFilter(string filterNameFromCreateFilter)
        {
            List<int> minAndMaxRange = new List<int>();
            GetUseExistingLinkElement().Click();
            SelectElement dropdown = new SelectElement(GetUseExistingDropdownLinkElement());
            dropdown.SelectByText(filterNameFromCreateFilter);
            minAndMaxRange.Add(Int32.Parse(GetStartRangeForWordsTextBoxElement().GetAttribute("value")));
            minAndMaxRange.Add(Int32.Parse(GetEndRangeForWordsTextBoxElement().GetAttribute("value")));
            return minAndMaxRange;
        }
        public List<int> GetRangeFromCookie(string filterNameFromCreateFilter)
        {
            List<int> minAndMaxRange = new List<int>();
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            //source url for decrypting cookie https://stackoverflow.com/questions/12898556/decode-percent-encoded-string-c-sharp-net
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            minAndMaxRange.Add(Int32.Parse(list[6]));
            minAndMaxRange.Add(Int32.Parse(list[8]));
            return minAndMaxRange;
        }
        public int GetUniqueUsesForCreatedFilter(string filterNameFromCreateFilter)
        {
            GetUseExistingLinkElement().Click();
            SelectElement dropdown = new SelectElement(GetUseExistingDropdownLinkElement());
            dropdown.SelectByText(filterNameFromCreateFilter);
            return Int32.Parse(GetUniqueUsesTextBoxElement().GetAttribute("value"));
        }
        public string GetOpacityForUniqueUsesOrLess()
        {
            int enableButtonOpacity = 1;
            string enabledButton = "";
            if (enableButtonOpacity == Int32.Parse(GetUniqueUsesOrLessButtonElement().GetCssValue("opacity")))
            {
                enabledButton = "or less";
            }
            return enabledButton;
        }
        public int GetUniqueUsesCountFromCookie(string filterNameFromCreateFilter)
        {
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            return Int32.Parse(list[11]);
        }
        public string GetComparisonOfUsesFromCookie(string filterNameFromCreateFilter)
        {
            string enabledButton = "";
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            if (Int32.Parse(list[13]) == 0)
            {
                enabledButton = "or less";
            }
            else if (Int32.Parse(list[13]) == 1)
            {
                enabledButton = "approximately";
            }
            else if (Int32.Parse(list[13]) == 2)
            {
                enabledButton = "or more";
            }
            return enabledButton;
        }
        public string GetOpacityForUniqueUsesApproximately()
        {
            int enableButtonOpacity = 1;
            string enabledButton = "";
            if (enableButtonOpacity == Int32.Parse(GetUniqueUsesApproximatelyButtonElement().GetCssValue("opacity")))
            {
                enabledButton = "approximately";
            }
            return enabledButton;
        }
        public string GetOpacityForUniqueUsesOrMore()
        {
            int enableButtonOpacity = 1;
            string enabledButton = "";
            if (enableButtonOpacity == Int32.Parse(GetUniqueUsesOrMoreButtonElement().GetCssValue("opacity")))
            {
                enabledButton = "or more";
            }
            return enabledButton;
        }
        public void ApplyCreatedFilter(string filterNameFromCreateFilter)
        {
            GetUseExistingLinkElement().Click();
            SelectElement dropdown = new SelectElement(GetUseExistingDropdownLinkElement());
            dropdown.SelectByText(filterNameFromCreateFilter);
        }
        public int GetPhrasesFromYear()
        {
            return Int32.Parse(GetPhrasesFromYearTextBoxElement().GetAttribute("value"));
        }
        public int GetPhrasesFromYearFromCookie(string filterNameFromCreateFilter)
        {
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            return Int32.Parse(list[16]);
        }
        public List<int> GetPhrasesYearRange()
        {
            List<int> PhrasesYearRange = new List<int>
            {
                Int32.Parse(GetStartRangeForYearTextBoxElement().GetAttribute("value")),
                Int32.Parse(GetEndRangeForYearTextBoxElement().GetAttribute("value"))
            };
            return PhrasesYearRange;
        }
        public List<int> GetPhrasesFromYearRangeFromCookie(string filterNameFromCreateFilter)
        {
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            List<int> PhrasesYearRange = new List<int>
            {
                Int32.Parse(list[18]),
                Int32.Parse(list[20])
            };
            return PhrasesYearRange;
        }
        public int GetPhrasesBeforeYear()
        {
            return Int32.Parse(GetPhrasesBeforeYearTextBoxElement().GetAttribute("value"));
        }
        public int GetPhrasesBeforeYearFromCookie(string filterNameFromCreateFilter)
        {
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            return Int32.Parse(list[16]);
        }
        public int GetPhrasesAfterYear()
        {
            return Int32.Parse(GetPhrasesAfterYearTextBoxElement().GetAttribute("value"));
        }
        public int GetPhrasesAfterYearFromCookie(string filterNameFromCreateFilter)
        {
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            return Int32.Parse(list[16]);
        }
        public string GetPhrasesEnglishOrTranslated()
        {
            string englishOrTranslated = "";

            if (GetEnglishPhrasesButtonElement().GetCssValue("opacity") == "1")
            {
                englishOrTranslated = "english_phrases_only";
            }
            else
            {
                englishOrTranslated = "translated_phrases_only";
            }
            return englishOrTranslated;
        }
        public string GetPhrasesEnglishOrTranslatedFromCookie(string filterNameFromCreateFilter)
        {
            string englishOrTranslated = "";
            Cookie filterCookie = GetFilterCookie(filterNameFromCreateFilter);
            string decryptedFilterInfo = System.Uri.UnescapeDataString(filterCookie.Value).Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
            string[] list = decryptedFilterInfo.Split(',', ':');
            if (list[3] == "0")
            {
                englishOrTranslated = "english_phrases_only";
            }
            else
            {
                englishOrTranslated = "translated_phrases_only";
            }
            return englishOrTranslated;
        }
        public void SearchRandomKeywordAndOpenSearchOptions(string randomWordFromDB)
        {
            EnterKeywordAndSearch(randomWordFromDB);
            SwitchToMainWindow();
            OpenSearchOptionsWindow();
        }
        public void UseCreatedFilterAndSearch(string filterNameFromCreateFilter)
        {
            GetUseExistingLinkElement().Click();
            SelectElement dropdown = new SelectElement(GetUseExistingDropdownLinkElement());
            dropdown.SelectByText(filterNameFromCreateFilter);
            SearchButtonElement().Click();
            _browser.SwitchtoPreviousWindow();
        }
        public List<string> GetPhrasesListFromUI(List<string> meaningsListFromDB, int count)
        {
            List<string> getPhrasesListFromUI = new List<string>();
            List<IWebElement> phrasesWebElement = GetPhrasesList();
            foreach (IWebElement phrasesElement in phrasesWebElement)
            {
                getPhrasesListFromUI.Add(phrasesElement.Text);
            }
            if (count < meaningsListFromDB.Count)
            {
                GetNextMeaning(count + 1).Click();
            }
            return getPhrasesListFromUI;
        }
        public string GetFilterBannerText()
        {
            return FilterBannerTextElement().Text;
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
        public List<string> GetWordsCountPhrasesList(List<XmlDocument> wordInfoXMLs, int count, string filterNameFromCreateFilter)
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
                List<int> wordsCount = GetRangeFromCookie(filterNameFromCreateFilter);
                if (wordsInPhrase.Count() >= wordsCount[0] && wordsInPhrase.Count() <= wordsCount[1])
                {
                    filteredPhrases.Add(phraseText);
                }
            }

            return filteredPhrases;
        }
        public void CloseBannerAndSelectFirstMeaning()
        {
            int count = 1;
            GetFilterBannerCloseButton().Click();
            GetNextMeaning(count).Click();
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
        public List<string> GetUniqueUsesOrLessPhrasesList(string filterNameFromCreateFilter, List<UniqueUsesCount> uniqueUsesAndPhrasesText)
        {
            List<string> filteredPhrases = new List<string>();
            int uniqueUsesValueFromFilter = GetUniqueUsesCountFromCookie(filterNameFromCreateFilter);
            for (int number = 0; number < uniqueUsesAndPhrasesText.Count; number++)
            {
                if (uniqueUsesAndPhrasesText[number].UniqueUses <= uniqueUsesValueFromFilter)
                {
                    filteredPhrases.Add(uniqueUsesAndPhrasesText[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetUniqueUsesApproximatelyPhrasesList(string filterNameFromCreateFilter, List<UniqueUsesCount> uniqueUsesAndPhrasesText)
        {
            List<string> filteredPhrases = new List<string>();
            int uniqueUsesValueFromFilter = GetUniqueUsesCountFromCookie(filterNameFromCreateFilter);
            for (int number = 0; number < uniqueUsesAndPhrasesText.Count; number++)
            {
                if (uniqueUsesAndPhrasesText[number].UniqueUses == uniqueUsesValueFromFilter)
                {
                    filteredPhrases.Add(uniqueUsesAndPhrasesText[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetUniqueUsesOrMorePhrasesList(string filterNameFromCreateFilter, List<UniqueUsesCount> uniqueUsesAndPhrasesText)
        {
            List<string> filteredPhrases = new List<string>();
            int uniqueUsesValueFromFilter = GetUniqueUsesCountFromCookie(filterNameFromCreateFilter);
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
        public List<string> GetPhrasesFromYearList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList)
        {
            List<string> filteredPhrases = new List<string>();
            int phrasesFromYear = GetPhrasesFromYearFromCookie(filterNameFromCreateFilter);
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year == phrasesFromYear)
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesFromYearRangeList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList)
        {
            List<string> filteredPhrases = new List<string>();
            List<int> phrasesFromYearRange = GetPhrasesFromYearRangeFromCookie(filterNameFromCreateFilter);
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year >= phrasesFromYearRange[0] && phraseYearAndPhrasesList[number].Year <= phrasesFromYearRange[1])
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesFromBeforeYearList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList)
        {
            List<string> filteredPhrases = new List<string>();
            int phrasesBeforeYear = GetPhrasesBeforeYearFromCookie(filterNameFromCreateFilter);
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year < phrasesBeforeYear)
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesFromAfterYearList(string filterNameFromCreateFilter, List<PhraseYear> phraseYearAndPhrasesList)
        {
            List<string> filteredPhrases = new List<string>();
            int phrasesAfterYear = GetPhrasesAfterYearFromCookie(filterNameFromCreateFilter);
            for (int number = 0; number < phraseYearAndPhrasesList.Count; number++)
            {
                if (phraseYearAndPhrasesList[number].Year > phrasesAfterYear)
                {
                    filteredPhrases.Add(phraseYearAndPhrasesList[number].PhraseText);
                }
            }
            return filteredPhrases;
        }
        public List<string> GetPhrasesListForEnglishOrTranslated(List<XmlDocument> wordInfoXMLs, int count, string filterNameFromCreateFilter)
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
            string englishOrTranslatedphrasesFromCookie = ""; GetPhrasesEnglishOrTranslatedFromCookie(filterNameFromCreateFilter);
            if (GetPhrasesEnglishOrTranslatedFromCookie(filterNameFromCreateFilter) == "english_phrases_only")
            {
                englishOrTranslatedphrasesFromCookie = "false";
            }
            else
            {
                englishOrTranslatedphrasesFromCookie = "true";
            }
            for (int number = 0; number < englishOrTranslatedList.Count; number++)
            {
                if (englishOrTranslatedList[number].EnglishOrTranslated.ToString() == englishOrTranslatedphrasesFromCookie)
                {
                    filteredPhrasesList.Add(englishOrTranslatedList[number].PhraseText);
                }
            }
            return filteredPhrasesList;
        }
        public string UsesCountApproximatelyFilterNameFromManageFilters()
        {
            return UniqueUsesApproximatelyFilterNameElement().Text;
        }
        public string UsesCountOrLessFilterNameFromManageFilters()
        {
            ManageFiltersLinkElement().Click();
            return UniqueUsesOrMoreFilterNameElement().Text;
        }
        public string RenameFilterNameFromManageFilters(string usesCountOrLessFilterNameFromCreateFilter)
        {
            ManageFiltersLinkElement().Click();
            GetRenameLinkElement().Click();
            GetRenameTextBoxElement().SendKeys("_Renamed");
            GetFilterContainerElement().Click();
            return usesCountOrLessFilterNameFromCreateFilter + "_Renamed";
        }
        public List<string> GetFiltersList(string usesCountOrLessFilterNameFromCreateFilter, string usesCountApproximatelyFilterNameFromCreateFilter)
        {
            List<string> filtersList = new List<string>
            {
                usesCountOrLessFilterNameFromCreateFilter,
                usesCountApproximatelyFilterNameFromCreateFilter
            };
            return filtersList;
        }
        public List<string> GetFiltersListAfterSelectingNoFromDeleteFilter()
        {
            List<string> filtersList = new List<string>();
            ManageFiltersLinkElement().Click();
            UniqueUsesApproximatelyFilterDeleteLinkElement().Click();
            NoButtonElementFromDeleteFilterConfirmation().Click();
            filtersList.Add(FilterNameElement().Text);
            filtersList.Add(UniqueUsesApproximatelyFilterNameElement().Text);
            return filtersList;
        }
        public void DeleteUsesCountOrLessFilter()
        {
            ManageFiltersLinkElement().Click();
            UniqueUsesOrLessFilterDeleteLinkElement().Click();
            YesButtonElementFromDeleteFilterConfirmation().Click();
        }
        public string GetExistingFilterName()
        {
            return FilterNameElement().Text;
        }
        public string GetNoFiltersMessageText()
        {
            string messageText = "";
            if (NoFiltersDisplayElement().GetCssValue("display") == "block")
            {
                messageText = NoFiltersDisplayElement().Text;
            }
            return messageText;
        }
    }
}
