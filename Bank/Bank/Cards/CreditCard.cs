using System;
using Bank.Exceptions;
using Bank.EventArguments;

namespace Bank.Cards
{
    sealed class CreditCard : Card
    {
        private DateTime creditDate;
        private DateTime creditTerm;

        public float MaxCreditTakingCount { get; private set; }
        public bool IsCredit { get; private set; }
        public float LastCreditSum { get; private set; }
        public float CreditAccount { get; private set; }

        public CreditCard(int customerId)
        {
            this.CustomerId = customerId;
            this.IsCredit = false;
            this.CreditAccount = 100;
            this.MaxCreditTakingCount = 5000;
        }

        public void PayCredit()
        {
            if(IsCredit)
            {
                if(Balance - LastCreditSum >= 0)
                {
                    Balance -= LastCreditSum;
                    IsCredit = false;

                    PayCreditNotifier?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    throw new InsufficientMoneyAmountException();
                }
            }
        }

        public void TakeCredit(float sum, int months)
        {
            if (!IsBlocked)
            {
                if (!IsCredit)
                {
                    if(sum <= MaxCreditTakingCount)
                    {
                        this.creditDate = DateTime.Now;
                        this.creditTerm = DateTime.Now.AddMonths(months);
                        this.IsCredit = true;

                        LastCreditSum = sum;
                        Refill(sum);

                        TakingCreditNotifier?.Invoke(this, new TakingCreditEventArgs(months, sum));
                    }
                    else
                    {
                        throw new SumExcessException();
                    }
                }
                else
                {
                    throw new TakingCreditException();
                }
            }
            else
            {
                throw new BlockedCardException();
            }
        }

        public event EventHandler PayCreditNotifier;
        public event EventHandler<TakingCreditEventArgs> TakingCreditNotifier;
    }
}