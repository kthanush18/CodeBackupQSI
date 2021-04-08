using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Quant.CardsGame.UITests.Web.CardsGame.Models;
using Quant.CardsGame.UITests.Web.CardsGame.Pages;
using Quant.CardsGame.UITests.Web.CardsGame.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardsGame
{
    [TestClass]
    public class OnlineArchiveTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _onlineArchive = new OnlineArchive(_browser);
            _commonCardsGame = new CardsGameCommon(_browser);
            _onlineHandViewer = new OnlineHandViewer(_browser);
            _onlineArchive.SelectOnlineArchiveOptionButton();
        }

        [TestMethod]
        public void TC_OpenOnlineArchive_VerfiyLoadingOfAllPages()
        {
            //Arrange
            _onlineArchive.WaitForPlayerPositionsToLoad();
            int totalPages = _commonCardsGame.GetAllPages();

            //Act and Assert
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                try
                {
                    _onlineArchive.SelectRequiredPageAndWaitForPageLoad(pageNumber);
                }
                catch (Exception Ex)
                {
                    _savedHands.HandleServerException(Ex, pageNumber);
                }
                Assert.IsTrue(_onlineArchive.IsFirstBoardDetailsLoaded());
            }
        }

        [TestMethod]
        public void TC_OpenOnlineArchiveHands_VerfiyPlayerNamesInAllDirections()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            PlayerNames playerName_OnlineArchive = _commonCardsGame.GetAllPlayerNames(boardID);

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            PlayerNames playerName_HandViewer = _onlineHandViewer.GetAllPlayerNames_OnlineArchive();

            //Assert
            Assert.IsTrue(playerName_OnlineArchive.North.SequenceEqual(playerName_HandViewer.North));
            Assert.IsTrue(playerName_OnlineArchive.East.SequenceEqual(playerName_HandViewer.East));
            Assert.IsTrue(playerName_OnlineArchive.South.SequenceEqual(playerName_HandViewer.South));
            Assert.IsTrue(playerName_OnlineArchive.West.SequenceEqual(playerName_HandViewer.West));
        }

        [TestMethod]
        public void TC_OpenOnlineArchiveHands_VerfiyBoardNumber_Deal_Vulnerability()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            string dealerDirection_OnlineArchive = _commonCardsGame.GetDealerDirection(boardID);
            string vulnerability_OnlineArchive = _commonCardsGame.GetVulnerabilityDirections(boardID);

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            string boardNumber_HandViewer = _onlineHandViewer.GetBoardNumber();
            string dealerDirection_HandViewer = _onlineHandViewer.GetDealerDirection();
            string vulnerability_HandViewer = _onlineHandViewer.GetVulnerabilityDirections();

            //Assert
            Assert.IsTrue(boardNumber_OnlineArchive.SequenceEqual(boardNumber_HandViewer));
            Assert.IsTrue(dealerDirection_OnlineArchive.SequenceEqual(dealerDirection_HandViewer));
            Assert.IsTrue(vulnerability_OnlineArchive.SequenceEqual(vulnerability_HandViewer));
        }

        [TestMethod]
        public void TC_OpenOnlineArchiveHands_VerfiyContractAndTableScore()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            string Contract_OnlineArchive = _commonCardsGame.GetContract(boardID);
            string TableScore_OnlineArchive = _commonCardsGame.GetTableScore(boardID);

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            string Contract_HandViewer = _onlineHandViewer.GetContract(playerRoom,boardCount_HandViewer);
            string TableScore_HandViewer = _onlineHandViewer.GetTableScore_OnlineArchive(playerRoom, boardCount_HandViewer);

            //Assert
            Assert.IsTrue(Contract_OnlineArchive.SequenceEqual(Contract_HandViewer));
            Assert.IsTrue(TableScore_OnlineArchive.SequenceEqual(TableScore_HandViewer));
        }

        [TestMethod]
        public void TC_OpenOnlineArchiveHands_VerfiyAreDefending_Declarer_OpeningHand()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            bool isDefending_OnlineArchive = _commonCardsGame.GetIsDefending(boardID);
            string declarer_OnlineArchive = _commonCardsGame.GetDeclarer(boardID);
            string openingHand_OnlineArchive = _commonCardsGame.GetOpeningHand(boardID);

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            List<IWebElement> playerNameElements = _onlineHandViewer.GetBoardPlayerNameElements_OnlineArchive();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            IWebElement contractHandElement = _onlineHandViewer.GetCompleteContract_ScoreBoard(playerRoom, boardCount_HandViewer);
            bool isDefending_HandViewer = _onlineHandViewer.GetIsDefending_ScoreBoard(contractHandElement);
            string declarer_HandViewer = _onlineHandViewer.GetDeclarer_ScoreBoard(contractHandElement,playerNameElements);
            string openingHand_HandViewer = _onlineHandViewer.GetOpeningHand_ScoreBoard(contractHandElement, playerNameElements);

            //Assert
            Assert.IsTrue(isDefending_OnlineArchive.Equals(isDefending_HandViewer));
            Assert.IsTrue(declarer_OnlineArchive.SequenceEqual(declarer_HandViewer));
            Assert.IsTrue(openingHand_OnlineArchive.SequenceEqual(openingHand_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyBoardNumber_Deal_Vulnerability()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber_HandsAndBidding();
            string dealerDirection_OnlineArchive = _commonCardsGame.GetDealerDirection_HandsAndBidding();
            string vulnerability_OnlineArchive = _commonCardsGame.GetVulnerabilityDirections_HandsAndBidding();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            string boardNumber_HandViewer = _onlineHandViewer.GetBoardNumber();
            string dealerDirection_HandViewer = _onlineHandViewer.GetDealerDirection();
            string vulnerability_HandViewer = _onlineHandViewer.GetVulnerabilityDirections();

            //Assert
            Assert.IsTrue(boardNumber_OnlineArchive.SequenceEqual(boardNumber_HandViewer));
            Assert.IsTrue(dealerDirection_OnlineArchive.SequenceEqual(dealerDirection_HandViewer));
            Assert.IsTrue(vulnerability_OnlineArchive.SequenceEqual(vulnerability_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyCardsInPlayerHandsAndDirection()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            List<Tuple<string, string>> directionAndName_OnlineArchive = _commonCardsGame.GetDirectionAndPlayerName();
            List<Tuple<string, List<string>>> northHandSuitsAndCards_OnlineArchive = _commonCardsGame.GetSuitsAndCardsInNorthPlayerHand(boardID);
            List<Tuple<string, List<string>>> westHandSuitsAndCards_OnlineArchive = _commonCardsGame.GetSuitsAndCardsInWestPlayerHand(boardID);
            List<Tuple<string, List<string>>> eastHandSuitsAndCards_OnlineArchive = _commonCardsGame.GetSuitsAndCardsInEastPlayerHand(boardID);
            List<Tuple<string, List<string>>> southHandSuitsAndCards_OnlineArchive = _commonCardsGame.GetSuitsAndCardsInSouthPlayerHand(boardID);
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            List<Tuple<string, string>> directionAndName_HandViewer = _onlineHandViewer.GetDirectionAndPlayerName_OnlineArchive();
            List<Tuple<string, List<string>>> northHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInNorthPlayerHand();
            List<Tuple<string, List<string>>> westHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInWestPlayerHand();
            List<Tuple<string, List<string>>> eastHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInEastPlayerHand();
            List<Tuple<string, List<string>>> southHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInSouthPlayerHand();

            //Assert
            Assert.IsTrue(Assertions.AssertionResultsForNameAndDirection(directionAndName_OnlineArchive,directionAndName_HandViewer));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(northHandSuitsAndCards_OnlineArchive, northHandSuitsAndCards_OnlineArchive));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(westHandSuitsAndCards_OnlineArchive, westHandSuitsAndCards_HandViewer));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(eastHandSuitsAndCards_OnlineArchive, eastHandSuitsAndCards_HandViewer));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(southHandSuitsAndCards_OnlineArchive, southHandSuitsAndCards_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyRandomPlayerHandDetails()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            string randomPlayerDirection = _commonCardsGame.SelectAndGetRandomPlayerDirection();
            PlayerHandSummary playerHandDetails_OnlineArchive = _commonCardsGame.GetPlayerHandDetailsSummary();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            PlayerHandSummary playerHandDetails_HandViewer = _onlineHandViewer.GetPlayerHandDetailsSummary_OnlineArchive(randomPlayerDirection);

            //Assert
            Assert.IsTrue(Assertions.AssertionResultsForPlayerHandDetails(playerHandDetails_OnlineArchive, playerHandDetails_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyBiddingSequence()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            BiddingSequence biddingSequence_OnlineArchive = _commonCardsGame.GetBiddingSequence();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            BiddingSequence biddingSequence_HandViewer = _onlineHandViewer.GetBiddingSequence();

            //Assert
            Assert.IsTrue(Assertions.AssertionResultsForBiddingSequence(biddingSequence_OnlineArchive, biddingSequence_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyBiddingSummary()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            BiddingSummary biddingSummary_OnlineArchive = _commonCardsGame.GetBiddingSummary();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            BiddingSummary biddingSummary_HandViewer = _onlineHandViewer.GetBiddingSummary();

            //Assert
            Assert.IsTrue(Assertions.AssertionResultsForBiddingSummary(biddingSummary_OnlineArchive, biddingSummary_HandViewer));
        }

        [TestMethod]
        public void TC_OpenOnlineArchiveHands_VerfiyScore_Type_OpeningLeadCard()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            string score_OnlineArchive = _commonCardsGame.GetScore(boardID);
            string type_OnlineArchive = _commonCardsGame.GetScoreType(boardID);
            string openingLeadCard_OnlineArchive = _commonCardsGame.GetOpeningLeadCard(boardID);

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            string score_HandViewer = _onlineHandViewer.GetScore_OnlineArchive(boardCount_HandViewer);
            string type_HandViewer = _onlineHandViewer.GetScoreType_OnlineArchive();
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            string openingLeadCard_HandViewer = _onlineHandViewer.GetOpeningLeadCard();

            //Assert
            Assert.IsTrue(score_OnlineArchive.SequenceEqual(score_HandViewer));
            Assert.IsTrue(type_OnlineArchive.SequenceEqual(type_HandViewer));
            Assert.IsTrue(openingLeadCard_OnlineArchive.SequenceEqual(openingLeadCard_HandViewer));
        }

        [TestMethod]
        [Description("Verifies data present in all columns for all boards with hand viewer tool")]
        public void TC_OpenOnlineArchive_VerfiyAllColumnsDataForAllBoards()
        {
            //Arrange
            _onlineArchive.WaitForPlayerPositionsToLoad();
            _commonCardsGame.DeleteExistingLogs();

            try
            {
                //Act
                _commonCardsGame.CreateAllDataLogFileAndAppendTitles();
                _commonCardsGame.IntializeHTMLReporter();
                int totalPages = _commonCardsGame.GetAllPages();
                for (int pageNumber = 1; pageNumber <= 1; pageNumber++)
                {
                    try
                    {
                        _onlineArchive.SelectRequiredPageAndWaitForPageLoad(pageNumber);
                        List<int> segmentIDsInPage = _onlineArchive.GetAllSegmentIDsInCurrentPage();
                        for (int segment = 0; segment < segmentIDsInPage.Count; segment++)
                        {
                            List<int> boardIDsInSegment = _onlineArchive.GetAllBoardIDsInCurrentSegment(segment, segmentIDsInPage);
                            string segmentName = _commonCardsGame.GetSegmentName(segmentIDsInPage[segment]);
                            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(segmentIDsInPage[segment]);
                            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
                            string eventName = _onlineHandViewer.GetEventName_Scoreboard();
                            for (int board = 0; board < boardIDsInSegment.Count; board++)
                            {
                                //cards game
                                _commonCardsGame.SwitchToCardsGameWebsite();
                                string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardIDsInSegment[board]);
                                BoardDetails boardDetails_OnlineArchive = _commonCardsGame.GetBoardDetails(boardIDsInSegment[board]);

                                //hand viewer
                                _commonCardsGame.SwitchToHandViewerWebsite();
                                _onlineHandViewer.NavigateToScoreBoard();
                                BoardDetails boardDetails_HandViewer = _onlineHandViewer.GetBoardDetails_OnlineArchive(boardNumber_OnlineArchive);

                                //Assert
                                _commonCardsGame.AssertAndLogBoardDetails(boardDetails_OnlineArchive, boardDetails_HandViewer, boardIDsInSegment[board], eventName, pageNumber, segmentName);
                            }
                            _onlineHandViewer.CloseLastTabAndSwitchToFirstTab();
                        }
                    }
                    catch (Exception Ex)
                    {
                        _commonCardsGame.LogAndHandleException(Ex, pageNumber);
                    }
                }
                if (_commonCardsGame.GetAssertionFailuresCount() >= 1)
                {
                    Assert.Fail("Assertion failed due to log errors");
                    LogInfo.WriteLine("Test method failed due to assertion failures");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(String.Format($"{ex.Message}\n\n{ex.StackTrace}"));
            }
            finally
            {
                _commonCardsGame.PublishHTMLReport();
                SendEmailOfTestStatus(_commonCardsGame.GetAssertionFailuresCount());
            }
        }

        [TestMethod]
        [Description("Verifies all contents present in hands & bidding popup for all boards")]
        public void TC_OpenOnlineArchive_VerfiyInfoFromHandsAndBiddingPopUpForAllBoards()
        {
            //Arrange
            _onlineArchive.WaitForPlayerPositionsToLoad();
            _commonCardsGame.DeleteExistingLogs();

            try
            {
                //Act
                _commonCardsGame.CreateAllDataLogFileAndAppendTitles();
                _commonCardsGame.IntializeHTMLReporter();
                int totalPages = _commonCardsGame.GetAllPages();
                for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
                {
                    try
                    {
                        _onlineArchive.SelectRequiredPageAndWaitForPageLoad(pageNumber);
                        List<int> segmentIDsInPage = _onlineArchive.GetAllSegmentIDsInCurrentPage();
                        for (int segment = 0; segment < segmentIDsInPage.Count; segment++)
                        {
                            List<int> boardIDsInSegment = _onlineArchive.GetAllBoardIDsInCurrentSegment(segment, segmentIDsInPage);
                            string segmentName = _commonCardsGame.GetSegmentName(segmentIDsInPage[segment]);
                            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(segmentIDsInPage[segment]);
                            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
                            string eventName = _onlineHandViewer.GetEventName_Scoreboard();
                            for (int board = 0; board < boardIDsInSegment.Count; board++)
                            {
                                //cards game
                                _commonCardsGame.SwitchToCardsGameWebsite();
                                string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardIDsInSegment[board]);
                                _commonCardsGame.OpenHandsAndBiddingPopUp(boardIDsInSegment[board]);
                                string randomPlayerDirection = _commonCardsGame.SelectAndGetRandomPlayerDirection();
                                HandsAndBiddingInfo handsAndBiddingInfo_OnlineArchive = _commonCardsGame.GetHandsAndBiddingInfo(boardIDsInSegment[board]);
                                _commonCardsGame.CloseHandsAndBiddingPopUp();

                                //hand viewer
                                _commonCardsGame.SwitchToHandViewerWebsite();
                                _onlineHandViewer.NavigateToScoreBoard();
                                HandsAndBiddingInfo handsAndBiddingInfo_HandViewer = _onlineHandViewer.GetHandsAndBiddingInfo_OnlineArchive(boardNumber_OnlineArchive, randomPlayerDirection);

                                //Assert
                                _commonCardsGame.AssertAndLogHandsAndBiddingInfo(handsAndBiddingInfo_OnlineArchive, handsAndBiddingInfo_HandViewer, boardIDsInSegment[board], eventName, pageNumber, segmentName);
                            }
                            _onlineHandViewer.CloseLastTabAndSwitchToFirstTab();
                        }
                    }
                    catch (Exception Ex)
                    {
                        _commonCardsGame.LogAndHandleException(Ex, pageNumber);
                    }
                }
                if (_commonCardsGame.GetAssertionFailuresCount() >= 1)
                {
                    Assert.Fail("Assertion failed due to log errors");
                    LogInfo.WriteLine("Test method failed due to assertion failures");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(String.Format($"{ex.Message}\n\n{ex.StackTrace}"));
            }
            finally
            {
                _commonCardsGame.PublishHTMLReport();
                SendEmailOfTestStatus(_commonCardsGame.GetAssertionFailuresCount());
            }
        }

        [TestMethod]
        public void TC_OpenOnlineArchiveHands_VerfiyOpeningLeadInfo()
        {
            //Arrange

            //Act
            //Crads game website
            _onlineArchive.SelectRandomPageNumber();
            int randomSegmentID = _onlineArchive.GetRandomSegmentID();
            int randomBoardNumber = _onlineArchive.GetRandomBoardNumber(randomSegmentID);
            int boardID = _onlineArchive.GetBoardIDForBoardNumberAndSegment(randomBoardNumber, randomSegmentID);
            string boardNumber_OnlineArchive = _commonCardsGame.GetBoardNumber(boardID);
            string openingLeadCard_OnlineArchive = _commonCardsGame.GetOpeningLeadCard(boardID);
            List<string> openingLeadInfo_OnlineArchive = _commonCardsGame.GetOpeningLeadInfo(boardID);

            //Online handviewer tool
            int linFileID = _onlineHandViewer.GetLinFileIDForRandomSegmentID(randomSegmentID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFileID);
            string playerRoom = _onlineHandViewer.GetRoomUsingPlayerNames();
            int boardCount_HandViewer = _onlineHandViewer.GetBoardCountFromBoardNumber(boardNumber_OnlineArchive);
            _onlineHandViewer.OpenRequiredBoardFromTheSegment(playerRoom, boardCount_HandViewer);
            string openingLeadCard_HandViewer = _onlineHandViewer.GetOpeningLeadCard();
            List<string> openingLeadInfo_HandViewer = _onlineHandViewer.GetOpeningLeadInfo(openingLeadCard_HandViewer);

            //Assert
            Assert.IsTrue(openingLeadCard_OnlineArchive.SequenceEqual(openingLeadCard_HandViewer));
            Assert.IsTrue(openingLeadInfo_OnlineArchive.SequenceEqual(openingLeadInfo_HandViewer));
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            LogInfo.WriteLine("TestCleanup Initialization");

            // Your relevant cleanup code comes here just before the call of base class 'TestCleanup'

            base.TestCleanup();
        }
    }
}
