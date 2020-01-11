using System;
using Bank.Accounts;

namespace Bank.EventArguments
{
    class RefillAnotherAccountEventArgs : SumChangedEventArgs
    {
        public Account Account { get; private set; }

        public RefillAnotherAccountEventArgs(Account account, float sum) : base(sum)
        {
            Account = account;
        }
    }
}
