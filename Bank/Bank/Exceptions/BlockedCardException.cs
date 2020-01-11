using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    class BlockedCardException : Exception
    {
        public BlockedCardException() : base(message: "Карточка заблокирована!") { }
    }
}
