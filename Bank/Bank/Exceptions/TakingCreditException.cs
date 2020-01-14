using System;

namespace Bank.Exceptions
{
    class TakingCreditException : Exception
    {
        public TakingCreditException() : base(message: "Ошибка взятия кредита!") { }
    }
}
