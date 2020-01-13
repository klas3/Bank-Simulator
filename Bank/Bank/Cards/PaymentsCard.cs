using Bank.Accounts;
using System;
using Bank.Exceptions;

namespace Bank.Cards
{
    sealed class PaymentsCard : Card
    {
        public DepositAccount DepositAccount { get; private set; }

        public PaymentsCard(int customerId) : base(customerId) { }

        public void AssignDepositAccount(DepositAccount account)
        {
            if(DepositAccount == null)
            {
                DepositAccount = account;
            }
            else
            {
                throw new ExistingItemException();
            }
        }
    }
}
