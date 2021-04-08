using OpenQA.Selenium;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Web;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Quant.Spice.Test.UI.Web.WebSpice.Pages
{
    public class Timeline : WebPage
    {
        protected static SearchKeywordDataAccess _dataAccess;
        readonly Random _random = new Random();
        public Timeline(WebBrowser browser) : base(browser)
        {

        }
        public bool WaitForSurroundingWordsToLoad()
        {
            return _browser.WaitForElement("surround-words-list", WebBrowser.ElementSelectorType.Class);
        }
        public IWebElement GetSearchWordTextBox()
        {
            return _browser.GetElement("txtSearchWord", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSearchButton()
        {
            return _browser.GetElement("btnGoSearchWord", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForMeaningsToLoad()
        {
            return _browser.WaitForElement("meanings-list", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRandomPhrase(int randomNumberFromPhrasesCount)
        {
            return _browser.GetElement("#phrases-container > ul > li:nth-child(" + randomNumberFromPhrasesCount + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool WaitForPhraseTextToLoad(string elementText)
        {
            return _browser.WaitForElementText(elementText, "timeline-phrase", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetPhraseTextElement()
        {
            return _browser.GetElement("timeline-phrase", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement TimelineLinkElement()
        {
            return _browser.GetElement("timeline-link", WebBrowser.ElementSelectorType.ID);
        }
        public bool EnterkeywordWaitForSurroundingWords(string randomWordFromDB)
        {
            IWebElement Searchbox = GetSearchWordTextBox();
            Searchbox.Clear();
            Searchbox.SendKeys("" + randomWordFromDB);
            return WaitForSurroundingWordsToLoad();
        }
        public string GetRandomWord()
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetRandomWord();
        }
        public List<XmlDocument> GetWordInfoXML(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetWordInfoXML(randomWordFromDB);
        }
        public List<Phrase> GetPhrasesList(List<XmlDocument> wordInfoXMLs)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.PhrasesListFromDB(wordInfoXMLs);
        }
        public int RandomNumberFromPhrasesCount(List<XmlDocument> wordInfoXMLs)
        {
            List<Phrase> phrases = GetPhrasesList(wordInfoXMLs);
            int number = _random.Next(1, phrases.Count);
            return number;
        }
        public string GetRandomPhraseDB(int randomNumberFromPhrasesCount, List<XmlDocument> wordInfoXMLs)
        {
            List<Phrase> phrases = GetPhrasesList(wordInfoXMLs);
            return phrases[randomNumberFromPhrasesCount - 1].Text;
        }
        public string GetRandomPhraseUI(int randomNumberFromPhrasesCount, string randomWordFromDB)
        {
            string elementText = "";
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            GetRandomPhrase(randomNumberFromPhrasesCount).Click();
            TimelineLinkElement().Click();
            _browser.SwitchtoCurrentWindow();
            WaitForPhraseTextToLoad(elementText);
            return GetPhraseTextElement().Text;
        }
        public void CloseUsageGraphWindow()
        {
            _browser.CloseBrowser();
            _browser.SwitchtoPreviousWindow();
        }
    }
}
