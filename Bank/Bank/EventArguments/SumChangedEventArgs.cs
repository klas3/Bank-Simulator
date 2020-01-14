using System;

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
