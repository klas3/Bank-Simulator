using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    class MoneyWithdrawBeforeTermException : Exception
    {
        public MoneyWithdrawBeforeTermException() : base(message: "Вы не можете снять деньги раньше срока!") { }
    }
}
