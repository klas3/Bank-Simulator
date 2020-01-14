using System;

namespace Bank.Exceptions
{
    class SumExcessException : Exception
    {
        public SumExcessException() : base(message: "Превышена сумма!") { }
    }
}
