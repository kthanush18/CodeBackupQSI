using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp;
using Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.Tests
{
    [TestClass]
    public class SearchOptionsTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _searchOptions = new SearchOptions(_windowUIDriver);
            _searchOptions.OpenSearchOptionsWindow();
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
        public void TC_CreateFilterForPhrasesWordCount_VerifyCreatedFilter()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForWordsCount(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesBeforeYear(minimumValue, maximumValue);
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            string filterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

            //Assert
            Assert.IsTrue(filterNameFromCreateFilter.SequenceEqual(filterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_CreateFilterForEnglishOrtranslatedPhrasesOnly_VerifyCreatedFilter()
        {
            //Arrange

            //Act
            string filterForEnglishOrTranslatedPhrasesFromCreateFilter = _searchOptions.CreateFilterForEnglishOrTranslatedPhrases();
            string filterForEnglishOrTranslatedPhrasesFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

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
            List<int> minAndMaxRangeFromUseExisting = _searchOptions.GetRangeForCreatedFilter(filterNameFromCreateFilter);
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<int> minAndMaxRangeFromLocalXML = _searchOptions.GetRangeFromLocalXML(filterNameFromCreateFilter,phrasesXML);

            //Assert
            Assert.IsTrue(minAndMaxRangeFromUseExisting.SequenceEqual(minAndMaxRangeFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesWithUsesCountOrLess_VerifyValuesOfUsesCount()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            int uniqueUsesCountFromUseExisting = _searchOptions.GetUniqueUsesFromCreatedFilter(filterNameFromCreateFilter);
            bool isOrLessOptionButtonSelectedFromUseExisting = _searchOptions.IsOrLessOptionButtonSelectedFromUseExisting();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            int uniqueUsesCountFromLocalXML = _searchOptions.GetUniqueUsesFromLocalXML(filterNameFromCreateFilter,phrasesXML);
            bool isOrLessOptionButtonSelectedFromLocalXML = _searchOptions.IsOrLessOptionButtonSelectedFromLocalXML(filterNameFromCreateFilter,phrasesXML);

            //Assert
            Assert.IsTrue(uniqueUsesCountFromUseExisting.Equals(uniqueUsesCountFromLocalXML));
            Assert.IsTrue(isOrLessOptionButtonSelectedFromUseExisting.Equals(isOrLessOptionButtonSelectedFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesWithUsesCountApproximately_VerifyValuesOfUsesCount()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountApproximately(minimumValue, maximumValue);
            int uniqueUsesCountFromUseExisting = _searchOptions.GetUniqueUsesFromCreatedFilter(filterNameFromCreateFilter);
            bool isApproximatelyOptionButtonSelectedFromUseExisting = _searchOptions.IsApproximatelyOptionButtonSelectedFromUseExisting();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            int uniqueUsesCountFromLocalXML = _searchOptions.GetUniqueUsesFromLocalXML(filterNameFromCreateFilter, phrasesXML);
            bool isApproximatelyOptionButtonSelectedFromLocalXML = _searchOptions.IsApproximatelyOptionButtonSelectedFromLocalXML(filterNameFromCreateFilter, phrasesXML);

            //Assert
            Assert.IsTrue(uniqueUsesCountFromUseExisting.Equals(uniqueUsesCountFromLocalXML));
            Assert.IsTrue(isApproximatelyOptionButtonSelectedFromUseExisting.Equals(isApproximatelyOptionButtonSelectedFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesWithUsesCountOrMore_VerifyValuesOfUsesCount()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrMore(minimumValue, maximumValue);
            int uniqueUsesCountFromUseExisting = _searchOptions.GetUniqueUsesFromCreatedFilter(filterNameFromCreateFilter);
            bool isOrMoreOptionButtonSelectedFromUseExisting = _searchOptions.IsOrMoreOptionButtonSelectedFromUseExisting();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            int uniqueUsesCountFromLocalXML = _searchOptions.GetUniqueUsesFromLocalXML(filterNameFromCreateFilter, phrasesXML);
            bool isOrMoreOptionButtonSelectedFromLocalXML = _searchOptions.IsOrMoreOptionButtonSelectedFromLocalXML(filterNameFromCreateFilter, phrasesXML);

            //Assert
            Assert.IsTrue(uniqueUsesCountFromUseExisting.Equals(uniqueUsesCountFromLocalXML));
            Assert.IsTrue(isOrMoreOptionButtonSelectedFromUseExisting.Equals(isOrMoreOptionButtonSelectedFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesFromYear_VerifyYearValue()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYear(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter();
            int phrasesFromYear = _searchOptions.GetPhrasesFromYear();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            int phrasesFromYearFromLocalXML = _searchOptions.GetPhrasesFromYearFromLocalXML(filterNameFromCreateFilter, phrasesXML);

            //Assert
            Assert.IsTrue(phrasesFromYear.Equals(phrasesFromYearFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesFromYearRange_VerifyYearRangeValues()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesFromYearRange(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter();
            List<int> phrasesFromYearRange = _searchOptions.GetPhrasesYearRange();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<int> phrasesFromYearRangeFromLocalXML = _searchOptions.GetPhrasesFromYearRangeFromLocalXML(filterNameFromCreateFilter,phrasesXML);

            //Assert
            Assert.IsTrue(phrasesFromYearRange.SequenceEqual(phrasesFromYearRangeFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesBeforeYear_VerifyYearValue()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesBeforeYear(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter();
            int phrasesBeforeYear = _searchOptions.GetPhrasesBeforeYear();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            int phrasesBeforeYearFromLocalXML = _searchOptions.GetPhrasesBeforeYearFromLocalXML(filterNameFromCreateFilter,phrasesXML);

            //Assert
            Assert.IsTrue(phrasesBeforeYear.Equals(phrasesBeforeYearFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingPhrasesAfterYear_VerifyYearValue()
        {
            //Arrange
            int minimumValue = 1600;
            int maximumValue = 2015;

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForPhrasesAfterYear(minimumValue, maximumValue);
            _searchOptions.ApplyCreatedFilter();
            int phrasesAfterYear = _searchOptions.GetPhrasesAfterYear();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            int phrasesAfterYearFromLocalXML = _searchOptions.GetPhrasesAfterYearFromLocalXML(filterNameFromCreateFilter,phrasesXML);

            //Assert
            Assert.IsTrue(phrasesAfterYear.Equals(phrasesAfterYearFromLocalXML));
        }
        [TestMethod]
        public void TC_UseExistingEnglishOrTranslated_VerifySelectionOfOptionButton()
        {
            //Arrange

            //Act
            string filterNameFromCreateFilter = _searchOptions.CreateFilterForEnglishOrTranslatedPhrases();
            _searchOptions.ApplyCreatedFilter();
            string phrasesForEnglishOrTranslated = _searchOptions.GetPhrasesEnglishOrTranslated();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            string phrasesForEnglishOrTranslatedFromLocalXML = _searchOptions.GetPhrasesEnglishOrTranslatedFromLocalXML(filterNameFromCreateFilter,phrasesXML);

            //Assert
            Assert.IsTrue(phrasesForEnglishOrTranslated.SequenceEqual(phrasesForEnglishOrTranslatedFromLocalXML));
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                filteredPhrasesListFromDB = _searchOptions.GetWordsCountPhrasesList(wordInfoXMLs, count, filterNameFromCreateFilter, phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<UniqueUsesCount> uniqueUsesAndPhrasesText = _searchOptions.GetUniqueUsesAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetUniqueUsesOrLessPhrasesList(filterNameFromCreateFilter, uniqueUsesAndPhrasesText, phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<UniqueUsesCount> uniqueUsesAndPhrasesText = _searchOptions.GetUniqueUsesAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetUniqueUsesApproximatelyPhrasesList(filterNameFromCreateFilter, uniqueUsesAndPhrasesText, phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<UniqueUsesCount> uniqueUsesAndPhrasesText = _searchOptions.GetUniqueUsesAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetUniqueUsesOrMorePhrasesList(filterNameFromCreateFilter, uniqueUsesAndPhrasesText,phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromYearList(filterNameFromCreateFilter, phraseYearAndPhrasesList,phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromYearRangeList(filterNameFromCreateFilter, phraseYearAndPhrasesList,phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromBeforeYearList(filterNameFromCreateFilter, phraseYearAndPhrasesList,phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                List<PhraseYear> phraseYearAndPhrasesList = _searchOptions.GetPhraseYearAndPhrasesList(wordInfoXMLs, count);
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesFromAfterYearList(filterNameFromCreateFilter, phraseYearAndPhrasesList,phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
            _searchOptions.ApplyCreatedFilter();
            _searchOptions.SearchAppliedFilter();
            XmlDocument phrasesXML = _searchOptions.GetLocallyStoredPhrasesXML();
            List<string> meaningsListFromDB = _searchOptions.GetMeaningsListFromDB(randomWordFromDB);
            List<XmlDocument> wordInfoXMLs = _searchOptions.GetWordInfoXML(randomWordFromDB);
            for (int count = 1; count <= meaningsListFromDB.Count; count++)
            {
                filteredPhrasesListFromDB = _searchOptions.GetPhrasesListForEnglishOrTranslated(wordInfoXMLs, count, filterNameFromCreateFilter,phrasesXML);
                filteredPhrasesListFromUI = _searchOptions.GetPhrasesListFromUI(meaningsListFromDB, count);
                Assert.IsTrue(filteredPhrasesListFromUI.SequenceEqual(filteredPhrasesListFromDB));
            }
            string filterNameForAssertion = "Filter: " + filterNameFromCreateFilter;
            string textDisplayedOnFilterBanner = _searchOptions.GetFilterBannerText(filterNameForAssertion);
            _searchOptions.CloseBannerAndSelectFirstMeaning(meaningsListFromDB);
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
        public void TC_RenameCreatedFilter_VerifyRenamedFilterName()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string usesCountOrLessFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            string renameFilterAndGetFilterName = _searchOptions.RenameFilterNameFromManageFilters(usesCountOrLessFilterNameFromCreateFilter);
            string usesCountOrLessfilterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

            //Assert
            Assert.IsTrue(renameFilterAndGetFilterName.SequenceEqual(usesCountOrLessfilterNameFromManageFilter));
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
            string usesCountOrLessfilterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();
            string usesCountApproximatelyfilterNameFromManageFilter = _searchOptions.UsesCountApproximatelyFilterNameFromManageFilters();
            string usesCountOrMorefilterNameFromManageFilter = _searchOptions.UsesCountOrMoreFilterNameFromManageFilters();

            //Assert
            Assert.IsTrue(usesCountOrLessFilterNameFromCreateFilter.SequenceEqual(usesCountOrLessfilterNameFromManageFilter));
            Assert.IsTrue(usesCountApproximatelyFilterNameFromCreateFilter.SequenceEqual(usesCountApproximatelyfilterNameFromManageFilter));
            Assert.IsTrue(usesCountOrMoreFilterNameFromCreateFilter.SequenceEqual(usesCountOrMorefilterNameFromManageFilter));
        }
        [TestMethod]
        public void TC_DeleteOneFilterWhenMultipleFiltersArePresent_VerifyDeletedFilter()
        {
            //Arrange
            int minimumValue = 1;
            int maximumValue = 10;

            //Act
            string usesCountOrLessFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrLess(minimumValue, maximumValue);
            string usesCountApproximatelyFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountApproximately(minimumValue, maximumValue);
            string usesCountOrMoreFilterNameFromCreateFilter = _searchOptions.CreateFilterForUsesCountOrMore(minimumValue, maximumValue);
            _searchOptions.ClickDeleteFilterAndSelectNoAtConfirmation();
            string usesCountOrLessfilterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

            //Assert
            Assert.IsTrue(usesCountOrLessFilterNameFromCreateFilter.SequenceEqual(usesCountOrLessfilterNameFromManageFilter));

            //Act
            _searchOptions.DeleteOneFilter();
            string usesCountApproximatelyfilterNameFromManageFilter = _searchOptions.OpenManageFiltersAndGetFirstFilterName();

            //Assert
            Assert.IsTrue(usesCountApproximatelyFilterNameFromCreateFilter.SequenceEqual(usesCountApproximatelyfilterNameFromManageFilter));
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

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            _searchOptions.DeleteCreatedFilter();
            base.TestCleanup();
        }
    }
}
