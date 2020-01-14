using Bank.Cards;
using System;
using Bank.Exceptions;
using Bank.EventArguments;

namespace Bank.Accounts
{
    abstract class Account : PaymentsMean
    {
        private static int count = 1;

        private const float startBalance = 0;

        public int AccountId { get; private set; }

        public Account(int customerId) : base(customerId, startBalance)
        {
            this.AccountId = count;

            count++;
        }

        public Account(int customerId, float balance) : base(customerId, balance)
        {
            this.AccountId = count;

            count++;
        }

        public virtual void Refill(float sum)
        {
            this.Balance += sum;
            RefillAccountNotifier?.Invoke(this, new SumChangedEventArgs(sum));
        }

        public virtual void Withdraw(float sum)
        {
            if(this.Balance - sum >= 0)
            {
                this.Balance -= sum;
                WithdrawAccountNotifier?.Invoke(this, new SumChangedEventArgs(sum));
            }
            else
            {
                throw new InsufficientMoneyAmountException();
            }
        }

        public virtual void RefillCard(float sum, Card card)
        {
            Withdraw(sum);
            card.Refill(sum);

            RefillCardNotifier?.Invoke(this, new RefillCardEventArgs(card, sum));
        }

        public virtual void RefillAnotherAccount(Account accountForRefill, float sum)
        {
            if (this.AccountId != accountForRefill.AccountId)
            {
                Withdraw(sum);
                accountForRefill.Refill(sum);

                RefillAnotherAccountNotifier?.Invoke(this, new RefillAnotherAccountEventArgs(accountForRefill, sum));
            }
            else
            {
                throw new SameItemsTransactionException();
            }
        }

        public override string ToString()
        {
            return $"Id: {AccountId}; Customer Id: {CustomerId}; Balance: {Balance}; ";
        }

        public event EventHandler<SumChangedEventArgs> RefillAccountNotifier;
        public event EventHandler<SumChangedEventArgs> WithdrawAccountNotifier;
        public event EventHandler<RefillCardEventArgs> RefillCardNotifier;
        public event EventHandler<RefillAnotherAccountEventArgs> RefillAnotherAccountNotifier;
    }
}

