using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.Models.CustomerSpiceApp
{
    public class AccountDetails
    {
        public string UserNameFromUI { get; set; }
        public string NameFromUI { get; set; }
        public string EmailFromUI { get; set; }
        public string ExpirationDateFromUI { get; set; }
        public string UserNameFromDB { get; set; }
        public string NameFromDB { get; set; }
        public string EmailFromDB { get; set; }
        public string ExpirationDateFromDB { get; set; }
    }
}
