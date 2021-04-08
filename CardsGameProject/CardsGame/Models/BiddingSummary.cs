using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.CardsGame.UITests.Web.CardsGame.Models
{
    public class BiddingSummary
    {
        public string NoOfPasses { get; set; }
        public string OpeningBid { get; set; }
        public string OpeningBidResponse { get; set; }
        public string Overcall { get; set; }
        public string LevelOfOvercall { get; set; }
        public string OvercallAt { get; set; }
        public string OvercallResponse { get; set; }
        public string Contract { get; set; }
    }
}
