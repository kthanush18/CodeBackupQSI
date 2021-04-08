using System;

namespace Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp
{
    public class UniqueUsesCount : IComparable<UniqueUsesCount>
    {
        public int UniqueUses { get; set; }
        public string PhraseText { get; set; }

        public int CompareTo(UniqueUsesCount uniqueUsesCount)
        {
            return this.PhraseText.CompareTo(uniqueUsesCount.PhraseText);
        }
    }
}
