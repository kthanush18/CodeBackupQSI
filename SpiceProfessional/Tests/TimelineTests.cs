using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.Tests
{
    [TestClass]
    public class TimelineTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _timeline = new Timeline(_windowUIDriver);
        }
        [TestMethod]
        public void TC_InsertTimelineImage_VerifySizeOfTheDocumentAfterInsertion()
        {
            //Arrange
            string randomWordFromDB = _timeline.GetRandomWord();

            //Act
            _timeline.SearchForKeywordAndOpenTimelineWindow(randomWordFromDB);
            int sizeOfAnEmptyDocumentInKB = _timeline.SaveAnEmptyDocumentAndGetSize();
            int sizeOfTimelineDocumentInKB = _timeline.SaveTimelineDocumentAfterInsertionAndGetSize();

            //Assert
            Assert.IsTrue(sizeOfTimelineDocumentInKB > sizeOfAnEmptyDocumentInKB);
        }
        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");
            _timeline.DeleteCreatedDocument();
            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            base.TestCleanup();
        }
    }
}
