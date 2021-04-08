namespace Quant.Spice.Test.UI.Common.Web
{
    public class WebPage
    {
        protected WebBrowser _browser;

        public WebPage(WebBrowser browser)
        {
            _browser = browser;

        }

        private static Log _logInfo;
        public static Log LogInfo
        {
            get
            {
                _logInfo = new Log();
                return _logInfo;
            }
            set
            {
                _logInfo = value;
            }
        }
    }
}
