using System;

namespace Bank.Exceptions
{
    class BlockedCardException : Exception
    {
        public BlockedCardException() : base(message: "Карточка заблокирована!") { }
    }
}
