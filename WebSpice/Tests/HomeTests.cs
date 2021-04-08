using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Common;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.Models.UITest;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WebSpice.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace Quant.Spice.Test.UI.Web.WebSpice.Tests
{
    [TestClass]
    public class HomeTests : TestBase
    {
        protected static WebPage _page;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _home = new Home(_browser);
        }

        [TestMethod]
        public void TC_LaunchWebSpicePortal_WaitTillThePageGetsLoaded()
        {
            //Arrange
            _home.WaitForPageToLoad();

            //Act

            //Assert
            Assert.IsTrue(_home.IsMainContainerVisible());
        }

        [TestMethod]
        public void TC_EnterSingleLetter_VerifyKeywordSuggestions()
        {
            //Arrange
            List<string> keywordSuggestionsFromUI = new List<string>();
            List<string> keywordSuggestionsFromDatabase = new List<string>();
            string RandomLetter = _home.GetRandomLetter();

            //Act
            keywordSuggestionsFromUI = _home.GetSurroundingWordsForSingleLetter(RandomLetter);
            keywordSuggestionsFromDatabase = _home.GetRandomSingleLetterFromDB(RandomLetter);

            //Assert
            Assert.IsTrue(keywordSuggestionsFromUI.SequenceEqual(keywordSuggestionsFromDatabase));
        }

        [TestMethod]
        public void TC_EnterMultipleLetters_VerifyKeywordSuggestions()
        {
            //Arrange
            List<string> keywordSuggestionsFromUI = new List<string>();
            List<string> keywordSuggestionsFromDatabase = new List<string>();
            string RandomLetter = _home.GetRandomMultipleLetters();

            //Act
            keywordSuggestionsFromUI = _home.GetSurroundingWordsForMultipleLetters(RandomLetter);
            keywordSuggestionsFromDatabase = _home.GetRandomMultipleLettersFromDB(RandomLetter);

            //Assert
            Assert.IsTrue(keywordSuggestionsFromUI.SequenceEqual(keywordSuggestionsFromDatabase));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsList()
        {
            //Arrange
            List<string> meaningsListFromUI = new List<string>();
            List<string> meaningsListFromDatabase = new List<string>();
            string randomWordFromDB = _home.GetRandomWord();

            //Act
            meaningsListFromUI = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            meaningsListFromDatabase = _home.GetMeaningsListFromDB(randomWordFromDB);

            //Assert
            Assert.IsTrue(meaningsListFromUI.SequenceEqual(meaningsListFromDatabase));
        }
        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningDisplyedOnLeft()
        {
            //Arrange
            string meaningOnLeftFromUI;
            string meaningOnLeftFromDB;
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);

            //Act
            _home.EnterkeywordAndSearch(randomWordFromDB);
            meaningOnLeftFromUI = _home.GetMeaningOnLeft();
            meaningOnLeftFromDB = _home.GetMeaningDisplayedOnLeftDB(randomWordFromDB, wordInfoXMLs);

            //Assert
            Assert.IsTrue(meaningOnLeftFromUI.SequenceEqual(meaningOnLeftFromDB));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifySynonymsListForSelectedMeaning()
        {
            //Arrange
            List<string> synonymsListForSelectedMeaningFromUI = new List<string>();
            List<string> synonymsListForSelectedMeaningFromDB = new List<string>();
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);

            //Act
            _home.EnterkeywordAndSearch(randomWordFromDB);
            synonymsListForSelectedMeaningFromUI = _home.GetSynonymsForSelectedMeaning_UI();
            synonymsListForSelectedMeaningFromDB = _home.GetSynonymsForSelectedMeaning_DB(wordInfoXMLs);

            //Assert
            Assert.IsTrue(synonymsListForSelectedMeaningFromUI.SequenceEqual(synonymsListForSelectedMeaningFromDB));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyPhrasesListForSelectedMeaning()
        {
            //Arrange
            List<string> phrasesListForSelectedMeaningFromUI = new List<string>();
            List<string> phrasesListForSelectedMeaningFromDB = new List<string>();
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);

            //Act
            phrasesListForSelectedMeaningFromUI = _home.GetPhrasesForFirstMeaning_UI(randomWordFromDB);
            phrasesListForSelectedMeaningFromDB = _home.GetPhrasesOfFirstMeaning_DB(wordInfoXMLs);

            //Assert
            Assert.IsTrue(phrasesListForSelectedMeaningFromUI.SequenceEqual(phrasesListForSelectedMeaningFromDB));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifySourcesList_Frequency_RelatedKeywordsForSelectedMeaning()
        {
            //Arrange
            List<string> sourcesListForSelectedMeaningFromUI = new List<string>();
            List<string> sourcesListForSelectedMeaningFromDB = new List<string>();
            string frequencyOfUseFromUI = "";
            string frequencyOfUseFromDB = "";
            List<string> relatedKeywordsListForSelectedMeaningFromUI = new List<string>();
            List<string> relatedKeywordsListForSelectedMeaningFromDB = new List<string>();
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);
            int phraseIDFromDB = _home.GetPhraseIDFromDB(wordInfoXMLs);
            XmlDocument sourcesXML = _home.GetSourcesXML(phraseIDFromDB);


            //Act
            //Sources
            sourcesListForSelectedMeaningFromUI = _home.GetSourcesForFirstPhrase_UI(randomWordFromDB);
            sourcesListForSelectedMeaningFromDB = _home.GetSourcesForSelectedPhrase_DB(sourcesXML);

            //FrequencyOfUse
            frequencyOfUseFromUI = _home.GetFrequencyOfUsesUI();
            frequencyOfUseFromDB = _home.GetFrequencyOfUsesDB(wordInfoXMLs);

            //RelatedKeywords
            relatedKeywordsListForSelectedMeaningFromUI = _home.GetRelatedKeywordsForSelectedPhrase_UI();
            relatedKeywordsListForSelectedMeaningFromDB = _home.GetRelatedKeywordsFromDB(wordInfoXMLs[0]);

            //Assert
            Assert.IsTrue(sourcesListForSelectedMeaningFromUI.SequenceEqual(sourcesListForSelectedMeaningFromDB));
            Assert.IsTrue(frequencyOfUseFromUI.SequenceEqual(frequencyOfUseFromDB));
            Assert.IsTrue(relatedKeywordsListForSelectedMeaningFromUI.SequenceEqual(relatedKeywordsListForSelectedMeaningFromDB));
        }

        [TestMethod]
        public void TC_SelectRelatedKeyword_VerifySelectedKeywordInTheTextbox()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            int countOfRelatedKeyword = _home.RandomCountOfRelatedKeyword(randomWordFromDB);
            string randomRelatedKeyword = _home.GetRandomRelatedKeyword(randomWordFromDB, countOfRelatedKeyword);
            string keywordDisplayedInTextBox = _home.RelatedKeywordFromTextBox(randomRelatedKeyword);
            string relatedKeyword = _home.CheckForRedirectedKeyword(randomRelatedKeyword, keywordDisplayedInTextBox);
            string redirectedFromKeyword = "redirected from " + randomRelatedKeyword;
            string redirectedKeywordTextFromWebElement = _home.RedirectedKeywordTextFromWebElement();

            //Assert
            Assert.IsTrue(relatedKeyword.SequenceEqual(keywordDisplayedInTextBox));
            if (randomRelatedKeyword != keywordDisplayedInTextBox)
            {
                Assert.IsTrue(redirectedFromKeyword.SequenceEqual(redirectedKeywordTextFromWebElement));
            }
        }


        [TestMethod]
        public void TC_SelectSeeAlsoWord_VerifySelectedKeywordInTheTextbox()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            int countOfSeeAlsoWord = _home.RandomCountOfSeeAlsoWords(randomWordFromDB);
            string RandomSeeAlsoWord = _home.GetRandomSeeAlsoWord(randomWordFromDB, countOfSeeAlsoWord);
            string KeywordDisplayedInTextBox = _home.SeeAlsoWordFromTextBox();

            //Assert
            Assert.IsTrue(RandomSeeAlsoWord.SequenceEqual(KeywordDisplayedInTextBox));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsNavigationNext()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            Searchedkeyword _searchedKeyword = new Searchedkeyword();
            List<string> meaningsListFromUI = new List<string>();

            //Act
            meaningsListFromUI = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            for (int meaningsCount = 1; meaningsCount <= meaningsListFromUI.Count; meaningsCount++)
            {
                _searchedKeyword.SelectedCircleColour = _home.SelectedCircleColor(meaningsCount);
                _searchedKeyword.SelectedMeaning = _home.SelectedMeaning();
                _searchedKeyword.MeaningIndexOnWebPage = _home.MeaningIndexOnWebPage();
                _searchedKeyword.MeaningIndex = _home.MeaningIndex(meaningsListFromUI, meaningsCount);

                Assert.IsTrue(meaningsListFromUI[meaningsCount - 1].SequenceEqual(_searchedKeyword.SelectedMeaning));
                Assert.IsTrue(_searchedKeyword.CssColourForSelectedCircle.SequenceEqual(_searchedKeyword.SelectedCircleColour));
                Assert.IsTrue(_searchedKeyword.MeaningIndex.SequenceEqual(_searchedKeyword.MeaningIndexOnWebPage));

                if (meaningsListFromUI.Count > 1)
                {
                    _home.ClickNavigationNextButton();
                    if (meaningsCount == meaningsListFromUI.Count)
                    {
                        Assert.IsTrue(_home.IsNavigationButtonNextDisabled());
                    }
                }
            }
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyClickingCircularButtons()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            Searchedkeyword _searchedKeyword = new Searchedkeyword();
            List<string> meaningsListFromUI = new List<string>();

            //Act
            meaningsListFromUI = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            for (int meaningsCount = 1; meaningsCount < meaningsListFromUI.Count; meaningsCount++)
            {
                _searchedKeyword.SelectedCircleColour = _home.SelectedCircleColor(meaningsCount);
                _searchedKeyword.SelectedMeaning = _home.SelectedMeaning();
                _searchedKeyword.MeaningIndexOnWebPage = _home.MeaningIndexOnWebPage();
                _searchedKeyword.MeaningIndex = _home.MeaningIndex(meaningsListFromUI, meaningsCount);

                Assert.IsTrue(meaningsListFromUI[meaningsCount - 1].SequenceEqual(_searchedKeyword.SelectedMeaning));
                Assert.IsTrue(_searchedKeyword.CssColourForSelectedCircle.SequenceEqual(_searchedKeyword.SelectedCircleColour));
                Assert.IsTrue(_searchedKeyword.MeaningIndex.SequenceEqual(_searchedKeyword.MeaningIndexOnWebPage));

                if (meaningsListFromUI.Count > 1)
                {
                    _home.ClickCircularButtons(meaningsCount + 1);

                    if (meaningsCount == meaningsListFromUI.Count)
                    {
                        Assert.IsTrue(_home.IsNavigationButtonNextDisabled());
                    }
                }
            }
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsNavigationPrevious()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            Searchedkeyword _searchedKeyword = new Searchedkeyword();
            List<string> meaningsListFromUI = new List<string>();

            //Act
            meaningsListFromUI = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            if (meaningsListFromUI.Count > 1)
            {
                _home.ClickLastNavigationButton();
            }
            for (int meaningsCount = meaningsListFromUI.Count; meaningsCount > 0; meaningsCount--)
            {
                _searchedKeyword.SelectedCircleColour = _home.SelectedCircleColor(meaningsCount);
                _searchedKeyword.SelectedMeaning = _home.SelectedMeaning();
                _searchedKeyword.MeaningIndexOnWebPage = _home.MeaningIndexOnWebPage();
                _searchedKeyword.MeaningIndex = _home.MeaningIndex(meaningsListFromUI, meaningsCount);

                Assert.IsTrue(meaningsListFromUI[meaningsCount - 1].SequenceEqual(_searchedKeyword.SelectedMeaning));
                Assert.IsTrue(_searchedKeyword.CssColourForSelectedCircle.SequenceEqual(_searchedKeyword.SelectedCircleColour));
                Assert.IsTrue(_searchedKeyword.MeaningIndex.SequenceEqual(_searchedKeyword.MeaningIndexOnWebPage));

                if (meaningsListFromUI.Count > 1)
                {
                    _home.ClickNavigationPreviousButton();

                    if (meaningsCount == 1)
                    {
                        Assert.IsTrue(_home.IsNavigationButtonPreviousDisabled());
                    }
                }
            }
        }
        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsNavigationLast()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            Searchedkeyword _searchedKeyword = new Searchedkeyword();
            List<string> meaningsListFromUI = new List<string>();

            //Act
            meaningsListFromUI = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            for (int meaningsCount = meaningsListFromUI.Count; meaningsCount <= meaningsListFromUI.Count; meaningsCount++)
            {
                if (meaningsListFromUI.Count > 1)
                {
                    _home.ClickLastNavigationButton();
                    if (meaningsCount == meaningsListFromUI.Count)
                    {
                        Assert.IsTrue(_home.IsNavigationButtonLastDisabled());
                    }
                }
                _searchedKeyword.SelectedCircleColour = _home.SelectedCircleColor(meaningsCount);
                _searchedKeyword.SelectedMeaning = _home.SelectedMeaning();
                _searchedKeyword.MeaningIndexOnWebPage = _home.MeaningIndexOnWebPage();
                _searchedKeyword.MeaningIndex = _home.MeaningIndex(meaningsListFromUI, meaningsCount);

                Assert.IsTrue(meaningsListFromUI[meaningsCount - 1].SequenceEqual(_searchedKeyword.SelectedMeaning));
                Assert.IsTrue(_searchedKeyword.CssColourForSelectedCircle.SequenceEqual(_searchedKeyword.SelectedCircleColour));
                Assert.IsTrue(_searchedKeyword.MeaningIndex.SequenceEqual(_searchedKeyword.MeaningIndexOnWebPage));
            }
        }
        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsNavigationFirst()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            Searchedkeyword _searchedKeyword = new Searchedkeyword();
            List<string> meaningsListFromUI = new List<string>();
            int meaningsCount = 1;

            //Act
            meaningsListFromUI = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            if (meaningsListFromUI.Count > 1)
            {
                _home.ClickLastNavigationButton();
                _home.ClickFirstNavigationButton();
                Assert.IsTrue(_home.IsNavigationButtonFirstDisabled());
            }
            _searchedKeyword.SelectedCircleColour = _home.SelectedCircleColor(meaningsCount);
            _searchedKeyword.SelectedMeaning = _home.SelectedMeaning();
            _searchedKeyword.MeaningIndexOnWebPage = _home.MeaningIndexOnWebPage();
            _searchedKeyword.MeaningIndex = _home.MeaningIndex(meaningsListFromUI, meaningsCount);

            Assert.IsTrue(meaningsListFromUI[meaningsCount - 1].SequenceEqual(_searchedKeyword.SelectedMeaning));
            Assert.IsTrue(_searchedKeyword.CssColourForSelectedCircle.SequenceEqual(_searchedKeyword.SelectedCircleColour));
            Assert.IsTrue(_searchedKeyword.MeaningIndex.SequenceEqual(_searchedKeyword.MeaningIndexOnWebPage));
        }
        [TestMethod]
        public void TC_SearchForKeywords_VerifyPreviousAndNextKeywordHistory()
        {
            //Arrange            
            List<string> searchedKeywordsList = new List<string>();
            string keywordFromTextBox = "";
            string disableButtonOpacity = "0.5";


            //Act
            searchedKeywordsList = _home.GetRandomKeywordsAndSearch();
            for (int keywordsCount = 4; keywordsCount > 0; keywordsCount--)
            {
                _home.ClickPreviousKeywordHistoryButton();
                keywordFromTextBox = _home.KeywordFromTextBox();
                Assert.IsTrue(searchedKeywordsList[keywordsCount - 1].SequenceEqual(keywordFromTextBox));
                if (keywordsCount == 1)
                {
                    Assert.IsTrue(_home.PreviousHistoryButtonDisabled().SequenceEqual(disableButtonOpacity));
                }
            }
            for (int keywordsCount = 1; keywordsCount <= 4; keywordsCount++)
            {
                _home.ClickNextKeywordHistoryButton();
                keywordFromTextBox = _home.KeywordFromTextBox();
                Assert.IsTrue(searchedKeywordsList[keywordsCount].SequenceEqual(keywordFromTextBox));
                if (keywordsCount == 4)
                {
                    Assert.IsTrue(_home.NextHistoryButtonDisabled().SequenceEqual(disableButtonOpacity));
                }
            }
        }
        [TestMethod]
        public void TC_LaunchWebSpicePortal_VerifyBubbleAlert()
        {
            //Arrange
            _home.WaitForPageToLoad();
            string bubbleImageText = "Enter KeyWord Here";

            //Act
            string textFromBubbleElement = _home.BubbleImageElement().Text;

            //Assert
            Assert.IsTrue(_home.IsBubbleImageVisible()); 
            Assert.IsTrue(bubbleImageText.SequenceEqual(textFromBubbleElement));
        }
        public void SendEmailOfTestStatus(List<KeywordAssertionFailure<string>> assertionFailures)
        {
            if (ConfigurationManager.AppSettings["TestStatusNotificationEmailAddresses"] != null)
            {
                string[] toAddresses = ConfigurationManager.AppSettings["TestStatusNotificationEmailAddresses"].Split(',');
                if (toAddresses[0] != "")
                {
                    string searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"].ToString();

                    //mail subject
                    string mailSubject = "WebSpice UI Automation Test Run Status";
                    if (searchCriteria == "Random")
                    {
                        int keywordsCount = Int32.Parse(ConfigurationManager.AppSettings["Random"]);
                        mailSubject = $"{mailSubject} - {searchCriteria} - {keywordsCount}";
                    }
                    else if (searchCriteria == "All")
                    {
                        mailSubject = $"{mailSubject} - {searchCriteria}";
                    }
                    else if (searchCriteria == "Range")
                    {
                        int minimumRange = Int32.Parse(ConfigurationManager.AppSettings["Range_Min"]);
                        int maximumRange = Int32.Parse(ConfigurationManager.AppSettings["Range_Max"]);
                        mailSubject = $"{mailSubject} - {searchCriteria} - {minimumRange} to {maximumRange}";
                    }
                    else
                    {
                        mailSubject = $"{mailSubject} - {searchCriteria}";
                    }

                    //Mail body
                    string mailBody;
                    if (assertionFailures.Count >= 1)
                    {
                        mailBody = "Test run was completed and Run failed Due to log errors";
                    }
                    else
                    {
                        mailBody = "Test run Was completed and Run successful without any errors";
                    }

                    Email email = new Email();
                    email.SendEmail(mailSubject, mailBody, toAddresses);
                }
            }
        }
        [TestMethod]
        public void TC_SearchMultipleKeywords_BasicData()
        {
            //Arrange
            string noResultsText = "We're Sorry. No results are found at this time.Try one of our keywords instead";
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            List<Word> keywords = null;
            _home.DeleteExistingLogFiles();

            try
            {
                keywords = _home.GetAllKeywordsRelatedToSearchCriteriaFromDB();
                StringBuilder keywordLogCSV = _home.CreateStringBuilderAndAppendTitles();
                _home.WriteKeywordLogCSVToFile(keywordLogCSV);
                StringBuilder errorLogCSV = _home.CreateStringBuilderAndAppendTitlesForErrorLog();
                _home.WriteErrorLogCSVToFile(errorLogCSV);

                //Act
                for (int i = 0; i < keywords.Count; i++)
                {
                    Word keyword = keywords[i];
                    List<KeywordAssertionFailure<string>> assertionFailuresToWriteToCSV = new List<KeywordAssertionFailure<string>>();
                    assertionFailuresToWriteToCSV.Clear();
                    keywordLogCSV.Clear();
                    string startTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                    try
                    {
                        _home.EnterkeywordAndSearch(keyword.Text);
                        
                        if (_home.TextFromPhrasesContainer() == noResultsText)
                        {
                            string message = "No Results";
                            KeywordAssertionFailure<string> failure = _home.KeywordSearchFailure(message, keyword.Text);
                            assertionFailures.Add(failure);
                            assertionFailuresToWriteToCSV.Add(failure);
                            keywordLogCSV = _home.WriteKeywordDataToCSVDocument(keyword.ID, keyword.Text, keywordLogCSV, startTime);
                            _home.WriteAsertionFailuresToDocument(assertionFailuresToWriteToCSV, errorLogCSV);
                        }
                        else
                        {
                            List<KeywordAssertionFailure<string>> keywordAssertionFailures = _home.GetAssertionFailuresForCommonMethod(keyword.Text, noResultsText);
                            foreach (KeywordAssertionFailure<string> failure in keywordAssertionFailures)
                            {
                                assertionFailures.Add(failure);
                                assertionFailuresToWriteToCSV.Add(failure);
                            }
                            keywordLogCSV = _home.WriteKeywordDataToCSVDocument(keyword.ID, keyword.Text, keywordAssertionFailures, keywordLogCSV, startTime);
                            _home.WriteAsertionFailuresToDocument(assertionFailuresToWriteToCSV, errorLogCSV);
                        }
                    }
                    catch (Exception Ex)
                    {
                        keywordLogCSV.AppendLine($"{keyword.ID},{keyword.Text},{""},{""},{"Test failed in basic data"},{startTime},\"{Ex.Message}\",\"{Ex.StackTrace}\"");
                    }
                    _home.WriteKeywordLogCSVToFile(keywordLogCSV);
                }
                if (assertionFailures.Count >= 1)
                {
                    Assert.Fail("Assertion failed due to log errors");
                    LogInfo.WriteLine("Test method failed due to assertion failures");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                SendEmailOfTestStatus(assertionFailures);
            }
        }

        [TestMethod]
        public void TC_SearchForKeywords_TestAllFeatures()
        {
            //Arrange
            string noResultsText = "We're Sorry. No results are found at this time.Try one of our keywords instead";
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
            List<string> searchedRelatedKeywordsList = new List<string>();
            List<Word> keywords = null;
            int testReInitializationCount = Int32.Parse(ConfigurationManager.AppSettings["CountForReInitializationOfTestRun"].ToString());
            _home.DeleteExistingLogFiles();

            try
            {
                keywords = _home.GetAllKeywordsRelatedToSearchCriteriaFromDB();
                StringBuilder keywordLogCSV = _home.CreateStringBuilderAndAppendTitles();
                _home.WriteKeywordLogCSVToFile(keywordLogCSV);
                StringBuilder errorLogCSV = _home.CreateStringBuilderAndAppendTitlesForErrorLog();
                _home.WriteErrorLogCSVToFile(errorLogCSV);

                //Act
                for (int i = 0; i < keywords.Count; i++)
                {
                    Word keyword = keywords[i];
                    List<KeywordAssertionFailure<string>> assertionFailuresToWriteToCSV = new List<KeywordAssertionFailure<string>>();
                    assertionFailuresToWriteToCSV.Clear();
                    keywordLogCSV.Clear();
                    string startTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");

                    try
                    {
                        _home.EnterkeywordAndSearch(keyword.Text);

                        if (_home.TextFromPhrasesContainer() == noResultsText)
                        {
                            string selectedkeyword = keyword.Text;
                            KeywordAssertionFailure<string> failure = _home.AddFailureAsNoResultsFound(selectedkeyword);
                            assertionFailures.Add(failure);
                            assertionFailuresToWriteToCSV.Add(failure);
                            keywordLogCSV = _home.WriteKeywordDataToCSVDocument(keyword.ID, keyword.Text, keywordLogCSV, startTime);
                            _home.WriteAsertionFailuresToDocument(assertionFailuresToWriteToCSV, errorLogCSV);
                        }
                        else
                        {
                            //Basic Data Assertion
                            List<KeywordAssertionFailure<string>> basicDataAssertionFailures = _home.GetAssertionFailuresForCommonMethod(keyword.Text, noResultsText);
                            foreach (KeywordAssertionFailure<string> failure in basicDataAssertionFailures)
                            {
                                assertionFailures.Add(failure);
                                assertionFailuresToWriteToCSV.Add(failure);
                            }

                            //Meanings data assertions
                            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(keyword.Text);
                            if (wordInfoXMLs.Count != 0)
                            {
                                int totalMeaningsCountFromDB = _home.GetMeaningsListFromDB(keyword.Text).Count;

                                for (int meaningsCount = 1; meaningsCount <= totalMeaningsCountFromDB; meaningsCount++)
                                {
                                    List<KeywordAssertionFailure<string>> meaningsAssertionFailures = _home.GetAssertionFailuresForEachMeaningsData(wordInfoXMLs, meaningsCount, keyword.Text);
                                    foreach (KeywordAssertionFailure<string> failure in meaningsAssertionFailures)
                                    {
                                        assertionFailures.Add(failure);
                                        assertionFailuresToWriteToCSV.Add(failure);
                                    }

                                    //Phrases Data Assertions
                                    List<string> totalPhrasesFromDB = _home.TotalPhrasesFromDBForSelectedMeaning(wordInfoXMLs, meaningsCount);
                                    for (int phrasesCount = 1; phrasesCount <= totalPhrasesFromDB.Count; phrasesCount++)
                                    {
                                        List<KeywordAssertionFailure<string>> phrasesAssertionFailures = _home.GetAssertionFailuresForEachPhrasessData(wordInfoXMLs, phrasesCount, keyword.Text, meaningsCount);
                                        foreach (KeywordAssertionFailure<string> failure in phrasesAssertionFailures)
                                        {
                                            assertionFailures.Add(failure);
                                            assertionFailuresToWriteToCSV.Add(failure);
                                        }

                                        //Related Keywords Data Assertions
                                        List<string> relatedKeywordsListForSelectedPhraseFromDB = _home.RelatedKeywordsListForCurrentMeaningAndPhrase(wordInfoXMLs, meaningsCount, phrasesCount);

                                        List<string> relatedKeywordsList = _home.GetRelatedKeywordsListForSelectedModes(relatedKeywordsListForSelectedPhraseFromDB, searchedRelatedKeywordsList);
                                        for (int relatedKeywordsCount = 1; relatedKeywordsCount <= relatedKeywordsList.Count; relatedKeywordsCount++)
                                        {
                                            string relatedKeywordSelected = relatedKeywordsList[relatedKeywordsCount - 1].Replace(",", "");
                                            if (relatedKeywordSelected != "N/A")
                                            {
                                                _home.SelectRequiredRelatedKeywordAndWaitForMeanings(meaningsCount, phrasesCount, relatedKeywordsListForSelectedPhraseFromDB, relatedKeywordSelected);
                                                if (_home.TextFromPhrasesContainer() == noResultsText)
                                                {
                                                    KeywordAssertionFailure<string> failure = _home.AddFailureAsNoResultsFound(relatedKeywordSelected);
                                                    assertionFailures.Add(failure);
                                                    assertionFailuresToWriteToCSV.Add(failure);
                                                }
                                                else
                                                {
                                                    _home.CheckForRedirectedKeywordAndWait(relatedKeywordSelected);
                                                    List<KeywordAssertionFailure<string>> relatedKeywordsAssertionFailures = _home.GetAssertionFailuresForCommonMethod(relatedKeywordSelected, noResultsText);
                                                    foreach (KeywordAssertionFailure<string> failure in relatedKeywordsAssertionFailures)
                                                    {
                                                        assertionFailures.Add(failure);
                                                        assertionFailuresToWriteToCSV.Add(failure);
                                                    }
                                                }
                                                _home.ClickPreviousKeywordHistoryButton();
                                            }
                                        }
                                        searchedRelatedKeywordsList = _home.AddRelatedKeywordsToList(relatedKeywordsListForSelectedPhraseFromDB, searchedRelatedKeywordsList);
                                    }

                                    //Synonyms Asertions
                                    List<string> totalSynonymsFromDB = _home.TotalSynonymsFromDBForSelectedMeaning(wordInfoXMLs, meaningsCount);
                                    for (int synonymsCount = 1; synonymsCount <= totalSynonymsFromDB.Count; synonymsCount++)
                                    {
                                        string synonymSelected = totalSynonymsFromDB[synonymsCount - 1].Replace(",", "");
                                        _home.SelectPreviousActiveMeaning(meaningsCount);
                                        _home.SelectRequiredSynonym(synonymsCount);
                                        if (_home.TextFromPhrasesContainer() == noResultsText)
                                        {
                                            KeywordAssertionFailure<string> failure = _home.AddFailureAsNoResultsFound(synonymSelected);
                                            assertionFailures.Add(failure);
                                            assertionFailuresToWriteToCSV.Add(failure);
                                        }
                                        else
                                        {
                                            List<KeywordAssertionFailure<string>> synonymsAssertionFailures = _home.GetAssertionFailuresForCommonMethod(synonymSelected, noResultsText);
                                            foreach (KeywordAssertionFailure<string> failure in synonymsAssertionFailures)
                                            {
                                                assertionFailures.Add(failure);
                                                assertionFailuresToWriteToCSV.Add(failure);
                                            }
                                        }
                                        _home.ClickPreviousKeywordHistoryButton();
                                    }
                                }

                                //SeeAlso Assertions
                                List<string> seeAlsoWordsFromDB = _home.GetSeeAlsoListFromDB(wordInfoXMLs[0]);
                                for (int seeAlsoCount = 1; seeAlsoCount <= seeAlsoWordsFromDB.Count; seeAlsoCount++)
                                {
                                    string seeAlsoSelected = seeAlsoWordsFromDB[seeAlsoCount - 1];
                                    _home.SelectRequiredSeeAlsoAndWaitForMeaning(seeAlsoCount);
                                    if (_home.TextFromPhrasesContainer() == noResultsText)
                                    {
                                        KeywordAssertionFailure<string> failure = _home.AddFailureAsNoResultsFound(seeAlsoSelected);
                                        assertionFailures.Add(failure);
                                        assertionFailuresToWriteToCSV.Add(failure);
                                    }
                                    else
                                    {
                                        List<KeywordAssertionFailure<string>> seeAlsoAssertionFailures = _home.GetAssertionFailuresForCommonMethod(seeAlsoSelected, noResultsText);
                                        foreach (KeywordAssertionFailure<string> failure in seeAlsoAssertionFailures)
                                        {
                                            assertionFailures.Add(failure);
                                            assertionFailuresToWriteToCSV.Add(failure);
                                        }
                                    }
                                    _home.ClickPreviousKeywordHistoryButton();
                                }
                            }
                            else
                            {
                                _home.CheckForAlertMessageAndConfirm();
                                string message = "Failed to extract Word InfoXML";
                                KeywordAssertionFailure<string> failure = _home.KeywordSearchFailure(message, keyword.Text);
                                assertionFailures.Add(failure);
                                assertionFailuresToWriteToCSV.Add(failure);
                            }
                        }
                        keywordLogCSV = _home.WriteKeywordDataToLogCSV(keyword.ID, keyword.Text, assertionFailuresToWriteToCSV, keywordLogCSV, startTime);
                        _home.WriteAsertionFailuresToDocument(assertionFailuresToWriteToCSV, errorLogCSV);
                        if (i % testReInitializationCount == 0 && i != 0)
                        {
                            base.TestCleanup();
                            base.TestInitialize();
                            _home = new Home(_browser);
                        }
                    }
                    catch (Exception Ex)
                    {
                        keywordLogCSV.AppendLine($"{keyword.ID},{keyword.Text},{""},{""},{"Test failed in basic data"},{startTime},\"{Ex.Message}\",\"{Ex.StackTrace}\"");
                    }
                    _home.WriteKeywordLogCSVToFile(keywordLogCSV);
                }
                if (assertionFailures.Count >= 1)
                {
                    Assert.Fail("Assertion failed due to log errors");
                    LogInfo.WriteLine("Test method failed due to assertion failures");
                }
            }
            catch (Exception Ex)
            {
                Assert.Fail(String.Format("{0}\n\n{1}", Ex.Message, Ex.StackTrace));
            }
            finally
            {
                SendEmailOfTestStatus(assertionFailures);
            }
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'

            base.TestCleanup();
        }
    }
}
