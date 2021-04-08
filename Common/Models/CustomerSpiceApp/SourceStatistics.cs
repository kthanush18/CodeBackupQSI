using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp
{
    public class SourceStatistics
    {
        public int TotalWorksFromUI { get; set; }
        public int TranslatedWorksFromUI { get; set; }
        public int EnglishSourceFromUI { get; set; }
        public int TranslatedSourceFromUI { get; set; }
        public int TotalWorksFromDB { get; set; }
        public int TranslatedWorksFromDB { get; set; }
        public int EnglishSourceFromDB { get; set; }
        public int TranslatedSourceFromDB { get; set; } 
    }
}
