using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    class SumExcessException : Exception
    {
        public SumExcessException() : base(message: "Превышена сумма!") { }
    }
}
