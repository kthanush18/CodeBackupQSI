using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Quant.Spice.Test.UI.Common.Web
{
    public class Log : ILogger
    {
        public void WriteLine(string messageToLog)
        {
            // Implement code here for writing the log information to file
            // As of now writing the information to console using Debug class
            Debug.WriteLine(messageToLog);
        }

        public void LogException(Exception ex, [Optional]string customText)
        {
            string customMessageText = string.IsNullOrEmpty(customText) ? "" : customText + "\n\n";

            Debug.WriteLine($"{customMessageText} Error:\n {ex.Message} \n\n StackTrace:\n {ex.StackTrace}");
        }
    }
}
