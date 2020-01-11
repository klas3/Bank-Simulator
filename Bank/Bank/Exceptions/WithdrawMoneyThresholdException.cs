using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    class WithdrawMoneyThresholdException : Exception
    {
        public WithdrawMoneyThresholdException() : base(message: "Вы превысили лимит снятий денежных средств за этот месяц!") { }
    }
}
