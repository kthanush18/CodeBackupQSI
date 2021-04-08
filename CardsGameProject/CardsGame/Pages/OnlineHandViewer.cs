using OpenQA.Selenium;
using Quant.CardsGame.UITests.Common.Web;
using Quant.CardsGame.UITests.Web.CardsGame.DataAccess;
using Quant.CardsGame.UITests.Web.CardsGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Quant.CardsGame.UITests.Web.CardsGame.Pages
{
    public class OnlineHandViewer : WebPage
    {
        protected static CardsGameDataAccess _dataAccess;
        private static readonly string _handViewerURL = ConfigurationManager.AppSettings["HandViewerURL"].ToString();
        private static readonly string _handViewerForLinFileURL = ConfigurationManager.AppSettings["HandViewerForLinFileURL"].ToString();
        private static readonly string _meckLastname = ConfigurationManager.AppSettings["MeckLastName"].ToString();
        private static readonly string _rodlastName = ConfigurationManager.AppSettings["RodLastname"].ToString();
        private static readonly string _meckFullName = ConfigurationManager.AppSettings["MeckFullName"].ToString();
        private static readonly string _rodFullName = ConfigurationManager.AppSettings["RodFullName"].ToString();
        private static readonly string _meckShortName = ConfigurationManager.AppSettings["MeckShortName"].ToString();
        private static readonly string _rodShortName = ConfigurationManager.AppSettings["RodShortName"].ToString();
        private static readonly string _openRoom = ConfigurationManager.AppSettings["OpenRoom"].ToString();
        private static readonly string _closedRoom = ConfigurationManager.AppSettings["ClosedRoom"].ToString();
        private static readonly string _webPagesDirectory = ConfigurationManager.AppSettings["SavedHandsWebpagesDirectory"].ToString();

        public OnlineHandViewer(WebBrowser browser) : base(browser)
        {

        }

        public enum Directions
        {
            South,West,North,East
        }
        public enum ShortDirections
        {
            S, W, N, E
        }
        public enum HighCardPoints
        {
            A = 4,
            K = 3,
            Q = 2,
            J = 1
        }
        public enum LeadCardPlaceOrder
        {
            [Description("3rd Best")]
            ThirdBest,
            [Description("4th Best")]
            FourthBest,
            [Description("5th Best")]
            FifthBest,
            [Description("6th Best")]
            SixthBest,
            [Description("7th Best")]
            SeventhBest
        }
        public int GetLinFileIDForRandomSegmentID(int randomSegmentID)
        {
            _dataAccess = new CardsGameDataAccess();
            return _dataAccess.GetLinFileIDUsingSegmentID(randomSegmentID);
        }
        public bool WaitForEventInfoToLoad()
        {
            return _browser.WaitForElement("10", WebBrowser.ElementSelectorType.ID);
        }
        public void LaunchAndSwitchToHandViewerTool(int linFileID)
        {
            _browser.OpenNewTab($"{_handViewerURL}{linFileID}");
            _browser.SwitchToLastTab();
            WaitForEventInfoToLoad();
        }
        public List<IWebElement> GetAllPlayerNameElements_OnlineArchive()
        {
            return _browser.GetElements(".playerNameDivStyle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<string> SetPlayerNamesToMeckAndRod(List<string> playerNames)
        {
            List<string> modifiedPlayerNames = new List<string>();
            foreach(string player in playerNames)
            {
                if (player.Contains(_meckLastname)|| player.Contains(_meckFullName))
                {
                    modifiedPlayerNames.Add(_meckShortName);
                }
                else if (player.Contains(_rodlastName)|| player.Contains(_rodFullName))
                {
                    modifiedPlayerNames.Add(_rodShortName);
                }
                else
                {
                    modifiedPlayerNames.Add(player.ToLower());
                }
            }
            return modifiedPlayerNames;
        }
        public List<string> TrimDirectionAttributesFromPlayerNames(List<IWebElement> boardPlayerNameElements)
        {
            List<string> trimmedPlayerNames = new List<string>();
            foreach (IWebElement player in boardPlayerNameElements)
            {
                string modifiedPlayer = player.Text.Replace("E: ", "").Replace("N: ", "").Replace("S: ", "").Replace("W: ", "").ToLower();
                trimmedPlayerNames.Add(modifiedPlayer);
            }
            return trimmedPlayerNames;
        }
        public List<IWebElement> GetBoardPlayerNameElements_OnlineArchive()
        {
            List<IWebElement> boardPlayerNameElements = new List<IWebElement>();
            List<IWebElement> allPlayerElements = GetAllPlayerNameElements_OnlineArchive();
            for (int index = 0; index < allPlayerElements.Count; index++)
            {
                if (allPlayerElements[index].Text.ToLower().Contains(_meckLastname))
                {
                    if (index == 0 || index == 1 || index == 4 || index == 5)
                    {
                        boardPlayerNameElements.Add(allPlayerElements[0]);
                        boardPlayerNameElements.Add(allPlayerElements[1]);
                        boardPlayerNameElements.Add(allPlayerElements[4]);
                        boardPlayerNameElements.Add(allPlayerElements[5]);
                    }
                    else
                    {
                        boardPlayerNameElements.Add(allPlayerElements[3]);
                        boardPlayerNameElements.Add(allPlayerElements[2]);
                        boardPlayerNameElements.Add(allPlayerElements[7]);
                        boardPlayerNameElements.Add(allPlayerElements[6]);
                    }
                    break;
                }
            }
            return boardPlayerNameElements;
        }
        public List<string> GetAllPlayerNamesInTheBoard_OnlineArchive()
        {
            List<string> trimmedPlayerNames = TrimDirectionAttributesFromPlayerNames(GetBoardPlayerNameElements_OnlineArchive());
            return trimmedPlayerNames;
        }
        public string GetLinFileForRandomBoardID(int boardID)
        {
            _dataAccess = new CardsGameDataAccess();
            return _dataAccess.GetLinFileUsingBoardID(boardID);
        }
        public bool WaitForBoardInfoToLoad()
        {
            return _browser.WaitForElement("playButton", WebBrowser.ElementSelectorType.ID);
        }
        public void LaunchAndSwitchToHandViewerTool(string linFile)
        {
            _browser.OpenNewTab();
            _browser.SwitchToLastTab();
            _browser.NavigateToUrl($"{_handViewerForLinFileURL}{linFile}");
            WaitForBoardInfoToLoad();
        }
        public IWebElement GetNorthPlayer()
        {
            return _browser.GetElement("//div[contains (text(),\"N\") and @class = \"nameInitialDivStyle\"]//following-sibling::div", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetEastPlayer()
        {
            return _browser.GetElement("//div[contains (text(),\"E\") and @class = \"nameInitialDivStyle\"]//following-sibling::div", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetSouthPlayer()
        {
            return _browser.GetElement("//div[contains (text(),\"S\") and @class = \"nameInitialDivStyle\"]//following-sibling::div", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetWestPlayer()
        {
            return _browser.GetElement("//div[contains (text(),\"W\") and @class = \"nameInitialDivStyle\"]//following-sibling::div", WebBrowser.ElementSelectorType.XPath);
        }
        public List<string> GetAllPlayerNamesInTheBoard_SavedHands()
        {
            List<string> playerNames = new List<string>
            {
                GetNorthPlayer().Text,
                GetEastPlayer().Text,
                GetSouthPlayer().Text,
                GetWestPlayer().Text
            };
            return playerNames;
        }
        public List<string> GetPlayerFullNamesFromDB(List<string> playersNames)
        {
            _dataAccess = new CardsGameDataAccess();
            return _dataAccess.GetPlayerFullNames(playersNames);
        }
        public string GetRoomUsingPlayerNames()
        {
            string playerRoom = "";
            List<IWebElement> allPlayerElements = GetAllPlayerNameElements_OnlineArchive();
            for (int index = 0; index < allPlayerElements.Count; index++)
            {
                if (allPlayerElements[index].Text.ToLower().Contains(_meckLastname))
                {
                    if (index == 0 || index == 1 || index == 4 || index == 5)
                    {
                        playerRoom = _openRoom;
                    }
                    else
                    {
                        playerRoom = _closedRoom;
                    }
                    break;
                }
            }
            return playerRoom;
        }
        public IWebElement GetWebElementFromElementID(int contractElementID)
        {
            return _browser.GetElement($"{contractElementID}", WebBrowser.ElementSelectorType.ID);
        }
        public bool WaitForBoardNumberToLoad()
        {
            return _browser.WaitForElement(".vulInnerDivStyle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<IWebElement> GetAllBoardNumberElements()
        {
            return _browser.GetElements($"//div[@class=\"scoreBoardTDStyle\" and not(@id) and not(contains (@style, \"background\"))]", WebBrowser.ElementSelectorType.XPath);
        }
        public int GetBoardCountFromBoardNumber(string boardNumber_OnlineArchive)
        {
            int randomBoardNumber = 0;
            List<IWebElement> listOfBoardNumberElements = GetAllBoardNumberElements();
            foreach (IWebElement boardNumberElement in listOfBoardNumberElements)
            {
                if (boardNumberElement.Text == boardNumber_OnlineArchive)
                {
                    randomBoardNumber = listOfBoardNumberElements.IndexOf(boardNumberElement) + 1;
                }
            }
            return randomBoardNumber;
        }
        public void OpenRequiredBoardFromTheSegment(string playerRoom, int boardCount_HandViewer)
        {
            int contractElementID = 0;
            if (playerRoom == _openRoom)
            {
                if (boardCount_HandViewer < 11)
                {
                    contractElementID = boardCount_HandViewer + 9;
                }
                else
                {
                    contractElementID = boardCount_HandViewer + 99;
                }
            }
            else
            {
                if (boardCount_HandViewer < 11)
                {
                    contractElementID = boardCount_HandViewer + 49;
                }
                else
                {
                    contractElementID = boardCount_HandViewer + 499;
                }
            }
            GetWebElementFromElementID(contractElementID).Click();
            WaitForBoardNumberToLoad();
        }
        public IWebElement GetBoardNumberElement()
        {
            return _browser.GetElement(".vulInnerDivStyle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<IWebElement> GetVulnerabilityElements()
        {
            return _browser.GetElements(".vulDivStyle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public string GetBoardNumber()
        {
            return GetBoardNumberElement().Text;
        }
        public string GetDealerDirection()
        {
            List<IWebElement> vulnerabilityElements = GetVulnerabilityElements();
            foreach(IWebElement element in vulnerabilityElements)
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
        public string GetVulnerabilityDirections()
        {
            List<string> vulnerableDirections = new List<string>();
            List<IWebElement> vulnerabilityElements = GetVulnerabilityElements();
            foreach (IWebElement element in vulnerabilityElements)
            {
                if (element.GetCssValue("background").Contains("rgb(203, 0, 0)"))
                {
                    int vulnerableIndex = vulnerabilityElements.IndexOf(element);
                    var vulnerableDirection = (Directions)vulnerableIndex;
                    vulnerableDirections.Add(vulnerableDirection.ToString());
                }
            }
            if(vulnerableDirections.Count !=0 && vulnerableDirections.Count != 4)
            {
                if(vulnerableDirections.Count == 1)
                {
                    return vulnerableDirections[0];
                }
                else
                {
                    if (vulnerableDirections[0].Contains("South")){
                        return $"{vulnerableDirections[1]}_{vulnerableDirections[0]}";
                    }
                    else
                    {
                        return $"{vulnerableDirections[0]}_{vulnerableDirections[1]}";
                    }
                }
            }
            else if(vulnerableDirections.Count == 0)
            {
                return "None";
            }
            else
            {
                return "Both";
            }
        }
        public string GetContractFromCompleteContract(IWebElement completeContract)
        {
            string contract = null;
            if (completeContract.Text.Contains("T") && completeContract.Text.Contains("x"))
            {
                contract = $"{completeContract.Text.Split('T')[0].Trim()}x";
            }
            else if (completeContract.Text.Contains("T"))
            {
                contract = completeContract.Text.Split('T')[0].Trim();
            }
            else
            {
                contract = completeContract.Text.Split('E','S','W','N')[0].Trim();
            }
            return contract;
        }
        public IWebElement GetCompleteContract_OpenRoom(int boardCount_HandViewer)
        {
            IWebElement completeContract;
            if (boardCount_HandViewer < 11)
            {
                completeContract = GetWebElementFromElementID(boardCount_HandViewer + 9);
                if (completeContract.Text == " ")
                {
                    completeContract = GetWebElementFromElementID(boardCount_HandViewer + 29);
                }
            }
            else
            {
                completeContract = GetWebElementFromElementID(boardCount_HandViewer + 99);
                if (completeContract.Text == " ")
                {
                    completeContract = GetWebElementFromElementID(boardCount_HandViewer + 299);
                }
            }
            return completeContract;
        }
        public IWebElement GetCompleteContract_ClosedRoom(int boardCount_HandViewer)
        {
            IWebElement completeContract;
            if (boardCount_HandViewer < 11)
            {
                completeContract = GetWebElementFromElementID(boardCount_HandViewer + 49);
                if (completeContract.Text == " ")
                {
                    completeContract = GetWebElementFromElementID(boardCount_HandViewer + 69);
                }
            }
            else
            {
                completeContract = GetWebElementFromElementID(boardCount_HandViewer + 499);
                if (completeContract.Text == " ")
                {
                    completeContract = GetWebElementFromElementID(boardCount_HandViewer + 699);
                }
            }
            return completeContract;
        }
        public IWebElement GetCompleteContract_ScoreBoard(string playerRoom, int boardCount_HandViewer)
        {
            IWebElement completeContractElement;
            if (playerRoom == _openRoom)
            {
                completeContractElement = GetCompleteContract_OpenRoom(boardCount_HandViewer);
            }
            else
            {
                completeContractElement = GetCompleteContract_ClosedRoom(boardCount_HandViewer);
            }
            return completeContractElement;
        }
        public string GetContract(string playerRoom, int boardCount_HandViewer)
        {
            IWebElement completeContractElement = GetCompleteContract_ScoreBoard(playerRoom,boardCount_HandViewer);
            return GetContractFromCompleteContract(completeContractElement);
        }
        public IWebElement GetScoreWebElement_OpenRoom(int boardCount_HandViewer)
        {
            IWebElement scoreElement = null;
            if (boardCount_HandViewer < 11)
            {
                scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 19);
                if (scoreElement.Text == " ")
                {
                    scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 39);
                }
            }
            else
            {
                scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 199);
                if (scoreElement.Text == " ")
                {
                    scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 399);
                }
            }
            return scoreElement;
        }
        public IWebElement GetScoreWebElement_ClosedRoom(int boardCount_HandViewer)
        {
            IWebElement scoreElement = null;
            if (boardCount_HandViewer < 11)
            {
                scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 59);
                if (scoreElement.Text == " ")
                {
                    scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 79);
                }
            }
            else
            {
                scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 599);
                if (scoreElement.Text == " ")
                {
                    scoreElement = GetWebElementFromElementID(boardCount_HandViewer + 799);
                }
            }
            return scoreElement;
        }
        public string GetTeamColorUsingPlayerName()
        {
            string playerTeamColor = "";
            List<IWebElement> allPlayerElements = GetAllPlayerNameElements_OnlineArchive();
            for (int index = 0; index < allPlayerElements.Count; index++)
            {
                if (allPlayerElements[index].Text.ToLower().Contains(_meckLastname))
                {
                    playerTeamColor = allPlayerElements[index].GetCssValue("background");
                    break;
                }
            }
            return playerTeamColor;
        }
        public string GetTableScoreWithSign(IWebElement tableScoreElement,string teamColor)
        {
            if (tableScoreElement.GetCssValue("background").Contains(teamColor))
            {
                return tableScoreElement.Text;
            }
            else
            {
                return $"-{tableScoreElement.Text}";
            }
        }
        public string GetTableScore_OnlineArchive(string playerRoom, int boardCount_HandViewer)
        {
            string teamColor = GetTeamColorUsingPlayerName();
            IWebElement tableScoreElement;
            if (playerRoom == _openRoom)
            {
                tableScoreElement = GetScoreWebElement_OpenRoom(boardCount_HandViewer);
                return GetTableScoreWithSign(tableScoreElement, teamColor);
            }
            else
            {
                tableScoreElement = GetScoreWebElement_ClosedRoom(boardCount_HandViewer);
                return GetTableScoreWithSign(tableScoreElement, teamColor);
            }
        }
        public IWebElement GetCompleteContract_HandDiagram()
        {
            return _browser.GetElement("//div [@class = \"auctionTableDivStyle\"]//following::div [@class = \"vulDivStyle\"][1]", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetContract_HandDiagram()
        {
            return GetContractFromCompleteContract(GetCompleteContract_HandDiagram());
        }
        public bool GetIsDefending_ScoreBoard (IWebElement contractHandElement)
        {
            string teamColor = GetTeamColorUsingPlayerName();
            if (contractHandElement.GetCssValue("background").Contains(teamColor))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void OpenRequiredBoardFromTheSegment(IWebElement contractHandElement)
        {
            contractHandElement.Click();
            WaitForBoardNumberToLoad();
        }
        public string GetDeclarerDirection(IWebElement contractHandElement)
        {
            char[] directions = { 'N', 'E', 'W', 'S' };
            if (contractHandElement.Text.Contains("T"))
            {
                string directionText = contractHandElement.Text.Split('T')[1];
                return directionText[directionText.IndexOfAny(directions)].ToString();
            }
            else
            {
                return contractHandElement.Text[contractHandElement.Text.IndexOfAny(directions)].ToString();
            }
        }
        public string GetDeclarer_ScoreBoard(IWebElement contractHandElement, List<IWebElement> playerNameElements)
        {
            List<IWebElement> playerElements = new List<IWebElement>();
            string declarerDirection = GetDeclarerDirection(contractHandElement);
            foreach (IWebElement playerElement in playerNameElements)
            {
                if (playerElement.Text.Contains($"{declarerDirection}: "))
                {
                    playerElements.Add(playerElement);
                    break;
                }
            }
            return SetPlayerNamesToMeckAndRod(TrimDirectionAttributesFromPlayerNames(playerElements))[0];
        }
        public string GetOpeningHandDirection(string declarerDirection)
        {
            int openingHandIndex = 0;
            Enum.TryParse(declarerDirection, out ShortDirections myDeclarerEnum);
            int declarerIndex = (int)myDeclarerEnum;
            if(declarerIndex != 3)
            {
                openingHandIndex = declarerIndex + 1;
            }
            else
            {
                openingHandIndex = 0;
            }
            return ((ShortDirections)openingHandIndex).ToString();
        }
        public string GetOpeningHand_ScoreBoard(IWebElement contractHandElement, List<IWebElement> playerNameElements)
        {
            List<IWebElement> playerElements = new List<IWebElement>();
            string declarerDirection = GetDeclarerDirection(contractHandElement);
            string openingHandDirection = GetOpeningHandDirection(declarerDirection);
            foreach (IWebElement playerElement in playerNameElements)
            {
                if (playerElement.Text.Contains($"{openingHandDirection}: "))
                {
                    playerElements.Add(playerElement);
                    break;
                }
            }
            return SetPlayerNamesToMeckAndRod(TrimDirectionAttributesFromPlayerNames(playerElements))[0];
        }
        public string GetDeclarerDirection_HandDiagram()
        {
            return GetDeclarerDirection(GetCompleteContract_HandDiagram());
        }
        public bool GetIsDefending_HandDiagram(string declarerDirection)
        {
            bool isDefending = true;
            if(declarerDirection.Contains("N") || declarerDirection.Contains("S"))
            {
                if (GetNorthPlayer().Text.Contains(_meckShortName) || GetSouthPlayer().Text.Contains(_meckShortName))
                    isDefending = false;
            }
            else
            {
                if (GetEastPlayer().Text.Contains(_meckShortName) || GetWestPlayer().Text.Contains(_meckShortName))
                    isDefending = false;
            }
            return isDefending;
        }
        public string GetPlayerNameUsingDirection(ShortDirections playerDirection)
        {
            switch (playerDirection)
            {
                case ShortDirections.E:
                    return GetEastPlayer().Text;
                case ShortDirections.N:
                    return GetNorthPlayer().Text;
                case ShortDirections.S:
                    return GetSouthPlayer().Text;
                case ShortDirections.W:
                    return GetWestPlayer().Text;
            }
            return null;
        }
        public string GetDeclarer_HandDiagram(string declarerDirection)
        {
            List<string> playerNames = new List<string>();
            Enum.TryParse(declarerDirection, out ShortDirections myDeclarerEnum);
            playerNames.Add(GetPlayerNameUsingDirection(myDeclarerEnum));
            return SetPlayerNamesToMeckAndRod(GetPlayerFullNamesFromDB(playerNames))[0];
        }
        public string GetOpeningHand_HandDiagram(string declarerDirection)
        {
            int openingHandIndex = 0;
            List<string> playerNames = new List<string>();
            Enum.TryParse(declarerDirection, out ShortDirections myDeclarerEnum);
            int declarerIndex = (int)myDeclarerEnum;
            if (declarerIndex != 3)
            {
                openingHandIndex = declarerIndex + 1;
            }
            else
            {
                openingHandIndex = 0;
            }
            playerNames.Add(GetPlayerNameUsingDirection((ShortDirections)openingHandIndex));
            return SetPlayerNamesToMeckAndRod(GetPlayerFullNamesFromDB(playerNames))[0];
        }
        public List<IWebElement> GetPlayerDirectionElements()
        {
            return _browser.GetElements(".nameInitialDivStyle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<IWebElement> GetPlayerNameElements()
        {
            return _browser.GetElements(".nameTextDivStyle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public List<string> GetModifiedPlayerNames_OnlineArchive(List<IWebElement> playerNameElements)
        {
            List<string> playerNames = new List<string>();
            foreach (IWebElement playerNameElement in playerNameElements)
            {
                playerNames.Add(playerNameElement.Text.ToLower());
            }
            return SetPlayerNamesToMeckAndRod(playerNames);
        }
        public List<string> GetModifiedPlayerNames_SavedHands(List<IWebElement> playerNameElements)
        {
            List<string> playerNames = new List<string>();
            foreach (IWebElement playerNameElement in playerNameElements)
            {
                playerNames.Add(playerNameElement.Text.ToLower());
            }
            List<string> playerFullNames = GetPlayerFullNamesFromDB(playerNames);
            return SetPlayerNamesToMeckAndRod(playerFullNames);
        }
        public Tuple<string, string> CombineDirectionAndPlayerName(string direction, string playerName)
        {
            return new Tuple<string, string>(direction, playerName);
        }
        public List<Tuple<string, string>> GetDirectionAndPlayerName_OnlineArchive()
        {
            List<Tuple<string, string>> playerDirectionsAndNames = new List<Tuple<string, string>>();
            List<IWebElement> playerDirectionElements = GetPlayerDirectionElements();
            List<IWebElement> playerNameElements = GetPlayerNameElements();
            List<string> modifiedPlayerNames = GetModifiedPlayerNames_OnlineArchive(playerNameElements);
            for (int index = 0; index < playerDirectionElements.Count; index++)
            {
                Tuple<string, string> playerDirectionAndName = CombineDirectionAndPlayerName(playerDirectionElements[index].Text, modifiedPlayerNames[index]);
                playerDirectionsAndNames.Add(playerDirectionAndName);
            }
            return playerDirectionsAndNames;
        }
        public List<Tuple<string, string>> GetDirectionAndPlayerName_SavedHands()
        {
            List<Tuple<string, string>> playerDirectionsAndNames = new List<Tuple<string, string>>();
            List<IWebElement> playerDirectionElements = GetPlayerDirectionElements();
            List<IWebElement> playerNameElements = GetPlayerNameElements();
            List<string> modifiedPlayerNames = GetModifiedPlayerNames_SavedHands(playerNameElements);
            for (int index = 0; index < playerDirectionElements.Count; index++)
            {
                Tuple<string, string> playerDirectionAndName = CombineDirectionAndPlayerName(playerDirectionElements[index].Text, modifiedPlayerNames[index]);
                playerDirectionsAndNames.Add(playerDirectionAndName);
            }
            return playerDirectionsAndNames;
        }
        public List<IWebElement> GetSuiteAndCardElements(string shortDirection,int count)
        {
            return _browser.GetElements($"//div [text()='{shortDirection}']//preceding::div[@class = 'suitRowDivStyle'][{count}]//ancestor::font", WebBrowser.ElementSelectorType.XPath);
        }
        public Tuple<string, List<string>> CombineSuiteAndCards(List<IWebElement> playerSuiteAndCardElements)
        {
            string suite = null;
            List<string> playerCards = new List<string>();
            for (int index = 0; index < playerSuiteAndCardElements.Count; index++)
            {
                if (index != 0)
                {
                    playerCards.Add(playerSuiteAndCardElements[index].Text);
                }
                else
                {
                    suite = playerSuiteAndCardElements[index].Text;
                }
            }
            return new Tuple<string, List<string>>(suite, playerCards);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsForPlayerDirection(string shortDirection)
        {
            List<Tuple<string, List<string>>> playerSuitsAndCards = new List<Tuple<string, List<string>>>(); 
            for (int count = 1; count <=4; count++)
            {
                List<IWebElement> playerSuiteAndCardElements = GetSuiteAndCardElements(shortDirection, count);
                Tuple<string, List<string>> playerSuitAndCards = CombineSuiteAndCards(playerSuiteAndCardElements);
                playerSuitsAndCards.Add(playerSuitAndCards);
            }
            return playerSuitsAndCards;
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInNorthPlayerHand()
        {
            string shortDirection = ShortDirections.N.ToString();
            return GetSuitsAndCardsForPlayerDirection(shortDirection);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInWestPlayerHand()
        {
            string shortDirection = ShortDirections.W.ToString();
            return GetSuitsAndCardsForPlayerDirection(shortDirection);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInEastPlayerHand()
        {
            string shortDirection = ShortDirections.E.ToString();
            return GetSuitsAndCardsForPlayerDirection(shortDirection);
        }
        public List<Tuple<string, List<string>>> GetSuitsAndCardsInSouthPlayerHand()
        {
            string shortDirection = ShortDirections.S.ToString();
            return GetSuitsAndCardsForPlayerDirection(shortDirection);
        }
        public string GetHighCardPointsValue(List<Tuple<string, List<string>>> playerSuitsAndCards)
        {
            List<string> highValueCards = new List<string>();
            int highCardsValue = 0;
            string[] highCards = { "A", "Q", "K", "J" };
            foreach (Tuple<string, List<string>> playerSuitAndCards in playerSuitsAndCards)
            {
                List<string> allCards = playerSuitAndCards.Item2;
                foreach(string card in allCards)
                {
                    if (highCards.Contains(card))
                    {
                        highValueCards.Add(card);
                    }
                }
            }
            foreach(string highValueCard in highValueCards)
            {
                Enum.TryParse(highValueCard, out HighCardPoints highCardEnum);
                highCardsValue = highCardsValue + (int)highCardEnum;
            }
            return highCardsValue.ToString();
        }
        public List<bool> GetAllCheckBoxConditions(List<Tuple<string, List<string>>> playerSuitsAndCards)
        {
            List<bool> allCheckBoxConditions = new List<bool>();
            bool result = false;
            bool decide = false;
            int singletons = 0;
            foreach (Tuple<string, List<string>> playerSuitAndCards in playerSuitsAndCards)
            {
                if (playerSuitAndCards.Item2.Count == 0)
                {
                    result = true;
                    decide = true;
                }
                else if (decide == false)
                {
                    result = false;
                }
            }
            allCheckBoxConditions.Add(result);
            decide = false;
            foreach (Tuple<string, List<string>> playerSuitAndCards in playerSuitsAndCards)
            {
                if (playerSuitAndCards.Item2.Count == 1)
                {
                    result = true;
                    singletons = singletons + 1;
                    decide = true;
                }
                else if (decide == false)
                {
                    result = false; 
                }
            }
            allCheckBoxConditions.Add(result);
            decide = false;
            foreach (Tuple<string, List<string>> playerSuitAndCards in playerSuitsAndCards)
            {
                if (playerSuitAndCards.Item2.Count == 1 && singletons==2)
                {
                    result = true;
                    decide = true;
                }
                else if(decide == false)
                {
                    result = false;
                }
            }
            allCheckBoxConditions.Add(result);
            decide = false;
            foreach (Tuple<string, List<string>> playerSuitAndCards in playerSuitsAndCards)
            {
                if (playerSuitAndCards.Item2.Count == 2)
                {
                    result = true;
                    decide = true;
                }
                else if(decide == false)
                {
                    result = false;
                }
            }
            allCheckBoxConditions.Add(result);
            return allCheckBoxConditions;
        }
        public PlayerHandSummary GetPlayerHandDetailsSummary_OnlineArchive(string randomPlayerDirection)
        {
            PlayerHandSummary handDetails = new PlayerHandSummary();
            List<Tuple<string, List<string>>> playerSuitsAndCards = GetSuitsAndCardsForPlayerDirection(randomPlayerDirection);
            List<bool> allCheckBoxConditions = GetAllCheckBoxConditions(playerSuitsAndCards);
            handDetails.PlayerName = GetDirectionAndPlayerName_OnlineArchive().Find(x => x.Item1 == ($"{randomPlayerDirection}")).Item2;
            handDetails.Spades = playerSuitsAndCards.Find(x => x.Item1 == "♠").Item2.Count.ToString();
            handDetails.Hearts = playerSuitsAndCards.Find(x => x.Item1 == "♥").Item2.Count.ToString();
            handDetails.Diamonds = playerSuitsAndCards.Find(x => x.Item1 == "♦").Item2.Count.ToString();
            handDetails.Clubs = playerSuitsAndCards.Find(x => x.Item1 == "♣").Item2.Count.ToString();
            handDetails.HighCardPoints = GetHighCardPointsValue(playerSuitsAndCards);
            handDetails.HasVoid = allCheckBoxConditions[0];
            handDetails.HasSingleton = allCheckBoxConditions[1];
            handDetails.HasMultipleSingletons = allCheckBoxConditions[2];
            handDetails.HasDoubleton = allCheckBoxConditions[3];
            return handDetails;
        }
        public PlayerHandSummary GetPlayerHandDetailsSummary_SavedHands(string randomPlayerDirection)
        {
            PlayerHandSummary handDetails = new PlayerHandSummary();
            List<Tuple<string, List<string>>> playerSuitsAndCards = GetSuitsAndCardsForPlayerDirection(randomPlayerDirection);
            List<bool> allCheckBoxConditions = GetAllCheckBoxConditions(playerSuitsAndCards);
            handDetails.PlayerName = GetDirectionAndPlayerName_SavedHands().Find(x => x.Item1 == ($"{randomPlayerDirection}")).Item2;
            handDetails.Spades = playerSuitsAndCards.Find(x => x.Item1 == "♠").Item2.Count.ToString();
            handDetails.Hearts = playerSuitsAndCards.Find(x => x.Item1 == "♥").Item2.Count.ToString();
            handDetails.Diamonds = playerSuitsAndCards.Find(x => x.Item1 == "♦").Item2.Count.ToString();
            handDetails.Clubs = playerSuitsAndCards.Find(x => x.Item1 == "♣").Item2.Count.ToString();
            handDetails.HighCardPoints = GetHighCardPointsValue(playerSuitsAndCards);
            handDetails.HasVoid = allCheckBoxConditions[0];
            handDetails.HasSingleton = allCheckBoxConditions[1];
            handDetails.HasMultipleSingletons = allCheckBoxConditions[2];
            handDetails.HasDoubleton = allCheckBoxConditions[3];
            return handDetails;
        }
        public IWebElement GetContractElement_HandDiagram()
        {
            return _browser.GetElement("//div [@class = 'auctionTableDivStyle'][3]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetNextButtonElement_HandDiagram()
        {
            return _browser.GetElement("nextButton", WebBrowser.ElementSelectorType.ID);
        }
        public List<IWebElement> GetDirectionsBiddingSequenceElements()
        {
            return _browser.GetElements("//div [@class = 'auctionTableDivStyle'][1]//ancestor::td", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetAllRowsBiddingSequenceElements()
        {
            return _browser.GetElements("//div [@class = 'auctionTableDivStyle'][2]//ancestor::tr", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetBiddingSequenceForRowElements(int rowCount)
        {
            return _browser.GetElements($"//div [@class = 'auctionTableDivStyle'][2]//ancestor::*/tr[{rowCount}]/td", WebBrowser.ElementSelectorType.XPath);
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
                westPlayerBidding.Add(biddingSequenceElements[0].Text.Replace("T",""));
                northPlayerBidding.Add(biddingSequenceElements[1].Text.Replace("T", ""));
                eastPlayerBidding.Add(biddingSequenceElements[2].Text.Replace("T", ""));
                southPlayerBidding.Add(biddingSequenceElements[3].Text.Replace("T", ""));
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
            while (GetContractElement_HandDiagram().Displayed == false)
            {
                GetNextButtonElement_HandDiagram().Click();
            }
            List<IWebElement> directionElements = GetDirectionsBiddingSequenceElements();
            List<IWebElement> allRowElements = GetAllRowsBiddingSequenceElements();
            List<List<string>> allPlayersBidding = GetAllPlayersBidding(allRowElements);

            playersBidding.WestHandBidding = new Tuple<string, List<string>>(directionElements[0].Text, allPlayersBidding[0]);
            playersBidding.NorthHandBidding = new Tuple<string, List<string>>(directionElements[1].Text, allPlayersBidding[1]);
            playersBidding.EastHandBidding = new Tuple<string, List<string>>(directionElements[2].Text, allPlayersBidding[2]);
            playersBidding.SouthHandBidding = new Tuple<string, List<string>>(directionElements[3].Text, allPlayersBidding[3]);

            return playersBidding;
        }
        public List<IWebElement> GetAllBiddingSequenceElements()
        {
            return _browser.GetElements("//div [@class='auctionTableDivStyle'][2]//td", WebBrowser.ElementSelectorType.XPath);
        }
        public List<string> GetCompleteBiddingSequence(List<IWebElement> biddingSequenceElements)
        {
            List<string> completeBiddingSequence = new List<string>();
            foreach (IWebElement element in biddingSequenceElements)
            {
                completeBiddingSequence.Add(element.Text.Replace("T", ""));
            }
            return completeBiddingSequence;
        }
        public List<string> GetOpeningBidSummary(List<string> completeBiddingSequence)
        {
            List<string> openingBidSummary = new List<string>();
            int passesCount = 0;
            int openingBidIndex = 0;
            List<string> biddingSequence = completeBiddingSequence.Where(x => x.ToString() != "").ToList();

            foreach (string bidding in biddingSequence)
            {
                if (bidding == "P")
                {
                    passesCount = passesCount + 1;
                }
                else
                {
                    openingBidIndex = biddingSequence.IndexOf(bidding);
                    break;
                }
            }

            openingBidSummary.Add(passesCount.ToString());
            openingBidSummary.Add(biddingSequence[openingBidIndex]);
            if(biddingSequence[openingBidIndex + 1] != "X")
            {
                openingBidSummary.Add(biddingSequence[openingBidIndex + 1]);
            }
            else
            {
                openingBidSummary.Add("");
            }
            return openingBidSummary;
        }
        public string GetOverCallAtForID(int overcallAtID)
        {
            _dataAccess = new CardsGameDataAccess();
            return _dataAccess.GetOvercallMadeAtUsingID(overcallAtID);
        }
        public int GetOverCallIDFromBiddingSequence(List<string> biddingSequenceWithoutNull, List<string> biddingSequenceWithoutPasses, List<string> overallBiddingFromHandViewer, string overcall)
        {
            int overcallAtID = 0;
            string partnerResponseToOpeningBid = biddingSequenceWithoutNull[biddingSequenceWithoutNull.IndexOf(biddingSequenceWithoutPasses[0]) + 2];
            if (partnerResponseToOpeningBid == "P")
            {
                partnerResponseToOpeningBid = "";
            }
            int overcallIndex = biddingSequenceWithoutNull.IndexOf(overcall);

            if (overallBiddingFromHandViewer.IndexOf(overcall) > 9 && biddingSequenceWithoutNull.IndexOf(overcall) > 1)
            {
                if (overallBiddingFromHandViewer.IndexOf(overcall) > 14)
                {
                    overcallAtID = 5;
                }
                else
                {
                    overcallAtID = 4;
                }

            }
            else if (biddingSequenceWithoutNull[overcallIndex - 1]==partnerResponseToOpeningBid)
            {
                overcallAtID = 3;
            }
            else if (biddingSequenceWithoutNull[overcallIndex - 1] == "P" && biddingSequenceWithoutNull[overcallIndex - 2] == "P")
            {
                overcallAtID = 2;
            }
            else if (biddingSequenceWithoutPasses.IndexOf(overcall) == 1)
            {
                overcallAtID = 1;
            }
            else
            {
                overcallAtID = 0;
            }
            return overcallAtID;
        }
        public List<string> GetOvercallSummary(List<string> completeBiddingSequence)
        {
            List<string> overcallSummary = new List<string>();
            List<string> numbersInBidding = new List<string>();
            string overcall = "";
            string overcallResponse = "";
            int overcallAtID = 0;

            List<string> biddingSequenceWithoutNull = completeBiddingSequence.Where(x => x.ToString() != "").ToList();
            List<string> biddingSequenceWithoutDoubles = biddingSequenceWithoutNull.Where(x => x.ToString() != "X" && x.ToString() != "XX").ToList();
            List<string> biddingSequenceWithoutPasses = biddingSequenceWithoutDoubles.Where(x => x.ToString() != "P").ToList();


            foreach (string bidding in biddingSequenceWithoutPasses)
            {
                numbersInBidding.Add(bidding);
                int count = numbersInBidding.Count;
                if (count >= 2)
                {
                    int currentIndex = biddingSequenceWithoutNull.IndexOf(numbersInBidding[count - 1]);
                    int currentButOneIndex = biddingSequenceWithoutNull.IndexOf(numbersInBidding[count -2]);
                    if ((currentIndex-currentButOneIndex) % 2 != 0)
                    {
                        overcall = biddingSequenceWithoutNull[currentIndex];
                        overcallResponse = biddingSequenceWithoutNull[currentIndex + 1];

                        overcallSummary.Add(overcall);
                        overcallSummary.Add(overcall[0].ToString());
                        break;
                    }
                }
            }

            if(overcall != "")
            {
                overcallAtID = GetOverCallIDFromBiddingSequence(biddingSequenceWithoutNull,biddingSequenceWithoutPasses,completeBiddingSequence,overcall);
                overcallSummary.Add(GetOverCallAtForID(overcallAtID).Replace(" Overcall",""));
                overcallSummary.Add(overcallResponse);
            }
            
            return overcallSummary;
        }
        public BiddingSummary GetBiddingSummary()
        {
            BiddingSummary biddingSummary = new BiddingSummary();
            while (GetContractElement_HandDiagram().Displayed == false)
            {
                GetNextButtonElement_HandDiagram().Click();
            }
            List<IWebElement> biddingSequenceElements = GetAllBiddingSequenceElements();
            List<string> completeBiddingSequence = GetCompleteBiddingSequence(biddingSequenceElements);
            List<string> openingBidSummary = GetOpeningBidSummary(completeBiddingSequence);
            List<string> overcallSummary = GetOvercallSummary(completeBiddingSequence);

            biddingSummary.NoOfPasses = openingBidSummary[0];
            biddingSummary.OpeningBid = openingBidSummary[1];
            biddingSummary.OpeningBidResponse = openingBidSummary[2];
            if(overcallSummary.Count != 0)
            {
                biddingSummary.Overcall = overcallSummary[0];
                biddingSummary.LevelOfOvercall = overcallSummary[1];
                biddingSummary.OvercallAt = overcallSummary[2];
                biddingSummary.OvercallResponse = overcallSummary[3];
            }
            else
            {
                biddingSummary.Overcall = "";
                biddingSummary.LevelOfOvercall = "";
                biddingSummary.OvercallAt = "";
                biddingSummary.OvercallResponse = "";
            }
            
            biddingSummary.Contract = GetContractFromCompleteContract(GetCompleteContract_HandDiagram());

            return biddingSummary;
        }
        public PlayerNames GetAllPlayerNames_OnlineArchive()
        {
            List<string> modifiedPlayerNames = SetPlayerNamesToMeckAndRod(GetAllPlayerNamesInTheBoard_OnlineArchive());
            PlayerNames playerNames = new PlayerNames
            {
                North = modifiedPlayerNames[0],
                East = modifiedPlayerNames[1],
                South = modifiedPlayerNames[2],
                West = modifiedPlayerNames[3]
            };

            return playerNames;
        }
        public PlayerNames GetAllPlayerNames_SavedHands()
        {
            List<string> playerFullNamesFromDB = GetPlayerFullNamesFromDB(GetAllPlayerNamesInTheBoard_SavedHands());
            List<string> modifiedPlayerNames = SetPlayerNamesToMeckAndRod(playerFullNamesFromDB);
            PlayerNames playerNames = new PlayerNames
            {
                North = modifiedPlayerNames[0],
                East = modifiedPlayerNames[1],
                South = modifiedPlayerNames[2],
                West = modifiedPlayerNames[3]
            };

            return playerNames;
        }
        public List<IWebElement> GetAllScoreElements()
        {
            return _browser.GetElements("//div [@class='scoreBoardTDStyle' and not(@id) and contains (@style, 'background')]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetEventNameElement()
        {
            return _browser.GetElement("//div [@class='scoreBoardTitleLineDivStyle'][1]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetTeamScoreElementForBoardNumber(List<IWebElement> allScoreElements, string teamColor, int boardCount_HandViewer)
        {
            
            List<IWebElement> teamScoreElements = new List<IWebElement>();
            foreach(IWebElement webElement in allScoreElements)
            {
                if(webElement.GetCssValue("background") == teamColor)
                {
                    teamScoreElements.Add(webElement);
                }
            }
            return teamScoreElements[boardCount_HandViewer - 1];
        }
        public IWebElement GetOponentScoreElementForBoardNumber(List<IWebElement> allScoreElements, string teamColor,int boardCount_HandViewer)
        {
            List<IWebElement> teamScoreElements = new List<IWebElement>();
            foreach (IWebElement webElement in allScoreElements)
            {
                if (webElement.GetCssValue("background") != teamColor)
                {
                    teamScoreElements.Add(webElement);
                }
            }
            return teamScoreElements[boardCount_HandViewer - 1];
        }
        public string GetEventName_Scoreboard()
        {
            return GetEventNameElement().Text;
        }
        public string GetScore_OnlineArchive(int boardCount_HandViewer)
        {
            string teamColor = GetTeamColorUsingPlayerName();
            List<IWebElement> allScoreElements = GetAllScoreElements();
            string eventName = GetEventName_Scoreboard();
            string score = GetTeamScoreElementForBoardNumber(allScoreElements, teamColor, boardCount_HandViewer).Text;
            if (eventName.Contains("Reisinger"))
            {
                if(score == "--")
                {
                    return "0.50";
                }
                else if (score != " ")
                {
                    return "1.00";
                }
                else
                {
                    return "0.00";
                }
            }
            else
            {
                if(score == "--")
                {
                    return score;
                }
                else if (score != " ")
                {
                    return $"{score}.00";
                }
                else
                {
                    return $"-{GetOponentScoreElementForBoardNumber(allScoreElements, teamColor, boardCount_HandViewer).Text}.00";
                }
            }
        }
        public string GetScoreType_OnlineArchive()
        {
            string eventName = GetEventName_Scoreboard(); 
            if (eventName.Contains("Reisinger"))
            {
                return "BAMs";
            }
            else
            {
                return "IMPs";
            }
        }
        public List<IWebElement> GetLeadCardElements()
        {
            return _browser.GetElements(".trickCardStyle", WebBrowser.ElementSelectorType.CssSelector);
        }
        public string GetOpeningLeadCard()
        {
            while (GetContractElement_HandDiagram().Displayed == false)
            {
                GetNextButtonElement_HandDiagram().Click();
            }
            GetNextButtonElement_HandDiagram().Click();
            List<IWebElement> leadCardElements = GetLeadCardElements();
            foreach (IWebElement element in leadCardElements)
            {
                if(element.Displayed == true)
                {
                    return element.Text;
                }
            }
            return null;
        }
        public void CloseLastTabAndSwitchToFirstTab()
        {
            if (_browser.GetCurrentTabCount() != 1)
            {
                _browser.SwitchToLastTab();
                _browser.CloseTab();
                _browser.SwitchToFirstTab();
            }
        }
        public IWebElement NavigateButton()
        {
            return _browser.GetElement("shuffleButton", WebBrowser.ElementSelectorType.ID);
        }
        public IWebElement ScoreBoardOptionButton()
        {
            return _browser.GetElement("scoreboard", WebBrowser.ElementSelectorType.ID);
        }
        public void NavigateToScoreBoard()
        {
            try
            {
                NavigateButton().Click();
                ScoreBoardOptionButton().Click();
            }
            catch
            {

            }
        }
        public BoardDetails GetBoardDetails_OnlineArchive(string boardNumber_OnlineArchive)
        {
            string playerRoom = GetRoomUsingPlayerNames();
            int boardCount_HandViewer = GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            List<IWebElement> playerNameElements = GetBoardPlayerNameElements_OnlineArchive();
            IWebElement contractHandElement = GetCompleteContract_ScoreBoard(playerRoom, boardCount_HandViewer);

            BoardDetails boardDetails = new BoardDetails();
            boardDetails.Score = GetScore_OnlineArchive(boardCount_HandViewer);
            boardDetails.Type = GetScoreType_OnlineArchive();
            boardDetails.Contract = GetContract(playerRoom, boardCount_HandViewer);
            boardDetails.Declarer = GetDeclarer_ScoreBoard(contractHandElement, playerNameElements);
            boardDetails.OpeningHand = GetOpeningHand_ScoreBoard(contractHandElement, playerNameElements);
            boardDetails.AllPlayers = GetAllPlayerNames_OnlineArchive();
            boardDetails.TableScore = GetTableScore_OnlineArchive(playerRoom, boardCount_HandViewer);
            OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            boardDetails.BoardNumber = Int32.Parse(GetBoardNumber());
            boardDetails.Dealer = GetDealerDirection();
            boardDetails.Vulnerability = GetVulnerabilityDirections();
            boardDetails.IsDefending = GetIsDefending_ScoreBoard(contractHandElement);
            boardDetails.OpeningLeadCard = GetOpeningLeadCard();
            boardDetails.OpeningLeadInfo = GetOpeningLeadInfo(boardDetails.OpeningLeadCard);

            return boardDetails;
        }
        public BoardDetails GetBoardDetails_SavedHands_ExceptTableScores()
        {
            string declarerDirection = GetDeclarerDirection_HandDiagram();

            BoardDetails boardDetails = new BoardDetails();
            boardDetails.BoardNumber = Int32.Parse(GetBoardNumber());
            boardDetails.Dealer = GetDealerDirection();
            boardDetails.Vulnerability = GetVulnerabilityDirections();
            boardDetails.AllPlayers = GetAllPlayerNames_SavedHands();
            boardDetails.IsDefending = GetIsDefending_HandDiagram(declarerDirection);
            boardDetails.Declarer = GetDeclarer_HandDiagram(declarerDirection);
            boardDetails.OpeningHand = GetOpeningHand_HandDiagram(declarerDirection);
            boardDetails.Contract = GetContract_HandDiagram();
            boardDetails.OpeningLeadCard = GetOpeningLeadCard();
            boardDetails.OpeningLeadInfo = GetOpeningLeadInfo(boardDetails.OpeningLeadCard);

            return boardDetails;
        }
        public IWebElement GetMovieLinkElement_SavedHands(string eventNumber, string boardNumber)
        {
            return _browser.GetElement($"//a[contains(text(),'{eventNumber}')]//following::tr[{boardNumber}]//td/a[text()='Movie']", WebBrowser.ElementSelectorType.XPath);
        }
        public void OpenHandDiagramAndSwitchToTheWindow(string eventNumber, string boardNumber_SavedHands)
        {
            IWebElement movieLinkElement = GetMovieLinkElement_SavedHands(eventNumber, boardNumber_SavedHands);
            _browser.MoveToWebElement(movieLinkElement);
            movieLinkElement.Click();
            _browser.SwitchtoCurrentWindow();
        }
        public void CloseHandDiagram()
        {
            _browser.CloseBrowser();
            _browser.SwitchtoCurrentWindow();
        }
        public BoardDetails GetBoardDetails_SavedHands(string eventName, string boardNumber_SavedHands)
        {
            BoardDetails boardDetails = new BoardDetails();
            string eventNumber = GetEventNumberFromEventName(eventName);
            boardDetails.Score = GetScore_SavedHands(eventNumber, boardNumber_SavedHands);
            boardDetails.Type = GetScoreType_SavedHands(boardDetails.Score);
            boardDetails.TableScore = GetTableScore_SavedHands(eventNumber, boardNumber_SavedHands);

            OpenHandDiagramAndSwitchToTheWindow(eventNumber, boardNumber_SavedHands);
            string declarerDirection = GetDeclarerDirection_HandDiagram();
            boardDetails.BoardNumber = Int32.Parse(GetBoardNumber());
            boardDetails.Dealer = GetDealerDirection();
            boardDetails.Vulnerability = GetVulnerabilityDirections();
            boardDetails.AllPlayers = GetAllPlayerNames_SavedHands();
            boardDetails.IsDefending = GetIsDefending_HandDiagram(declarerDirection);
            boardDetails.Declarer = GetDeclarer_HandDiagram(declarerDirection);
            boardDetails.OpeningHand = GetOpeningHand_HandDiagram(declarerDirection);
            boardDetails.Contract = GetContract_HandDiagram();
            boardDetails.OpeningLeadCard = GetOpeningLeadCard();
            CloseHandDiagram();

            return boardDetails;
        }
        public string GetEventMonthAndYear(string eventDate)
        {
            string [] dateSplit = eventDate.Replace(",", "").Split(' ').ToArray();
            return $"{dateSplit[1]}_{dateSplit[2]}";
        }
        public void OpenRequiredHTMLFileUsingEventDate(string eventDate)
        {
            string[] filePaths = Directory.GetFiles(_webPagesDirectory, "*.html",SearchOption.AllDirectories);
            string filePathToOpen = filePaths.ToList().Find(x => x.Contains($"{GetEventMonthAndYear(eventDate)}"));
            _browser.OpenNewTab();
            _browser.SwitchToLastTab();
            _browser.NavigateToUrl(filePathToOpen);
        }
        public IWebElement GetScoreElement_SavedHands(string eventNumber, string boardNumber)
        {
            return _browser.GetElement($"//a[contains(text(),'{eventNumber}')]//following::tr[{boardNumber}]//td[9]", WebBrowser.ElementSelectorType.XPath);
        }
        public IWebElement GetTableScoreElement_SavedHands(string eventNumber, string boardNumber)
        {
            return _browser.GetElement($"//a[contains(text(),'{eventNumber}')]//following::tr[{boardNumber}]//td[8]", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetEventNumberFromEventName(string eventName)
        {
            return eventName.Split(' ')[0].ToString();
        }
        public string GetScore_SavedHands(string eventNumber, string boardNumber)
        {
            return GetScoreElement_SavedHands(eventNumber,boardNumber).Text;
        }
        public string GetTableScore_SavedHands(string eventNumber, string boardNumber)
        {
            return GetTableScoreElement_SavedHands(eventNumber, boardNumber).Text;
        }
        public string GetScoreType_SavedHands(string score_HandViewer)
        {
            if (score_HandViewer.Contains("%"))
            {
                return "MPs";
            }
            else
            {
                return "IMPs";
            }
        }
        public string EvaluateAndGetRankOfTheCard(string openingLeadCard_HandViewer)
        {
            char[] highPointCards = { 'J', 'Q', 'K', 'A' };
            if (highPointCards.Any(openingLeadCard_HandViewer.Contains))
            {
                return "Honor";
            }
            else
            {
                return "Small Card";
            }
        }
        public IWebElement GetOpeningLeadCardElement()
        {
            return _browser.GetElement($"//font [@color = '808080']", WebBrowser.ElementSelectorType.XPath);
        }
        public List<IWebElement> GetOpeningLeadSuiteElements()
        {
            return _browser.GetElements($"//font [@color = '808080']/../..//div//font", WebBrowser.ElementSelectorType.XPath);
        }
        public string GetEnumDescription(Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
        public string EvaluateAndGetPlaceOrder()
        {
            IWebElement leadCardElement = GetOpeningLeadCardElement();
            List<IWebElement> leadCardSuiteElements = GetOpeningLeadSuiteElements();
            if(leadCardSuiteElements.Count == 1)
            {
                return "Singleton";
            }
            else if (leadCardSuiteElements.Count == 2)
            {
                return "Doubleton";
            }
            else if (leadCardSuiteElements.IndexOf(leadCardElement) > 1 && leadCardSuiteElements.Count >2)
            {
                int value = leadCardSuiteElements.IndexOf(leadCardElement) - 2;
                return GetEnumDescription((LeadCardPlaceOrder)value);
            }
            return null;
        }
        public List<string> GetOpeningLeadInfo(string openingLeadCard_HandViewer)
        {
            List<string> openingLeadInfo = new List<string>();
            string rankOfTheCard = EvaluateAndGetRankOfTheCard(openingLeadCard_HandViewer);
            string placeOfCardInSuite = EvaluateAndGetPlaceOrder();

            openingLeadInfo.Add(rankOfTheCard);
            if(placeOfCardInSuite != null)
            {
                openingLeadInfo.Add(placeOfCardInSuite);
            }

            return openingLeadInfo;
        }
        public bool IsEventNameVisibleInWebPage(string eventNumber)
        {
            return _browser.IsElementVisible($"//a[contains(text(),'{eventNumber}')]", WebBrowser.ElementSelectorType.XPath);
        }
        public void OpenSavedHTMLFileInNewTab(string eventDate, string eventName)
        {
            if(_browser.GetCurrentTabCount() == 1)
            {
                OpenRequiredHTMLFileUsingEventDate(eventDate);
            }
            else
            {
                _browser.SwitchToLastTab();
                string eventNumber = eventName.Split(' ')[0].ToString();
                if (IsEventNameVisibleInWebPage(eventNumber) == false)
                {
                    CloseLastTabAndSwitchToFirstTab();
                    OpenRequiredHTMLFileUsingEventDate(eventDate);
                }
            }
        }
        public HandsAndBiddingInfo GetHandsAndBiddingInfo_OnlineArchive(string boardNumber_OnlineArchive,string randomPlayerDirection)
        {
            string playerRoom = GetRoomUsingPlayerNames();
            int boardCount_HandViewer = GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);

            HandsAndBiddingInfo handsAndBiddingInfo = new HandsAndBiddingInfo
            {
                BoardNumber = Int32.Parse(GetBoardNumber()),
                Dealer = GetDealerDirection(),
                Vulnerability = GetVulnerabilityDirections(),
                DirectionAndName = GetDirectionAndPlayerName_OnlineArchive(),
                NorthHandSuitsAndCards = GetSuitsAndCardsInNorthPlayerHand(),
                WestHandSuitsAndCards = GetSuitsAndCardsInWestPlayerHand(),
                EastHandSuitsAndCards = GetSuitsAndCardsInEastPlayerHand(),
                SouthHandSuitsAndCards = GetSuitsAndCardsInSouthPlayerHand(),
                PlayerHandSummary = GetPlayerHandDetailsSummary_OnlineArchive(randomPlayerDirection),
                BiddingSequence = GetBiddingSequence(),
                BiddingSummary = GetBiddingSummary()
            };

            return handsAndBiddingInfo;
        }
    }
}
