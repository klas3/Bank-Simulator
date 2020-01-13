using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    class SameItemsTransactionException : Exception
    {
        public SameItemsTransactionException() : base(message: "Вы переводите деньги между 2 одинаковыми элементами!") { }
    }
}
