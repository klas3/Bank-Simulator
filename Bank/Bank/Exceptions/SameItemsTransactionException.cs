using System;

namespace Bank.Exceptions
{
    class SameItemsTransactionException : Exception
    {
        public SameItemsTransactionException() : base(message: "Вы переводите деньги между 2 одинаковыми элементами!") { }
    }
}
