using Bank.Cards;
using System;
using System.Windows;
using Bank.Exceptions;

namespace Bank.Accounts
{
    sealed class SavingAccount : Account
    {
        public int WithdrawCountForMonth { get; private set; }

        public SavingAccount(int customerId)
        {
            this.Balance = 100;
            this.WithdrawCountForMonth = 0;
            this.CustomerId = customerId;
        }

        public override void RefillCard(float count, Card card)
        {
            if (WithdrawCountForMonth < 6)
            {
                base.RefillCard(count, card);
                WithdrawCountForMonth++;
            }
            else
            {
                throw new WithdrawMoneyThresholdException();
            }
        }

        public override void RefillAnotherAccount(Account accountForRefill, float count)
        {
            if (WithdrawCountForMonth < 6)
            {
                base.RefillAnotherAccount(accountForRefill, count);
                WithdrawCountForMonth++;
            }
            else
            {
                throw new WithdrawMoneyThresholdException();
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} Withdraws for month: {WithdrawCountForMonth}";
        }
    }
}
