using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WebSpice.Pages;
using System.Xml;
using SourceStatisticsModel = Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp.SourceStatistics;

namespace Quant.Spice.Test.UI.Web.WebSpice.Tests
{
    [TestClass]
    public class SourceStatisticsTests : TestBase
    {
        protected static WebPage _page;         

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _sourceStatistics = new SourceStatistics(_browser);
            _sourceStatistics.OpenSourceStatisticsWindow();
        }
        [TestMethod]
        public void TC_SearchForDefualtInfo_VerifyAllValues() 
        {
            //Arrange
            SourceStatisticsModel _sources = new SourceStatisticsModel();
            XmlDocument sourcesXML = _sourceStatistics.GetSourceStatisticsXML();
            
            //Act
            _sourceStatistics.ClickSearchButton();
            _sources.TotalWorksFromDB = _sourceStatistics.TotalWorksDB(sourcesXML);
            _sources.TotalWorksFromUI = _sourceStatistics.TotalWorksUI();

            _sources.TranslatedWorksFromDB = _sourceStatistics.TranslatedWorksDB(sourcesXML);
            _sources.TranslatedWorksFromUI = _sourceStatistics.TranslatedWorksUI();

            _sources.EnglishSourceFromDB = _sourceStatistics.EnglishSourceDB(sourcesXML);
            _sources.EnglishSourceFromUI = _sourceStatistics.EnglishSourceUI();

            _sources.TranslatedSourceFromDB = _sourceStatistics.TranslatedSourceDB(sourcesXML);
            _sources.TranslatedSourceFromUI = _sourceStatistics.TranslatedSourceUI();

            //Assert
            Assert.IsTrue(_sources.TotalWorksFromUI.Equals(_sources.TotalWorksFromDB));
            Assert.IsTrue(_sources.TranslatedWorksFromUI.Equals(_sources.TranslatedWorksFromDB));
            Assert.IsTrue(_sources.EnglishSourceFromUI.Equals(_sources.EnglishSourceFromDB));
            Assert.IsTrue(_sources.TranslatedSourceFromUI.Equals(_sources.TranslatedSourceFromDB));
        }
        [TestMethod]
        public void TC_SearchAuthorName_VerifyAuthorStatistics() 
        {
            //Arrange
            string columnName = _sourceStatistics.ColumnName();
            string authorName = _sourceStatistics.AuthorName(columnName);
            int sourcesCountFromDB = _sourceStatistics.GetSourcesCountByAuthorNameDB(authorName);
            int sourcesCountFromUI = _sourceStatistics.GetSourcesCountByAuthorNameUI(authorName);

            //Act

            //Assert
            Assert.IsTrue(sourcesCountFromDB.Equals(sourcesCountFromUI));
        }
        [TestMethod]
        public void TC_SearchPhrasesFromYear_VerifyNumberOfPhrasesFromYear() 
        {
            //Arrange
            int randomYear = _sourceStatistics.RandomYear();
            int phrasesFromYearDB = _sourceStatistics.PhrasesFromYearDB(randomYear);
            int PhrasesFromYearUI = _sourceStatistics.PhrasesFromYearUI(randomYear);

            //Act

            //Assert
            Assert.IsTrue(phrasesFromYearDB.Equals(PhrasesFromYearUI));
        }
        [TestMethod]
        public void TC_SearchPhrasesWithUniqueUsesAndWords_VerifyNumberOfPhrasesForUniqueUsesAndWords() 
        {
            //Arrange
            int randomNumberOfUniqueUses = _sourceStatistics.GenerateRandomNumber(1,10);
            int phrasesWithUniqueUsesDB = _sourceStatistics.PhrasesWithUniqueUsesDB(randomNumberOfUniqueUses);
            int phrasesWithUniqueUsesUI = _sourceStatistics.PhrasesWithUniqueUsesUI(randomNumberOfUniqueUses);

            int randomNumberOfWordsCount = _sourceStatistics.GenerateRandomNumber(1,10);            
            int phrasesWithWordsCountDB = _sourceStatistics.PhrasesWithWordsCountDB(randomNumberOfWordsCount);
            int phrasesWithWordsCountUI = _sourceStatistics.PhrasesWithWordsCountUI(randomNumberOfWordsCount);

            int phrasesWithUniqueUsesAndWordsCountDB = _sourceStatistics.PhrasesWithUniqueUsesAndWordsCountDB(randomNumberOfUniqueUses, randomNumberOfWordsCount);            
            int phrasesWithUniqueUsesAndWordsCountUI = _sourceStatistics.PhrasesWithUniqueUsesAndWordsCountUI(randomNumberOfUniqueUses, randomNumberOfWordsCount);

            //Act

            //Assert
            Assert.IsTrue(phrasesWithUniqueUsesDB.Equals(phrasesWithUniqueUsesUI));
            Assert.IsTrue(phrasesWithWordsCountDB.Equals(phrasesWithWordsCountUI));
            Assert.IsTrue(phrasesWithUniqueUsesAndWordsCountDB.Equals(phrasesWithUniqueUsesAndWordsCountUI));
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            _sourceStatistics.CloseUsageGraphWindow();
        }
    }
}
