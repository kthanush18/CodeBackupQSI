using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.CardsGame.UITests.Web.CardsGame.Models
{
    public class BiddingSequence
    {
        public Tuple<string, List<string>> WestHandBidding { get; set; }
        public Tuple<string, List<string>> NorthHandBidding { get; set; }
        public Tuple<string, List<string>> EastHandBidding { get; set; }
        public Tuple<string, List<string>> SouthHandBidding { get; set; }
    }
}
