using Bank.Cards;
using System;
using Bank.Exceptions;
using Bank.EventArguments;

namespace Bank.Accounts
{
    abstract class Account
    {
        private static int count = 1;

        private const float startBalance = 0;

        public int AccountId { get; private set; }
        public int CustomerId { get; private set; }
        public float Balance { get; private set; }

        public Account(int customerId)
        {
            this.AccountId = count;
            this.CustomerId = customerId;
            this.Balance = startBalance;

            count++;
        }

        public Account(int customerId, float balance)
        {
            this.AccountId = count;
            this.CustomerId = customerId;
            this.Balance = balance;

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

