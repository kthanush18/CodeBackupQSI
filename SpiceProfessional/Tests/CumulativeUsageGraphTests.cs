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
    public class CumulativeUsageGraphTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _cumulativeUsageGraph = new CumulativeUsageGraph(_windowUIDriver);
        }
        [TestMethod]
        public void TC_InsertCumulativeUsageGraphImage_VerifySizeOfTheDocumentAfterInsertion()
        {
            //Arrange
            string randomWordFromDB = _cumulativeUsageGraph.GetRandomWord();

            //Act
            _cumulativeUsageGraph.SearchForKeywordAndOpenCumulativeUsageGraphWindow(randomWordFromDB);
            int sizeOfAnEmptyDocumentInKB = _cumulativeUsageGraph.SaveAnEmptyDocumentAndGetSize();
            int sizeOfCumulativeUsageGraphDocumentInKB = _cumulativeUsageGraph.SaveCumulativeUsageGraphDocumentAfterInsertionAndGetSize();

            //Assert
            Assert.IsTrue(sizeOfCumulativeUsageGraphDocumentInKB > sizeOfAnEmptyDocumentInKB);
        }
        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");
            _cumulativeUsageGraph.DeleteCreatedDocument();
            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            base.TestCleanup();
        }
    }
}
