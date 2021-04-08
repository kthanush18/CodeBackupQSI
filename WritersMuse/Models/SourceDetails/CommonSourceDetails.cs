using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Web.WritersMuse.Models
{
    public class CommonSourceDetails
    {
        private IWebElement title;
        private IWebElement referenceURL;
        private IWebElement year;
        private IWebElement author;
        private IWebElement publisher;
        private IWebElement city;
        private IWebElement iSBN;
        private IWebElement issueDate;
        private IWebElement volume;

        protected IWebElement Title { get => title; set => title = value; }
        protected IWebElement ReferenceURL { get => referenceURL; set => referenceURL = value; }
        protected IWebElement Year { get => year; set => year = value; }
        protected IWebElement Author { get => author; set => author = value; }
        protected IWebElement Publisher { get => publisher; set => publisher = value; }
        protected IWebElement City { get => city; set => city = value; }
        protected IWebElement ISBN { get => iSBN; set => iSBN = value; }
        protected IWebElement IssueDate { get => issueDate; set => issueDate = value; }
        protected IWebElement Volume { get => volume; set => volume = value; }
    }
}
