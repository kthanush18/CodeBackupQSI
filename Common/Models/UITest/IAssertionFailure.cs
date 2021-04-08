using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common.Models.UITest
{
    public class  IAssertionFailure <T>
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public List<T> ExpectedValues { get; set; }
        public List<T> ActualValues { get; set; }
        public T ExpectedValue { get; set; }
        public T ActualValue { get; set; }
    }
}
