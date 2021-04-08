using System;
using System.Collections.Generic;

namespace Quant.CardsGame.UITests.Web.CardsGame.Models
{
    public class HandsAndBiddingInfo
    {
        public int BoardNumber { get; set; }
        public string Dealer { get; set; }
        public string Vulnerability { get; set; }
        public List<Tuple<string, string>> DirectionAndName { get; set; }
        public List<Tuple<string, List<string>>> NorthHandSuitsAndCards { get; set; }
        public List<Tuple<string, List<string>>> WestHandSuitsAndCards { get; set; }
        public List<Tuple<string, List<string>>> EastHandSuitsAndCards { get; set; }
        public List<Tuple<string, List<string>>> SouthHandSuitsAndCards { get; set; }
        public PlayerHandSummary PlayerHandSummary { get; set; }
        public BiddingSequence BiddingSequence { get; set; }
        public BiddingSummary BiddingSummary { get; set; }

    }
}
