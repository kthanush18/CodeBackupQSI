using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails
{
    public class ReferencedQuotation : CommonSourceDetails
    {
        private IWebElement phraseAuthor;

        public IWebElement PhraseAuthor { get => phraseAuthor; set => phraseAuthor = value; }
    }
}
