using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.CardsGame.UITests.Web.CardsGame.Models;
using Quant.CardsGame.UITests.Web.CardsGame.Pages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quant.CardsGame.UITests.Web.CardsGame.Tests
{
    [TestClass]
    public class SavedHandsTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _savedHands = new SavedHands(_browser);
            _commonCardsGame = new CardsGameCommon(_browser);
            _onlineHandViewer = new OnlineHandViewer(_browser);
            _savedHands.SelectSavedHandsOptionButton();
        }

        [TestMethod]
        public void TC_OpenSavedHands_VerfiyLoadingOfAllPages()
        {
            //Arrange
            _savedHands.WaitForPlayerPositionsToLoad();
            int totalPages = _commonCardsGame.GetAllPages();

            //Act and Assert
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                try
                {
                    _savedHands.SelectRequiredPageAndWaitForPageLoad(pageNumber);
                }
                catch(Exception Ex)
                {
                    _savedHands.HandleServerException(Ex,pageNumber);
                }
                Assert.IsTrue(_savedHands.IsFirstBoardDetailsLoaded());
            }
        }

        [TestMethod]
        public void TC_OpenSavedHands_VerfiyPlayerNamesInAllDirections()
        {
            //Arrange
            _savedHands.WaitForPlayerPositionsToLoad();

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            PlayerNames playerName_SavedHands = _commonCardsGame.GetAllPlayerNames(boardID);

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            PlayerNames playerName_HandViewer = _onlineHandViewer.GetAllPlayerNames_SavedHands();

            //Assert
            Assert.IsTrue(playerName_SavedHands.North.SequenceEqual(playerName_HandViewer.North));
            Assert.IsTrue(playerName_SavedHands.East.SequenceEqual(playerName_HandViewer.East));
            Assert.IsTrue(playerName_SavedHands.South.SequenceEqual(playerName_HandViewer.South));
            Assert.IsTrue(playerName_SavedHands.West.SequenceEqual(playerName_HandViewer.West));
        }

        [TestMethod]
        public void TC_OpenSavedHands_VerfiyBoardNumber_Deal_Vulnerability()
        {
            //Arrange
            _savedHands.WaitForPlayerPositionsToLoad();

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            string boardNumber_SavedHands = _commonCardsGame.GetBoardNumber(boardID);
            string dealerDirection_SavedHands = _commonCardsGame.GetDealerDirection(boardID);
            string vulnerability_SavedHands = _commonCardsGame.GetVulnerabilityDirections(boardID);

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            string boardNumber_HandViewer = _onlineHandViewer.GetBoardNumber();
            string dealerDirection_HandViewer = _onlineHandViewer.GetDealerDirection();
            string vulnerability_HandViewer = _onlineHandViewer.GetVulnerabilityDirections();

            //Assert
            Assert.IsTrue(boardNumber_SavedHands.SequenceEqual(boardNumber_HandViewer));
            Assert.IsTrue(dealerDirection_SavedHands.SequenceEqual(dealerDirection_HandViewer));
            Assert.IsTrue(vulnerability_SavedHands.SequenceEqual(vulnerability_HandViewer));
        }

        [TestMethod]
        public void TC_OpenSavedHands_VerfiyContract()
        {
            //Arrange
            _savedHands.WaitForPlayerPositionsToLoad();

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            string Contract_OnlineArchive = _commonCardsGame.GetContract(boardID);

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            string Contract_HandViewer = _onlineHandViewer.GetContract_HandDiagram();

            //Assert
            Assert.IsTrue(Contract_OnlineArchive.SequenceEqual(Contract_HandViewer));
        }

        [TestMethod]
        public void TC_OpenSavedHands_VerfiyAreDefending_Declarer_OpeningHand()
        {
            //Arrange

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            bool isDefending_SavedHands = _commonCardsGame.GetIsDefending(boardID);
            string declarer_SavedHands = _commonCardsGame.GetDeclarer(boardID);
            string openingHand_SavedHands = _commonCardsGame.GetOpeningHand(boardID);

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            string declarerDirection = _onlineHandViewer.GetDeclarerDirection_HandDiagram();
            bool isDefending_HandViewer = _onlineHandViewer.GetIsDefending_HandDiagram(declarerDirection);
            string declarer_HandViewer = _onlineHandViewer.GetDeclarer_HandDiagram(declarerDirection);
            string openingHand_HandViewer = _onlineHandViewer.GetOpeningHand_HandDiagram(declarerDirection);

            //Assert
            Assert.IsTrue(isDefending_SavedHands.Equals(isDefending_HandViewer));
            Assert.IsTrue(declarer_SavedHands.SequenceEqual(declarer_HandViewer));
            Assert.IsTrue(openingHand_SavedHands.SequenceEqual(openingHand_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyBoardNumber_Deal_Vulnerability()
        {
            //Arrange

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            string boardNumber_SavedHands = _commonCardsGame.GetBoardNumber_HandsAndBidding();
            string dealerDirection_SavedHands = _commonCardsGame.GetDealerDirection_HandsAndBidding();
            string vulnerability_SavedHands = _commonCardsGame.GetVulnerabilityDirections_HandsAndBidding();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            string boardNumber_HandViewer = _onlineHandViewer.GetBoardNumber();
            string dealerDirection_HandViewer = _onlineHandViewer.GetDealerDirection();
            string vulnerability_HandViewer = _onlineHandViewer.GetVulnerabilityDirections();

            //Assert
            Assert.IsTrue(boardNumber_SavedHands.SequenceEqual(boardNumber_HandViewer));
            Assert.IsTrue(dealerDirection_SavedHands.SequenceEqual(dealerDirection_HandViewer));
            Assert.IsTrue(vulnerability_SavedHands.SequenceEqual(vulnerability_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyCardsInPlayerHandsAndDirection()
        {
            //Arrange

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            List<Tuple<string, string>> directionAndName_OnlineArchive = _commonCardsGame.GetDirectionAndPlayerName();
            List<Tuple<string, List<string>>> northHandSuitsAndCards_SavedHands = _commonCardsGame.GetSuitsAndCardsInNorthPlayerHand(boardID);
            List<Tuple<string, List<string>>> westHandSuitsAndCards_SavedHands = _commonCardsGame.GetSuitsAndCardsInWestPlayerHand(boardID);
            List<Tuple<string, List<string>>> eastHandSuitsAndCards_SavedHands = _commonCardsGame.GetSuitsAndCardsInEastPlayerHand(boardID);
            List<Tuple<string, List<string>>> southHandSuitsAndCards_SavedHands = _commonCardsGame.GetSuitsAndCardsInSouthPlayerHand(boardID);
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            List<Tuple<string, string>> directionAndName_HandViewer = _onlineHandViewer.GetDirectionAndPlayerName_SavedHands();
            List<Tuple<string, List<string>>> northHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInNorthPlayerHand();
            List<Tuple<string, List<string>>> westHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInWestPlayerHand();
            List<Tuple<string, List<string>>> eastHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInEastPlayerHand();
            List<Tuple<string, List<string>>> southHandSuitsAndCards_HandViewer = _onlineHandViewer.GetSuitsAndCardsInSouthPlayerHand();

            //Assert
            Assert.IsTrue(Assertions.AssertionResultsForNameAndDirection(directionAndName_OnlineArchive, directionAndName_HandViewer));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(northHandSuitsAndCards_SavedHands, northHandSuitsAndCards_HandViewer));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(westHandSuitsAndCards_SavedHands, westHandSuitsAndCards_HandViewer));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(eastHandSuitsAndCards_SavedHands, eastHandSuitsAndCards_HandViewer));
            Assert.IsTrue(Assertions.AssertionResultsForSuitsAndCards(southHandSuitsAndCards_SavedHands, southHandSuitsAndCards_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyRandomPlayerHandDetails()
        {
            //Arrange

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            string randomPlayerDirection = _commonCardsGame.SelectAndGetRandomPlayerDirection();
            PlayerHandSummary playerHandDetails_OnlineArchive = _commonCardsGame.GetPlayerHandDetailsSummary();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            PlayerHandSummary playerHandDetails_HandViewer = _onlineHandViewer.GetPlayerHandDetailsSummary_SavedHands(randomPlayerDirection);

            //Assert
            Assert.IsTrue(Assertions.AssertionResultsForPlayerHandDetails(playerHandDetails_OnlineArchive, playerHandDetails_HandViewer));
        }

        [TestMethod]
        public void TC_OpenHandsAndBidding_VerfiyBiddingSequence()
        {
            //Arrange

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            BiddingSequence biddingSequence_OnlineArchive = _commonCardsGame.GetBiddingSequence();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
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
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            _commonCardsGame.OpenHandsAndBiddingPopUp(boardID);
            BiddingSummary biddingSummary_OnlineArchive = _commonCardsGame.GetBiddingSummary();
            _commonCardsGame.CloseHandsAndBiddingPopUp();

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
            BiddingSummary biddingSummary_HandViewer = _onlineHandViewer.GetBiddingSummary();

            //Assert
            Assert.IsTrue(Assertions.AssertionResultsForBiddingSummary(biddingSummary_OnlineArchive, biddingSummary_HandViewer));
        }

        [TestMethod]
        public void TC_OpenSavedHands_VerifyBoardScore()
        {
            //Arrange

            //Act
            //Crads game website
            int pageNumber = _savedHands.GetRandomPageNumberFromSavedWebPages();
            _savedHands.SelectRequiredPageAndWaitForPageLoad(pageNumber);
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            string eventName = _savedHands.GetEventName(boardID);
            string eventDate = _savedHands.GetEventDate(boardID);
            string boardNumber = _commonCardsGame.GetBoardNumber(boardID);
            string score_SavedHands = _commonCardsGame.GetScore(boardID);
            string tableScore_SavedHands = _commonCardsGame.GetTableScore(boardID);
            string scoreType_SavedHands = _commonCardsGame.GetScoreType(boardID);

            //Online handviewer tool
            _onlineHandViewer.OpenRequiredHTMLFileUsingEventDate(eventDate);
            string eventNumber = _onlineHandViewer.GetEventNumberFromEventName(eventName);
            string score_HandViewer = _onlineHandViewer.GetScore_SavedHands(eventNumber, boardNumber);
            string tableScore_HandViewer = _onlineHandViewer.GetTableScore_SavedHands(eventNumber, boardNumber);
            string scoreType_HandViewer = _onlineHandViewer.GetScoreType_SavedHands(score_HandViewer);

            //Assert
            Assert.IsTrue(score_SavedHands.SequenceEqual(score_HandViewer));
            Assert.IsTrue(tableScore_SavedHands.SequenceEqual(tableScore_HandViewer));
            Assert.IsTrue(scoreType_SavedHands.SequenceEqual(scoreType_HandViewer));
        }

        [TestMethod]
        public void TC_OpenSavedHands_VerfiyAllColumnsExceptTableScoresForBoards()
        {
            //Arrange
            _savedHands.WaitForPlayerPositionsToLoad();
            _commonCardsGame.DeleteExistingLogs();

            try
            {
                //Act
                _commonCardsGame.CreateAllDataLogFileAndAppendTitles();
                _commonCardsGame.IntializeHTMLReporter();
                int totalPages = 14;
                for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
                {
                    try
                    {
                        _savedHands.SelectRequiredPageAndWaitForPageLoad(pageNumber);
                        List<int> boardIDs = _savedHands.GetAllBoardIDsFromSelectedPage();

                        for (int board = 0; board < boardIDs.Count; board++)
                        {
                            //cards game
                            _onlineHandViewer.CloseLastTabAndSwitchToFirstTab();
                            string eventName = _savedHands.GetEventName(boardIDs[board]);
                            BoardDetails boardDetails_SavedHands = _commonCardsGame.GetBoardDetails_ExceptTableScores(boardIDs[board]);

                            //hand viewer
                            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardIDs[board]);
                            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
                            BoardDetails boardDetails_HandViewer = _onlineHandViewer.GetBoardDetails_SavedHands_ExceptTableScores();

                            //Assert
                            _commonCardsGame.AssertAndLogBoardDetails(boardDetails_SavedHands, boardDetails_HandViewer, boardIDs[board], eventName, pageNumber);
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
        public void TC_OpenSavedHands_VerfiyAllColumnsForBoards()
        {
            //Arrange
            _savedHands.WaitForPlayerPositionsToLoad();
            _commonCardsGame.DeleteExistingLogs();

            try
            {
                //Act
                _commonCardsGame.CreateAllDataLogFileAndAppendTitles();
                _commonCardsGame.IntializeHTMLReporter();
                int totalPages = _commonCardsGame.GetAllPages();
                for (int pageNumber = 15; pageNumber <= totalPages; pageNumber++)
                {
                    try
                    {
                        _savedHands.SelectRequiredPageAndWaitForPageLoad(pageNumber);
                        List<int> boardIDs = _savedHands.GetAllBoardIDsFromSelectedPage();

                        for (int board = 0; board < boardIDs.Count; board++)
                        {
                            //cards game
                            _commonCardsGame.SwitchToCardsGameWebsite();
                            string eventDate = _savedHands.GetEventDate(boardIDs[board]);
                            string eventName = _savedHands.GetEventName(boardIDs[board]);
                            string boardNumber_SavedHands = _commonCardsGame.GetBoardNumber(boardIDs[board]);
                            BoardDetails boardDetails_SavedHands = _commonCardsGame.GetBoardDetails(boardIDs[board]);

                            //hand viewer
                            _onlineHandViewer.OpenSavedHTMLFileInNewTab(eventDate, eventName);
                            BoardDetails boardDetails_HandViewer = _onlineHandViewer.GetBoardDetails_SavedHands(eventName,boardNumber_SavedHands);

                            //Assert
                            _commonCardsGame.AssertAndLogBoardDetails(boardDetails_SavedHands, boardDetails_HandViewer, boardIDs[board], eventName, pageNumber);
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
        public void TC_OpenSavedHands_VerfiyOpeningLeadInfo()
        {
            //Arrange

            //Act
            //Crads game website
            _savedHands.SelectRandomPageNumber();
            int boardID = _savedHands.GetRandomBoardIDFromTheSelectedPage();
            string openingLeadCard_OnlineArchive = _commonCardsGame.GetOpeningLeadCard(boardID);
            List<string> openingLeadInfo_OnlineArchive = _commonCardsGame.GetOpeningLeadInfo(boardID);

            //Online handviewer tool
            string linFile = _onlineHandViewer.GetLinFileForRandomBoardID(boardID);
            _onlineHandViewer.LaunchAndSwitchToHandViewerTool(linFile);
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
