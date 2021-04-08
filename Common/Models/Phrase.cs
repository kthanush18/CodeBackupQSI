using System;

namespace Quant.Spice.Test.UI.Common.Models
{
    public class Phrase : IComparable<Phrase>
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public int CompareTo(Phrase phrase)
        {
            return this.Text.CompareTo(phrase.Text);
        }
    }
}
