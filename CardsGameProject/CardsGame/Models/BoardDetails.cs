using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.CardsGame.UITests.Web.CardsGame.Models
{
    public class BoardDetails
    {
        public int BoardNumber { get; set; }
        public string Dealer { get; set; }
        public string Type { get; set; }
        public string Score { get; set; }
        public string Vulnerability { get; set; }
        public PlayerNames AllPlayers { get; set; }
        public bool IsDefending { get; set; }
        public string Declarer { get; set; }
        public string OpeningHand { get; set; }
        public string TableScore { get; set; }
        public string Contract { get; set; }
        public string OpeningLeadCard { get; set; }
        public List<string> OpeningLeadInfo { get; set; }

    }
}
