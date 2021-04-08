using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails.Book
{
    public class WorkInAnthology : CommonSourceDetails
    {
        private IWebElement sectionAuthor;
        private IWebElement anthologyTitle;

        public IWebElement SectionAuthor { get => sectionAuthor; set => sectionAuthor = value; }
        public IWebElement AnthologyTitle { get => anthologyTitle; set => anthologyTitle = value; }
    }
}
