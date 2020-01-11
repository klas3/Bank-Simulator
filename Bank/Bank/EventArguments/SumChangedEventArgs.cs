using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.EventArguments
{
    class SumChangedEventArgs : EventArgs
    {
        public float Sum { get; set; }

        public SumChangedEventArgs(float sum)
        {
            Sum = sum;
        }
    }
}
