using System;
using System.Collections.Generic;

namespace Quant.Spice.Test.UI.Common.Models
{
    public class Meaning : IComparable<Meaning>
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public List<Phrase> Phrases { get; set; }

        public int CompareTo(Meaning meaning)
        {
            return this.Text.CompareTo(meaning.Text);
        }
    }
}
