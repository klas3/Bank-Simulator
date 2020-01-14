using System;

namespace Bank.Exceptions
{
    class UnexistingItemException : Exception
    {
        public UnexistingItemException() : base(message: "Такого элемента не существует!") { }
    }
}
