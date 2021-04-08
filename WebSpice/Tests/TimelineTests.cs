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
    public class TimelineTests : TestBase
    {
        protected static WebPage _page;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _timeline = new Timeline(_browser);
        }
        [TestMethod]
        public void TC_SearchKeyword_VerifyPhraseText()
        {
            //Arrange
            string randomWordFromDB = _timeline.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _timeline.GetWordInfoXML(randomWordFromDB);
            int randomNumberFromPhrasesCount = _timeline.RandomNumberFromPhrasesCount(wordInfoXMLs);
            string phraseFromDB = _timeline.GetRandomPhraseDB(randomNumberFromPhrasesCount, wordInfoXMLs);
            string PhraseFromUI = _timeline.GetRandomPhraseUI(randomNumberFromPhrasesCount, randomWordFromDB);

            //Act

            //Assert
            Assert.IsTrue(phraseFromDB.SequenceEqual(PhraseFromUI));
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            _timeline.CloseUsageGraphWindow();
        }
    }
}
