using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms;
using System.Xml;
using SourceStatisticsModel = Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp.SourceStatistics;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.Tests
{
    [TestClass]
    public class SourceStatisticsTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _sourceStatistics = new SourceStatistics(_windowUIDriver);
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
            _sources = _sourceStatistics.SourceStatisticsFromDB(sourcesXML);
            _sources = _sourceStatistics.SourceStatisticsFromUI();

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
            int randomNumberOfUniqueUses = _sourceStatistics.GenerateRandomNumber(1, 10);
            int phrasesWithUniqueUsesDB = _sourceStatistics.PhrasesWithUniqueUsesDB(randomNumberOfUniqueUses);
            int phrasesWithUniqueUsesUI = _sourceStatistics.PhrasesWithUniqueUsesUI(randomNumberOfUniqueUses);

            int randomNumberOfWordsCount = _sourceStatistics.GenerateRandomNumber(1, 10);
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
    }
}
