using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails.Other
{
    public class Lyric : CommonSourceDetails
    {
        private IWebElement composer;
        private IWebElement performer;
        private IWebElement nameOfCD;
        private IWebElement dateRecorded;

        public IWebElement Composer { get => composer; set => composer = value; }
        public IWebElement Performer { get => performer; set => performer = value; }
        public IWebElement NameOfCD { get => nameOfCD; set => nameOfCD = value; }
        public IWebElement DateRecorded { get => dateRecorded; set => dateRecorded = value; }
    }
}
