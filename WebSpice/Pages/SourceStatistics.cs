using OpenQA.Selenium;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Web;
using System;
using System.Collections.Generic;
using System.Xml;
using SourceStatisticsModel = Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp.SourceStatistics;

namespace Quant.Spice.Test.UI.Web.WebSpice.Pages
{
    public class SourceStatistics : WebPage
    {
        protected static SourceStatisticsDataAccess _sourceStatisticsDataAccess;
        SourceStatisticsModel _sourceStatistics = new SourceStatisticsModel();
        Random _random = new Random();
        readonly string elementText = "------";

        public SourceStatistics(WebBrowser browser) : base(browser)
        {

        }
        public IWebElement SourceStatisticsLinkElement()
        {
            return _browser.GetElement("#source-statistics-link", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement SearchButtonElement()
        {
            return _browser.GetElement("#btnSearch", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement TotalWorksElement()
        {
            return _browser.GetElement("#english-works-total", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement TranslatedWorksElement()
        {
            return _browser.GetElement("#translated-works-total", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement EnglishSourceElement()
        {
            return _browser.GetElement("#oldest-english-source-year", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement TranslatedSourceElement()
        {
            return _browser.GetElement("#oldest-translated-source-year", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool WaitForAuthorElementTextToLoad()
        {
            return _browser.WaitForElementText(elementText, "#author-works-total", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool WaitForYearElementTextToLoad()
        {
            return _browser.WaitForElementText(elementText, "#phrases-in-year", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool WaitForUniqueUsesTextToLoad()
        {
            return _browser.WaitForElementText(elementText, "#unique-uses-length", WebBrowser.ElementSelectorType.CssSelector);
        }
        public void OpenSourceStatisticsWindow()
        {
            SourceStatisticsLinkElement().Click();
            _browser.SwitchtoCurrentWindow();
        }
        public void ClickSearchButton()
        {
            SearchButtonElement().Click();
        }
        public int TotalWorksUI()
        {
            return Int32.Parse(TotalWorksElement().Text);
        }
        public int TranslatedWorksUI()
        {
            return Int32.Parse(TranslatedWorksElement().Text);
        }
        public int EnglishSourceUI()
        {
            return Int32.Parse(EnglishSourceElement().Text);
        }
        public int TranslatedSourceUI()
        {
            return Int32.Parse(TranslatedSourceElement().Text);
        }
        public XmlDocument GetSourceStatisticsXML()
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.GetSourceStatisticsXML();
        }
        public int TotalWorksDB(XmlDocument sourcesXML)
        {
            foreach (XmlNode SourceStatisticsNode in sourcesXML.SelectNodes("//SOURCES_STATISTICS"))
            {
                _sourceStatistics.TotalWorksFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("ALL_SOURCES").InnerText.ToString());
            }
            return _sourceStatistics.TotalWorksFromDB;
        }
        public int TranslatedWorksDB(XmlDocument sourcesXML)
        {
            foreach (XmlNode SourceStatisticsNode in sourcesXML.SelectNodes("//SOURCES_STATISTICS"))
            {
                _sourceStatistics.TranslatedWorksFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("ALL_TRANS").InnerText.ToString());
            }
            return _sourceStatistics.TranslatedWorksFromDB;
        }
        public int EnglishSourceDB(XmlDocument sourcesXML)
        {
            foreach (XmlNode SourceStatisticsNode in sourcesXML.SelectNodes("//SOURCES_STATISTICS"))
            {
                _sourceStatistics.EnglishSourceFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("OLDEST_SOURCE_YR").InnerText.ToString());
            }
            return _sourceStatistics.EnglishSourceFromDB;
        }
        public int TranslatedSourceDB(XmlDocument sourcesXML)
        {
            foreach (XmlNode SourceStatisticsNode in sourcesXML.SelectNodes("//SOURCES_STATISTICS"))
            {
                _sourceStatistics.TranslatedSourceFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("OLDEST_TRANS_YR").InnerText.ToString());
            }
            return _sourceStatistics.TranslatedSourceFromDB;
        }
        public IWebElement SourcesCountByAuthorElement()
        {
            return _browser.GetElement("#author-works-total", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement AuthorTextBoxElement()
        {
            return _browser.GetElement("#author-works-textbox", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement PhrasesYearTextBoxElement()
        {
            return _browser.GetElement("phrases-in-year-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement PhrasesFromYearCountElement()
        {
            return _browser.GetElement("#phrases-in-year", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement UniqueUsesTextBoxElement()
        {
            return _browser.GetElement("unique-uses-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement WordsCountTextBoxElement()
        {
            return _browser.GetElement("word-count-textbox", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement UniqueUsesAndWordsCountElement()
        {
            return _browser.GetElement("unique-uses-length", WebBrowser.ElementSelectorType.ID);
        }
        public int GetSourcesCountByAuthorNameUI(string authorName)
        {
            AuthorTextBoxElement().SendKeys(authorName);
            ClickSearchButton();
            WaitForAuthorElementTextToLoad();
            string sourceCount = SourcesCountByAuthorElement().Text;
            int count = Int32.Parse(sourceCount);
            return count;
        }
        public string ColumnName()
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            List<string> columnNames = _sourceStatisticsDataAccess.ColumnNames();
            Random _random = new Random();
            int index = _random.Next(columnNames.Count);
            string name = columnNames[index];
            return name;
        }
        public string AuthorName(string columnName)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.AuthorName(columnName);
        }
        public int GetSourcesCountByAuthorNameDB(string authorName)
        {
            int SourcesCount = 0;
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            SourcesCount = _sourceStatisticsDataAccess.SourceStatisticsCount(authorName);

            return SourcesCount;
        }
        public int RandomYear()
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.RandomYear();
        }
        public int PhrasesFromYearDB(int randomYear)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesFromYear(randomYear);
        }
        public int PhrasesFromYearUI(int randomYear)
        {
            PhrasesYearTextBoxElement().SendKeys(randomYear.ToString());
            ClickSearchButton();
            WaitForYearElementTextToLoad();
            int count = Int32.Parse(PhrasesFromYearCountElement().Text);
            return count;
        }
        public int GenerateRandomNumber(int startingNumber, int endingNumber)
        {
            return _random.Next(startingNumber, endingNumber);
        }
        public int PhrasesWithUniqueUsesDB(int randomNumberOfUniqueUses)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesWithUniqueUses(randomNumberOfUniqueUses);
        }
        public int PhrasesWithWordsCountDB(int randomNumberOfWordsCount)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesWithWordsCount(randomNumberOfWordsCount);
        }
        public int PhrasesWithUniqueUsesAndWordsCountDB(int randomNumberOfUniqueUses, int randomNumberOfWordsCount)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesWithUniqueUsesAndWordsCount(randomNumberOfUniqueUses, randomNumberOfWordsCount);
        }
        public int PhrasesWithUniqueUsesUI(int randomNumberOfUniqueUses)
        {
            UniqueUsesTextBoxElement().SendKeys(randomNumberOfUniqueUses.ToString());
            ClickSearchButton();
            WaitForUniqueUsesTextToLoad();
            int count = Int32.Parse(UniqueUsesAndWordsCountElement().Text);
            return count;
        }
        public int PhrasesWithWordsCountUI(int randomNumberOfWordsCount)
        {
            UniqueUsesTextBoxElement().Clear();
            WordsCountTextBoxElement().SendKeys(randomNumberOfWordsCount.ToString());
            ClickSearchButton();
            WaitForUniqueUsesTextToLoad();
            int count = Int32.Parse(UniqueUsesAndWordsCountElement().Text);
            return count;
        }
        public int PhrasesWithUniqueUsesAndWordsCountUI(int randomNumberOfUniqueUses, int randomNumberOfWordsCount)
        {
            UniqueUsesTextBoxElement().Clear();
            WordsCountTextBoxElement().Clear();
            UniqueUsesTextBoxElement().SendKeys(randomNumberOfUniqueUses.ToString());
            WordsCountTextBoxElement().SendKeys(randomNumberOfWordsCount.ToString());
            ClickSearchButton();
            WaitForUniqueUsesTextToLoad();
            int count = Int32.Parse(UniqueUsesAndWordsCountElement().Text);
            return count;
        }
        public void CloseUsageGraphWindow()
        {
            _browser.CloseBrowser();
            _browser.SwitchtoPreviousWindow();
        }
    }
}
