using OpenQA.Selenium;
using Quant.CardsGame.UITests.Common.Web;
using Quant.CardsGame.UITests.Web.CardsGame.Models;
using System;
using System.Collections.Generic;

namespace Quant.CardsGame.UITests.Web.CardsGame.Pages
{
    public class SavedHands : WebPage
    {
        readonly Random _random = new Random();

        public SavedHands(WebBrowser browser) : base(browser)
        {
            
        }
        public bool WaitForSavedHandsOptionButtonToLoad()
        {
            return _browser.WaitForElement("radio-saved-hands", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetSavedHandsOptionButton()
        {
            return _browser.GetElement("radio-saved-hands", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForPlayerPositionsToLoad()
        {
            return _browser.WaitForElement("//div[@class=\"players-sitting-positions\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public void SelectSavedHandsOptionButton()
        {
            WaitForSavedHandsOptionButtonToLoad();
            GetSavedHandsOptionButton().Click();
            WaitForPlayerPositionsToLoad();
        }
        public IWebElement GetTotalHandsElement()
        {
            return _browser.GetElement("hands-total-span", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement GetRequiredPageNumberElement(int randomPageNumber)
        {
            return _browser.GetElement($"//ul [@id = 'saved-hands-pages']//a[@class = 'item_{randomPageNumber}']", WebBrowser.ElementSelectorType.XPath);
        }
        public bool WaitForSavedHandsPreLoaderToLoad()
        {
            return _browser.WaitForElement("//div [@id = \"saved-hands-results\"]//following-sibling::img [@style = \"display: none;\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public void SelectRandomPageNumber()
        {
            int totalHands = Int32.Parse(GetTotalHandsElement().Text);
            double totalPageCount = (double)totalHands / (double)100;
            int roundedPageNumber = Convert.ToInt32(Math.Ceiling(totalPageCount));
            int randomPageNumber = _random.Next(1, roundedPageNumber);
            if (randomPageNumber != 1)
            {
                GetRequiredPageNumberElement(randomPageNumber).Click();
                WaitForSavedHandsPreLoaderToLoad();
            }
        }
        public bool IsFirstBoardDetailsLoaded()
        {
            return _browser.IsElementVisible("//div [@id = 'saved-hands-results']//tbody/tr[3]", WebBrowser.ElementSelectorType.XPath);
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
            WaitForSavedHandsPreLoaderToLoad();
        }
        public void HandleServerException(Exception Ex, int pageNumber)
        {
            Console.WriteLine($"PageNumber-{pageNumber}");
            Console.WriteLine(Ex.Message);

            try
            {
                CloseErrorAlertAndRefresh();
                SelectSavedHandsOptionButton();
                WaitForPlayerPositionsToLoad();
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to handle server error");
            }
        }
        public List<IWebElement> GetAllBoardIDsInCurrentPage()
        {
            return _browser.GetElements("//div [@class = 'saved-hands-table']//input [@id = 'board-id']", WebBrowser.ElementSelectorType.XPath);
        }
        public int GetRandomBoardIDFromTheSelectedPage()
        {
            int randomIndex = _random.Next(0, 99);
            List<IWebElement> BoardIDsListElements = GetAllBoardIDsInCurrentPage();
            return Int32.Parse(BoardIDsListElements[randomIndex].GetAttribute("value"));
        }
        public List<int> GetAllBoardIDsFromSelectedPage()
        {
            List<int> boardIDs = new List<int>();
            List<IWebElement> BoardIDsListElements = GetAllBoardIDsInCurrentPage();
            foreach(IWebElement element in BoardIDsListElements)
            {
                boardIDs.Add(Int32.Parse(element.GetAttribute("value")));
            }
            return boardIDs;
        }
        public IWebElement GetEventNameElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//preceding::td [@colspan = '18'][1]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetEventDateElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//preceding::td [@colspan = '18'][1]/b", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetEventName(int boardID)
        {
            return GetEventNameElement(boardID).Text.Split(new string[] {"      "}, StringSplitOptions.None)[0].Trim();
        }
        public string GetEventDate(int boardID)
        {
            return GetEventDateElement(boardID).Text;
        }
        public int GetRandomPageNumberFromSavedWebPages()
        {
            int totalHands = Int32.Parse(GetTotalHandsElement().Text);
            double totalPageCount = (double)totalHands / (double)100;
            int roundedPageNumber = Convert.ToInt32(Math.Ceiling(totalPageCount));
            return _random.Next(15, roundedPageNumber);
        }
    }
}
