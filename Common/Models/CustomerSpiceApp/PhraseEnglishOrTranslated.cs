using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp
{
    public class PhraseEnglishOrTranslated : IComparable<PhraseEnglishOrTranslated>
    {
        public string EnglishOrTranslated { get; set; }
        public string PhraseText { get; set; }

        public int CompareTo(PhraseEnglishOrTranslated phraseEnglishOrTranslated)
        {
            return this.PhraseText.CompareTo(phraseEnglishOrTranslated.PhraseText);
        }
    }
}
