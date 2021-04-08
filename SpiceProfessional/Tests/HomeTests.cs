using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Test.UI.Common;
using Quant.Spice.Test.UI.Common.Models;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.Models.UITest;
using Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.Tests
{
    [TestClass]
    public class HomeTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _home = new Home(_windowUIDriver);
        }

        [TestMethod]
        public void TC_LaunchSpiceProfessionalApplication_WaitUntillWindowGetsLoaded()
        {
            //Arrange
            _home.WaitForHomeWindowToLoad();

            //Act

            //Assert
            Assert.IsTrue(_home.IsPhrasesTabVisible());
        }

        [TestMethod]
        public void TC_EnterSingleLetter_VerifyKeywordSuggestions()
        {
            //Arrange
            List<string> keywordSuggestionsFromWindow = new List<string>();
            List<string> keywordSuggestionsFromDatabase = new List<string>();
            string RandomLetter = _home.GetRandomLetter();

            //Act
            keywordSuggestionsFromWindow = _home.GetSurroundingWordsForSingleLetter(RandomLetter);
            keywordSuggestionsFromDatabase = _home.GetRandomSingleLetterFromDB(RandomLetter);

            //Assert
            Assert.IsTrue(keywordSuggestionsFromWindow.SequenceEqual(keywordSuggestionsFromDatabase));
        }

        [TestMethod]
        public void TC_EnterMultipleLetters_VerifyKeywordSuggestions()
        {
            //Arrange
            List<string> keywordSuggestionsFromWindow = new List<string>();
            List<string> keywordSuggestionsFromDatabase = new List<string>();
            string RandomLetter = _home.GetRandomMultipleLetters();

            //Act
            keywordSuggestionsFromWindow = _home.GetSurroundingWordsForMultipleLetters(RandomLetter);
            keywordSuggestionsFromDatabase = _home.GetRandomMultipleLettersFromDB(RandomLetter);

            //Assert
            Assert.IsTrue(keywordSuggestionsFromWindow.SequenceEqual(keywordSuggestionsFromDatabase));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsList()
        {
            //Arrange
            List<string> meaningsListFromWindow = new List<string>();
            List<string> meaningsListFromDatabase = new List<string>();
            string randomWordFromDB = _home.GetRandomWord();

            //Act
            meaningsListFromWindow = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            meaningsListFromDatabase = _home.GetMeaningsListFromDB(randomWordFromDB);

            //Assert
            Assert.IsTrue(meaningsListFromWindow.SequenceEqual(meaningsListFromDatabase));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsContentForSelectedMeaning()
        {
            //Arrange
            string meaningOnLeftFromWindow;
            string meaningOnLeftFromDB;
            List<string> phrasesListForSelectedMeaningFromWindows = new List<string>();
            List<string> phrasesListForSelectedMeaningFromDB = new List<string>();
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);

            //Act
            _home.EnterkeywordWaitForPhraseToLoad(randomWordFromDB);
            meaningOnLeftFromWindow = _home.GetMeaningOnLeft();
            meaningOnLeftFromDB = _home.GetMeaningDisplayedOnLeftDB(randomWordFromDB, wordInfoXMLs);
            phrasesListForSelectedMeaningFromWindows = _home.GetPhrasesForSelectedMeaning_Windows(randomWordFromDB);
            phrasesListForSelectedMeaningFromDB = _home.GetPhrasesOfFirstMeaning(wordInfoXMLs);

            //Assert
            Assert.IsTrue(meaningOnLeftFromWindow.SequenceEqual(meaningOnLeftFromDB));
            Assert.IsTrue(phrasesListForSelectedMeaningFromWindows.SequenceEqual(phrasesListForSelectedMeaningFromDB));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyPhrasesContentForSelectedPhrase() 
        {
            //Arrange
            List<string> sourcesListForSelectedMeaningFromWindows = new List<string>();
            List<string> sourcesListForSelectedMeaningFromDB = new List<string>();
            string frequencyOfUseFromUI = "";
            string frequencyOfUseFromDB = "";
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);
            int phraseIDFromDB = _home.GetPhraseIDFromDB(wordInfoXMLs);
            XmlDocument sourcesXML = _home.GetSourcesXML(phraseIDFromDB);

            //Act
            //Sources
            _home.EnterkeywordWaitForPhraseToLoad(randomWordFromDB);
            sourcesListForSelectedMeaningFromWindows = _home.GetSourcesForSelectedPhrase_Windows();
            sourcesListForSelectedMeaningFromDB = _home.GetSourcesForSelectedPhrase_DB(sourcesXML);

            //FrequencyOfUse
            List<AppiumWebElement> commonAppiumElements = _home.GetCommonAppiumElements();
            frequencyOfUseFromUI = _home.GetFrequencyOfUsesWindows(commonAppiumElements);
            frequencyOfUseFromDB = _home.GetFrequencyOfUsesDB(wordInfoXMLs);

            //Assert
            Assert.IsTrue(sourcesListForSelectedMeaningFromWindows.SequenceEqual(sourcesListForSelectedMeaningFromDB));
            Assert.IsTrue(frequencyOfUseFromUI.SequenceEqual(frequencyOfUseFromDB));
        }

        [TestMethod]
        public void TC_SelectNextMeaningLink_VerifySelectedMeaning()
        {
            //Arrange
            List<string> meaningsListFromDatabase = new List<string>();
            int randomNumberFromMeaningsCount = 0;
            string selectedMeaningFromWindows = "";
            string selectedMeaningFromDB = "";
            string nextMeaningLinkText = "To view the next meaning, click here";
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);

            //Act
            meaningsListFromDatabase = _home.GetMeaningsListFromDB(randomWordFromDB);
            if (meaningsListFromDatabase.Count > 1)
            {
                randomNumberFromMeaningsCount = _home.GetRandomNumberFromMeaningsCount(meaningsListFromDatabase);
                _home.SelectRandomMeaningAndClickNextMeaningButton(meaningsListFromDatabase, randomNumberFromMeaningsCount, nextMeaningLinkText,randomWordFromDB);
                selectedMeaningFromWindows = _home.GetSelectedMeaningFromWindows();
                selectedMeaningFromDB = meaningsListFromDatabase[randomNumberFromMeaningsCount];
            }
            
            //Assert
            Assert.IsTrue(selectedMeaningFromWindows.SequenceEqual(selectedMeaningFromDB));
        }

        [TestMethod]
        public void TC_SelectSeeAlsoWord_VerifySelectedKeywordInTheTextbox()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            
            //Act
            string RandomSeeAlsoWord = _home.GetRandomSeeAlsoWordAndSelect(randomWordFromDB);
            string KeywordDisplayedInTextBox = _home.KeywordFromTextBox();

            //Assert
            Assert.IsTrue(RandomSeeAlsoWord.SequenceEqual(KeywordDisplayedInTextBox));
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyClickingCircularButtons()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            List<string> meaningsListFromWindow = new List<string>();

            //Act
            meaningsListFromWindow = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            List<AppiumWebElement> circularButtonElements = _home.GetCircularButtonElemnets();
            for (int meaningsCount = 1; meaningsCount < meaningsListFromWindow.Count; meaningsCount++)
            {
                string SelectedMeaning = _home.GetSelectedMeaningFromWindows();

                Assert.IsTrue(meaningsListFromWindow[meaningsCount - 1].SequenceEqual(SelectedMeaning));

                if (meaningsListFromWindow.Count > 1)
                {
                    _home.ClickCircularButtons(meaningsCount,circularButtonElements);
                }
            }
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsNavigationNextAndPrevious()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            List<string> meaningsListFromWindow = new List<string>();
            int meaningsCount = 0;

            //Act
            meaningsListFromWindow = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            if (_home.IsNavigationButtonNextVisible())
            {
                while (_home.IsNavigationButtonNextVisible() == true)
                {
                    _home.ClickNavigationButtonNext();
                    string SelectedMeaning = _home.GetSelectedMeaningFromWindows();
                    meaningsCount++;
                    Assert.IsTrue(meaningsListFromWindow[meaningsCount].SequenceEqual(SelectedMeaning));
                }
                Assert.IsFalse(_home.IsNavigationButtonNextVisible());
                while(_home.IsNavigationButtonPreviousVisible() == true)
                {
                    _home.ClickNavigationButtonPrevious();
                    string SelectedMeaning = _home.GetSelectedMeaningFromWindows();
                    meaningsCount--;
                    Assert.IsTrue(meaningsListFromWindow[meaningsCount].SequenceEqual(SelectedMeaning));
                }
                Assert.IsFalse(_home.IsNavigationButtonPreviousVisible());
            }
            else
            {
                Assert.IsFalse(_home.IsNavigationButtonNextVisible());
            }
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsNavigationFirst()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            List<string> meaningsListFromWindow = new List<string>();

            //Act
            meaningsListFromWindow = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            if (_home.IsNavigationButtonLastVisible())
            {
                _home.ClickNavigationButtonLast();
                _home.ClickNavigationButtonFirst();
                string SelectedMeaning = _home.GetSelectedMeaningFromWindows();

                Assert.IsTrue(meaningsListFromWindow.First().SequenceEqual(SelectedMeaning));
                Assert.IsFalse(_home.IsNavigationButtonFirstVisible());
            }
            else
            {
                Assert.IsFalse(_home.IsNavigationButtonLastVisible());
            }
        }

        [TestMethod]
        public void TC_SearchKeyword_VerifyMeaningsNavigationLast()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            List<string> meaningsListFromWindow = new List<string>();

            //Act
            meaningsListFromWindow = _home.GetMeaningsForEnteredKeyword(randomWordFromDB);
            if (_home.IsNavigationButtonLastVisible())
            {
                _home.ClickNavigationButtonLast();
                string SelectedMeaning = _home.GetSelectedMeaningFromWindows();

                Assert.IsTrue(meaningsListFromWindow.Last().SequenceEqual(SelectedMeaning));
                Assert.IsFalse(_home.IsNavigationButtonLastVisible());
            }
            else
            {
                Assert.IsFalse(_home.IsNavigationButtonLastVisible());
            }
        }

        [TestMethod]
        public void TC_SearchForKeywords_VerifyPreviousAndNextKeywordHistory()
        {
            //Arrange            
            List<string> searchedKeywordsList = new List<string>();
            string keywordFromTextBox = "";

            //Act
            searchedKeywordsList = _home.GetRandomKeywordsAndSearch();
            for (int keywordsCount = 4; keywordsCount > 0; keywordsCount--)
            {
                _home.ClickPreviousKeywordHistoryButton();
                keywordFromTextBox = _home.KeywordFromTextBox();
                Assert.IsTrue(searchedKeywordsList[keywordsCount - 1].SequenceEqual(keywordFromTextBox));
                if (keywordsCount == 1)
                {
                    Assert.IsFalse(_home.IsPreviousHistoryButtonDisabled());
                }
            }
            for (int keywordsCount = 1; keywordsCount <= 4; keywordsCount++)
            {
                _home.ClickNextKeywordHistoryButton();
                keywordFromTextBox = _home.KeywordFromTextBox();
                Assert.IsTrue(searchedKeywordsList[keywordsCount].SequenceEqual(keywordFromTextBox));
                if (keywordsCount == 4)
                {
                    Assert.IsFalse(_home.IsNextHistoryButtonDisabled());
                }
            }
        }

        [TestMethod]
        public void TC_OpenAccountTab_VerifyKeywordSuggestions()
        {
            //Arrange
            AccountDetails _accountDetails = new AccountDetails();
            XmlDocument accountDetailsXML = _home.GetAccountDetailsXMLFromDB();

            //Act
            _home.NavigateToAccountTab();
            _accountDetails = _home.GetAccountDetailsFromDB(accountDetailsXML);
            _accountDetails = _home.GetAccountDetailsFromUI();

            //Assert
            Assert.IsTrue(_accountDetails.UserNameFromDB.SequenceEqual(_accountDetails.UserNameFromUI));
            Assert.IsTrue(_accountDetails.NameFromDB.SequenceEqual(_accountDetails.NameFromUI));
            Assert.IsTrue(_accountDetails.EmailFromDB.SequenceEqual(_accountDetails.EmailFromUI));
            Assert.IsTrue(_accountDetails.ExpirationDateFromDB.SequenceEqual(_accountDetails.ExpirationDateFromUI));
        }
        [TestMethod]
        public void TC_CopyPhraseAndPasteInNotepad_VerifyPhraseFromNotepad()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);

            //Act
            WindowsElement randomPhraseWindowsElement = _home.GetRandomPhrasesWindowsElement(randomWordFromDB);
            string textFromNotePad = _home.CopyRandomPhraseToNotepad(randomPhraseWindowsElement);
            string textFromWindowsElement = _home.GetModifiedTextForAssertion(randomPhraseWindowsElement);

            //Assert
            Assert.IsTrue(textFromWindowsElement.SequenceEqual(textFromNotePad));
        }
        [TestMethod]
        public void TC_InsertPhraseIntoWordDocumnet_VerifyInsertedPhraseText()
        {
            //Arrange
            string randomWordFromDB = _home.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(randomWordFromDB);

            //Act
            WindowsElement randomPhraseWindowsElement = _home.GetRandomPhrasesWindowsElement(randomWordFromDB);
            string textFromWordDocument = _home.InsertRandomPhraseToWordDocument(randomPhraseWindowsElement);
            string textFromWindowsElement = _home.GetModifiedTextForAssertion(randomPhraseWindowsElement);

            //Assert
            Assert.IsTrue(textFromWindowsElement.Equals(textFromWordDocument));
        }
        [TestMethod]
        public void TC_SearchForKeywords_TestAllFeatures()
        {
            //Arrange
            string noResultsText = "We're sorry. No results are found at this time.";
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
                    keyword.Text = "like";
                    List<KeywordAssertionFailure<string>> assertionFailuresToWriteToCSV = new List<KeywordAssertionFailure<string>>();
                    assertionFailuresToWriteToCSV.Clear();
                    keywordLogCSV.Clear();
                    string startTime = DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss.fff");
                    try
                    {
                        _home.EnterkeywordAndSearch(keyword.Text);
                        if (_home.IsOutDatedKeywordAlertDisplayed())
                        {
                            string message = "Outdated Keyword";
                            KeywordAssertionFailure<string> failure = _home.KeywordSearchFailure(message, keyword.Text);
                            assertionFailures.Add(failure);
                            assertionFailuresToWriteToCSV.Add(failure);
                            keywordLogCSV = _home.WriteKeywordDataToCSVDocument(keyword.ID, keyword.Text, keywordLogCSV, startTime);
                            _home.WriteAsertionFailuresToDocument(assertionFailuresToWriteToCSV, errorLogCSV);
                        }
                        else if (_home.TextFromPhrasesContainer() == noResultsText)
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
                            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(keyword.Text);
                            List<AppiumWebElement> commonAppiumElements = _home.GetCommonAppiumElements();
                            List<string> meaningsListFromUI = _home.GetMeaningsForSelectedKeyword(commonAppiumElements);
                            List<string> meaningsListFromDatabase = _home.GetMeaningsListFromDB(keyword.Text);
                            int totalMeaningsCountFromDB = meaningsListFromDatabase.Count;
                            List<string> seeAlsoWordsFromDB = _home.GetSeeAlsoListFromDB(wordInfoXMLs[0]);
                            for (int seeAlsoCount = 1; seeAlsoCount <= seeAlsoWordsFromDB.Count; seeAlsoCount++)
                            {
                                string seeAlsoSelected = seeAlsoWordsFromDB[seeAlsoCount - 1];
                                List<WindowsElement> seeAlsoElements = _home.GetAllSeeAlsoElementsForSelectedKeyword();
                                _home.SelectRequiredSeeAlsoElement(seeAlsoCount,seeAlsoElements);
                                List<KeywordAssertionFailure<string>> synonymsAssertionFailures = _home.GetAssertionFailuresForCommonMethod(seeAlsoSelected, noResultsText);
                                foreach (KeywordAssertionFailure<string> failure in synonymsAssertionFailures)
                                {
                                    assertionFailures.Add(failure);
                                }
                                _home.ClickPreviousKeywordHistoryButton();
                            }
                            for (int meaningsCount = 1; meaningsCount <= totalMeaningsCountFromDB; meaningsCount++)
                            {
                                List<KeywordAssertionFailure<string>> meaningsAssertionFailures = _home.GetAssertionFailuresForEachMeaningsData(wordInfoXMLs, meaningsCount, keyword.Text);
                                foreach (KeywordAssertionFailure<string> failure in meaningsAssertionFailures)
                                {
                                    assertionFailures.Add(failure);
                                }
                                List<string> totalPhrasesFromDB = _home.TotalPhrasesFromDBForSelectedMeaning(wordInfoXMLs, meaningsCount);
                                List<WindowsElement> totalPhraseElements = _home.GetAllPhraseElementsForSelectedMeaning();
                                for (int phrasesCount = 1; phrasesCount <= totalPhrasesFromDB.Count; phrasesCount++)
                                {
                                    //Phrases Data Assertions
                                    List<KeywordAssertionFailure<string>> phrasesAssertionFailures = _home.GetAssertionFailuresForEachPhrasessData(wordInfoXMLs, phrasesCount, keyword.Text, meaningsCount, totalPhraseElements, commonAppiumElements);
                                    foreach (KeywordAssertionFailure<string> failure in phrasesAssertionFailures)
                                    {
                                        assertionFailures.Add(failure);
                                    }
                                }
                            }
                            //Assert
                            try
                            {
                                Assert.IsTrue(meaningsListFromUI.SequenceEqual(meaningsListFromDatabase));
                            }
                            catch (Exception ex)
                            {
                                string nameOfAssertion = "MeaningsList";
                                KeywordAssertionFailure<string> failure = _home.StringCollectionOfFailures(ex, meaningsListFromDatabase, meaningsListFromUI, keyword.Text, nameOfAssertion);
                                assertionFailures.Add(failure);
                            }
                            keywordLogCSV = _home.WriteKeywordDataToCSVDocument(keyword.ID, keyword.Text, assertionFailures, keywordLogCSV, startTime);
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
        public void TC_SearchMultipleKeywords_BasicData()
        {
            //Arrange
            string noResultsText = "We're sorry. No results are found at this time.";
            List<KeywordAssertionFailure<string>> assertionFailures = new List<KeywordAssertionFailure<string>>();
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
                        if (_home.IsOutDatedKeywordAlertDisplayed())
                        {
                            string message = "Outdated Keyword";
                            KeywordAssertionFailure<string> failure = _home.KeywordSearchFailure(message, keyword.Text);
                            assertionFailures.Add(failure);
                            assertionFailuresToWriteToCSV.Add(failure);
                            keywordLogCSV = _home.WriteKeywordDataToCSVDocument(keyword.ID, keyword.Text, keywordLogCSV, startTime);
                            _home.WriteAsertionFailuresToDocument(assertionFailuresToWriteToCSV, errorLogCSV);
                        }
                        else if (_home.TextFromPhrasesContainer() == noResultsText)
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
                            List<XmlDocument> wordInfoXMLs = _home.GetWordInfoXML(keyword.Text);
                            int phraseIDFromDB = _home.GetPhraseIDFromDB(wordInfoXMLs);
                            XmlDocument sourcesXML = _home.GetSourcesXML(phraseIDFromDB);
                            List<KeywordAssertionFailure<string>> keywordAssertionFailures = _home.GetAssertionFailuresForDefualtData(wordInfoXMLs, keyword.Text, sourcesXML);
                            foreach (KeywordAssertionFailure<string> failure in keywordAssertionFailures)
                            {
                                assertionFailures.Add(failure);
                                assertionFailuresToWriteToCSV.Add(failure);
                            }
                            keywordLogCSV = _home.WriteKeywordDataToCSVDocument(keyword.ID, keyword.Text, keywordAssertionFailures, keywordLogCSV, startTime);
                            _home.WriteAsertionFailuresToDocument(assertionFailuresToWriteToCSV, errorLogCSV);
                           }
                        if (i % testReInitializationCount == 0 && i != 0)
                        {
                            base.TestCleanup();
                            base.TestInitialize();
                            _home = new Home(_windowUIDriver);
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
        public void SendEmailOfTestStatus(List<KeywordAssertionFailure<string>> assertionFailures)
        {
            if(ConfigurationManager.AppSettings["TestStatusNotificationEmailAddresses"] != null)
            {
                string[] toAddresses = ConfigurationManager.AppSettings["TestStatusNotificationEmailAddresses"].Split(',');
                if(toAddresses[0] != "")
                {
                    string searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"].ToString();

                    //mail subject
                    string mailSubject = "UI Automation Test Run Status";
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

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'

            base.TestCleanup();
        }
    }
}
