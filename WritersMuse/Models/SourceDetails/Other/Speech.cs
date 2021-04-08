using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails.Other
{
    public class Speech : CommonSourceDetails
    {
        private IWebElement phraseAuthor;
        private IWebElement workEditor;

        public IWebElement PhraseAuthor { get => phraseAuthor; set => phraseAuthor = value; }
        public IWebElement WorkEditor { get => workEditor; set => workEditor = value; }
    }
}
