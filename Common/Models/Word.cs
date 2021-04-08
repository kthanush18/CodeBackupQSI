using System.Collections.Generic;

namespace Quant.Spice.Test.UI.Common.Models
{
    public class Word
    {
        public int ID { get; set; }

        public string Text { get; set; }

        public List<Meaning> Meanings { get; set; }

        public int CompareTo(Word word)
        {
            return this.Text.CompareTo(word.Text);
        }
    }
}
