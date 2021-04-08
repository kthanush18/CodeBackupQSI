using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails.Periodical
{
    public class Newspaper
    {
        private IWebElement newspaperTitle;

        public IWebElement NewspaperTitle { get => newspaperTitle; set => newspaperTitle = value; }
    }
}
