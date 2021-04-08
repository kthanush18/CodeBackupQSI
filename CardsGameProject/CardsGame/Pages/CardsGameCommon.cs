using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Quant.CardsGame.UITests.Common;
using Quant.CardsGame.UITests.Common.Web;
using Quant.CardsGame.UITests.Web.CardsGame.Models;
using Quant.CardsGame.UITests.Web.CardsGame.Tests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Quant.CardsGame.UITests.Web.CardsGame.Pages
{
    public class CardsGameCommon : WebPage
    {
        private static StringBuilder _logCSVBuilder;
        private static Reports _reports;
        private static int _assertionFailures;
        private readonly string _allDataLogFileDirectory = ConfigurationManager.AppSettings["AllDataLogCSV"].ToString();
        public CardsGameCommon (WebBrowser browser) : base(browser)
        {

        }

        readonly Random _random = new Random();
        public enum Directions
        {
            North, West, East, South
        }
        public bool WaitForPlayerPositionsToLoad()
        {
            return _browser.WaitForElement("//div[@class=\"players-sitting-positions\"]", WebBrowser.ElementSelectorType.XPath);
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
        public int GetAllPages()
        {
            int totalHands = Int32.Parse(GetTotalHandsElement().Text);
            double totalPageCount = (double)totalHands / (double)100;
            return Convert.ToInt32(Math.Ceiling(totalPageCount));
        }
        public IWebElement GetNorthPlayerElement(int boardID)
        {
            return _browser.GetElement($"//input[@name = \"{boardID}-north-player-hidden-field\"]//preceding-sibling::span", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetEastPlayerElement(int boardID)
        {
            return _browser.GetElement($"//input[@name = \"{boardID}-east-player-hidden-field\"]//preceding-sibling::span", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetSouthPlayerElement(int boardID)
        {
            return _browser.GetElement($"//input[@name = \"{boardID}-south-player-hidden-field\"]//preceding-sibling::span", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetWestPlayerElement(int boardID)
        {
            return _browser.GetElement($"//input[@name = \"{boardID}-west-player-hidden-field\"]//preceding-sibling::span", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetNorthPlayerName_UI(int boardID)
        {
            return GetNorthPlayerElement(boardID).Text.ToLower();
        }
        public string GetEastPlayerName_UI(int boardID)
        {
            return GetEastPlayerElement(boardID).Text.ToLower();
        }
        public string GetSouthPlayerName_UI(int boardID)
        {
            return GetSouthPlayerElement(boardID).Text.ToLower();
        }
        public string GetWestPlayerName_UI(int boardID)
        {
            return GetWestPlayerElement(boardID).Text.ToLower();
        }
        public IWebElement GetBoardNumberElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//ancestor::td", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetSegmentNameElement(int segmentID)
        {
            return _browser.GetElement($"//input [@value = '{segmentID}']//ancestor::td", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetDealerElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//following::td[1]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetVulnerabilityElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//following::td[4]", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetBoardNumber(int boardID)
        {
            return GetBoardNumberElement(boardID).Text;
        }
        public string GetSegmentName(int segmentID)
        {
            return GetSegmentNameElement(segmentID).Text;
        }
        public string GetDealerDirection(int boardID)
        {
            return GetDealerElement(boardID).Text;
        }
        public string GetVulnerabilityDirections(int boardID)
        {
            return GetVulnerabilityElement(boardID).Text;
        }
        public IWebElement GetContractElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = \"{boardID}\"]//following::span [@class = \"card-text-size-medium\"][1]", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetContract(int boardID)
        {
            return GetContractElement(boardID).Text;
        }
        public IWebElement GetTableScoreElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = \"{boardID}\"]//following::b[1]", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetTableScore(int boardID)
        {
            return GetTableScoreElement(boardID).Text;
        }
        public IWebElement GetIsDefendingElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = \"{boardID}\"]//following::input[@class = \"check-box\"][1]", WebBrowser.ElementSelectorType.XPath);
        }
        public bool GetIsDefending(int boardID)
        {
            if (GetIsDefendingElement(boardID).GetAttribute("checked") != null)
            {
                return bool.Parse(GetIsDefendingElement(boardID).GetAttribute("checked"));
            }
            else
            {
                return false;
            }
        }
        public IWebElement GetDeclarerElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = \"{boardID}\"]//following::td[7]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetOpeningHandElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = \"{boardID}\"]//following::td[8]", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetDeclarer(int boardID)
        {
            return GetDeclarerElement(boardID).Text.ToLower();
        }
        public string GetOpeningHand(int boardID)
        {
            return GetOpeningHandElement(boardID).Text.ToLower();
        }
        public IWebElement GetHandsAndBiddingViewElement(int boardID)
        {
            return _browser.GetElement($"//input [@value=\"{boardID}\"]//following::a[1]", WebBrowser.ElementSelectorType.XPath);
        }
        public bool WaitForHandsAndBiddingPopUpDisplay()
        {
            return _browser.WaitForElement("//div [@style = \"display: block;\"]", WebBrowser.ElementSelectorType.XPath);
        }
        public void OpenHandsAndBiddingPopUp(int boardID)
        {
            GetHandsAndBiddingViewElement(boardID).Click();
            WaitForHandsAndBiddingPopUpDisplay();
        }
        public IWebElement GetBoardNumberElement_HandsAndBidding()
        {
            return _browser.GetElement(".board-number", WebBrowser.ElementSelectorType.CssSelector);
        }
        public string GetBoardNumber_HandsAndBidding()
        {
            return GetBoardNumberElement_HandsAndBidding().Text;
        }
        public List<IWebElement> GetVulnerabilityElements_HandsAndBidding()
        {
            return _browser.GetElements("//td [contains (@class, \"sitting\")]", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetDealerDirection_HandsAndBidding()
        {
            List<IWebElement> vulnerabilityElements = GetVulnerabilityElements_HandsAndBidding();
            foreach (IWebElement element in vulnerabilityElements)
            {
                if (element.Text == "D")
                {
                    int dealerIndex = vulnerabilityElements.IndexOf(element);
                    var dealerDirection = (Directions)dealerIndex;
                    return dealerDirection.ToString();
                }
            }
            return null;
        }
        public string GetVulnerabilityDirections_HandsAndBidding()
        {
            List<string> vulnerableDirections = new List<string>();
            List<IWebElement> vulnerabilityElements = GetVulnerabilityElements_HandsAndBidding();
            foreach (IWebElement element in vulnerabilityElements)
            {
                if (element.GetCssValue("background-color").Contains("rgba(203, 0, 0, 1)"))
                {
                    int vulnerableIndex = vulnerabilityElements.IndexOf(element);
                    var vulnerableDirection = (Directions)vulnerableIndex;
                    vulnerableDirections.Add(vulnerableDirection.ToString());
                }
            }
            if (vulnerableDirections.Count != 0 && vulnerableDirections.Count != 4)
            {
                if (vulnerableDirections.Count == 1)
                {
                    return vulnerableDirections[0];
                }
                else
                {
                    return $"{vulnerableDirections[0]}_{vulnerableDirections[1]}";
                }
            }
            else if (vulnerableDirections.Count == 0)
            {
                return "None";
            }
            else
            {
                return "Both";
            }
        }
        public List<IWebElement> GetPlayerDirectionElements_HandsAndBidding()
        {
            return _browser.GetElements("//p[@class = 'player-name']//preceding::p[1]", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetPlayerNameElements_HandsAndBidding()
        {
            return _browser.GetElements(".player-name", WebBrowser.ElementSelectorType.CssSelector);
        }
        public Tuple<string, string> CombineDirectionAndPlayerName(string direction,string playerName)
        {
            return new Tuple<string, string>(direction, playerName);
        }
        public List<Tuple<string,string>> GetDirectionAndPlayerName()
        {
            List<Tuple<string, string>> playerDirectionsAndNames = new List<Tuple<string, string>>();
            List<IWebElement> playerDirectionElements = GetPlayerDirectionElements_HandsAndBidding();
            List<IWebElement> playerNameElements = GetPlayerNameElements_HandsAndBidding();
            for (int index=0; index < playerDirectionElements.Count; index++)
            {
                Tuple<string, string> playerDirectionAndName = CombineDirectionAndPlayerName(playerDirectionElements[index].Text, playerNameElements[index].Text.ToLower());
                playerDirectionsAndNames.Add(playerDirectionAndName);
            }
            return playerDirectionsAndNames;
        }
        public List<IWebElement> GetSuiteAndCardElements_HandsAndBidding(int boardID,string direction)
        {
            return _browser.GetElements($"//div [@id = '{boardID}-{direction}-hand-cards']//ancestor::li[@class = 'font-large']", WebBrowser.ElementSelectorType.XPath);
        }
        public Tuple<string, List<string>> CombineSuiteAndCards(IWebElement suiteAndCardsElement)
        {
            string suite = null;
            List<string> playerCards = new List<string>();
            string[] suiteAndCards = suiteAndCardsElement.Text.Split(' ');
            for (int index = 0; index < suiteAndCards.Length; index++)
            {
                if (index != 0)
                {
                    playerCards.Add(suiteAndCards[index]);
                }
                else
                {
                    suite = suiteAndCards[index];
                }
            }
            return new Tuple<string, List<string>>(suite, playerCards);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsForPlayerDirection(int boardID,string direction)
        {
            List<Tuple<string, List<string>>> playerSuitsAndCards = new List<Tuple<string, List<string>>>();
            List<IWebElement> playerSuiteElements = GetSuiteAndCardElements_HandsAndBidding(boardID, direction);
            for (int index = 0; index < playerSuiteElements.Count; index++)
            {
                Tuple<string, List<string>> playerSuitAndCards = CombineSuiteAndCards(playerSuiteElements[index]);
                playerSuitsAndCards.Add(playerSuitAndCards);
            }
            return playerSuitsAndCards;
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInNorthPlayerHand(int boardID)
        {
            string direction = Directions.North.ToString().ToLower();
            return GetSuitsAndCardsForPlayerDirection(boardID,direction);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInWestPlayerHand(int boardID)
        {
            string direction = Directions.West.ToString().ToLower();
            return GetSuitsAndCardsForPlayerDirection(boardID, direction);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInEastPlayerHand(int boardID)
        {
            string direction = Directions.East.ToString().ToLower();
            return GetSuitsAndCardsForPlayerDirection(boardID, direction);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInSouthPlayerHand(int boardID)
        {
            string direction = Directions.South.ToString().ToLower();
            return GetSuitsAndCardsForPlayerDirection(boardID, direction);
        }
        public IWebElement GetCloseButton_HandsAndBidding()
        {
            return _browser.GetElement($"//div [@id = 'modal-hands-bidding']//following-sibling::button [text()='Close']", WebBrowser.ElementSelectorType.XPath);
        }
        public void CloseHandsAndBiddingPopUp()
        {
            GetCloseButton_HandsAndBidding().Click();
            _browser.SwitchtoPreviousWindow();
        }
        public List<IWebElement> GetHandInfoElements_HandsAndBidding()
        {
            return _browser.GetElements($"//li [contains(@class, 'hand-info')]", WebBrowser.ElementSelectorType.XPath);
        }
        public string SelectAndGetRandomPlayerDirection()
        {
            List<IWebElement> playerHandInfoElements = GetHandInfoElements_HandsAndBidding();
            List<IWebElement> playerDirectionElements = GetPlayerDirectionElements_HandsAndBidding();
            int randomIndex = _random.Next(1, playerHandInfoElements.Count-1);
            playerHandInfoElements[randomIndex].Click();
            return playerDirectionElements[randomIndex].Text;
        }
        public List<IWebElement> GetPlayerHandInfoTextElements_HandsAndBidding()
        {
            return _browser.GetElements($"//div[@class = 'hand-details']//following-sibling::td[not(div)]", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetPlayerHandInfoCheckBoxElements_HandsAndBidding()
        {
            return _browser.GetElements($"//div[@class = 'hand-details']//following-sibling::td/div/input", WebBrowser.ElementSelectorType.XPath);
        }
        public PlayerHandSummary GetPlayerHandDetailsSummary()
        {
            PlayerHandSummary handDetails = new PlayerHandSummary();
            List<IWebElement> playerHandInfoTextElements = GetPlayerHandInfoTextElements_HandsAndBidding();
            List<IWebElement> playerHandInfoCheckBoxElements = GetPlayerHandInfoCheckBoxElements_HandsAndBidding();
            List<bool> checkBoxConditions = new List<bool>();

            foreach(IWebElement checkBoxElement in playerHandInfoCheckBoxElements)
            {
                if (checkBoxElement.GetAttribute("checked") != null)
                {
                    checkBoxConditions.Add(bool.Parse(checkBoxElement.GetAttribute("checked")));
                }
                else
                {
                    checkBoxConditions.Add(false);
                }
            }

            handDetails.PlayerName = playerHandInfoTextElements[0].Text.ToLower();
            handDetails.Spades = playerHandInfoTextElements[1].Text;
            handDetails.Hearts = playerHandInfoTextElements[2].Text;
            handDetails.Diamonds = playerHandInfoTextElements[3].Text;
            handDetails.Clubs = playerHandInfoTextElements[4].Text;
            handDetails.HighCardPoints = playerHandInfoTextElements[5].Text;
            handDetails.HasVoid = checkBoxConditions[0];
            handDetails.HasSingleton = checkBoxConditions[1];
            handDetails.HasMultipleSingletons = checkBoxConditions[2];
            handDetails.HasDoubleton = checkBoxConditions[3];
            return handDetails;
        }
        public List<IWebElement> GetDirectionsBiddingSequenceElements()
        {
            return _browser.GetElements("//div [@class = 'bidding-sequence']//ancestor::*/tr/th", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetAllRowsBiddingSequenceElements()
        {
            return _browser.GetElements("//div [@class = 'bidding-sequence']//ancestor::*/tr[not(th)]", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetBiddingSequenceForRowElements(int rowCount)
        {
            return _browser.GetElements($"//div [@class = 'bidding-sequence']//ancestor::*/tr[not(th)][{rowCount}]/td", WebBrowser.ElementSelectorType.XPath);
        }
        public List<List<string>> GetAllPlayersBidding(List<IWebElement> allRowElements)
        {
            List<List<string>> allPlayersBidding = new List<List<string>>();
            List<string> westPlayerBidding = new List<string>();
            List<string> northPlayerBidding = new List<string>();
            List<string> eastPlayerBidding = new List<string>();
            List<string> southPlayerBidding = new List<string>();

            for (int rowCount = 1; rowCount <= allRowElements.Count; rowCount++)
            {
                List<IWebElement> biddingSequenceElements = GetBiddingSequenceForRowElements(rowCount);
                westPlayerBidding.Add(biddingSequenceElements[0].Text);
                northPlayerBidding.Add(biddingSequenceElements[1].Text);
                eastPlayerBidding.Add(biddingSequenceElements[2].Text);
                southPlayerBidding.Add(biddingSequenceElements[3].Text);
            }

            allPlayersBidding.Add(westPlayerBidding.Where(x => x.ToString() != "").ToList());
            allPlayersBidding.Add(northPlayerBidding.Where(x => x.ToString() != "").ToList());
            allPlayersBidding.Add(eastPlayerBidding.Where(x => x.ToString() != "").ToList());
            allPlayersBidding.Add(southPlayerBidding.Where(x => x.ToString() != "").ToList());
            return allPlayersBidding;
        }
        public BiddingSequence GetBiddingSequence()
        {
            BiddingSequence playersBidding = new BiddingSequence();
            List<IWebElement> directionElements = GetDirectionsBiddingSequenceElements();
            List<IWebElement> allRowElements = GetAllRowsBiddingSequenceElements();
            List<List<string>> allPlayersBidding = GetAllPlayersBidding(allRowElements);

            playersBidding.WestHandBidding = new Tuple<string, List<string>>(directionElements[0].Text, allPlayersBidding[0]);
            playersBidding.NorthHandBidding = new Tuple<string, List<string>>(directionElements[1].Text, allPlayersBidding[1]);
            playersBidding.EastHandBidding = new Tuple<string, List<string>>(directionElements[2].Text, allPlayersBidding[2]);
            playersBidding.SouthHandBidding = new Tuple<string, List<string>>(directionElements[3].Text, allPlayersBidding[3]);

            return playersBidding;
        }
        public List<IWebElement> GetAllBiddingSummaryElements()
        {
            return _browser.GetElements("//div[@class = 'bidding-summary']//following-sibling::td", WebBrowser.ElementSelectorType.XPath);
        }
        public BiddingSummary GetBiddingSummary()
        {
            BiddingSummary biddingSummary = new BiddingSummary();
            List<IWebElement> biddingSummaryElements = GetAllBiddingSummaryElements();
            biddingSummary.NoOfPasses = biddingSummaryElements[0].Text;
            biddingSummary.OpeningBid = biddingSummaryElements[1].Text;
            biddingSummary.OpeningBidResponse = biddingSummaryElements[2].Text;
            biddingSummary.Overcall = biddingSummaryElements[3].Text;
            biddingSummary.LevelOfOvercall = biddingSummaryElements[4].Text;
            biddingSummary.OvercallAt = biddingSummaryElements[5].Text;
            biddingSummary.OvercallResponse = biddingSummaryElements[6].Text;
            biddingSummary.Contract = biddingSummaryElements[7].Text;
            return biddingSummary;
        }
        public PlayerNames GetAllPlayerNames(int boardID)
        {
            PlayerNames playerNames = new PlayerNames
            {
                North = GetNorthPlayerName_UI(boardID),
                East = GetEastPlayerName_UI(boardID),
                South = GetSouthPlayerName_UI(boardID),
                West = GetWestPlayerName_UI(boardID)
            };

            return playerNames;
        }
        public IWebElement GetScoreElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//following::td[3]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetTypeElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//following::td[2]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetOpeningLeadCardElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//following::td[12]/span/span", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetScore(int boardID)
        {
            return GetScoreElement(boardID).Text;
        }
        public string GetScoreType(int boardID)
        {
            return GetTypeElement(boardID).Text;
        }
        public string GetOpeningLeadCard(int boardID)
        {
            return GetOpeningLeadCardElement(boardID).Text;
        }

        public void WriteToAllDataLogFile()
        {
            File.AppendAllText(_allDataLogFileDirectory, _logCSVBuilder.ToString());
            _logCSVBuilder.Clear();
        }
        public void CreateAllDataLogFileAndAppendTitles()
        {
            _logCSVBuilder = new StringBuilder();
            _logCSVBuilder.AppendLine("Page_Number,Segment_Name,Board_Number,Result");
            WriteToAllDataLogFile();
        }
        public void CloseErrorAlertAndRefresh()
        {
            _browser.SwitchToAlertWindowAndAccept();
            _browser.Refresh();
        }
        public void LogAndHandleException(Exception Ex,int pageNumber)
        {
            _assertionFailures = _assertionFailures + 1;
            _logCSVBuilder.AppendLine($"{pageNumber},,,Fail");
            WriteToAllDataLogFile();
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
        public void DeleteExistingLogs()
        {
            try
            {
                File.Delete(_allDataLogFileDirectory);
            }
            catch (Exception)
            {
                Console.WriteLine("Log files not found");
            }
        }
        public void SwitchToCardsGameWebsite()
        {
            _browser.SwitchToFirstTab();
        }
        public void SwitchToHandViewerWebsite()
        {
            _browser.SwitchToLastTab();
        }
        public BoardDetails GetBoardDetails(int boardID)
        {
            _browser.MoveToWebElement(GetBoardNumberElement(boardID));
            BoardDetails boardDetails = new BoardDetails
            {
                BoardNumber = Int32.Parse(GetBoardNumber(boardID)),
                Dealer = GetDealerDirection(boardID),
                Type = GetScoreType(boardID),
                Score = GetScore(boardID),
                Vulnerability = GetVulnerabilityDirections(boardID),
                AllPlayers = GetAllPlayerNames(boardID),
                IsDefending = GetIsDefending(boardID),
                Declarer = GetDeclarer(boardID),
                OpeningHand = GetOpeningHand(boardID),
                TableScore = GetTableScore(boardID),
                Contract = GetContract(boardID),
                OpeningLeadCard = GetOpeningLeadCard(boardID),
                OpeningLeadInfo = GetOpeningLeadInfo(boardID)
            };

            return boardDetails;
        }
        public BoardDetails GetBoardDetails_ExceptTableScores(int boardID)
        {
            BoardDetails boardDetails = new BoardDetails
            {
                BoardNumber = Int32.Parse(GetBoardNumber(boardID)),
                Dealer = GetDealerDirection(boardID),
                Vulnerability = GetVulnerabilityDirections(boardID),
                AllPlayers = GetAllPlayerNames(boardID),
                IsDefending = GetIsDefending(boardID),
                Declarer = GetDeclarer(boardID),
                OpeningHand = GetOpeningHand(boardID),
                Contract = GetContract(boardID),
                OpeningLeadCard = GetOpeningLeadCard(boardID)
            };

            return boardDetails;
        }
        public int GetAssertionFailuresCount()
        {
            return _assertionFailures;
        }
        public void IntializeHTMLReporter()
        {
            _reports = new Reports(_browser); 
        }
        public void PublishHTMLReport()
        {
            _reports.PublishExtentReports();
        }
        public void AssertAndLogBoardDetails(BoardDetails boardDetails_OnlineArchive, BoardDetails boardDetails_HandViewer, int boardID, string eventName, int pageNumber, [Optional] string segmentName)
        {
            int failures = 0;
            string boardNumber = boardDetails_OnlineArchive.BoardNumber.ToString();
            string testName = $"BoardID-{boardID}";
            _reports.CreateTestInExtentReport(testName);
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.BoardNumber.Equals(boardDetails_HandViewer.BoardNumber));
                _reports.LogTestInfo("Board Number");
            }
            catch(Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Board Number", Ex, boardDetails_OnlineArchive.BoardNumber.ToString(), boardDetails_HandViewer.BoardNumber.ToString());
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.Dealer.SequenceEqual(boardDetails_HandViewer.Dealer));
                _reports.LogTestInfo("Dealer");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Dealer", Ex, boardDetails_OnlineArchive.Dealer, boardDetails_HandViewer.Dealer);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.Type.SequenceEqual(boardDetails_HandViewer.Type));
                _reports.LogTestInfo("Type");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Type", Ex, boardDetails_OnlineArchive.Type, boardDetails_HandViewer.Type);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.Score.SequenceEqual(boardDetails_HandViewer.Score));
                _reports.LogTestInfo("Score");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Score", Ex, boardDetails_OnlineArchive.Score, boardDetails_HandViewer.Score);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.Vulnerability.SequenceEqual(boardDetails_HandViewer.Vulnerability));
                _reports.LogTestInfo("Vulnerability");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Vulnerability", Ex, boardDetails_OnlineArchive.Vulnerability, boardDetails_HandViewer.Vulnerability);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.AllPlayers.North.SequenceEqual(boardDetails_HandViewer.AllPlayers.North));
                _reports.LogTestInfo("North Player Name");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("North Player Name", Ex, boardDetails_OnlineArchive.AllPlayers.North, boardDetails_HandViewer.AllPlayers.North);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.AllPlayers.East.SequenceEqual(boardDetails_HandViewer.AllPlayers.East));
                _reports.LogTestInfo("East Player Name");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("East Player Name", Ex, boardDetails_OnlineArchive.AllPlayers.East, boardDetails_HandViewer.AllPlayers.East);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.AllPlayers.South.SequenceEqual(boardDetails_HandViewer.AllPlayers.South));
                _reports.LogTestInfo("South Player Name");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("South Player Name", Ex, boardDetails_OnlineArchive.AllPlayers.South, boardDetails_HandViewer.AllPlayers.South);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.AllPlayers.West.SequenceEqual(boardDetails_HandViewer.AllPlayers.West));
                _reports.LogTestInfo("West Player Name");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("West Player Name", Ex, boardDetails_OnlineArchive.AllPlayers.West, boardDetails_HandViewer.AllPlayers.West);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.IsDefending.Equals(boardDetails_HandViewer.IsDefending));
                _reports.LogTestInfo("Is Defending");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Is Defending", Ex, boardDetails_OnlineArchive.IsDefending.ToString(), boardDetails_HandViewer.IsDefending.ToString());
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.Declarer.SequenceEqual(boardDetails_HandViewer.Declarer));
                _reports.LogTestInfo("Declarer");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Declarer", Ex, boardDetails_OnlineArchive.Declarer, boardDetails_HandViewer.Declarer);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.OpeningHand.SequenceEqual(boardDetails_HandViewer.OpeningHand));
                _reports.LogTestInfo("Opening Hand");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Opening Hand", Ex, boardDetails_OnlineArchive.OpeningHand, boardDetails_HandViewer.OpeningHand);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.TableScore.SequenceEqual(boardDetails_HandViewer.TableScore));
                _reports.LogTestInfo("Table Score");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Table Score", Ex, boardDetails_OnlineArchive.TableScore, boardDetails_HandViewer.TableScore);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.Contract.SequenceEqual(boardDetails_HandViewer.Contract));
                _reports.LogTestInfo("Contract");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Contract", Ex, boardDetails_OnlineArchive.Contract, boardDetails_HandViewer.Contract);
            }
            try
            {
                Assert.IsTrue(boardDetails_OnlineArchive.OpeningLeadCard.SequenceEqual(boardDetails_HandViewer.OpeningLeadCard));
                _reports.LogTestInfo("Opening Lead");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Opening Lead", Ex, boardDetails_OnlineArchive.OpeningLeadCard, boardDetails_HandViewer.OpeningLeadCard);
            }
            for(int index = 0; index < boardDetails_OnlineArchive.OpeningLeadInfo.Count; index++)
            {
                try
                {
                    Assert.IsTrue(boardDetails_OnlineArchive.OpeningLeadInfo[index].SequenceEqual(boardDetails_HandViewer.OpeningLeadInfo[index]));
                    _reports.LogTestInfo("Opening Lead Info");
                }
                catch (Exception Ex)
                {
                    failures = failures + 1;
                    _reports.LogTestInfo("Opening Lead Info", Ex, boardDetails_OnlineArchive.OpeningLeadInfo[index], boardDetails_HandViewer.OpeningLeadInfo[index]);
                }
            }
            if (failures == 0)
            {
                _logCSVBuilder.AppendLine($"{pageNumber},{boardNumber},{segmentName},Pass");
                WriteToAllDataLogFile();
                Status logStatus = Status.Pass;
                _reports.LogTestStatus(logStatus,testName, pageNumber.ToString(), boardNumber, eventName, segmentName);
            }
            else
            {
                _assertionFailures = _assertionFailures + 1;
                _logCSVBuilder.AppendLine($"{pageNumber},{boardNumber},{segmentName},Fail");
                WriteToAllDataLogFile();
                Status logStatus = Status.Fail;
                _reports.LogTestStatus(logStatus, testName, pageNumber.ToString(), boardNumber, eventName, segmentName);
            }
        }
        public IWebElement GetOpeningLeadInfoElement(int boardID)
        {
            return _browser.GetElement($"//input [@value = '{boardID}']//following::td[12]/span", WebBrowser.ElementSelectorType.XPath);
        }
        public List<string> GetOpeningLeadInfo(int boardID)
        {
            List<string> leadInfo = new List<string>();
            string openingLeadInfo = GetOpeningLeadInfoElement(boardID).Text;
            string[] leadInfos = openingLeadInfo.Split(new string[] { "\r\n" }, StringSplitOptions.None).Skip(1).ToArray();
            foreach(string lead in leadInfos)
            {
                leadInfo.Add(lead);
            }
            return leadInfo;
        }
        public HandsAndBiddingInfo GetHandsAndBiddingInfo(int boardID)
        {
            HandsAndBiddingInfo handsAndBiddingInfo = new HandsAndBiddingInfo
            {
                BoardNumber = Int32.Parse(GetBoardNumber_HandsAndBidding()),
                Dealer = GetDealerDirection_HandsAndBidding(),
                Vulnerability = GetVulnerabilityDirections_HandsAndBidding(),
                DirectionAndName = GetDirectionAndPlayerName(),
                NorthHandSuitsAndCards = GetSuitsAndCardsInNorthPlayerHand(boardID),
                WestHandSuitsAndCards = GetSuitsAndCardsInWestPlayerHand(boardID),
                EastHandSuitsAndCards = GetSuitsAndCardsInEastPlayerHand(boardID),
                SouthHandSuitsAndCards = GetSuitsAndCardsInSouthPlayerHand(boardID),
                PlayerHandSummary = GetPlayerHandDetailsSummary(),
                BiddingSequence = GetBiddingSequence(),
                BiddingSummary = GetBiddingSummary()
            };

            return handsAndBiddingInfo;
        }
        public void AssertAndLogHandsAndBiddingInfo(HandsAndBiddingInfo handsAndBiddingInfo_OnlineArchive, HandsAndBiddingInfo handsAndBiddingInfo_HandViewer, int boardID, string eventName, int pageNumber, [Optional] string segmentName)
        {
            int failures = 0;
            string boardNumber = handsAndBiddingInfo_OnlineArchive.BoardNumber.ToString();
            string testName = $"BoardID-{boardID}";
            _reports.CreateTestInExtentReport(testName);
            try
            {
                Assert.IsTrue(handsAndBiddingInfo_OnlineArchive.BoardNumber.Equals(handsAndBiddingInfo_HandViewer.BoardNumber));
                _reports.LogTestInfo("Board Number");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Board Number", Ex, handsAndBiddingInfo_OnlineArchive.BoardNumber.ToString(), handsAndBiddingInfo_HandViewer.BoardNumber.ToString());
            }
            try
            {
                Assert.IsTrue(handsAndBiddingInfo_OnlineArchive.Dealer.SequenceEqual(handsAndBiddingInfo_HandViewer.Dealer));
                _reports.LogTestInfo("Dealer");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Dealer", Ex, handsAndBiddingInfo_OnlineArchive.Dealer, handsAndBiddingInfo_HandViewer.Dealer);
            }
            try
            {
                Assert.IsTrue(handsAndBiddingInfo_OnlineArchive.Vulnerability.SequenceEqual(handsAndBiddingInfo_HandViewer.Vulnerability));
                _reports.LogTestInfo("Vulnerability");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
                _reports.LogTestInfo("Vulnerability", Ex, handsAndBiddingInfo_OnlineArchive.Vulnerability, handsAndBiddingInfo_HandViewer.Vulnerability);
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForNameAndDirection(handsAndBiddingInfo_OnlineArchive.DirectionAndName,handsAndBiddingInfo_HandViewer.DirectionAndName));
                _reports.LogTestInfo("Directon and Name");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(handsAndBiddingInfo_OnlineArchive.NorthHandSuitsAndCards, handsAndBiddingInfo_HandViewer.NorthHandSuitsAndCards));
                _reports.LogTestInfo("North hand suits and cards");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(handsAndBiddingInfo_OnlineArchive.WestHandSuitsAndCards, handsAndBiddingInfo_HandViewer.WestHandSuitsAndCards));
                _reports.LogTestInfo("West hand suits and cards");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(handsAndBiddingInfo_OnlineArchive.EastHandSuitsAndCards, handsAndBiddingInfo_HandViewer.EastHandSuitsAndCards));
                _reports.LogTestInfo("East hand suits and cards");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(handsAndBiddingInfo_OnlineArchive.SouthHandSuitsAndCards, handsAndBiddingInfo_HandViewer.SouthHandSuitsAndCards));
                _reports.LogTestInfo("South hand suits and cards");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForPlayerHandDetails(handsAndBiddingInfo_OnlineArchive.PlayerHandSummary, handsAndBiddingInfo_HandViewer.PlayerHandSummary));
                _reports.LogTestInfo("Player hand summary");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForBiddingSequence(handsAndBiddingInfo_OnlineArchive.BiddingSequence, handsAndBiddingInfo_HandViewer.BiddingSequence));
                _reports.LogTestInfo("Bidding sequence");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            try
            {
                Assert.IsTrue(Assertions.AssertionResultsForBiddingSummary(handsAndBiddingInfo_OnlineArchive.BiddingSummary, handsAndBiddingInfo_HandViewer.BiddingSummary));
                _reports.LogTestInfo("Bidding summary");
            }
            catch (Exception Ex)
            {
                failures = failures + 1;
            }
            if (failures == 0)
            {
                _logCSVBuilder.AppendLine($"{pageNumber},{boardNumber},{segmentName},Pass");
                WriteToAllDataLogFile();
                Status logStatus = Status.Pass;
                _reports.LogTestStatus(logStatus, testName, pageNumber.ToString(), boardNumber, eventName, segmentName);
            }
            else
            {
                _assertionFailures = _assertionFailures + 1;
                _logCSVBuilder.AppendLine($"{pageNumber},{boardNumber},{segmentName},Fail");
                WriteToAllDataLogFile();
                Status logStatus = Status.Fail;
                _reports.LogTestStatus(logStatus, testName, pageNumber.ToString(), boardNumber, eventName, segmentName);
            }
        }
    }
}
