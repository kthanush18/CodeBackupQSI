using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails.Other
{
    public class Film : CommonSourceDetails
    {
        private IWebElement director;
        private IWebElement actors;
        private IWebElement productionCompany;

        public IWebElement Director { get => director; set => director = value; }
        public IWebElement Actors { get => actors; set => actors = value; }
        public IWebElement ProductionCompany { get => productionCompany; set => productionCompany = value; }
    }
}
