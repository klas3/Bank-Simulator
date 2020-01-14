using System;

namespace Bank.Exceptions
{
    class MoneyWithdrawBeforeTermException : Exception
    {
        public MoneyWithdrawBeforeTermException() : base(message: "Вы не можете снять деньги раньше срока!") { }
    }
}
