using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Web;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Quant.Spice.Test.UI.Web.WebSpice.Pages
{
    public class CumulativeUsageGraph : WebPage
    {
        protected static SearchKeywordDataAccess _dataAccess;
        readonly Random _random = new Random();
        public CumulativeUsageGraph(WebBrowser browser) : base(browser)
        {

        }
        public IWebElement CumulativeUsageLinkElement()
        {
            return _browser.GetElement("cumulativegraph-link", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSearchWordTextBox()
        {
            return _browser.GetElement("txtSearchWord", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForSurroundingWordsToLoad()
        {
            return _browser.WaitForElement("surround-words-list", WebBrowser.ElementSelectorType.Class);
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
        public IWebElement GetPhraseTextElement()
        {
            return _browser.GetElement("phraseText", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetFrequencyOfUseElement()
        {
            return _browser.GetElement("uniqueUsesCount", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForPhraseTextToLoad(string elementText)
        {
            return _browser.WaitForElementText(elementText, "phraseText", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForCumulativeGraph()
        {
            return _browser.WaitForElement("#chartAxis > svg > circle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<IWebElement> GetAllCircleElements()
        {
            return _browser.GetElements("#chartAxis > svg > circle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool WaitForsourceElement(string elementText)
        {
            return _browser.WaitForElementText(elementText, "#sourceinfo-display", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetSourceElement()
        {
            return _browser.GetElement("#sourceinfo-display", WebBrowser.ElementSelectorType.CssSelector);
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
        public int GetFrequencyOfUseDB(List<XmlDocument> wordInfoXMLs)
        {
            int frequencyOfUse = 0;
            List<Phrase> phrases = GetPhrasesList(wordInfoXMLs);
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO//MNGS//MNG//PHRASES//PHRASE"))
            {
                frequencyOfUse = Int32.Parse(Meanings.ChildNodes[3].InnerText);
                break;
            }
            return frequencyOfUse;
        }
        public string GetRandomPhraseUI(int randomNumberFromPhrasesCount, string randomWordFromDB)
        {
            string elementText = "";
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            GetRandomPhrase(randomNumberFromPhrasesCount).Click();
            CumulativeUsageLinkElement().Click();
            _browser.SwitchtoCurrentWindow();
            WaitForPhraseTextToLoad(elementText);
            return GetPhraseTextElement().Text;
        }
        public int GetFrequencyOfUseUI()
        {
            return Int32.Parse(GetFrequencyOfUseElement().Text);
        }
        public int GetRandomPhraseIDFromDB(int randomNumberFromPhrasesCount, List<XmlDocument> wordInfoXMLs)
        {
            List<Phrase> phrases = GetPhrasesList(wordInfoXMLs);
            return phrases[randomNumberFromPhrasesCount - 1].ID;
        }
        public XmlDocument GetCumulativeUsageXML(int phraseIDFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetCumulativeUsageXML(phraseIDFromDB);
        }
        public List<string> GetTotalSourcesListFromDB(XmlDocument cumulativeXML)
        {
            List<string> totalCumulativeSources = new List<string>();
            foreach (XmlNode source in cumulativeXML.SelectNodes("//CUMULATIVE_USAGE//PHRASE//VR_SOURCES//VR_SOURCE"))
            {
                totalCumulativeSources.Add(source.SelectSingleNode("YEAR").InnerText.ToString());
            }
            if (cumulativeXML.ChildNodes[0].ChildNodes[0].ChildNodes.Count != 4)
            {
                foreach (XmlElement UNVR_YEAR in cumulativeXML.SelectSingleNode("//CUMULATIVE_USAGE//PHRASE//UNVR_SOURCES"))
                {
                    totalCumulativeSources.Add(UNVR_YEAR.InnerText);
                }
            }
            return totalCumulativeSources;
        }
        public int GetRandomNumber(List<string> totalSourcesFromDB, bool verifiedSource)
        {
            int randomNumber = 0;
            if (verifiedSource == false && totalSourcesFromDB.Count <= 5)
            {
                return randomNumber;
            }
            if (verifiedSource == true || totalSourcesFromDB.Count <= 5)
            {
                randomNumber = _random.Next(1, totalSourcesFromDB.Count < 5 ? totalSourcesFromDB.Count : 5);
            }
            else if (verifiedSource == false)
            {
                randomNumber = _random.Next(5, totalSourcesFromDB.Count);
            }
            return randomNumber;
        }
        public string GetVerifiedSourcesListFromDB(XmlDocument cumulativeXML, int randomNumberOfVerifiedSources)
        {
            List<string> verifiedCumulativeSources = new List<string>();
            foreach (XmlNode source in cumulativeXML.SelectNodes("//CUMULATIVE_USAGE//PHRASE//VR_SOURCES//VR_SOURCE"))
            {
                string year = source.SelectSingleNode("YEAR").InnerText.ToString();
                string firstName = source.ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText;
                string lastName = source.ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText;
                string title = source.SelectSingleNode("TITLE").InnerText.ToString();
                verifiedCumulativeSources.Add(year + " : " + firstName + " " + lastName + " : " + title);
            }
            return verifiedCumulativeSources[randomNumberOfVerifiedSources - 1];
        }
        public string GetUnverifiedSourcesListFromDB(XmlDocument cumulativeXML, int randomNumberOfUnverifiedSources)
        {
            List<string> unverifiedCumulativeSources = new List<string>();
            foreach (XmlElement UNVR_YEAR in cumulativeXML.SelectSingleNode("//CUMULATIVE_USAGE//PHRASE//UNVR_SOURCES"))
            {
                unverifiedCumulativeSources.Add(UNVR_YEAR.InnerText);
            }
            return unverifiedCumulativeSources[randomNumberOfUnverifiedSources - 1];
        }
        public List<string> GetTotalSourcesListFromUI(string randomWordFromDB, int randomNumberFromPhrasesCount)
        {

            Actions action = new Actions(_browser._webDriver);
            List<string> totalCumulativeSources = new List<string>();
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            GetRandomPhrase(randomNumberFromPhrasesCount).Click();
            CumulativeUsageLinkElement().Click();
            _browser.SwitchtoCurrentWindow();
            WaitForCumulativeGraph();
            List<IWebElement> sourcesWebElement = GetAllCircleElements();
            action.MoveToElement(sourcesWebElement[0]).Perform();
            totalCumulativeSources.Add(GetSourceElement().Text);
            for (int sourcesCount = 2; sourcesCount <= sourcesWebElement.Count; sourcesCount++)
            {
                action.MoveToElement(sourcesWebElement[sourcesCount - 1]).Perform();
                string elementText = totalCumulativeSources[sourcesCount - 2];
                WaitForsourceElement(elementText);
                totalCumulativeSources.Add(GetSourceElement().Text);
            }
            return totalCumulativeSources;
        }
        public string GetVerifiedSourceFromUI(int randomNumberOfVerifiedSources)
        {
            Actions action = new Actions(_browser._webDriver);
            string verifiedCumulativeSource = "";
            List<IWebElement> sourcesWebElement = GetAllCircleElements();
            action.MoveToElement(sourcesWebElement[randomNumberOfVerifiedSources - 1]).Perform();
            verifiedCumulativeSource = GetSourceElement().Text;
            return verifiedCumulativeSource;
        }
        public string GetUnverifiedSourceFromUI(int randomNumberOfUnverifiedSources)
        {
            Actions action = new Actions(_browser._webDriver);
            string unverifiedCumulativeSource = "";
            List<IWebElement> sourcesWebElement = GetAllCircleElements();
            action.MoveToElement(sourcesWebElement[randomNumberOfUnverifiedSources - 1]).Perform();
            unverifiedCumulativeSource = GetSourceElement().Text;
            return unverifiedCumulativeSource;
        }
        public void CloseUsageGraphWindow()
        {
            _browser.CloseBrowser();
            _browser.SwitchtoPreviousWindow();
        }
    }
}
