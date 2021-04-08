using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
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
    public class SettingsTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _settings = new Settings(_windowUIDriver);
        }
        [TestMethod]
        public void TC_TurnOffFootnotingOptionFromSettings_VerifyInsertedTextInWordDocument()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.TurnFootnotingOFF();
            string textFromWordDocument = _settings.InsertPhraseToDocumentAndGetInsertedText(randomPhraseWindowsElement);
            string textFromWindowsElement = _settings.GetModifiedTextForAssertion(randomPhraseWindowsElement);

            //Assert
            Assert.IsTrue(textFromWordDocument.SequenceEqual(textFromWindowsElement));
        }
        [TestMethod]
        public void TC_SetLocationOfFootNoteToEndOfThePage_VerifyInsertedTextAtTheEndOfTheDocument()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.UncheckInTextCheckBoxAndCheckEndOfPageBox();
            XmlDocument WordXmlDocument = _settings.InsertPhraseToDocumentAndGetWordDocumentXml(randomPhraseWindowsElement);
            string textFromWordDocument = _settings.GetFootnoteFromXmlDocument(WordXmlDocument);
            string textFromWindowsElement = _settings.GetSourceTextForAssertionFromUI();

            //Assert
            Assert.IsTrue(textFromWordDocument.SequenceEqual(textFromWindowsElement));
        }
        [TestMethod]
        public void TC_SetFontStyleToItalic_VerifyInsertedPhraseTextIsItalicized()
        {
            //Arrange
            string fontStyleForAssertion = "Italic";
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.SelectItalicOptionButtonFromFontStyle();
            XmlDocument WordXmlDocument = _settings.InsertPhraseToDocumentAndGetWordDocumentXml(randomPhraseWindowsElement);
            string fontStyleFromXmlDocument = _settings.GetFontStyleFromXmlDocument(WordXmlDocument);

            //Assert
            Assert.IsTrue(fontStyleFromXmlDocument.SequenceEqual(fontStyleForAssertion));
        }
        [TestMethod]
        public void TC_SetFontStyleToBold_VerifyInsertedPhraseTextIsBold()
        {
            //Arrange
            string fontStyleForAssertion = "Bold";
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.SelectBoldOptionButtonFromFontStyle();
            XmlDocument WordXmlDocument = _settings.InsertPhraseToDocumentAndGetWordDocumentXml(randomPhraseWindowsElement);
            string fontStyleFromXmlDocument = _settings.GetFontStyleBoldFromXmlDocument(WordXmlDocument);

            //Assert
            Assert.IsTrue(fontStyleFromXmlDocument.SequenceEqual(fontStyleForAssertion));
        }
        [TestMethod]
        public void TC_SetFontStyleToNormal_VerifyInsertedPhraseTextIsNormal()
        {
            //Arrange
            string fontStyleForAssertion = "Normal";
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.SelectNormalOptionButtonFromFontStyle();
            XmlDocument WordXmlDocument = _settings.InsertPhraseToDocumentAndGetWordDocumentXml(randomPhraseWindowsElement);
            string fontStyleFromXmlDocument = _settings.GetFontStyleNormalFromXmlDocument(WordXmlDocument);

            //Assert
            Assert.IsTrue(fontStyleFromXmlDocument.SequenceEqual(fontStyleForAssertion));
        }
        [TestMethod]
        public void TC_SetFontSizeToSmall_VerifySizeOfThePhraseText()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement phraseWindowsElement = _settings.SearchForKeywordAndGetPhraseElement(randomWordFromDB);
            int fontSizeForNormal = _settings.GetFontSizeForPhraseElement(phraseWindowsElement);
            _settings.SelectSmallOptionButtonFromFontSize();
            int fontSizeForSmall = _settings.GetFontSizeForAppliedSettings();

            //Assert
            Assert.IsTrue(fontSizeForSmall < fontSizeForNormal);
        }
        [TestMethod]
        public void TC_SetFontSizeToLarge_VerifySizeOfThePhraseText()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement phraseWindowsElement = _settings.SearchForKeywordAndGetPhraseElement(randomWordFromDB);
            int fontSizeForNormal = _settings.GetFontSizeForPhraseElement(phraseWindowsElement);
            _settings.SelectLargeOptionButtonFromFontSize();
            int fontSizeForLarge = _settings.GetFontSizeForAppliedSettings();

            //Assert
            Assert.IsTrue(fontSizeForLarge > fontSizeForNormal);
        }
        [TestMethod]
        public void TC_ResetFontSizeToNormal_VerifyBothTheSizesOfThePhraseTextBeforeAndAfterReset()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement phraseWindowsElement = _settings.SearchForKeywordAndGetPhraseElement(randomWordFromDB);
            int fontSizeForNormalBeforeReset = _settings.GetFontSizeForPhraseElement(phraseWindowsElement);
            _settings.SelectSmallOptionButtonFromFontSize();
            _settings.ResetSettings();
            int fontSizeForNormalAfterReset = _settings.GetFontSizeForAppliedSettings(); 

            //Assert
            Assert.IsTrue(fontSizeForNormalBeforeReset.Equals(fontSizeForNormalAfterReset));
        }
        [TestMethod]
        public void TC_SetLocationOfFootNoteToBibliography_VerifyInsertedTextInTheLastLine()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.UncheckInTextCheckBoxAndCheckBibliography();
            _settings.InsertPhraseToDocument(randomPhraseWindowsElement);
            string textFromWordDocument = _settings.GetInsertedSourcetext();
            string textFromWindowsElement = _settings.GetSourceTextForAssertionFromUI();

            //Assert
            Assert.IsTrue(textFromWordDocument.SequenceEqual(textFromWindowsElement));
        }
        [TestMethod]
        public void TC_SetQuotationMarksToAll_VerifyInsertedPhraseText()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.SelectQuotationMarksAllOptionButton();
            _settings.InsertPhraseToDocument(randomPhraseWindowsElement);
            string textFromWordDocument = _settings.GetTextInsideTheQuotationMarks();
            string textFromWindowsElement = _settings.GetModifiedTextForAssertion(randomPhraseWindowsElement);

            //Assert
            Assert.IsTrue(textFromWordDocument.SequenceEqual(textFromWindowsElement));
        }
        [TestMethod]
        public void TC_SetQuotationMarksForMoreThanFiveWords_VerifyInsertedPhraseText()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            int numberOfWordsInPhrase = _settings.GetNumberOfWordsInPhrase(randomPhraseWindowsElement);
            string textFromWindowsElement = _settings.GetModifiedTextForAssertion(randomPhraseWindowsElement);
            _settings.SelectQuotationMarksForMoreThanFiveWordsOptionButton();
            _settings.InsertPhraseToDocument(randomPhraseWindowsElement);
            if (numberOfWordsInPhrase <= 5)
            {
                Assert.IsFalse(_settings.IsQuotationMarkspresent());
            }
            else
            {
                string textFromWordDocument = _settings.GetTextInsideTheQuotationMarks();
                Assert.IsTrue(_settings.IsQuotationMarkspresent());
                Assert.IsTrue(textFromWordDocument.SequenceEqual(textFromWindowsElement));
            }
        }
        [TestMethod]
        public void TC_SetQuotationMarksForMoreThanRandomNumberOfWords_VerifyInsertedPhraseText()
        {
            //Arrange
            //Minimum value is set to 4 as the minimum value acceptable in the +words textbox is 4
            int minimumValue = 4;
            int maximumValue = 10;
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            int numberOfWordsInPhrase = _settings.GetNumberOfWordsInPhrase(randomPhraseWindowsElement);
            string textFromWindowsElement = _settings.GetModifiedTextForAssertion(randomPhraseWindowsElement);
            int randomNumberOfWords = _settings.GetRandomNumberOfWords(minimumValue, maximumValue);
            _settings.SelectQuotationMarksForMoreThanRandomNumberOfWords(randomNumberOfWords);
            _settings.InsertPhraseToDocument(randomPhraseWindowsElement);
            if (numberOfWordsInPhrase <= randomNumberOfWords)
            {
                Assert.IsFalse(_settings.IsQuotationMarkspresent());
            }
            else
            {
                string textFromWordDocument = _settings.GetTextInsideTheQuotationMarks();
                Assert.IsTrue(_settings.IsQuotationMarkspresent());
                Assert.IsTrue(textFromWordDocument.SequenceEqual(textFromWindowsElement));
            }
        }
        [TestMethod]
        public void TC_SetQuotationMarksToNone_VerifyInsertedPhraseText()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.SelectQuotationMarksNone();
            _settings.InsertPhraseToDocument(randomPhraseWindowsElement);

            //Assert
            Assert.IsFalse(_settings.IsQuotationMarkspresent());
        }
        [TestMethod]
        public void TC_SetFootnoteStyleToAPA_VerifySourceFormatOnUI()
        {
            //Arrange
            List<string> sourcesListForSelectedMeaningFromWindows = new List<string>();
            List<string> sourcesListForSelectedMeaningFromDB = new List<string>();
            string randomWordFromDB = _settings.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _settings.GetWordInfoXML(randomWordFromDB);
            int phraseIDFromDB = _settings.GetPhraseIDFromDB(wordInfoXMLs);
            XmlDocument sourcesXML = _settings.GetSourcesXML(phraseIDFromDB);

            //Act
            sourcesListForSelectedMeaningFromDB = _settings.GetSourcesForSelectedPhraseIn_APA_Format_DB(sourcesXML);
            _settings.OpenSettingsAndSetFootnoteStyleTo_APA_Format();
            sourcesListForSelectedMeaningFromWindows = _settings.GetSourcesForSelectedPhrase_Windows(randomWordFromDB);

            //Assert
            Assert.IsTrue(sourcesListForSelectedMeaningFromWindows.SequenceEqual(sourcesListForSelectedMeaningFromDB));
        }
        [TestMethod]
        public void TC_SetFootnoteStyleToChicago_VerifySourceFormatOnUI()
        {
            //Arrange
            List<string> sourcesListForSelectedMeaningFromWindows = new List<string>();
            List<string> sourcesListForSelectedMeaningFromDB = new List<string>();
            string randomWordFromDB = _settings.GetRandomWord();
            List<XmlDocument> wordInfoXMLs = _settings.GetWordInfoXML(randomWordFromDB);
            int phraseIDFromDB = _settings.GetPhraseIDFromDB(wordInfoXMLs);
            XmlDocument sourcesXML = _settings.GetSourcesXML(phraseIDFromDB);

            //Act
            sourcesListForSelectedMeaningFromDB = _settings.GetSourcesForSelectedPhraseIn_Chicago_Format_DB(sourcesXML);
            _settings.OpenSettingsAndSetFootnoteStyleTo_Chicago_Format();
            sourcesListForSelectedMeaningFromWindows = _settings.GetSourcesForSelectedPhrase_Windows(randomWordFromDB);

            //Assert
            Assert.IsTrue(sourcesListForSelectedMeaningFromWindows.SequenceEqual(sourcesListForSelectedMeaningFromDB));
        }
        [TestMethod]
        public void TC_SetFootnoteStyleToMLA_VerifyInsertedTextInTheDocument()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.UncheckInTextCheckBoxAndCheckEndOfPage_Bibliography_Boxes();
            XmlDocument WordXmlDocument = _settings.InsertPhraseToDocumentAndGetWordDocumentXml(randomPhraseWindowsElement);
            FootnoteLocation footnoteAndBibliographytextFromWordDocument = _settings.GetFootnoteAndBibliographyFromXmlDocument(WordXmlDocument);
            string textFromWindowsElement = _settings.GetSourceTextForAssertionFromUI();

            //Assert
            Assert.IsTrue(footnoteAndBibliographytextFromWordDocument.EndOfPage.SequenceEqual(textFromWindowsElement));
            Assert.IsTrue(footnoteAndBibliographytextFromWordDocument.Bibliography.SequenceEqual(textFromWindowsElement));
        }
        [TestMethod]
        public void TC_SetFootnoteStyleToAPA_VerifyInsertedTextInTheDocument()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.SetFootnoteStyleTo_APA_Format();
            _settings.UncheckInTextCheckBoxAndCheckEndOfPage_Bibliography_Boxes();
            XmlDocument WordXmlDocument = _settings.InsertPhraseToDocumentAndGetWordDocumentXml(randomPhraseWindowsElement);
            FootnoteLocation footnoteAndBibliographytextFromWordDocument = _settings.GetFootnoteAndBibliographyFromXmlDocument(WordXmlDocument);
            string textFromWindowsElement = _settings.GetSourceTextForAssertionFromUI();

            //Assert
            Assert.IsTrue(footnoteAndBibliographytextFromWordDocument.EndOfPage.SequenceEqual(textFromWindowsElement));
            Assert.IsTrue(footnoteAndBibliographytextFromWordDocument.Bibliography.SequenceEqual(textFromWindowsElement));
        }
        [TestMethod]
        public void TC_SetFootnoteStyleToChicago_VerifyInsertedTextInTheDocument()
        {
            //Arrange
            string randomWordFromDB = _settings.GetRandomWord();

            //Act
            WindowsElement randomPhraseWindowsElement = _settings.GetRandomPhraseElementAndOpenSettingTab(randomWordFromDB);
            _settings.SetFootnoteStyleTo_Chicago_Format();
            _settings.UncheckInTextCheckBoxAndCheckEndOfPage_Bibliography_Boxes();
            XmlDocument WordXmlDocument = _settings.InsertPhraseToDocumentAndGetWordDocumentXml(randomPhraseWindowsElement);
            FootnoteLocation footnoteAndBibliographytextFromWordDocument = _settings.GetFootnoteAndBibliographyFromXmlDocument(WordXmlDocument);
            string textFromWindowsElement = _settings.GetSourceTextForAssertionFromUI();

            //Assert
            Assert.IsTrue(footnoteAndBibliographytextFromWordDocument.EndOfPage.SequenceEqual(textFromWindowsElement));
            Assert.IsTrue(footnoteAndBibliographytextFromWordDocument.Bibliography.SequenceEqual(textFromWindowsElement));
        }
        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");
            _settings.ResetSettings();
            _settings.DeleteSettingsDocument();
            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'
            base.TestCleanup();
        }
    }
}
