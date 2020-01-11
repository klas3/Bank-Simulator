using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    class TakingCreditException : Exception
    {
        public TakingCreditException() : base(message: "Ошибка взятия кредита!") { }
    }
}
