using System;
using Bank.Accounts;
using Bank.Cards;
using Bank.EventArguments;

namespace Bank.Other
{
    class Customer
    {
        private static int cutomersCount = 1;

        private string customerName;
        private string customerPassword;

        private SavingAccount savingAccount;
        private CreditCard creditCard;
        private PaymentsCard paymentsCard;

        public int Id { get; private set; }
        public string Email { get; private set; }
        public int Age { get; private set; }

        public Customer(string name, string email, string password, int age)
        {
            this.Id = cutomersCount;
            this.customerName = name;
            this.Email = email;
            this.customerPassword = password;
            this.Age = age;

            cutomersCount++;
        }

        public void AssignPaymentsMean(PaymentsMean mean, PaymentsMeans type)
        {
            if(mean != null)
            {
                if(type == PaymentsMeans.CreditCard)
                {
                    creditCard = (CreditCard)mean;
                }
                else if(type == PaymentsMeans.PaymentsCard)
                {
                    paymentsCard = (PaymentsCard)mean;
                }
                else if(type == PaymentsMeans.SavingAccount)
                {
                    savingAccount = (SavingAccount)mean;
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void UnassignPaymentsMean(PaymentsMeans type)
        {
            if (type == PaymentsMeans.CreditCard)
            {
                creditCard = null;
            }
            else if (type == PaymentsMeans.PaymentsCard)
            {
                paymentsCard = null;
            }
            else if (type == PaymentsMeans.SavingAccount)
            {
                savingAccount = null;
            }
        }

        public (string, string, string, string) GetInfo()
        {
            return (customerName, Email, customerPassword, Age.ToString());
        }

        public void TransferFromCardToCard(Card cardFrom, Card cardTo, float sum)
        {
            cardFrom.Withdraw(sum);
            cardTo.Refill(sum);

            TransferFromCardToCardNotifier?.Invoke(this, new TransferFromCardToCardEventArgs(cardFrom, cardTo, sum));
        }

        public (CreditCard, SavingAccount, PaymentsCard) GetCardsAndAccountInfo()
        {
            return (creditCard, savingAccount, paymentsCard);
        }

        public override string ToString()
        {
            return $"Name: {customerName}; Email: {Email}; Age: {Age};";
        }

        public event EventHandler<TransferFromCardToCardEventArgs> TransferFromCardToCardNotifier;
    }
}
