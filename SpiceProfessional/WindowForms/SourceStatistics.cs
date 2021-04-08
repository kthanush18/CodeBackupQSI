using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Quant.Spice.Test.UI.Common.DataAccess.Production;
using Quant.Spice.Test.UI.Common.WindowsUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;
using SourceStatisticsModel = Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp.SourceStatistics;

namespace Quant.Spice.Test.UI.Windows.SpiceProfessional.WindowForms
{
    public class SourceStatistics : WindowForm
    {
        protected static SourceStatisticsDataAccess _sourceStatisticsDataAccess;
        readonly SourceStatisticsModel _sourceStatistics = new SourceStatisticsModel();
        readonly Random _random = new Random();

        public SourceStatistics(WindowUIDriver window) : base(window)
        {

        }
        public void WaitForHomeWindowToLoad()
        {
            _windowUIDriver.WaitForWindowsElement("picbxPhrasesTab", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<WindowsElement> SourceStatisticsElements()
        {
            return _windowUIDriver.GetElements("source statistics", WindowUIDriver.ElementSelectorType.Name);
        }
        public void OpenSourceStatisticsWindow()
        {
            WaitForHomeWindowToLoad();
            List<WindowsElement> sourceStatisticsElements = SourceStatisticsElements();
            WindowsElement SourceStatisticsLinkElement = sourceStatisticsElements.Last();
            SourceStatisticsLinkElement.Click();
            _windowUIDriver.SwitchToFirstWindow();
        }
        public XmlDocument GetSourceStatisticsXML()
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.GetSourceStatisticsXML();
        }
        public WindowsElement SearchButtonElement()
        {
            return _windowUIDriver.GetElement("picbxSearch", WindowUIDriver.ElementSelectorType.ID);
        }
        public void ClickSearchButton()
        {
            SearchButtonElement().Click();
        }
        public SourceStatisticsModel SourceStatisticsFromDB(XmlDocument sourcesXML)
        {
            foreach (XmlNode SourceStatisticsNode in sourcesXML.SelectNodes("//SOURCES_STATISTICS"))
            {
                _sourceStatistics.TotalWorksFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("ALL_SOURCES").InnerText.ToString());
                _sourceStatistics.TranslatedWorksFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("ALL_TRANS").InnerText.ToString());
                _sourceStatistics.EnglishSourceFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("OLDEST_SOURCE_YR").InnerText.ToString());
                _sourceStatistics.TranslatedSourceFromDB = Int32.Parse(SourceStatisticsNode.SelectSingleNode("OLDEST_TRANS_YR").InnerText.ToString());
            }
            return _sourceStatistics;
        }
        public WindowsElement GetSourceStatisticsCommonElement()
        {
            return _windowUIDriver.GetElement("frmSourceStatistics", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<AppiumWebElement> GetSourceStatisticsTextElements(WindowsElement SourceStatisticsCommonElement)
        {
            return _windowUIDriver.GetAppiumElements("Text", WindowUIDriver.ElementSelectorType.TagName, SourceStatisticsCommonElement);
        }
        public WindowsElement CloseButton()
        {
            return _windowUIDriver.GetElement("picbxCancel", WindowUIDriver.ElementSelectorType.ID);
        }
        public SourceStatisticsModel SourceStatisticsFromUI()
        {
            WindowsElement SourceStatisticsCommonElement = GetSourceStatisticsCommonElement();
            List<AppiumWebElement> SourceStatisticsTextElements = GetSourceStatisticsTextElements(SourceStatisticsCommonElement);
            _sourceStatistics.TotalWorksFromUI = Int32.Parse(SourceStatisticsTextElements[19].Text);
            _sourceStatistics.TranslatedWorksFromUI = Int32.Parse(SourceStatisticsTextElements[18].Text);
            _sourceStatistics.EnglishSourceFromUI = Int32.Parse(SourceStatisticsTextElements[14].Text);
            _sourceStatistics.TranslatedSourceFromUI = Int32.Parse(SourceStatisticsTextElements[13].Text);
            
            return _sourceStatistics;
        }
        public string ColumnName()
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            List<string> columnNames = _sourceStatisticsDataAccess.ColumnNames();
            Random _random = new Random();
            int index = _random.Next(columnNames.Count);
            string name = columnNames[index];
            return name;
        }
        public string AuthorName(string columnName)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.AuthorName(columnName);
        }
        public int GetSourcesCountByAuthorNameDB(string authorName)
        {
            int SourcesCount = 0;
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            SourcesCount = _sourceStatisticsDataAccess.SourceStatisticsCount(authorName);

            return SourcesCount;
        }
        public WindowsElement AuthorTextBoxElement()
        {
            return _windowUIDriver.GetElement("txtWorksBy", WindowUIDriver.ElementSelectorType.ID);
        }
        public int GetSourcesCountByAuthorNameUI(string authorName)
        {
            AuthorTextBoxElement().Click();
            AuthorTextBoxElement().SendKeys(authorName);
            ClickSearchButton();
            WindowsElement SourceStatisticsCommonElement = GetSourceStatisticsCommonElement();
            List<AppiumWebElement> SourceStatisticsTextElements = GetSourceStatisticsTextElements(SourceStatisticsCommonElement);
            return Int32.Parse(SourceStatisticsTextElements[16].Text);
        }
        public int RandomYear()
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.RandomYear();
        }
        public int PhrasesFromYearDB(int randomYear)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesFromYear(randomYear);
        }
        public WindowsElement PhrasesYearTextBoxElement()
        {
            return _windowUIDriver.GetElement("txtPhrasesFrom", WindowUIDriver.ElementSelectorType.ID);
        }
        public int PhrasesFromYearUI(int randomYear)
        {
            PhrasesYearTextBoxElement().Click();
            PhrasesYearTextBoxElement().SendKeys(randomYear.ToString());
            ClickSearchButton();
            WindowsElement SourceStatisticsCommonElement = GetSourceStatisticsCommonElement();
            List<AppiumWebElement> SourceStatisticsTextElements = GetSourceStatisticsTextElements(SourceStatisticsCommonElement);
            return Int32.Parse(SourceStatisticsTextElements[11].Text);
        }
        public int GenerateRandomNumber(int startingNumber, int endingNumber)
        {
            return _random.Next(startingNumber, endingNumber);
        }
        public int PhrasesWithUniqueUsesDB(int randomNumberOfUniqueUses)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesWithUniqueUses(randomNumberOfUniqueUses);
        }
        public int PhrasesWithWordsCountDB(int randomNumberOfWordsCount)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesWithWordsCount(randomNumberOfWordsCount);
        }
        public int PhrasesWithUniqueUsesAndWordsCountDB(int randomNumberOfUniqueUses, int randomNumberOfWordsCount)
        {
            _sourceStatisticsDataAccess = new SourceStatisticsDataAccess();
            return _sourceStatisticsDataAccess.PhrasesWithUniqueUsesAndWordsCount(randomNumberOfUniqueUses, randomNumberOfWordsCount);
        }
        public WindowsElement UniqueUsesTextBoxElement()
        {
            return _windowUIDriver.GetElement("txtUniqueUses", WindowUIDriver.ElementSelectorType.ID);
        }
        public WindowsElement WordsCountTextBoxElement()
        {
            return _windowUIDriver.GetElement("txtWordCount", WindowUIDriver.ElementSelectorType.ID);
        }
        public List<WindowsElement> GetAllTextBoxElements()
        {
            return _windowUIDriver.GetElements("SpiceWPFTextBox", WindowUIDriver.ElementSelectorType.ID);
        }
        public int PhrasesWithUniqueUsesUI(int randomNumberOfUniqueUses)
        {
            UniqueUsesTextBoxElement().Click();
            UniqueUsesTextBoxElement().SendKeys(randomNumberOfUniqueUses.ToString());
            ClickSearchButton();
            WindowsElement SourceStatisticsCommonElement = GetSourceStatisticsCommonElement();
            List<AppiumWebElement> SourceStatisticsTextElements = GetSourceStatisticsTextElements(SourceStatisticsCommonElement);
            return Int32.Parse(SourceStatisticsTextElements[8].Text);
        }
        public int PhrasesWithWordsCountUI(int randomNumberOfWordsCount)
        {
            List<WindowsElement> allTextBoxElements = GetAllTextBoxElements();
            foreach(AppiumWebElement textBox in allTextBoxElements)
            {
                textBox.Clear();
            }
            WordsCountTextBoxElement().Click();
            WordsCountTextBoxElement().SendKeys(randomNumberOfWordsCount.ToString());
            ClickSearchButton();
            WindowsElement SourceStatisticsCommonElement = GetSourceStatisticsCommonElement();
            List<AppiumWebElement> SourceStatisticsTextElements = GetSourceStatisticsTextElements(SourceStatisticsCommonElement);
            return Int32.Parse(SourceStatisticsTextElements[8].Text);
        }
        public int PhrasesWithUniqueUsesAndWordsCountUI(int randomNumberOfUniqueUses, int randomNumberOfWordsCount)
        {
            List<WindowsElement> allTextBoxElements = GetAllTextBoxElements();
            foreach (AppiumWebElement textBox in allTextBoxElements)
            {
                textBox.Clear();
            }
            UniqueUsesTextBoxElement().Click();
            UniqueUsesTextBoxElement().SendKeys(randomNumberOfUniqueUses.ToString());
            WordsCountTextBoxElement().Click();
            WordsCountTextBoxElement().SendKeys(randomNumberOfWordsCount.ToString());
            ClickSearchButton();
            WindowsElement SourceStatisticsCommonElement = GetSourceStatisticsCommonElement();
            List<AppiumWebElement> SourceStatisticsTextElements = GetSourceStatisticsTextElements(SourceStatisticsCommonElement);
            return Int32.Parse(SourceStatisticsTextElements[8].Text);
        }
        public void CloseCurrentlyOpenedWindow()
        {
            CloseButton().Click();
        }
    }
}
