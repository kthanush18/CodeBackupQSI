using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.Models.UITest
{
    public class KeywordAssertionFailure <T> : IAssertionFailure <T>
    {
        public string Keyword { get; set; }
        public string Meaning { get; set; }
        public string Phrase { get; set; }
        public string NameOfAssertion { get; set; }

    }
}
