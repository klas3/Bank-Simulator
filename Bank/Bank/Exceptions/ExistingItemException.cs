using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    class ExistingItemException : Exception
    {
        public ExistingItemException() : base(message: "Этот элемент уже существует!") { }
    }
}
