using Bank.Cards;
using System;
using System.Windows;
using Bank.Exceptions;

namespace Bank.Accounts
{
    sealed class SavingAccount : Account
    {
        private const float startBalance = 100;
        private const int maxWithdrawCountForMonth = 6;

        public int WithdrawCountForMonth { get; private set; }

        public SavingAccount(int customerId) : base(customerId, startBalance)
        {
            this.WithdrawCountForMonth = 0;
        }

        public override void RefillCard(float count, Card card)
        {
            if (WithdrawCountForMonth < maxWithdrawCountForMonth)
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
            if (WithdrawCountForMonth < maxWithdrawCountForMonth)
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
