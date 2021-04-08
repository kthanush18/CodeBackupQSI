using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WebSpice.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Quant.Spice.Test.UI.Web.WebSpice.Tests
{
    [TestClass]
    public class SearchOptionsTests : TestBase
    {
        protected static WebPage _page;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _searchOptions = new SearchOptions(_browser);
            _searchOptions.OpenSearchOptionsWindow();
        }
        [TestMethod]
        public void TC_SearchKeyword_VerifyKeywordTextinTextbox() 
        {
            //Arrange
            string randomWordFromDB = _searchOptions.GetRandomWord();

            //Act
            _searchOptions.EnterKeywordAndSearch(randomWordFromDB);
            string keywordFromTextbox = _searchOptions.KeywordFromTextBox();

            //Assert
            Assert.IsTrue(randomWordFromDB.SequenceEqual(keywordFromTextbox));
        }
        [TestMethod]
        public void TC_SearchContainingWords_VerifyPhrasesList() 
        {
            //Arrange
            List<string> containingWords = _searchOptions.ContainingWordsFromDB();
            List<string> phraseResultsFromDB = _searchOptions.PhraseResultsFromDB(containingWords);
            List<string> phraseResultsFromUI = _searchOptions.PhraseResultsFromUI(containingWords);

            //Act

            //Assert
            Assert.IsTrue(phraseResultsFromDB.SequenceEqual(phraseResultsFromUI));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesWordCount_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForWordsCount(minimumValue,maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesWithUsesCountOrLess_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesWithUsesCountApproximately_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 5;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountApproximately(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesWithUsesCountOrMore_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrMore(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesFromYear_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYear(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesForYearRange_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYearRange(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesBeforeYear_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesAfterYear(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForPhrasesAfterYear_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesAfterYear(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForEnglishOrtranslatedPhrasesOnly_VerifyCreatedFilter()
        {
            //Arrange

            //Act
            string filterForEnglishOrTranslatedPhrasesFromCreateFilter = _searchOptions.CreateFilterForEnglishOrTranslatedPhrases();
            string filterForEnglishOrTranslatedPhrasesFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(filterForEnglishOrTranslatedPhrasesFromCreateFilter.SequenceEqual(filterForEnglishOrTranslatedPhrasesFromManageFilter));
        }
        [TestMethod]
        public void TC_UseExistingFilterForPhrasesWordCount_VerifyValuesOfWordsCount()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForWordsCount(minimumValue, maximumValue);
            List <int> minAndMaxRangeFromUseExisting =  _searchOptions.GetRangeForCreatedFilter(filterNameFromCreateFilter);
            List<int> minAndMaxRangeFromCookie = _searchOptions.GetRangeFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(minAndMaxRangeFromUseExisting.SequenceEqual(minAndMaxRangeFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesWithUsesCountOrLess_VerifyValuesOfUsesCount()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;            

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            int uniqueUsesCountFromUseExisting = _searchOptions.GetUniqueUsesForCreatedFilter(filterNameFromCreateFilter);
            string comparisonForUsesFromUseExisting = _searchOptions.GetOpacityForUniqueUsesOrLess();
            int uniqueUsesCountFromCookie = _searchOptions.GetUniqueUsesCountFromCookie(filterNameFromCreateFilter);
            string comparisonForUsesFromCookie = _searchOptions.GetComparisonOfUsesFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(uniqueUsesCountFromUseExisting.Equals(uniqueUsesCountFromCookie));
            Assert.IsTrue(comparisonForUsesFromUseExisting.SequenceEqual(comparisonForUsesFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesWithUsesCountApproximately_VerifyValuesOfUsesCount()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountApproximately(minimumValue, maximumValue);
            int uniqueUsesCountFromUseExisting = _searchOptions.GetUniqueUsesForCreatedFilter(filterNameFromCreateFilter);
            string comparisonForUsesFromUseExisting = _searchOptions.GetOpacityForUniqueUsesApproximately();
            int uniqueUsesCountFromCookie = _searchOptions.GetUniqueUsesCountFromCookie(filterNameFromCreateFilter);
            string comparisonForUsesFromCookie = _searchOptions.GetComparisonOfUsesFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(uniqueUsesCountFromUseExisting.Equals(uniqueUsesCountFromCookie));
            Assert.IsTrue(comparisonForUsesFromUseExisting.SequenceEqual(comparisonForUsesFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesWithUsesCountOrMore_VerifyValuesOfUsesCount()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrMore(minimumValue, maximumValue);
            int uniqueUsesCountFromUseExisting = _searchOptions.GetUniqueUsesForCreatedFilter(filterNameFromCreateFilter);
            string comparisonForUsesFromUseExisting = _searchOptions.GetOpacityForUniqueUsesOrMore();
            int uniqueUsesCountFromCookie = _searchOptions.GetUniqueUsesCountFromCookie(filterNameFromCreateFilter);
            string comparisonForUsesFromCookie = _searchOptions.GetComparisonOfUsesFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(uniqueUsesCountFromUseExisting.Equals(uniqueUsesCountFromCookie));
            Assert.IsTrue(comparisonForUsesFromUseExisting.SequenceEqual(comparisonForUsesFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesFromYear_VerifyYearValue()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYear(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter(filterNameFromCreateFilter);
            int phrasesFromYear = _searchOptions.GetPhrasesFromYear();
            int phrasesFromYearFromCookie = _searchOptions.GetPhrasesFromYearFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(phrasesFromYear.Equals(phrasesFromYearFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesFromYearRange_VerifyYearRangeValues()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYearRange(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter(filterNameFromCreateFilter);
            List<int> phrasesFromYearRange = _searchOptions.GetPhrasesYearRange();
            List<int> phrasesFromYearRangeFromCookie = _searchOptions.GetPhrasesFromYearRangeFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(phrasesFromYearRange.SequenceEqual(phrasesFromYearRangeFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesBeforeYear_VerifyYearValue()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesBeforeYear(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter(filterNameFromCreateFilter);
            int phrasesBeforeYear = _searchOptions.GetPhrasesBeforeYear();
            int phrasesBeforeYearFromCookie = _searchOptions.GetPhrasesBeforeYearFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(phrasesBeforeYear.Equals(phrasesBeforeYearFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesAfterYear_VerifyYearValue()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesAfterYear(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter(filterNameFromCreateFilter);
            int phrasesAfterYear = _searchOptions.GetPhrasesAfterYear();
            int phrasesAfterYearFromCookie = _searchOptions.GetPhrasesAfterYearFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(phrasesAfterYear.Equals(phrasesAfterYearFromCookie));
        }
        [TestMethod]
        public void TC_UseExistingEnglishOrTranslated_VerifySelectionOfOptionButton() 
        {
            //Arrange

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForEnglishOrTranslatedPhrases();
            _searchOptions.ApplyCreatedFilter(filterNameFromCreateFilter);
            string phrasesForEnglishOrTranslated = _searchOptions.GetPhrasesEnglishOrTranslated();
            string phrasesForEnglishOrTranslatedFromCookie = _searchOptions.GetPhrasesEnglishOrTranslatedFromCookie(filterNameFromCreateFilter);

            //Assert
            Assert.IsTrue(phrasesForEnglishOrTranslated.SequenceEqual(phrasesForEnglishOrTranslatedFromCookie));
        }
        [TestMethod]
        public void TC_ApplyPhrasesWordCountFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 5;
            int maximumValue = 10;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForWordsCount(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count =1; count <= meaningsListFromDB.Count; count++)
            {
                filteredPhrasesListFromDB = _searchOptions.GetWordsCountPhrasesList(wordInfoXMLs,count,filterNameFromCreateFilter);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB,count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyPhrasesWithUniqueUsesOrLessFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<UniqueUsesCount> uniqueUsesAndPhrasesText = _searchOptions.GetUniqueUsesAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetUniqueUsesOrLessPhrasesList(filterNameFromCreateFilter, uniqueUsesAndPhrasesText);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyPhrasesWithUniqueUsesApproximatelyFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountApproximately(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<UniqueUsesCount> uniqueUsesAndPhrasesText = _searchOptions.GetUniqueUsesAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetUniqueUsesApproximatelyPhrasesList(filterNameFromCreateFilter, uniqueUsesAndPhrasesText);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyPhrasesWithUniqueUsesOrMoreFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrMore(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<UniqueUsesCount> uniqueUsesAndPhrasesText = _searchOptions.GetUniqueUsesAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetUniqueUsesOrMorePhrasesList(filterNameFromCreateFilter, uniqueUsesAndPhrasesText);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyPhrasesFromYearFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 1900;
            int maximumValue = 2015;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYear(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromYearList(filterNameFromCreateFilter, phraseYearAndPhrasesList);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyPhrasesFromYearRangeFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYearRange(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromYearRangeList(filterNameFromCreateFilter, phraseYearAndPhrasesList);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyPhrasesBeforeYearFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesBeforeYear(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromBeforeYearList(filterNameFromCreateFilter, phraseYearAndPhrasesList);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyPhrasesAfterYearFilter_VerifyPhrasesList()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesAfterYear(minimumValue, maximumValue);
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromAfterYearList(filterNameFromCreateFilter, phraseYearAndPhrasesList);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_ApplyEnglishOrTranslatedPhrasesFilter_VerifyPhrasesList()
        {
            //Arrange
            List<string> filteredPhrasesListFromDB = new List<string>();
            List<string> filteredPhrasesListFromUI = new List<string>();
            List<string> totalPhrasesListFromDB = new List<string>();
            List<string> totalPhrasesListFromUI = new List<string>();

            //Act
            string randomWordFromDB = _searchOptions.GetRandomWord();
            _searchOptions.SearchRandomKeywordAndOpenSearchOptions(randomWordFromDB);
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForEnglishOrTranslatedPhrases();
            _searchOptions.UseCreatedFilterAndSearch(filterNameFromCreateFilter);
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesListForEnglishOrTranslated(wordInfoXMLs, count, filterNameFromCreateFilter);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText();
            _searchOptions.CloseBannerAndSelectFirstMeaning();
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                totalPhrasesListFromDB = _searchOptions.GetPhrasesList(wordInfoXMLs, count);
                totalPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(totalPhrasesListFromDB.SequenceEqual(totalPhrasesListFromUI));
            }

            //Assert
            Assert.IsTrue(textDisplayedOnFilterBanner.SequenceEqual(filterNameForAssertion));
        }
        [TestMethod]
        public void TC_CreateFilters_VerifyCreatedFiltersListInManageFilters()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string usesCountOrLessFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            string usesCountApproximatelyFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountApproximately(minimumValue, maximumValue);
            string usesCountOrMoreFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrMore(minimumValue, maximumValue);
            string usesCountOrLessfilterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();
            string usesCountApproximatelyfilterNameFromManageFilter = _searchOptions.UsesCountApproximatelyFilterNameFromManageFilters();
            string usesCountOrMorefilterNameFromManageFilter = _searchOptions.UsesCountOrLessFilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(usesCountOrLessFilterNameFromCreateFilter.SequenceEqual(usesCountOrLessfilterNameFromManageFilter));
            Assert.IsTrue(usesCountApproximatelyFilterNameFromCreateFilter.SequenceEqual(usesCountApproximatelyfilterNameFromManageFilter));
            Assert.IsTrue(usesCountOrMoreFilterNameFromCreateFilter.SequenceEqual(usesCountOrMorefilterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_RenameCreatedFilter_VerifyRenamedFilterName()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string usesCountOrLessFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            string renameFilterAndGetFilterName = _searchOptions.RenameFilterNameFromManageFilters(usesCountOrLessFilterNameFromCreateFilter);
            string usesCountOrLessfilterNameFromManageFilter = _searchOptions.FilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(renameFilterAndGetFilterName.SequenceEqual(usesCountOrLessfilterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_DeleteFilterWhenOnlyOneFilterIsLeft_VerifyTextDisplayedInManageFilters()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;
            string noFiltersPresentText = "You have no filters at present";

            //Act
            string usesCountOrLessFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            _searchOptions.DeleteUsesCountOrLessFilter();
            string noFiltersPresentTextFromManageFilters = _searchOptions.GetNoFiltersMessageText();

            //Assert
            Assert.IsTrue(noFiltersPresentText.SequenceEqual(noFiltersPresentTextFromManageFilters));
        }
    }
}
