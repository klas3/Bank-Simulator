namespace Bank.EventArguments
{
    class TakingCreditEventArgs : SumChangedEventArgs
    {
        public int Months { get; private set; }

        public TakingCreditEventArgs(int months, float sum) : base(sum)
        {
            Months = months;
        }
    }
}
