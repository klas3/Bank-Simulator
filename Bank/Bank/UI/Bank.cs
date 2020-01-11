using Bank.Collections;
using Bank.Other;

namespace Bank
{
    sealed class Bank
    {
        public CustomersCollection Customers { get; private set; }
        public RecordsCollection Records { get; private set; }
        public AccountsCollection Accounts { get; private set; }
        public CardsCollection Cards { get; private set; }

        public Bank()
        {
            this.Customers = new CustomersCollection();
            this.Records = new RecordsCollection();
            this.Accounts = new AccountsCollection();
            this.Cards = new CardsCollection();
        }

        public Customer Login(string email, string password)
        {
            Customer customer = Customers.GetCustomer(email);

            if (customer != null)
            {
                return customer;
            }

            return null;
        }

        public void CreateRecord(string recordComment, float sumChange, float currentBalance, int customerId)
        {
            Record record = new Record(recordComment, sumChange, currentBalance, customerId);
            Records.AddRecord(record);
        }
    }
}
