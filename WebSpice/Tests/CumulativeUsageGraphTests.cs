using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Common.Web;
using Quant.Spice.Test.UI.Web.WebSpice.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Quant.Spice.Test.UI.Web.WebSpice.Tests
{
    [TestClass]
    public class CumulativeUsageGraphTests : TestBase
    {
        protected static WebPage _page;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _cumulativeUsageGraph = new CumulativeUsageGraph(_browser);
        }
        [TestMethod]
        public void TC_SearchKeyword_VerifyPhraseTextAndUniqueUses()
        {
            //Arrange
            string randomWordFromDB = _cumulativeUsageGraph.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _cumulativeUsageGraph.GetWordInfoXML(randomWordFromDB);
            int randomNumberFromPhrasesCount = _cumulativeUsageGraph.RandomNumberFromPhrasesCount(wordInfoXMLs);
            string phraseFromDB = _cumulativeUsageGraph.GetRandomPhraseDB(randomNumberFromPhrasesCount, wordInfoXMLs);
            int frequencyOfUseFromDB = _cumulativeUsageGraph.GetFrequencyOfUseDB(wordInfoXMLs);
            string PhraseFromUI = _cumulativeUsageGraph.GetRandomPhraseUI(randomNumberFromPhrasesCount,randomWordFromDB);
            int frequencyOfUseFromUI = _cumulativeUsageGraph.GetFrequencyOfUseUI();

            //Act

            //Assert
            Assert.IsTrue(phraseFromDB.SequenceEqual(PhraseFromUI));
            Assert.IsTrue(frequencyOfUseFromDB.Equals(frequencyOfUseFromUI));
        }
        [TestMethod]
        public void TC_SearchKeyword_TestVerifiedAndUnverifiedSourceTexts() 
        {
            //Arrange
            string unverifiedSourceFromDB = "";
            string UnverifiedSourceFromUI = "";

            //Act
            string randomWordFromDB = _cumulativeUsageGraph.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _cumulativeUsageGraph.GetWordInfoXML(randomWordFromDB);
            int randomNumberFromPhrasesCount = _cumulativeUsageGraph.RandomNumberFromPhrasesCount(wordInfoXMLs);
            int phraseIDFromDB = _cumulativeUsageGraph.GetRandomPhraseIDFromDB(randomNumberFromPhrasesCount, wordInfoXMLs);
            XmlDocument cumulativeXML = _cumulativeUsageGraph.GetCumulativeUsageXML(phraseIDFromDB);
            List<string> totalSourcesFromDB = _cumulativeUsageGraph.GetTotalSourcesListFromDB(cumulativeXML);
            int randomNumberOfVerifiedSources = _cumulativeUsageGraph.GetRandomNumber(totalSourcesFromDB,true);
            int randomNumberOfUnverifiedSources = _cumulativeUsageGraph.GetRandomNumber(totalSourcesFromDB,false);
            string verifiedSourceFromDB = _cumulativeUsageGraph.GetVerifiedSourcesListFromDB(cumulativeXML, randomNumberOfVerifiedSources);
            if (totalSourcesFromDB.Count > 5)
            {
                unverifiedSourceFromDB = _cumulativeUsageGraph.GetUnverifiedSourcesListFromDB(cumulativeXML, randomNumberOfUnverifiedSources);
            }
            List<string> totalSourcesFromUI = _cumulativeUsageGraph.GetTotalSourcesListFromUI(randomWordFromDB, randomNumberFromPhrasesCount);
            string verifiedSourceFromUI = _cumulativeUsageGraph.GetVerifiedSourceFromUI( randomNumberOfVerifiedSources);
            if(totalSourcesFromUI.Count > 5)
            {
                UnverifiedSourceFromUI = _cumulativeUsageGraph.GetUnverifiedSourceFromUI(randomNumberOfUnverifiedSources);
            }

            //Assert
            Assert.IsTrue(totalSourcesFromDB.Count.Equals(totalSourcesFromUI.Count));
            Assert.IsTrue(verifiedSourceFromDB.SequenceEqual(verifiedSourceFromUI));
            Assert.IsTrue(unverifiedSourceFromDB.SequenceEqual(UnverifiedSourceFromUI));
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            _cumulativeUsageGraph.CloseUsageGraphWindow();
        }
    }
}
