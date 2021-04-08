using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common
{
    public class CommonMethods
    {
        public string RemoveExtraSpaces(string strSentence)
        {
            strSentence = Strings.Trim(strSentence);

            int i = Strings.InStr(strSentence, Constants.vbNewLine);

            while (((Strings.InStr(strSentence, "  ") > 0) | (Strings.InStr(strSentence, Constants.vbNewLine) > 0)))
            {
                strSentence = Strings.Replace(strSentence, "  ", " ");
                strSentence = Strings.Replace(strSentence, Constants.vbNewLine, " ");
            }

            return strSentence;
        }

    }
}
