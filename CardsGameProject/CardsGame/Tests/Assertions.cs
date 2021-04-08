using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quant.CardsGame.UITests.Web.CardsGame.Models;
using Quant.CardsGame.UITests.Web.CardsGame.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.CardsGame.UITests.Web.CardsGame.Tests
{
    public class Assertions 
    {
        public static bool AssertionResultsForNameAndDirection(List<Tuple<string, string>> directionAndName_OnlineArchive, List<Tuple<string, string>> directionAndName_HandViewer)
        {
            try
            {
                Assert.IsTrue(directionAndName_OnlineArchive.Find(x => x.Item1 == "N").Item2.SequenceEqual(directionAndName_HandViewer.Find(x => x.Item1 == "N").Item2));
                Assert.IsTrue(directionAndName_OnlineArchive.Find(x => x.Item1 == "W").Item2.SequenceEqual(directionAndName_HandViewer.Find(x => x.Item1 == "W").Item2));
                Assert.IsTrue(directionAndName_OnlineArchive.Find(x => x.Item1 == "E").Item2.SequenceEqual(directionAndName_HandViewer.Find(x => x.Item1 == "E").Item2));
                Assert.IsTrue(directionAndName_OnlineArchive.Find(x => x.Item1 == "S").Item2.SequenceEqual(directionAndName_HandViewer.Find(x => x.Item1 == "S").Item2));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static bool AssertionResultsForSuitsAndCards(List<Tuple<string, List<string>>> handSuitsAndCards_OnlineArchive, List<Tuple<string, List<string>>> handSuitsAndCards_HandViewer)
        {
            try
            {
                Assert.IsTrue(handSuitsAndCards_OnlineArchive.Find(x => x.Item1 == "♠").Item2.SequenceEqual(handSuitsAndCards_HandViewer.Find(x => x.Item1 == "♠").Item2));
                Assert.IsTrue(handSuitsAndCards_OnlineArchive.Find(x => x.Item1 == "♥").Item2.SequenceEqual(handSuitsAndCards_HandViewer.Find(x => x.Item1 == "♥").Item2));
                Assert.IsTrue(handSuitsAndCards_OnlineArchive.Find(x => x.Item1 == "♦").Item2.SequenceEqual(handSuitsAndCards_HandViewer.Find(x => x.Item1 == "♦").Item2));
                Assert.IsTrue(handSuitsAndCards_OnlineArchive.Find(x => x.Item1 == "♣").Item2.SequenceEqual(handSuitsAndCards_HandViewer.Find(x => x.Item1 == "♣").Item2));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static bool AssertionResultsForPlayerHandDetails(PlayerHandSummary playerHandDetails_CardsGame, PlayerHandSummary playerHandDetails_HandViewer)
        {
            try
            {
                Assert.IsTrue(playerHandDetails_CardsGame.PlayerName.SequenceEqual(playerHandDetails_HandViewer.PlayerName));
                Assert.IsTrue(playerHandDetails_CardsGame.Spades.SequenceEqual(playerHandDetails_HandViewer.Spades));
                Assert.IsTrue(playerHandDetails_CardsGame.Hearts.SequenceEqual(playerHandDetails_HandViewer.Hearts));
                Assert.IsTrue(playerHandDetails_CardsGame.Diamonds.SequenceEqual(playerHandDetails_HandViewer.Diamonds));
                Assert.IsTrue(playerHandDetails_CardsGame.Clubs.SequenceEqual(playerHandDetails_HandViewer.Clubs));
                Assert.IsTrue(playerHandDetails_CardsGame.HighCardPoints.SequenceEqual(playerHandDetails_HandViewer.HighCardPoints));
                Assert.IsTrue(playerHandDetails_CardsGame.HasVoid.Equals(playerHandDetails_HandViewer.HasVoid));
                Assert.IsTrue(playerHandDetails_CardsGame.HasSingleton.Equals(playerHandDetails_HandViewer.HasSingleton));
                Assert.IsTrue(playerHandDetails_CardsGame.HasMultipleSingletons.Equals(playerHandDetails_HandViewer.HasMultipleSingletons));
                Assert.IsTrue(playerHandDetails_CardsGame.HasDoubleton.Equals(playerHandDetails_HandViewer.HasDoubleton));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static bool AssertionResultsForBiddingSequence(BiddingSequence biddingSequence_OnlineArchive, BiddingSequence biddingSequence_HandViewer)
        {
            try
            {
                Assert.IsTrue(biddingSequence_OnlineArchive.WestHandBidding.Item2.SequenceEqual(biddingSequence_HandViewer.WestHandBidding.Item2));
                Assert.IsTrue(biddingSequence_OnlineArchive.NorthHandBidding.Item2.SequenceEqual(biddingSequence_HandViewer.NorthHandBidding.Item2));
                Assert.IsTrue(biddingSequence_OnlineArchive.EastHandBidding.Item2.SequenceEqual(biddingSequence_HandViewer.EastHandBidding.Item2));
                Assert.IsTrue(biddingSequence_OnlineArchive.SouthHandBidding.Item2.SequenceEqual(biddingSequence_HandViewer.SouthHandBidding.Item2));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static bool AssertionResultsForBiddingSummary(BiddingSummary biddingSummary_OnlineArchive, BiddingSummary biddingSummary_HandViewer)
        {
            try
            {
                Assert.IsTrue(biddingSummary_OnlineArchive.NoOfPasses.SequenceEqual(biddingSummary_HandViewer.NoOfPasses));
                Assert.IsTrue(biddingSummary_OnlineArchive.OpeningBid.SequenceEqual(biddingSummary_HandViewer.OpeningBid));
                Assert.IsTrue(biddingSummary_OnlineArchive.OpeningBidResponse.SequenceEqual(biddingSummary_HandViewer.OpeningBidResponse));
                Assert.IsTrue(biddingSummary_OnlineArchive.Overcall.SequenceEqual(biddingSummary_HandViewer.Overcall));
                Assert.IsTrue(biddingSummary_OnlineArchive.LevelOfOvercall.SequenceEqual(biddingSummary_HandViewer.LevelOfOvercall));
                Assert.IsTrue(biddingSummary_OnlineArchive.OvercallAt.SequenceEqual(biddingSummary_HandViewer.OvercallAt));
                Assert.IsTrue(biddingSummary_OnlineArchive.OvercallResponse.Equals(biddingSummary_HandViewer.OvercallResponse));
                Assert.IsTrue(biddingSummary_OnlineArchive.Contract.Equals(biddingSummary_HandViewer.Contract));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
