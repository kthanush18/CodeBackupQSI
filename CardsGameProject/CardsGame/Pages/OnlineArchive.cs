using OpenQA.Selenium;
using Quant.CardsGame.UITests.Common;
using Quant.CardsGame.UITests.Common.Web;
using Quant.CardsGame.UITests.Web.CardsGame.DataAccess;
using Quant.CardsGame.UITests.Web.CardsGame.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace Quant.CardsGame.UITests.Web.CardsGame.Pages
{
    public class OnlineArchive : WebPage
    {
        readonly Random _random = new Random();
        private static readonly int _preLoaderWait = Int32.Parse(ConfigurationManager.AppSettings["PreloaderWaitTime"].ToString());
        protected static CardsGameDataAccess _dataAccess;

        public OnlineArchive(WebBrowser browser) : base(browser)
        {

        }
        public bool WaitForOnlineArchiveOptionButtonToLoad()
        {
            return _browser.WaitForElement("radio-online-archives", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetOnlineArchiveOptionButton()
        {
            return _browser.GetElement("radio-online-archives", WebBrowser.ElementSelectorType.ID);
        }
        public void SelectOnlineArchiveOptionButton()
        {
            WaitForOnlineArchiveOptionButtonToLoad();
            GetOnlineArchiveOptionButton().Click();
            WaitForPlayerPositionsToLoad();
        }
        public IWebElement GetTotalHandsElement()
        {
            return _browser.GetElement("hands-total-span", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRequiredPageNumberElement(int randomPageNumber)
        {
            return _browser.GetElement($"//ul [@id = 'online-archives-hands-pages']//a[@class = 'item_{randomPageNumber}']", WebBrowser.ElementSelectorType.XPath);
        }
        public bool WaitForOnlineArchivePreLoaderToLoad()
        {
            return _browser.WaitForElement("//div [@id = \"online-archive-results\"]//following-sibling::img [@style = \"display: none;\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public void SelectRandomPageNumber()
        {
            int totalHands = Int32.Parse(GetTotalHandsElement().Text);
            double totalPageCount = (double) totalHands / (double) 100;
            int roundedPageNumber = Convert.ToInt32(Math.Ceiling(totalPageCount));
            int randomPageNumber = _random.Next(1, roundedPageNumber);
            if(randomPageNumber != 1)
            {
                GetRequiredPageNumberElement(randomPageNumber).Click();
                WaitForOnlineArchivePreLoaderToLoad();
            }
        }
        public bool IsSearchButtonLoaded()
        {
            return _browser.IsElementVisible("search-button", WebBrowser.ElementSelectorType.ID);
        }
        public void CloseErrorAlertAndRefresh()
        {
            _browser.SwitchToAlertWindowAndAccept();
            _browser.Refresh();
        }
        public void SelectRequiredPageAndWaitForPageLoad(int pageNumber)
        {
            if (pageNumber != 1)
                GetRequiredPageNumberElement(pageNumber).Click();
            IsSearchButtonLoaded();
            WaitForOnlineArchivePreLoaderToLoad();
        }
        public void HandleServerException(Exception Ex,int pageNumber)
        {
            Console.WriteLine($"PageNumber-{pageNumber}");
            Console.WriteLine(Ex.Message);

            try
            {
                CloseErrorAlertAndRefresh();
                SelectOnlineArchiveOptionButton();
                WaitForPlayerPositionsToLoad();
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to handle server error");
            }
        }
        public bool IsFirstBoardDetailsLoaded()
        {
            return _browser.IsElementVisible("//div [@id = 'online-archive-results']//tbody/tr[3]", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetAllSegments()
        {
            return _browser.GetElements("segment-id", WebBrowser.ElementSelectorType.ID);
        }
        public int GetRandomSegmentID()
        {
            List<IWebElement> listOfSegmentsInCurrentPage = GetAllSegments();
            int randomSegment = _random.Next(1, listOfSegmentsInCurrentPage.Count);
            return Int32.Parse(listOfSegmentsInCurrentPage[randomSegment].GetAttribute("value"));
        }
        public IWebElement GetFirstBoardInTheSegment(int randomBoardNumber, int randomSegmentID)
        {
            return _browser.GetElement($"//input [@value = \"{randomSegmentID.ToString()}\"]//following::input[@id = \"board-id\"][{randomBoardNumber}]", WebBrowser.ElementSelectorType.XPath);
        }
        public int GetBoardIDForBoardNumberAndSegment(int randomBoardNumber, int randomSegmentID)
        {
            return Int32.Parse(GetFirstBoardInTheSegment(randomBoardNumber, randomSegmentID).GetAttribute("value"));
        }
        public bool WaitForPlayerPositionsToLoad()
        {
            return _browser.WaitForElement("//div[@class=\"players-sitting-positions\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetAllBoardsInParticularSegment(int randomSegmentID, int remainingIndexes)
        {
            return _browser.GetElements($"//input [@id = 'board-id'][preceding::input[@value = '{randomSegmentID}'] and following::input[@id = 'segment-id'][{remainingIndexes}]]", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetAllBoardsInLastSegment(int randomSegmentID)
        {
            return _browser.GetElements($"//input [@id = \"board-id\"][preceding::input[@value = \"{randomSegmentID}\"]]", WebBrowser.ElementSelectorType.XPath);
        }
        public int GetRandomBoardNumber(int randomSegmentID)
        {
            int boardsCount = 0;
            List<IWebElement> listOfSegmentsInCurrentPage = GetAllSegments();
            foreach(IWebElement segmentElement in listOfSegmentsInCurrentPage)
            {
                if (segmentElement.GetAttribute("value") == randomSegmentID.ToString())
                {
                    int selectedSegmentIndex = listOfSegmentsInCurrentPage.IndexOf(segmentElement);
                    int allSegmentsIndex = listOfSegmentsInCurrentPage.Count - 1;
                    int remainingIndexes = allSegmentsIndex - selectedSegmentIndex;
                    List<IWebElement> listOfBoardsInCurrentSegment = GetAllBoardsInParticularSegment(randomSegmentID,remainingIndexes);
                    if (listOfBoardsInCurrentSegment.Count != 0)
                    {
                        boardsCount = _random.Next(1, listOfBoardsInCurrentSegment.Count);
                    }
                    else
                    {
                        List<IWebElement> listOfBoardsInLastSegment = GetAllBoardsInLastSegment(randomSegmentID);
                        boardsCount = _random.Next(1, listOfBoardsInLastSegment.Count);
                    }
                }
            }
            return boardsCount;
        }
        public List<int> GetAllSegmentIDsInCurrentPage()
        {
            List<int> segmentIDs = new List<int>();
            List<IWebElement> listOfSegmentElementsInCurrentPage = GetAllSegments();
            foreach (IWebElement segmentElement in listOfSegmentElementsInCurrentPage)
            {
                segmentIDs.Add(Int32.Parse(segmentElement.GetAttribute("value")));
            }
            return segmentIDs;
        }
        public List<IWebElement> GetAllBoardsBetweenSegmnetIDs(int currentSegmentID, int nextSegmentID)
        {
            return _browser.GetElements($"//input [@id = 'board-id'][preceding::input[@value = '{currentSegmentID}'] and following::input[@value = '{nextSegmentID}']]", WebBrowser.ElementSelectorType.XPath);
        }
        public List<int> GetAllBoardIDsInCurrentSegment(int segment, List<int> segmentIDsInPage)
        {
            List<IWebElement> allBoardElements = new List<IWebElement>();
            List<int> boardIDs = new List<int>();

            if (segment != (segmentIDsInPage.Count - 1))
            {
                allBoardElements = GetAllBoardsBetweenSegmnetIDs(segmentIDsInPage[segment], segmentIDsInPage[segment + 1]);
            }
            else
            {
                allBoardElements = GetAllBoardsInLastSegment(segmentIDsInPage[segment]);
            }
            foreach (IWebElement boardElement in allBoardElements)
            {
                boardIDs.Add(Int32.Parse(boardElement.GetAttribute("value")));
            }
            return boardIDs;
        }
    }
}
