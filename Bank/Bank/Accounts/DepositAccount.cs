using System;
using Bank.Cards;
using Bank.Exceptions;

namespace Bank.Accounts
{
    sealed class DepositAccount : Account
    {
        public DateTime DayForWithdraw { get; private set; }

        public DepositAccount(int customerId) : base(customerId) { }

        public override void RefillCard(float count, Card card)
        {
            if (DateTime.Compare(DateTime.Now, DayForWithdraw) >= 0)
            {
                base.RefillCard(count, card);
            }
            else
            {
                throw new MoneyWithdrawBeforeTermException();
            }
        }

        public override void Refill(float sum)
        {
            this.DayForWithdraw = DateTime.Now.AddMonths(12);
            base.Refill(sum);
        }

        public override void RefillAnotherAccount(Account accountForRefill, float sum)
        {
            if (DateTime.Compare(DateTime.Now, DayForWithdraw) >= 0)
            {
                base.RefillAnotherAccount(accountForRefill, sum);
            }
            else
            {
                throw new MoneyWithdrawBeforeTermException();
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} Day for withdraw: {DayForWithdraw}";
        }
    }
}
