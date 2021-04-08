using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp
{
    public class PhraseYear : IComparable<PhraseYear>
    {
        public int Year { get; set; }
        public string PhraseText { get; set; }

        public int CompareTo(PhraseYear phraseYear)
        {
            return this.PhraseText.CompareTo(phraseYear.PhraseText);
        }
    }
}
