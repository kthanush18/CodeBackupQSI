using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Quant.Spice.Core.SpiceThesaurus;
using Quant.Spice.ServiceAccess;
using Quant.Spice.Test.UI.Common;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.Models.UITest;
using Quant.Spice.Test.UI.Common.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using Meaning = Quant.Spice.Test.UI.Common.Models.Meaning;

namespace Quant.Spice.Test.UI.Web.WebSpice.Pages
{
    public class Home : WebPage
    {
        protected static SearchKeywordDataAccess _dataAccess;
        Random _random = new Random();

        public string GetPartsOfSpeech(int SpeechValues)
        {
            string Speech = null;
            switch (SpeechValues)
            {
                case 0:
                    Speech = " :";
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
        public Home(WebBrowser browser) : base(browser)
        {

        }
        public string GetRandomWord()
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetRandomWord();
        }
        public string GetRandomLetter()
        {
            int number = _random.Next(0, 26);
            char singleLetter = (char)('a' + number);
            return singleLetter.ToString();
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
        public List<string> GetMeaningsListFromDB(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.MeaningsList(randomWordFromDB);
        }
        public List<string> GetRandomSingleLetterFromDB(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.KeywordSuggestionsForSingleLetter(randomWordFromDB);
        }
        public List<string> GetRandomMultipleLettersFromDB(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.KeywordSuggestionsForMultipleLetters(randomWordFromDB);
        }
        public List<XmlDocument> GetWordInfoXML(string randomWordFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetWordInfoXML(randomWordFromDB);
        }
        public int GetPhraseIDFromDB(List<XmlDocument> wordInfoXMLs)
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
            int meaningID = meanings[0].ID;

            List<Phrase> phrases = new List<Phrase>();
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
            return phrases[0].ID;
        }
        public XmlDocument GetSourcesXML(int phraseIDFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetSourcesXML(phraseIDFromDB);
        }
        public bool WaitForPageToLoad()
        {
            return _browser.WaitForElement("home-main-container", WebBrowser.ElementSelectorType.ID);
        }

        public bool IsMainContainerVisible()
        {
            return _browser.IsElementVisible("home-main-container", WebBrowser.ElementSelectorType.ID);
        }

        public IWebElement GetSearchWordTextBox()
        {
            return _browser.GetElement("txtSearchWord", WebBrowser.ElementSelectorType.ID);
        }

        public bool WaitForSurroundingWordsToLoad()
        {
            return _browser.WaitForElement("surround-words-list", WebBrowser.ElementSelectorType.Class);
        }

        public List<IWebElement> GetSurroundingWordsList()
        {
            return _browser.GetElements("#surrounding-words > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }

        public IWebElement GetSearchButton()
        {
            return _browser.GetElement("btnGoSearchWord", WebBrowser.ElementSelectorType.ID);
        }

        public bool WaitForMeaningsToLoad()
        {
            return _browser.WaitForElement("meanings-list", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForMeaningOnLeftToLoad()
        {
            return _browser.WaitForElement("meaning-on-left", WebBrowser.ElementSelectorType.ID);
        }
        public List<IWebElement> GetMeaningsList()
        {
            return _browser.GetElements("#meanings > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetMeaningDisplayedOnLeftUI()
        {
            return _browser.GetElement("meaning-on-left", WebBrowser.ElementSelectorType.ID);
        }

        public List<IWebElement> GetSynonymsList()
        {
            return _browser.GetElements("#synonyms > ui > li", WebBrowser.ElementSelectorType.CssSelector);
        }

        public List<IWebElement> GetPhrasesList()
        {
            return _browser.GetElements("#phrases-container > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<IWebElement> GetSourcesList()
        {
            return _browser.GetElements("#sources-container > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetFrequencyOfUse()
        {
            return _browser.GetElement("unique-uses-count", WebBrowser.ElementSelectorType.ID);
        }
        public List<IWebElement> GetRelatedKeywords()
        {
            return _browser.GetElements("#related-keywords-container > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement RandomRelatedKeywordFromList(int countOfRelatedKeyword)
        {
            IWebElement webElement = _browser.GetElement("#related-keywords-container > ul li:nth-child(" + countOfRelatedKeyword + ")", WebBrowser.ElementSelectorType.CssSelector);
            return webElement;
        }
        public bool WaitForRedirectedKeywordElement()
        {
            return _browser.WaitForElement("#redirected-from", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement RedirectedKeywordElement()
        {
            return _browser.GetElement("#redirected-from", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<IWebElement> GetSeeAlsoWords()
        {
            return _browser.GetElements("#see-also-words > ui > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement RandomSeeAlsoWordFromList(int countOfSeeAlsoWords)
        {
            IWebElement webElement = _browser.GetElement("#see-also-words > ui li:nth-child(" + countOfSeeAlsoWords + ")", WebBrowser.ElementSelectorType.CssSelector);
            return webElement;
        }

        public IWebElement TextboxElement()
        {
            return _browser.GetElement("txtSearchWord", WebBrowser.ElementSelectorType.ID);
        }

        public IWebElement NextMeaningNavigationButton()
        {
            return _browser.GetElement("nextSearchdiv", WebBrowser.ElementSelectorType.ID);
        }

        public IWebElement GetSelectedMeaning()
        {
            return _browser.GetElement("#meanings > ul > li.active", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetActiveCircleElement()
        {
            return _browser.GetElement("#meanings-navigator > ul > li.circle.active", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetCircleIndexElement(int indexOfElement)
        {
            return _browser.GetElement("#meanings-navigator > ul > li:nth-child(" + indexOfElement + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public string GetCSSColour(IWebElement CircleIndexElement)
        {
            return CircleIndexElement.GetCssValue("background-color");
        }
        public IWebElement GetCurrentMeaningIndex()
        {
            return _browser.GetElement("mng-index-top", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetTotalMeaningsIndex()
        {
            return _browser.GetElement("mngs-total-top", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsNavigationButtonNextDisabled()
        {
            return _browser.IsElementVisible("#nextSearchdiv.arrow-right.arrow-right-inactive", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement CircularButton(int meaningsCount)
        {
            return _browser.GetElement("#meanings-navigator > ul li:nth-child(" + meaningsCount + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement LastMeaningNavigationButton()
        {
            return _browser.GetElement("#lastSearchdiv", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement PreviousMeaningNavigationButton()
        {
            return _browser.GetElement("previousSearchdiv", WebBrowser.ElementSelectorType.ID);
        }
        public bool IsNavigationButtonPreviousDisabled()
        {
            return _browser.IsElementVisible("#previousSearchdiv.arrow-left.arrow-left-inactive", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool IsNavigationButtonLastDisabled()
        {
            return _browser.IsElementVisible("#lastSearchArrow.arrow-right.lastSearchArrow-inactive", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement FirstMeaningNavigationButton()
        {
            return _browser.GetElement("#firstSearchdiv", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool IsNavigationButtonFirstDisabled()
        {
            return _browser.IsElementVisible("#firstSearchArrow.arrow-left.firstSearchArrow-inactive", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement PreviousKeywordHistoryButton()
        {
            return _browser.GetElement("#backward-button", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement PreviousKeywordHistoryButtonElement()
        {
            return _browser.GetElement("#backward-button.round-button", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement NextKeywordHistoryButton()
        {
            return _browser.GetElement("#forward-button", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement NextKeywordHistoryButtonElement()
        {
            return _browser.GetElement("#forward-button.round-button", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool IsBubbleImageVisible()
        {
            return _browser.IsElementVisible("#keyword-bubble-image", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement BubbleImageElement()
        {
            return _browser.GetElement("#keyword-bubble-image", WebBrowser.ElementSelectorType.CssSelector);
        }

        public bool EnterSingleLetterWaitForSurroundingWords(string RandomLetter)
        {
            IWebElement Searchbox = GetSearchWordTextBox();
            Searchbox.SendKeys("" + RandomLetter);
            return WaitForSurroundingWordsToLoad();
        }
        public bool EnterMultipleLettersWaitForSurroundingWords(string MultipleLetters)
        {
            IWebElement Searchbox = GetSearchWordTextBox();
            Searchbox.SendKeys("" + MultipleLetters);
            return WaitForSurroundingWordsToLoad();
        }
        public bool EnterkeywordWaitForSurroundingWords(string randomWordFromDB)
        {
            IWebElement Searchbox = GetSearchWordTextBox();
            Searchbox.Clear();
            Searchbox.SendKeys("" + randomWordFromDB);
            return WaitForSurroundingWordsToLoad();
        }
        public List<string> GetSurroundingWordsForSingleLetter(string RandomLetter)
        {
            List<string> surroundingWordsFromUI = new List<string>();
            EnterSingleLetterWaitForSurroundingWords(RandomLetter);
            List<IWebElement> surroundingWordsWebElement = GetSurroundingWordsList();
            foreach (IWebElement surroundingWordwebElement in surroundingWordsWebElement)
            {
                surroundingWordsFromUI.Add(surroundingWordwebElement.Text);
            }
            return surroundingWordsFromUI;
        }
        public List<string> GetSurroundingWordsForMultipleLetters(string RandomLetter)
        {
            List<string> surroundingWordsFromUI = new List<string>();
            EnterMultipleLettersWaitForSurroundingWords(RandomLetter);
            List<IWebElement> surroundingWordsWebElement = GetSurroundingWordsList();
            foreach (IWebElement surroundingWordwebElement in surroundingWordsWebElement)
            {
                surroundingWordsFromUI.Add(surroundingWordwebElement.Text);
            }
            return surroundingWordsFromUI;
        }

        public List<string> GetMeaningsForEnteredKeyword(string randomWordFromDB)
        {
            List<string> meaningsFromUI = new List<string>();
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            List<IWebElement> meaningsWebElement = GetMeaningsList();
            foreach (IWebElement meaningElement in meaningsWebElement)
            {
                meaningsFromUI.Add(meaningElement.Text);
            }
            return meaningsFromUI;
        }
        public void EnterkeywordAndSearch(string randomWordFromDB)
        {
            WaitForPageToLoad();
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
        }
        public string GetMeaningOnLeft()
        {
            IWebElement MeaningOnLeftWebElement = GetMeaningDisplayedOnLeftUI();
            return MeaningOnLeftWebElement.Text.ToString();
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
            }
            foreach (XmlNode SpeechValue in wordInfoXMLs[0].SelectNodes("//WORDINFO//MNGS//MNG"))
            {
                PartsOfSpeechFromDB = GetPartsOfSpeech(Convert.ToInt32(SpeechValue.SelectSingleNode("SPCH").InnerText.ToString()));
            }
            MeaningsDisplayedOnLeftFromDB = meanings[0].Text + PartsOfSpeechFromDB;
            return MeaningsDisplayedOnLeftFromDB;
        }

        public List<string> GetSynonymsForSelectedMeaning_UI()
        {
            List<string> getSynonymsListFromUI = new List<string>();
            List<IWebElement> synonymsWebElement = GetSynonymsList();
            foreach (IWebElement synonymElement in synonymsWebElement)
            {
                getSynonymsListFromUI.Add(synonymElement.Text);
            }
            return getSynonymsListFromUI;
        }

        public List<string> GetSynonymsForSelectedMeaning_DB(List<XmlDocument> wordInfoXMLs)
        {
            List<string> getSynonymsListFromDB = new List<string>();
            foreach (XmlNode Synonyms in wordInfoXMLs[0].SelectNodes("/WORDINFO/GRP/MNGS/MNG/SYNMS"))
            {
                wordInfoXMLs[0].RemoveAll();
                wordInfoXMLs[0].AppendChild(Synonyms);

                foreach (XmlNode SynonymOfSelectedMeaning in wordInfoXMLs[0].GetElementsByTagName("SYNM"))
                {
                    getSynonymsListFromDB.Add(SynonymOfSelectedMeaning.InnerText.ToString() + ",");
                }
                int IndexOfLastElement = getSynonymsListFromDB.Count - 1;
                getSynonymsListFromDB[IndexOfLastElement] = getSynonymsListFromDB[IndexOfLastElement].Replace(",", "");
                break;
            }
            return getSynonymsListFromDB;
        }
        public List<string> GetPhrasesForFirstMeaning_UI(string randomWordFromDB)
        {
            List<string> getPhrasesListFromUI = new List<string>();
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            List<IWebElement> phrasesWebElement = GetPhrasesList();
            foreach (IWebElement phrasesElement in phrasesWebElement)
            {
                getPhrasesListFromUI.Add(phrasesElement.Text);
            }
            return getPhrasesListFromUI;
        }

        public List<string> GetPhrasesOfFirstMeaning_DB(List<XmlDocument> wordInfoXMLs)
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
                    string phraseText = node1.SelectSingleNode("TEXT").InnerText.ToString();
                    phrases.Add(phraseText);
                }
            }
            phrases.Sort();

            return phrases;
        }

        public List<string> GetSourcesForFirstPhrase_UI(string randomWordFromDB)
        {
            List<string> getSourcesListFromUI = new List<string>();
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            List<IWebElement> sourceWebElement = GetSourcesList();
            foreach (IWebElement sourceElement in sourceWebElement)
            {
                getSourcesListFromUI.Add(sourceElement.Text);
            }
            return getSourcesListFromUI;
        }
        public string GetFrequencyOfUsesUI()
        {
            IWebElement FrequencyOfUse = GetFrequencyOfUse();
            string FrequencyOfUseCount = FrequencyOfUse.Text;

            return FrequencyOfUseCount;
        }
        public List<string> GetRelatedKeywordsForSelectedPhrase_UI()
        {
            List<string> getRelatedKeywordsListFromUI = new List<string>();
            List<IWebElement> relatedKeywordWebElement = GetRelatedKeywords();
            foreach (IWebElement RelatedKeywordElement in relatedKeywordWebElement)
            {
                getRelatedKeywordsListFromUI.Add(RelatedKeywordElement.Text);
            }
            return getRelatedKeywordsListFromUI;
        }

        public List<string> GetSourcesForSelectedPhrase_DB(XmlDocument _SourcesXML)
        {
            List<string> CompareSourcesDB = new List<string>();
            CommonMethods removeSpaces = new CommonMethods();
            WordInfoBL objSource = new WordInfoBL();
            List<SpiceSource> sources = new List<SpiceSource>();
            SourcesFormat sourceformat;
            string datasorcesXml = string.Empty;
            List<string> listsources = new List<string>();

            foreach (XmlNode nodeSource in _SourcesXML.SelectSingleNode("//SOURCES").ChildNodes)
            {
                sources.Add(objSource.LoadSource(nodeSource));
                sourceformat = new SourcesFormat();
                datasorcesXml = sourceformat.GetXML_In_MLA_Format(objSource.LoadSource(nodeSource));
                listsources.Add(datasorcesXml);
            }

            string sourceFormatFromDB = string.Empty;
            foreach (string FormatSourecesDB in listsources)
            {
                sourceFormatFromDB = FormatSourecesDB.Replace("<i>", "").Replace("</i>", "").Trim();
                string removeSpacesFromSource = removeSpaces.RemoveExtraSpaces(sourceFormatFromDB);
                CompareSourcesDB.Add(WebUtility.HtmlDecode(removeSpacesFromSource));
            }

            return CompareSourcesDB;
        }
        public string GetFrequencyOfUsesDB(List<XmlDocument> wordInfoXMLs)
        {
            string FrequencyOfUse = "";
            foreach (XmlNode Meanings in wordInfoXMLs[0].SelectNodes("//WORDINFO//MNGS//MNG//PHRASES//PHRASE"))
            {
                FrequencyOfUse = Meanings.ChildNodes[3].InnerText;
                break;
            }
            return FrequencyOfUse;
        }
        public List<string> GetRelatedKeywordsFromDB(XmlDocument _DecryptedXML)
        {
            List<int> IDs = new List<int>();
            List<string> relatedkeywords = new List<string>();
            foreach (XmlNode phrase in _DecryptedXML.SelectNodes("//WORDINFO//MNGS//MNG//PHRASES//PHRASE"))
            {
                int ID = Int32.Parse(phrase.SelectSingleNode("ID").InnerText.ToString());
                IDs.Add(ID);
            }
            foreach (XmlNode RelatedKeywords in _DecryptedXML.SelectNodes("//WORDINFO//MNGS//MNG//PHRASES//PHRASE/HOOKS"))
            {
                if (Int32.Parse(RelatedKeywords.ParentNode.ChildNodes[0].InnerText) == IDs[0])
                {
                    foreach (XmlElement Hook in RelatedKeywords.ChildNodes)
                    {
                        relatedkeywords.Add(Hook.InnerText + ",");
                    }
                    int IndexOfLastElement = relatedkeywords.Count - 1;
                    relatedkeywords[IndexOfLastElement] = relatedkeywords[IndexOfLastElement].Replace(",", "");
                }
            }
            return relatedkeywords;
        }
        public int RandomCountOfRelatedKeyword(string randomWordFromDB)
        {
            List<string> getRelatedKeywordsListFromUI = new List<string>();
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            List<IWebElement> relatedKeywordWebElement = GetRelatedKeywords();
            foreach (IWebElement RelatedKeywordElement in relatedKeywordWebElement)
            {
                getRelatedKeywordsListFromUI.Add(RelatedKeywordElement.Text);
            }
            Random _random = new Random();
            int number = _random.Next(1, getRelatedKeywordsListFromUI.Count);
            return number;
        }
        public string GetRandomRelatedKeyword(string randomWordFromDB, int countOfRelatedKeyword)
        {
            string RandomRelatedKeyword = "";
            List<string> getRelatedKeywordsListFromUI = new List<string>();
            List<IWebElement> relatedKeywordWebElement = GetRelatedKeywords();
            foreach (IWebElement RelatedKeywordElement in relatedKeywordWebElement)
            {
                getRelatedKeywordsListFromUI.Add(RelatedKeywordElement.Text.Replace(",", ""));
            }

            if (countOfRelatedKeyword > 0)
            {
                RandomRelatedKeyword = getRelatedKeywordsListFromUI[countOfRelatedKeyword - 1];
            }
            else
            {
                RandomRelatedKeyword = "";
            }
            RandomRelatedKeywordFromList(countOfRelatedKeyword).Click();
            return RandomRelatedKeyword;
        }
        public string CheckForRedirectedKeyword(string randomRelatedKeyword, string keywordDisplayedInTextBox)
        {
            string RedirectedKeyword = "";
            if (randomRelatedKeyword == "N/A" || randomRelatedKeyword == keywordDisplayedInTextBox)
            {
                RedirectedKeyword = randomRelatedKeyword;
            }
            else
            {
                RedirectedKeyword = _dataAccess.GetRedirectedKeyword(randomRelatedKeyword);
            }
            return RedirectedKeyword;
        }
        public string RelatedKeywordFromTextBox(string randomRelatedKeyword)
        {
            string KeywordFromTextBox = "";
            if (randomRelatedKeyword == "N/A")
            {
                KeywordFromTextBox = "N/A";
            }
            else
            {
                KeywordFromTextBox = TextboxElement().GetAttribute("value");
            }
            return KeywordFromTextBox.ToLower();
        }
        public string RedirectedKeywordTextFromWebElement()
        {
            return RedirectedKeywordElement().Text;
        }
        public int RandomCountOfSeeAlsoWords(string randomWordFromDB)
        {
            int number = 0;
            List<string> getSeeAlsoWordsListFromUI = new List<string>();
            EnterkeywordWaitForSurroundingWords(randomWordFromDB);
            GetSearchButton().Click();
            WaitForMeaningsToLoad();
            List<IWebElement> seeAlsoWordWebElement = GetSeeAlsoWords();
            foreach (IWebElement SeeAlsoWordElement in seeAlsoWordWebElement)
            {
                getSeeAlsoWordsListFromUI.Add(SeeAlsoWordElement.Text);
            }
            if (getSeeAlsoWordsListFromUI.Count == 0)
            {
                number = 0;
            }
            else
            {
                Random _random = new Random();
                number = _random.Next(1, getSeeAlsoWordsListFromUI.Count);
            }
            return number;
        }
        public string GetRandomSeeAlsoWord(string randomWordFromDB, int countOfSeeAlsoWord)
        {
            string RandomSeeAlsoWord = "";
            List<string> getSeeAlsoWordsListFromUI = new List<string>();
            List<IWebElement> seeAlsoWordWebElement = GetSeeAlsoWords();
            foreach (IWebElement SeeAlsoWordElement in seeAlsoWordWebElement)
            {
                getSeeAlsoWordsListFromUI.Add(SeeAlsoWordElement.Text);
            }

            if (countOfSeeAlsoWord > 0)
            {
                RandomSeeAlsoWord = getSeeAlsoWordsListFromUI[countOfSeeAlsoWord - 1];
                RandomSeeAlsoWordFromList(countOfSeeAlsoWord).Click();
            }
            else
            {
                RandomSeeAlsoWord = "";
            }

            return RandomSeeAlsoWord;
        }
        public string SeeAlsoWordFromTextBox()
        {
            string KeywordFromTextBox = "";
            List<string> getSeeAlsoWordsListFromUI = new List<string>();
            List<IWebElement> seeAlsoWordWebElement = GetSeeAlsoWords();
            foreach (IWebElement SeeAlsoWordElement in seeAlsoWordWebElement)
            {
                getSeeAlsoWordsListFromUI.Add(SeeAlsoWordElement.Text);
            }
            if (getSeeAlsoWordsListFromUI.Count == 0)
            {
                KeywordFromTextBox = "";
            }
            else
            {
                KeywordFromTextBox = TextboxElement().GetAttribute("value");
            }
            return KeywordFromTextBox;
        }

        public void ClickNavigationNextButton()
        {
            NextMeaningNavigationButton().Click();
        }
        public string SelectedMeaning()
        {
            string selectedMeaning = "";
            IWebElement selectedWebElement = GetSelectedMeaning();
            selectedMeaning = selectedWebElement.Text;
            return selectedMeaning;
        }
        public string CircleColorForSingleMeaning()
        {
            string CircleColor = "";
            IWebElement circleActiveElement = GetActiveCircleElement();
            CircleColor = circleActiveElement.GetCssValue("background-color");
            return CircleColor;
        }

        public string SelectedCircleColor(int meaningsCount)
        {
            string selectedCircleColor = "";

            IWebElement circleActiveElement = GetActiveCircleElement();
            IWebElement CircleIndexElement = GetCircleIndexElement(meaningsCount);
            selectedCircleColor = GetCSSColour(CircleIndexElement);
            return selectedCircleColor;
        }

        public string MeaningIndexOnWebPage()
        {
            string meaningIndex = "";
            IWebElement currentMeaningIndexElement = GetCurrentMeaningIndex();
            IWebElement TotalMeaningsIndexElement = GetTotalMeaningsIndex();
            meaningIndex = currentMeaningIndexElement.Text + TotalMeaningsIndexElement.Text;

            return meaningIndex;
        }
        public string SingleMeaningIndex(List<string> meaningsListFromUI, int singleMeaning)
        {
            string meaningIndex = "";
            meaningIndex = (singleMeaning + "-" + meaningsListFromUI.Count).ToString();

            return meaningIndex;
        }
        public string MeaningIndex(List<string> meaningsListFromUI, int meaningsCount)
        {
            string meaningIndex = "";
            meaningIndex = (meaningsCount + "-" + meaningsListFromUI.Count).ToString();

            return meaningIndex;
        }
        public void ClickCircularButtons(int meaningsCount)
        {
            CircularButton(meaningsCount).Click();
        }
        public void ClickLastNavigationButton()
        {
            LastMeaningNavigationButton().Click();
        }
        public void ClickNavigationPreviousButton()
        {
            PreviousMeaningNavigationButton().Click();
        }
        public void ClickFirstNavigationButton()
        {
            FirstMeaningNavigationButton().Click();
        }
        public List<string> GetRandomKeywordsAndSearch()
        {
            List<string> searchedKeywordsList = new List<string>();
            for (int RandomKeyword = 1; RandomKeyword <= 5; RandomKeyword++)
            {
                string randomWordFromDB = GetRandomWord();
                searchedKeywordsList.Add(randomWordFromDB);
                EnterkeywordWaitForSurroundingWords(randomWordFromDB);
                GetSearchButton().Click();
                WaitForMeaningsToLoad();
            }
            return searchedKeywordsList;
        }

        public string KeywordFromTextBox()
        {
            string KeywordFromTextBox = TextboxElement().GetAttribute("value");
            return KeywordFromTextBox;
        }
        public void ClickPreviousKeywordHistoryButton()
        {
            PreviousKeywordHistoryButton().Click();
        }
        public void ClickNextKeywordHistoryButton()
        {
            NextKeywordHistoryButton().Click();
        }
        public string PreviousHistoryButtonDisabled()
        {
            return PreviousKeywordHistoryButtonElement().GetCssValue("opacity");
        }
        public string NextHistoryButtonDisabled()
        {
            return NextKeywordHistoryButtonElement().GetCssValue("opacity");
        }
        
        public KeywordAssertionFailure<string> StringCollectionOfFailures(Exception ex, string message, string stackTrace, List<string> expectedListFromDatabase, List<string> actualListFromUI, string keywordFromDB, string nameOfAssertion)
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
        public KeywordAssertionFailure<string> StringCollectionOfFailures(Exception ex, string message, string stackTrace, List<string> expectedListFromDatabase, List<string> actualListFromUI, string keywordFromDB, string meaningFromDB, string nameOfAssertion)
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
        public KeywordAssertionFailure<string> StringCollectionOfFailures(Exception ex, string message, string stackTrace, List<string> expectedListFromDatabase, List<string> actualListFromUI, string keywordFromDB, string meaningFromDB, string phraseFromDB, string nameOfAssertion)
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
        public KeywordAssertionFailure<string> SingleValueOfFailure(Exception ex, string message, string stackTrace, string expectedValueFromDatabase, string actualValueFromUI, string keywordFromDB, string meaningFromDB, string nameOfAssertion)
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
        public KeywordAssertionFailure<string> SingleValueOfFailure(Exception ex, string message, string stackTrace, string expectedValueFromDatabase, string actualValueFromUI, string keywordFromDB, string meaningFromDB, string phraseFromDB, string nameOfAssertion)
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
        public KeywordAssertionFailure<string> SingleActualValueInputOfFailure(Exception ex, string message, string stackTrace, string actualValueFromUI, string keywordFromDB, string meaningFromDB, string nameOfAssertion)
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
        public string GetSelectedElementCSSColour(IWebElement phrasesWebElement)
        {
            return phrasesWebElement.GetCssValue("background-color");
        }
        public bool IsFirstPhraseSelected()
        {
            bool selection = false;
            List<IWebElement> phrasesWebElement = GetPhrasesList();
            string selectedPhraseElementColour = GetSelectedElementCSSColour(phrasesWebElement[0]);
            if(selectedPhraseElementColour == "rgba(221, 201, 176, 1)")
            {
                selection = true;
            }
            return selection;
        }
        public IWebElement GetMeaningElement(int meaningCount)
        {
            return _browser.GetElement("#meanings > ul > li:nth-child(" + meaningCount + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<string> GetPhrasesForSelectedMeaning_UI(int meaningCount)
        {
            CommonMethods removeSpaces = new CommonMethods();
            List<string> getPhrasesListFromUI = new List<string>();
            GetMeaningElement(meaningCount).Click();
            List<IWebElement> phrasesWebElement = GetPhrasesList();
            foreach (IWebElement phrasesElement in phrasesWebElement)
            {
                getPhrasesListFromUI.Add(removeSpaces.RemoveExtraSpaces(phrasesElement.Text));
            }
            return getPhrasesListFromUI;
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
        public List<string> GetPhrasesOfSelectedMeaning_DB(List<XmlDocument> wordInfoXMLs,int meaningIndex,List<Meaning> meanings)
        {
            int meaningID = meanings[meaningIndex].ID;
            CommonMethods removeSpaces = new CommonMethods();
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
                    phrases.Add(removeSpaces.RemoveExtraSpaces(phraseText));
                }
            }
            phrases.Sort();

            return phrases;
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
        public List<string> GetSynonymsForSelectedMeaningID_DB(List<XmlDocument> wordInfoXMLs, int meaningIndex, List<Meaning> meanings)
        {
            List<string> getSynonymsListFromDB = new List<string>();
            int meaningID = meanings[meaningIndex].ID;
            XmlNodeList synonymNodes = null;
            synonymNodes = wordInfoXMLs[0].SelectNodes("//WORDINFO/GRP/MNGS/MNG[ID/text()='" + meaningID + "']/SYNMS/SYNM");
            foreach (XmlNode synonymNode in synonymNodes)
            {
                getSynonymsListFromDB.Add(synonymNode.InnerText.ToString()+",");
            }
            if(getSynonymsListFromDB.Count > 0)
            {
                int IndexOfLastElement = getSynonymsListFromDB.Count - 1;
                getSynonymsListFromDB[IndexOfLastElement] = getSynonymsListFromDB[IndexOfLastElement].Replace(",", "");
            }
            return getSynonymsListFromDB;
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForEachMeaningsData(List<XmlDocument> wordInfoXMLs, int meaningsCount, string randomWordFromDB)
        {
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            Searchedkeyword searchedKeyword = new Searchedkeyword();
            CommonCollections common = new CommonCollections();
            common.MeaningsListFromUI = GetMeaningsForSelectedKeyword();
            common.MeaningsListFromDatabase = GetMeaningsListFromDB(randomWordFromDB);

            //Phrases
            common.PhrasesListForSelectedMeaningFromUI = GetPhrasesForSelectedMeaning_UI(meaningsCount);
            common.SortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            common.PhrasesListForSelectedMeaningFromDB = GetPhrasesOfSelectedMeaning_DB(wordInfoXMLs, meaningsCount - 1, common.SortedMeaningsAndMeaningIDsFromDB);

            //Meaning index 
            searchedKeyword.MeaningIndexOnWebPage = MeaningIndexOnWebPage();
            searchedKeyword.MeaningIndex = MeaningIndex(common.MeaningsListFromUI, meaningsCount);

            //Meaning on left
            common.MeaningOnLeftFromUI = GetMeaningOnLeft();
            common.MeaningOnLeftFromDB = GetSelectedMeaningDisplayedOnLeftDB(wordInfoXMLs, meaningsCount - 1, common.SortedMeaningsAndMeaningIDsFromDB);

            //Synonyms 
            common.SynonymsListForSelectedMeaningFromUI = GetSynonymsForSelectedMeaning_UI();
            common.SynonymsListForSelectedMeaningFromDB = GetSynonymsForSelectedMeaningID_DB(wordInfoXMLs, meaningsCount - 1, common.SortedMeaningsAndMeaningIDsFromDB);
            searchedKeyword.SelectedCircleColour = SelectedCircleColor(meaningsCount);

            //Assertions
            try
            {
                Assert.IsTrue(common.PhrasesListForSelectedMeaningFromUI.SequenceEqual(common.PhrasesListForSelectedMeaningFromDB));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Phrases List";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, ex.Message, ex.StackTrace, common.PhrasesListForSelectedMeaningFromDB, common.PhrasesListForSelectedMeaningFromUI, randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1],common.PhrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(IsFirstPhraseSelected());
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Is First Phrase Selected";
                KeywordAssertionFailure<string> failure = SingleActualValueInputOfFailure(ex, ex.Message, ex.StackTrace, IsFirstPhraseSelected().ToString(), randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(searchedKeyword.MeaningIndex.SequenceEqual(searchedKeyword.MeaningIndexOnWebPage));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Meaning Index";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, searchedKeyword.MeaningIndex, searchedKeyword.MeaningIndexOnWebPage, randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1], common.PhrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(common.MeaningOnLeftFromDB.SequenceEqual(common.MeaningOnLeftFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Meaning On Left";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, common.MeaningOnLeftFromDB, common.MeaningOnLeftFromUI, randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1], common.PhrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(common.SynonymsListForSelectedMeaningFromDB.SequenceEqual(common.SynonymsListForSelectedMeaningFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Synonyms List";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, ex.Message, ex.StackTrace, common.SynonymsListForSelectedMeaningFromDB, common.SynonymsListForSelectedMeaningFromUI, randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1], common.PhrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(searchedKeyword.CssColourForSelectedCircle.SequenceEqual(searchedKeyword.SelectedCircleColour));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "SelectedCircleColor";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, searchedKeyword.CssColourForSelectedCircle, searchedKeyword.SelectedCircleColour, randomWordFromDB, common.MeaningsListFromDatabase[meaningsCount - 1], common.PhrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                assertionFailures.Add(failure);
            }
            return assertionFailures;
        }
        public IWebElement GetPhrasesElement(int phrasesCount)
        {
            return _browser.GetElement("#phrases-container > ul > li:nth-child(" + phrasesCount + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<string> GetSourcesForSelectedPhrase_UI(int phrasesCount, int meaningsCount)
        {
            CommonMethods removeSpaces = new CommonMethods();
            List<string> getSourcesListFromUI = new List<string>();
            List<IWebElement> sourceWebElement = GetSourcesList();
            foreach (IWebElement sourceElement in sourceWebElement)
            {
                getSourcesListFromUI.Add(removeSpaces.RemoveExtraSpaces(sourceElement.Text));
            }
            return getSourcesListFromUI;
        }
        public string GetFrequencyOfUsesForSelectedPhraseDB(List<XmlDocument> wordInfoXMLs,int meaningID,int phraseID)
        {
            string FrequencyOfUse = "";
            foreach(XmlDocument wordInfoXML in wordInfoXMLs)
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
                if(frequencyNode != null)
                {
                    FrequencyOfUse = frequencyNode.InnerText.ToString();
                }
            }
            return FrequencyOfUse;
        }
        public List<string> GetRelatedKeywordsForSelectedPhraseDB(List<XmlDocument> wordInfoXMLs, int meaningID, int phraseID)
        {
            List<string> relatedkeywords = new List<string>();
            foreach (XmlDocument wordInfoXML in wordInfoXMLs)
            {
                XmlNodeList relatedKeywordNodes;
                if (wordInfoXML.OuterXml.Contains("WORDINFO"))
                {
                    relatedKeywordNodes = wordInfoXML.SelectNodes("//WORDINFO/MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE[ID/text()='" + phraseID + "']/HOOKS/HOOK");
                }
                else
                {
                    relatedKeywordNodes = wordInfoXML.SelectNodes("//MNGS/MNG[ID/text()='" + meaningID + "']/PHRASES/PHRASE[ID/text()='" + phraseID + "']/HOOKS/HOOK");
                }
                foreach (XmlNode relatedKeywordNode in relatedKeywordNodes)
                {
                    relatedkeywords.Add(relatedKeywordNode.InnerText.ToString() + ",");
                }
            }
            if (relatedkeywords.Count == 0)
            {
                relatedkeywords.Add("N/A");
            }
            else
            {
                int IndexOfLastElement = relatedkeywords.Count - 1;
                relatedkeywords[IndexOfLastElement] = relatedkeywords[IndexOfLastElement].Replace(",", "");
            }
            return relatedkeywords;
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForEachPhrasessData(List<XmlDocument> wordInfoXMLs, int phrasesCount, string randomWordFromDB, int meaningsCount)
        {
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            CommonCollections common = new CommonCollections();
            int waitForPreLoader = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"]);
            GetMeaningElement(meaningsCount).Click();
            GetPhrasesElement(phrasesCount).Click();
            common.SortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            common.SortedPhrasesAndPhraseIDsFromDB = GetSortedPhrasesForMeaningID(wordInfoXMLs, common.SortedMeaningsAndMeaningIDsFromDB, meaningsCount - 1);
            int meaningID = common.SortedMeaningsAndMeaningIDsFromDB[meaningsCount - 1].ID;
            string meaningText = common.SortedMeaningsAndMeaningIDsFromDB[meaningsCount - 1].Text;
            int phraseID = common.SortedPhrasesAndPhraseIDsFromDB[phrasesCount - 1].ID;
            string phraseText = common.SortedPhrasesAndPhraseIDsFromDB[phrasesCount - 1].Text;
            XmlDocument sourcesXML = GetSourcesXML(phraseID);

            //Sources
            common.SourcesListForSelectedMeaningFromUI = GetSourcesForSelectedPhrase_UI(phrasesCount,meaningsCount);
            if (common.SourcesListForSelectedMeaningFromUI.Count == 0)
            {
                Thread.Sleep(waitForPreLoader);
                common.SourcesListForSelectedMeaningFromUI = GetSourcesForSelectedPhrase_UI(phrasesCount, meaningsCount);
            }
            common.SourcesListForSelectedMeaningFromDB = GetSourcesForSelectedPhrase_DB(sourcesXML);

            //FrequencyOfUse
            common.FrequencyOfUseFromUI = GetFrequencyOfUsesUI();
            common.FrequencyOfUseFromDB = GetFrequencyOfUsesForSelectedPhraseDB(wordInfoXMLs,meaningID,phraseID);

            //RelatedKeywords
            common.RelatedKeywordsListForSelectedPhraseFromUI = GetRelatedKeywordsForSelectedPhrase_UI();
            common.RelatedKeywordsListForSelectedPhraseFromDB = GetRelatedKeywordsForSelectedPhraseDB(wordInfoXMLs,meaningID,phraseID);

            //Assertions
            try
            {
                Assert.IsTrue(common.SourcesListForSelectedMeaningFromDB.SequenceEqual(common.SourcesListForSelectedMeaningFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Sources List";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, ex.Message, ex.StackTrace, common.SourcesListForSelectedMeaningFromDB, common.SourcesListForSelectedMeaningFromUI, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(common.FrequencyOfUseFromDB.SequenceEqual(common.FrequencyOfUseFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "frequency Of use";
                KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, common.FrequencyOfUseFromDB, common.FrequencyOfUseFromUI, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                assertionFailures.Add(failure);
            }
            try
            {
                Assert.IsTrue(common.RelatedKeywordsListForSelectedPhraseFromDB.SequenceEqual(common.RelatedKeywordsListForSelectedPhraseFromUI));
            }
            catch (Exception ex)
            {
                string nameOfAssertion = "Related Keywords";
                KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, ex.Message, ex.StackTrace, common.RelatedKeywordsListForSelectedPhraseFromDB, common.RelatedKeywordsListForSelectedPhraseFromUI, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                assertionFailures.Add(failure);
            }
            return assertionFailures;
        }
        public bool IsFirstMeaningSelected()
        {
            bool selection = false;
            List<IWebElement> meaningsWebElement = GetMeaningsList();
            string selectedMeaningElementColour = GetSelectedElementCSSColour(meaningsWebElement[0]);
            if (selectedMeaningElementColour == "rgba(0, 0, 0, 0)")
            {
                selection = true;
            }
            return selection;
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
        public List<string> GetSeeAlsoWordsFromUI()
        {
            List<string> seeAlsoWordsList = new List<string>();
            List<IWebElement> seeAlsoWebElements = GetSeeAlsoWords();
            foreach (IWebElement seeAlsoElement in seeAlsoWebElements)
            {
                seeAlsoWordsList.Add(seeAlsoElement.Text);
            }
            return seeAlsoWordsList;
        }
        public IWebElement GetSynonymForNumber(int number)
        {
            return _browser.GetElement("#synonyms > ui > li:nth-child(" + number + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public void SelectRequiredSynonym(int synonymsCount)
        {
            GetSynonymForNumber(synonymsCount).Click();   
        }
        public List<string> GetMeaningsForSelectedKeyword()
        {
            WaitForMeaningsToLoad();
            List<string> meaningsFromUI = new List<string>();
            List<IWebElement> meaningsWebElement = GetMeaningsList();
            foreach (IWebElement meaningElement in meaningsWebElement)
            {
                meaningsFromUI.Add(meaningElement.Text);
            }
            return meaningsFromUI;
        }
        public bool WaitForSynonymsToLoad()
        {
            return _browser.WaitForElement("#synonyms > ui > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public void SelectPreviousActiveMeaning(int meaningCount)
        {
            GetMeaningElement(meaningCount).Click();
            WaitForSynonymsToLoad();
        }
        public string GetKeywordFromTextBox()
        {
            return TextboxElement().GetAttribute("value");
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForCommonMethod(string selectedKeyword, string noResultsText)
        {
            List<KeywordAssertionFailure<string>> commonAssertionFailures = new List<KeywordAssertionFailure<string>>();

            CommonCollections common = new CommonCollections();
            WaitForRelatedkeywordsToLoad();
            string KeywordFromTextBox = GetKeywordFromTextBox();
            List<XmlDocument> wordInfoXMLs = GetWordInfoXML(KeywordFromTextBox);

            if (wordInfoXMLs.Count != 0)
            {

                //Act
                WaitForMeaningsToLoad();
                common.MeaningsListFromUI = GetMeaningsForSelectedKeyword();
                common.MeaningsListFromDatabase = GetMeaningsListFromDB(KeywordFromTextBox);
                common.SeeAlsoWordsFromDB = GetSeeAlsoListFromDB(wordInfoXMLs[0]);
                common.SeeAlsoWordsFromUI = GetSeeAlsoWordsFromUI();
                int meaningsCount = 1;
                int phrasesCount = 1;

                List<KeywordAssertionFailure<string>> meaningsAssertionFailures = GetAssertionFailuresForEachMeaningsData(wordInfoXMLs, meaningsCount, KeywordFromTextBox);
                foreach (KeywordAssertionFailure<string> failure in meaningsAssertionFailures)
                {
                    commonAssertionFailures.Add(failure);
                }
                List<KeywordAssertionFailure<string>> phrasesAssertionFailures = GetAssertionFailuresForEachPhrasessData(wordInfoXMLs, phrasesCount, KeywordFromTextBox, meaningsCount);
                foreach (KeywordAssertionFailure<string> failure in phrasesAssertionFailures)
                {
                    commonAssertionFailures.Add(failure);
                }

                //Assertions
                try
                {
                    Assert.IsTrue(common.MeaningsListFromDatabase.SequenceEqual(common.MeaningsListFromUI));
                }
                catch (Exception ex)
                {
                    string nameOfAssertion = "Meanings List";
                    KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, ex.Message, ex.StackTrace, common.MeaningsListFromDatabase, common.MeaningsListFromUI, KeywordFromTextBox, common.MeaningsListFromDatabase[meaningsCount - 1], common.PhrasesListForSelectedMeaningFromDB[0], nameOfAssertion);
                    commonAssertionFailures.Add(failure);
                }
                try
                {
                    Assert.IsTrue(IsFirstMeaningSelected());
                }
                catch (Exception ex)
                {
                    string nameOfAssertion = "Is First Meaning Selected";
                    KeywordAssertionFailure<string> failure = SingleActualValueInputOfFailure(ex, ex.Message, ex.StackTrace, IsFirstMeaningSelected().ToString(), KeywordFromTextBox, common.MeaningsListFromDatabase[meaningsCount - 1], nameOfAssertion);
                    commonAssertionFailures.Add(failure);
                }
                try
                {
                    Assert.IsTrue(common.SeeAlsoWordsFromDB.SequenceEqual(common.SeeAlsoWordsFromUI));
                }
                catch (Exception ex)
                {
                    string nameOfAssertion = "See Also List";
                    KeywordAssertionFailure<string> failure = StringCollectionOfFailures(ex, ex.Message, ex.StackTrace, common.SeeAlsoWordsFromDB, common.SeeAlsoWordsFromUI, KeywordFromTextBox, common.MeaningsListFromDatabase[meaningsCount - 1], nameOfAssertion);
                    commonAssertionFailures.Add(failure);
                }
            }
            else
            {
                string message = "Failed to extract Word InfoXML";
                KeywordAssertionFailure<string> failure = KeywordSearchFailure(message,KeywordFromTextBox);
                commonAssertionFailures.Add(failure);
            }
            return commonAssertionFailures;
        }
        public void SelectRequiredRelatedKeyword(int relatedKeywordsCount)
        {
            RandomRelatedKeywordFromList(relatedKeywordsCount).Click();
        }
        public bool WaitForPhrasesListToLoad()
        {
            return _browser.WaitForElement("#phrases-container > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public bool WaitForRelatedkeywordsToLoad()
        {
            return _browser.WaitForElement("#related-keywords-container > ui > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public void SelectPreviousActivePhrase(int phrasesCount)
        {
            GetPhrasesElement(phrasesCount).Click();
            WaitForRelatedkeywordsToLoad();
        }
        public IWebElement GetSeeAlsoForNumber(int number)
        {
            return _browser.GetElement("#see-also-words > ui > li:nth-child(" + number + ")", WebBrowser.ElementSelectorType.CssSelector);
        }
        public void SelectRequiredSeeAlsoAndWaitForMeaning(int seeAlsoCount)
        {
            GetSeeAlsoForNumber(seeAlsoCount).Click();
            WaitForMeaningOnLeftToLoad();
        }
        public List<string> RelatedKeywordsListForCurrentMeaningAndPhrase(List<XmlDocument> wordInfoXMLs,int meaningsCount, int phrasesCount)
        {
            CommonCollections common = new CommonCollections();
            common.SortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            common.SortedPhrasesAndPhraseIDsFromDB = GetSortedPhrasesForMeaningID(wordInfoXMLs, common.SortedMeaningsAndMeaningIDsFromDB, meaningsCount - 1);
            int meaningID = common.SortedMeaningsAndMeaningIDsFromDB[meaningsCount - 1].ID;
            int phraseID = common.SortedPhrasesAndPhraseIDsFromDB[phrasesCount - 1].ID;
            return GetRelatedKeywordsForSelectedPhraseDB(wordInfoXMLs, meaningID, phraseID);
        }
        public List<string> TotalPhrasesFromDBForSelectedMeaning(List<XmlDocument> wordInfoXMLs,int meaningsCount)
        {
            List<Meaning> sortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            return GetPhrasesOfSelectedMeaning_DB(wordInfoXMLs, meaningsCount - 1, sortedMeaningsAndMeaningIDsFromDB);
        }
        public List<string> TotalSynonymsFromDBForSelectedMeaning(List<XmlDocument> wordInfoXMLs, int meaningsCount)
        {
            List<Meaning> sortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            return GetSynonymsForSelectedMeaningID_DB(wordInfoXMLs, meaningsCount - 1, sortedMeaningsAndMeaningIDsFromDB);
        }
        public bool IsMeaningsListVisible()
        {
            return _browser.IsElementVisible("#meanings > ul > li", WebBrowser.ElementSelectorType.CssSelector);
        }
        public string TextFromPhrasesContainer()
        {
            return _browser.GetElement("#phrases-grid", WebBrowser.ElementSelectorType.CssSelector).Text.Replace("\r\n","");
        }
        public IWebElement TimelineLinkElement()
        {
            return _browser.GetElement("timeline-link", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForTimelinePhraseToLoad()
        {
            return _browser.WaitForElement("timeline-phrase", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetTimelinePhraseTextElement()
        {
            return _browser.GetElement("timeline-phrase", WebBrowser.ElementSelectorType.ID);
        }
        public string GetPhraseTextFromTimeline()
        {
            TimelineLinkElement().Click();
            _browser.SwitchtoCurrentWindow();
            WaitForTimelinePhraseToLoad();
            string phraseText = GetTimelinePhraseTextElement().Text;
            _browser.CloseBrowser();
            _browser.SwitchtoPreviousWindow();
            return phraseText;
        }
        public IWebElement CumulativeUsageLinkElement()
        {
            return _browser.GetElement("cumulativegraph-link", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForCumulativePhraseToLoad()
        {
            return _browser.WaitForElement("phraseText", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetCumulativePhraseTextElement()
        {
            return _browser.GetElement("phraseText", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetFrequencyOfUseElement()
        {
            return _browser.GetElement("uniqueUsesCount", WebBrowser.ElementSelectorType.ID);
        }
        public string GetPhraseTextFromUsageGraphWindow()
        {
            CumulativeUsageLinkElement().Click();
            _browser.SwitchtoCurrentWindow();
            WaitForCumulativePhraseToLoad();
            return GetCumulativePhraseTextElement().Text;
        }
        public string GetFrequencyOfUseFromUsageGraphWindow()
        {
            return GetFrequencyOfUseElement().Text;
        }
        public void CloseCumulativeUsageGraphWindow()
        {
            _browser.CloseBrowser();
            _browser.SwitchtoPreviousWindow();
        }
        public XmlDocument GetCumulativeUsageXML(int phraseIDFromDB)
        {
            _dataAccess = new SearchKeywordDataAccess();
            return _dataAccess.GetCumulativeUsageXML(phraseIDFromDB);
        }
        public List<string> GetVerifiedSourcesListFromDB(XmlDocument cumulativeXML)
        {
            List<string> verifiedCumulativeSources = new List<string>();
            CommonMethods removeSpaces = new CommonMethods();
            foreach (XmlNode source in cumulativeXML.SelectNodes("//CUMULATIVE_USAGE//PHRASE//VR_SOURCES//VR_SOURCE"))
            {
                string year = source.SelectSingleNode("YEAR").InnerText.ToString();
                string firstName = source.ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText;
                string lastName = source.ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText;
                string title = source.SelectSingleNode("TITLE").InnerText.ToString();
                verifiedCumulativeSources.Add(removeSpaces.RemoveExtraSpaces(year + " : " + firstName + " " + lastName + " : " + title));
            }
            return verifiedCumulativeSources;
        }
        public List<string> GetUnverifiedSourcesListFromDB(XmlDocument cumulativeXML)
        {
            List<string> unverifiedCumulativeSources = new List<string>();
            if (cumulativeXML.SelectSingleNode("//CUMULATIVE_USAGE//PHRASE//UNVR_SOURCES") != null)
            {
                foreach (XmlElement UNVR_YEAR in cumulativeXML.SelectSingleNode("//CUMULATIVE_USAGE//PHRASE//UNVR_SOURCES"))
                {
                    unverifiedCumulativeSources.Add(UNVR_YEAR.InnerText);
                }
            }
            return unverifiedCumulativeSources;
        }
        public List<IWebElement> GetAllCircleElements()
        {
            return _browser.GetElements("#chartAxis > svg > circle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public IWebElement GetSourceElement()
        {
            return _browser.GetElement("#sourceinfo-display", WebBrowser.ElementSelectorType.CssSelector);
        }
        public string GetVerifiedSourceFromUI(int randomNumberFromVerifiedSourcesCount)
        {
            CommonMethods removeSpaces = new CommonMethods();
            Actions action = new Actions(_browser._webDriver);
            List<IWebElement> sourcesWebElement = GetAllCircleElements();
            action.MoveToElement(sourcesWebElement[randomNumberFromVerifiedSourcesCount - 1]).Perform();
            string verifiedCumulativeSource = removeSpaces.RemoveExtraSpaces(GetSourceElement().Text);
            return verifiedCumulativeSource;
        }
        public string GetUnverifiedSourceFromUI(int randomNumberFromUnverifiedSourcesCount)
        {
            Actions action = new Actions(_browser._webDriver);
            List<IWebElement> sourcesWebElement = GetAllCircleElements();
            action.MoveToElement(sourcesWebElement[randomNumberFromUnverifiedSourcesCount - 1]).Perform();
            string unverifiedCumulativeSource = GetSourceElement().Text;
            return unverifiedCumulativeSource;
        }
        public List<KeywordAssertionFailure<string>> GetAssertionFailuresForTimelineAndUsageGraph(string randomWordFromDB, List<XmlDocument> wordInfoXMLs, int meaningsCount, int phrasesCount)
        {
            List<KeywordAssertionFailure<string>> timelineAndUsageGraphAssertionFailures = new List<KeywordAssertionFailure<string>>();
            CommonCollections common = new CommonCollections();
            WaitForRelatedkeywordsToLoad();
            string phraseText = GetPhrasesElement(phrasesCount).Text;
            common.FrequencyOfUseFromUI = GetFrequencyOfUsesUI();
            common.SortedMeaningsAndMeaningIDsFromDB = GetSortedMeaningsForWordXML(wordInfoXMLs);
            common.SortedPhrasesAndPhraseIDsFromDB = GetSortedPhrasesForMeaningID(wordInfoXMLs, common.SortedMeaningsAndMeaningIDsFromDB, meaningsCount - 1);
            int meaningID = common.SortedMeaningsAndMeaningIDsFromDB[meaningsCount - 1].ID;
            string meaningText = common.SortedMeaningsAndMeaningIDsFromDB[meaningsCount - 1].Text;
            int phraseID = common.SortedPhrasesAndPhraseIDsFromDB[phrasesCount - 1].ID;

            if (common.FrequencyOfUseFromUI != "0")
            {
                //Act
                common.PhraseTextFromTimelineWindow = GetPhraseTextFromTimeline();

                //Assertion
                try
                {
                    Assert.IsTrue(phraseText.SequenceEqual(common.PhraseTextFromTimelineWindow));
                }
                catch (Exception ex)
                {
                    string nameOfAssertion = "Timeline Phrase Text";
                    KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, phraseText, common.PhraseTextFromTimelineWindow, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                    timelineAndUsageGraphAssertionFailures.Add(failure);
                }
            }

            //Cummulative
            if (common.FrequencyOfUseFromUI != "0")
            {
                //Act
                common.PhraseTextFromUsageGraphWindow = GetPhraseTextFromUsageGraphWindow();
                common.FrequencyOfUseFromUsageGraphWindow = GetFrequencyOfUseFromUsageGraphWindow();
                XmlDocument cumulativeXML = GetCumulativeUsageXML(phraseID);
                common.VerifiedSourcesFromDB = GetVerifiedSourcesListFromDB(cumulativeXML);
                common.UnverifiedSourcesFromDB = GetUnverifiedSourcesListFromDB(cumulativeXML);
                common.TotalNumberOFSourcesFromDB = common.VerifiedSourcesFromDB.Count + common.UnverifiedSourcesFromDB.Count;
                int randomNumberFromVerifiedSourcesCount = _random.Next(1, common.VerifiedSourcesFromDB.Count);
                int randomNumberFromUnverifiedSourcesCount = 0;
                common.VerifiedSourceFromUI = GetVerifiedSourceFromUI(randomNumberFromVerifiedSourcesCount);
                if (common.TotalNumberOFSourcesFromDB > 5)
                {
                    randomNumberFromUnverifiedSourcesCount = _random.Next(1, common.UnverifiedSourcesFromDB.Count);
                    common.UnverifiedSourceFromUI = GetUnverifiedSourceFromUI(randomNumberFromUnverifiedSourcesCount + common.VerifiedSourcesFromDB.Count);
                    try
                    {
                        Assert.IsTrue(common.UnverifiedSourcesFromDB[randomNumberFromUnverifiedSourcesCount - 1].SequenceEqual(common.UnverifiedSourceFromUI));
                    }
                    catch (Exception ex)
                    {
                        string nameOfAssertion = "Unverified Sources In Usage Graph";
                        KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, common.UnverifiedSourcesFromDB[randomNumberFromUnverifiedSourcesCount - 1], common.UnverifiedSourceFromUI, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                        timelineAndUsageGraphAssertionFailures.Add(failure);
                    }
                }
                CloseCumulativeUsageGraphWindow();

                //Assertion
                try
                {
                    Assert.IsTrue(phraseText.SequenceEqual(common.PhraseTextFromTimelineWindow));
                }
                catch (Exception ex)
                {
                    string nameOfAssertion = "Usage Graph Phrase Text";
                    KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, phraseText, common.PhraseTextFromUsageGraphWindow, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                    timelineAndUsageGraphAssertionFailures.Add(failure);
                }
                try
                {
                    Assert.IsTrue(common.FrequencyOfUseFromUI.SequenceEqual(common.FrequencyOfUseFromUsageGraphWindow));
                }
                catch (Exception ex)
                {
                    string nameOfAssertion = "Frequency Of Use In Usage Graph";
                    KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, common.FrequencyOfUseFromUI, common.FrequencyOfUseFromUsageGraphWindow, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                    timelineAndUsageGraphAssertionFailures.Add(failure);
                }
                try
                {
                    Assert.IsTrue(common.VerifiedSourcesFromDB[randomNumberFromVerifiedSourcesCount - 1].SequenceEqual(common.VerifiedSourceFromUI));
                }
                catch (Exception ex)
                {
                    string nameOfAssertion = "Verified Sources In Usage Graph";
                    KeywordAssertionFailure<string> failure = SingleValueOfFailure(ex, ex.Message, ex.StackTrace, common.VerifiedSourcesFromDB[randomNumberFromVerifiedSourcesCount - 1], common.VerifiedSourceFromUI, randomWordFromDB, meaningText, phraseText, nameOfAssertion);
                    timelineAndUsageGraphAssertionFailures.Add(failure);
                }
            }
            return timelineAndUsageGraphAssertionFailures;
        }
        public void DeleteExistingLogFiles()
        {
            string errorFileLocation = ConfigurationManager.AppSettings["OutputErrorFileLocation"].ToString();
            string allKeywordsLogFileLocation = ConfigurationManager.AppSettings["AllKeywordsStatusFileLocation"].ToString();
            try
            {
                File.Delete(errorFileLocation);
                File.Delete(allKeywordsLogFileLocation);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to delete existing log files");
            }
        }
        public List<Word> GetArrayKeywordsFromCSVFile()
        {
            string keywordArrayFileLocation = ConfigurationManager.AppSettings["KeywordArrayInput"].ToString();
            List<Word> keywords = new List<Word>();
            Word keyword = null;
            string[] keywordsFromCSV = File.ReadAllLines(keywordArrayFileLocation);
            string[] keywordsFromCSVAfterRemovingTitles = keywordsFromCSV.Skip(1).ToArray();
            foreach (string eachKeyword in keywordsFromCSVAfterRemovingTitles)
            {
                string[] singleKeyword = eachKeyword.Split(',');
                keyword = new Word
                {
                    ID = Convert.ToInt32(singleKeyword[0]),
                    Text = Convert.ToString(singleKeyword[1])
                };
                keywords.Add(keyword);
            }
            return keywords;
        }
        public List<Word> GetAllKeywordsRelatedToSearchCriteriaFromDB()
        {
            string searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"].ToString();
            List<Word> keywordsList = new List<Word>();
            if (searchCriteria == "Array")
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
        public StringBuilder CreateStringBuilderAndAppendTitles()
        {
            StringBuilder keywordLogCSV = new StringBuilder();
            keywordLogCSV.AppendLine("ID,Keyword,Meaning,Phrase,Result,Start time,Exception message,Exception stacktrace");
            return keywordLogCSV;
        }
        public void WriteKeywordLogCSVToFile(StringBuilder keywordLogCSV)
        {
            string allKeywordsLogFileLocation = ConfigurationManager.AppSettings["AllKeywordsStatusFileLocation"].ToString();
            File.AppendAllText(allKeywordsLogFileLocation, keywordLogCSV.ToString());
        }
        public StringBuilder CreateStringBuilderAndAppendTitlesForErrorLog()
        {
            StringBuilder errorLogCSV = new StringBuilder();
            errorLogCSV.AppendLine("Assertion name,Keyword,Meaning,Phrase,Expected,Actual,Assertion failure,Custom message");
            return errorLogCSV;
        }
        public void WriteErrorLogCSVToFile(StringBuilder errorLogCSV)
        {
            string errorFileLocation = ConfigurationManager.AppSettings["OutputErrorFileLocation"].ToString();
            File.AppendAllText(errorFileLocation, errorLogCSV.ToString());
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
        public StringBuilder WriteKeywordDataToCSVDocument(int IDFromDB, string randomWordFromDB, StringBuilder keywordLogCSV, string startTime)
        {
            keywordLogCSV.Clear();
            keywordLogCSV.AppendLine($"{IDFromDB},\"{randomWordFromDB}\",{""},{""},Fail,{startTime}");
            return keywordLogCSV;
        }
        public StringBuilder WriteKeywordDataToCSVDocument(int IDFromDB, string randomWordFromDB, List<KeywordAssertionFailure<string>> assertionFailures, StringBuilder keywordLogCSV, string startTime)
        {
            List<XmlDocument> wordInfoXMLs = GetWordInfoXML(randomWordFromDB);
            List<string> meaningsListFromDatabase = GetMeaningsListFromDB(randomWordFromDB);
            List<string> phrasesListForSelectedMeaningFromDB = GetPhrasesOfFirstMeaningFromDB(wordInfoXMLs);
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
        public void WriteAsertionFailuresToDocument(List<KeywordAssertionFailure<string>> assertionFailures, StringBuilder errorLogCSV)
        {
            string errorFileLocation = ConfigurationManager.AppSettings["OutputErrorFileLocation"].ToString();
            errorLogCSV.Clear();
            foreach (KeywordAssertionFailure<string> failure in assertionFailures)
            {
                try
                {
                    if (failure.ExceptionMessage == "Outdated Keyword" || failure.ExceptionMessage == "No Results")
                    {
                        errorLogCSV.AppendLine($"{failure.ExceptionMessage},\"{failure.Keyword}\"");
                    }
                    else if (failure.NameOfAssertion == "Meanings List" || failure.NameOfAssertion == "Phrases List" || failure.NameOfAssertion == "Sources List" || failure.NameOfAssertion == "Related Keywords" || failure.NameOfAssertion == "Synonyms List")
                    {
                        List<string> differentExpectedValuesList = failure.ExpectedValues.Except(failure.ActualValues).ToList();
                        List<string> differentActualValuesList = failure.ActualValues.Except(failure.ExpectedValues).ToList();
                        
                        if (differentExpectedValuesList.Count == differentActualValuesList.Count)
                        {
                            errorLogCSV.AppendLine($"{failure.NameOfAssertion},\"{failure.Keyword}\",\"{failure.Meaning}\",\"{failure.Phrase.Replace("\"","")}\",\"{differentExpectedValuesList[0].Replace("\"", "")}\",\"{differentActualValuesList[0].Replace("\"", "")}\",\"{failure.ExceptionMessage}\"");
                            if (differentActualValuesList.Count > 1)
                            {
                                for (int count = 2; count <= differentActualValuesList.Count; count++)
                                {
                                    errorLogCSV.AppendLine($",,,,\"{differentExpectedValuesList[count - 1].Replace("\"", "")}\",\"{differentActualValuesList[count - 1].Replace("\"", "")}\",");
                                }
                            }

                        }
                        else
                        {
                            if(differentActualValuesList.Count == 0 || differentExpectedValuesList.Count == 0)
                            {
                                if (differentActualValuesList.Count > differentExpectedValuesList.Count)
                                {
                                    for (int count = 1; count <= differentActualValuesList.Count; count++)
                                    {
                                        errorLogCSV.AppendLine($",,,,,\"{differentActualValuesList[count - 1].Replace("\"", "")}\",");
                                    }
                                }
                                else
                                {
                                    for (int count = 1; count <= differentExpectedValuesList.Count; count++)
                                    {
                                        errorLogCSV.AppendLine($",,,,\"{differentExpectedValuesList[count - 1].Replace("\"", "")}\",,");
                                    }
                                }
                            }
                            else
                            {
                                errorLogCSV.AppendLine($"{failure.NameOfAssertion},\"{failure.Keyword}\",\"{failure.Meaning}\",\"{failure.Phrase.Replace("\"", "")}\",\"{differentExpectedValuesList[0].Replace("\"", "")}\",\"{differentActualValuesList[0].Replace("\"", "")}\",\"{failure.ExceptionMessage}\"");
                                if (differentExpectedValuesList.Count > differentActualValuesList.Count)
                                {
                                    for (int count = 2; count <= differentActualValuesList.Count; count++)
                                    {
                                        errorLogCSV.AppendLine($",,,,\"{differentExpectedValuesList[count - 1].Replace("\"", "")}\",\"{differentActualValuesList[count - 1].Replace("\"", "")}\",");
                                    }
                                    for (int count = differentActualValuesList.Count; count <= differentExpectedValuesList.Count; count++)
                                    {
                                        errorLogCSV.AppendLine($",,,,,\"{differentExpectedValuesList[count - 1].Replace("\"", "")}\",,");
                                    }
                                }
                                else
                                {
                                    for (int count = 2; count <= differentExpectedValuesList.Count; count++)
                                    {
                                        errorLogCSV.AppendLine($",,,,\"{differentExpectedValuesList[count - 1].Replace("\"", "")}\",\"{differentActualValuesList[count - 1].Replace("\"", "")}\",");
                                    }
                                    for (int count = differentExpectedValuesList.Count; count <= differentActualValuesList.Count; count++)
                                    {
                                        errorLogCSV.AppendLine($",,,,,,\"{differentActualValuesList[count - 1].Replace("\"", "")}\",");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        errorLogCSV.AppendLine($"{failure.NameOfAssertion},\"{failure.Keyword}\",\"{failure.Meaning}\",\"{failure.Phrase.Replace("\"", "")}\",\"{failure.ExpectedValue.Replace("\"", "")}\",\"{failure.ActualValue.Replace("\"", "")}\",{failure.ExceptionMessage}");
                    }
                }
                catch (Exception)
                {
                    errorLogCSV.AppendLine($"Error Occured in Building CSV File,\"{failure.Keyword}\",\"{failure.Meaning}\",\"{failure.Phrase.Replace("\"", "")}\"");
                }
            }
            File.AppendAllText(errorFileLocation, errorLogCSV.ToString());
        }
        public List<string> GetPhrasesOfFirstMeaningFromDB(List<XmlDocument> wordInfoXMLs)
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
                    string phraseText = node1.SelectSingleNode("TEXT").InnerText.ToString();
                    phrases.Add(phraseText);
                }
            }
            phrases.Sort();
            List<string> phrasesRemovedExtraSpaces = new List<string>();
            foreach (string phrase in phrases)
            {
                phrasesRemovedExtraSpaces.Add(removeSpaces.RemoveExtraSpaces(phrase));
            }
            return phrasesRemovedExtraSpaces;
        }
        public StringBuilder WriteKeywordDataToLogCSV(int IDFromDB, string randomWordFromDB, List<KeywordAssertionFailure<string>> assertionFailures, StringBuilder keywordLogCSV, string startTime)
        {
            List<XmlDocument> wordInfoXMLs = GetWordInfoXML(randomWordFromDB);
            List<string> meaningsListFromDatabase = GetMeaningsListFromDB(randomWordFromDB);
            List<string> phrasesListForSelectedMeaningFromDB = GetPhrasesOfFirstMeaningFromDB(wordInfoXMLs);
            
            keywordLogCSV.Clear();
            if (assertionFailures.Count == 0)
            {
                keywordLogCSV.AppendLine($"{IDFromDB},\"{randomWordFromDB}\",\"{meaningsListFromDatabase[0]}\",\"{phrasesListForSelectedMeaningFromDB[0].Replace("\"", "")}\",Pass,{startTime}");
            }
            else
            {
                keywordLogCSV.AppendLine($"{IDFromDB},\"{randomWordFromDB}\",\"{meaningsListFromDatabase[0]}\",\"{phrasesListForSelectedMeaningFromDB[0].Replace("\"", "")}\",Fail,{startTime}");
            }

            return keywordLogCSV;
        }
        public void SelectRequiredRelatedKeywordAndWaitForMeanings(int meaningsCount, int phrasesCount, List<string> relatedKeywordsListForSelectedPhraseFromDB, string relatedKeywordSelected)
        {
            int waitForPreLoader = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"]);
            SelectPreviousActiveMeaning(meaningsCount);
            SelectPreviousActivePhrase(phrasesCount);
            List<string> relatedKeywordsList = new List<string>();
            foreach (string relatedKeyword in relatedKeywordsListForSelectedPhraseFromDB)
            {
                relatedKeywordsList.Add(relatedKeyword.Replace(",", ""));
            }
            int relatedKeywordsCount = relatedKeywordsList.FindIndex(str => str.Contains(relatedKeywordSelected));
            SelectRequiredRelatedKeyword(relatedKeywordsCount + 1);
            Thread.Sleep(waitForPreLoader);
        }
        public List<string> GetRelatedKeywordsListForSelectedModes(List<string> relatedKeywordsListForSelectedPhraseFromDB, List<string> allRelatedKeywordsList)
        {
            string relatedKeywordSearchMode = ConfigurationManager.AppSettings["RelatedKeywordsModes"].ToString();
            List<string> relatedKeywordsList = new List<string>();
            if (relatedKeywordSearchMode == "All")
            {
                relatedKeywordsList = relatedKeywordsListForSelectedPhraseFromDB;
            }
            else if (relatedKeywordSearchMode == "Non-Reapeated")
            {
                List<string> relatedKeywordsWithoutComma = new List<string>();
                foreach(string relatedkeyword in relatedKeywordsListForSelectedPhraseFromDB)
                {
                    relatedKeywordsWithoutComma.Add(relatedkeyword.Replace(",", ""));
                }
                relatedKeywordsList = relatedKeywordsWithoutComma.Except(allRelatedKeywordsList).ToList();
            }
            else if (relatedKeywordSearchMode == "None")
            {
                relatedKeywordsList.Clear();
            }
            return relatedKeywordsList;
        }
        public KeywordAssertionFailure<string> AddFailureAsNoResultsFound(string keyword)
        {
            string message = "No Results";
            return KeywordSearchFailure(message, keyword);
        }
        public List<string> AddRelatedKeywordsToList(List<string> relatedKeywordsListForSelectedPhraseFromDB, List<string> allRelatedKeywordsList)
        {
            foreach (string relatedKeyword in relatedKeywordsListForSelectedPhraseFromDB)
            {
                allRelatedKeywordsList.Add(relatedKeyword.Replace(",",""));
            }
            return allRelatedKeywordsList.Distinct().ToList();
        }
        public void CheckForRedirectedKeywordAndWait(string randomRelatedKeyword)
        {
            int waitForPreLoader = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"]);
            try
            {
                string RedirectedKeyword = _dataAccess.GetRedirectedKeyword(randomRelatedKeyword);
                if(RedirectedKeyword != null)
                {
                    WaitForRedirectedKeywordElement();
                    Thread.Sleep(waitForPreLoader);
                }
            }
            catch (Exception)
            {

            }
        }
        public IWebElement GetOkButtonElement()
        {
            return _browser.GetElement("ui-dialog-buttonset", WebBrowser.ElementSelectorType.Class);
        }
        public void CheckForAlertMessageAndConfirm()
        {
            _browser.SwitchtoCurrentWindow();
            GetOkButtonElement().Click();
        }
    }
}
