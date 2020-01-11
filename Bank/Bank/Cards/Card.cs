using System;
using Bank.Exceptions;
using Bank.EventArguments;

namespace Bank.Cards
{
    abstract partial class Card
    {
        private static int count = 1;

        private int startBalance = 100;

        public float Balance { get; protected set; }
        public int CardId { get; private set; }
        public bool IsBlocked { get; private set; }
        public string Number { get; private set; }
        public int CustomerId { get; protected set; }
        public int Pin { get; private set; }

        public Card()
        {
            this.IsBlocked = false;
            this.Pin = GeneratePin();
            this.Number = GenerateNumber();
            this.CardId = count;
            this.Balance = startBalance;

            count++;
        }

        public int GeneratePin()
        {
            Random rand = new Random();
            return rand.Next(1111, 9999);
        }

        public string GenerateNumber()
        {
            Random rand = new Random();
            string result = "";

            for (int i = 0; i < 16; i++)
            {
                result += rand.Next(0, 10).ToString();
            }

            return result;
        }

        public virtual void Withdraw(float sum)
        {
            if (IsBlocked)
            {
                throw new BlockedCardException();
            }

            if (Balance - sum < 0)
            {
                throw new InsufficientMoneyAmountException();
            }

            Balance -= sum;

            WithdrawCardNotifier?.Invoke(this, new SumChangedEventArgs(sum));
        }

        public virtual void Refill(float sum)
        {
            if (IsBlocked)
            {
                throw new BlockedCardException();
            }

            Balance += sum;

            RefillCardNotifier?.Invoke(this, new SumChangedEventArgs(sum));
        }

        public void ChangeCardStatus()
        {
            IsBlocked = !IsBlocked;
        }

        public override string ToString()
        {
            return $"Is blocked: {IsBlocked}; Card number: {Number}; Pin: {Pin}; ";
        }

        public event EventHandler<SumChangedEventArgs> RefillCardNotifier;
        public event EventHandler<SumChangedEventArgs> WithdrawCardNotifier;
    }
}
