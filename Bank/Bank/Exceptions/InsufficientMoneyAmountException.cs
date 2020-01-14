using System;

namespace Bank.Exceptions
{
    class InsufficientMoneyAmountException : Exception
    {
        public InsufficientMoneyAmountException() : base(message: "Недостаточно денег на счету!") { }
    }
}
