using System;

namespace Bank.Exceptions
{
    class WithdrawMoneyThresholdException : Exception
    {
        public WithdrawMoneyThresholdException() : base(message: "Вы превысили лимит снятий денежных средств за этот месяц!") { }
    }
}
