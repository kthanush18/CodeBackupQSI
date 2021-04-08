using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models.SourceDetails.Other
{
    public class Television : CommonSourceDetails
    {
        private IWebElement seriesTitle;
        private IWebElement director;
        private IWebElement network;
        private IWebElement episodeAirDate;

        public IWebElement SeriesTitle { get => seriesTitle; set => seriesTitle = value; }
        public IWebElement Director { get => director; set => director = value; }
        public IWebElement Network { get => network; set => network = value; }
        public IWebElement EpisodeAirDate { get => episodeAirDate; set => episodeAirDate = value; }
    }
}
