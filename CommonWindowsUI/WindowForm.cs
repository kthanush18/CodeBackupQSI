namespace Quant.Spice.Test.UI.Common.WindowsUI
{
    public class WindowForm
    {
        protected WindowUIDriver _windowUIDriver;

        public WindowForm(WindowUIDriver windowUIDriver)
        {
            _windowUIDriver = windowUIDriver;
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
