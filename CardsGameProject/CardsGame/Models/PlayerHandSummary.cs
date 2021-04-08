using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.CardsGame.UITests.Web.CardsGame.Models
{
    public class PlayerHandSummary
    {
        public string PlayerName { get; set; }
        public string Spades { get; set; }
        public string Hearts { get; set; }
        public string Diamonds { get; set; }
        public string Clubs { get; set; }
        public string HighCardPoints { get; set; }
        public bool HasVoid { get; set; }
        public bool HasSingleton { get; set; }
        public bool HasMultipleSingletons { get; set; }
        public bool HasDoubleton { get; set; }

    }
}
