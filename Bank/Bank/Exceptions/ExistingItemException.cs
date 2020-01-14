using System;

namespace Bank.Exceptions
{
    class ExistingItemException : Exception
    {
        public ExistingItemException() : base(message: "Этот элемент уже существует!") { }
    }
}
