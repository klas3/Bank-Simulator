using System.Collections.Generic;
using Bank.Other;

namespace Bank.Collections
{
    sealed class CustomersCollection
    {
        private Dictionary<int, Customer> customers;

        public CustomersCollection()
        {
            this.customers = new Dictionary<int, Customer>();
        }

        public Customer GetCustomer(string email)
        {
            foreach (KeyValuePair<int, Customer> entry in customers)
            {
                if (entry.Value.Email == email)
                {
                    return entry.Value;
                }
            }

            return null;
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer.Id, customer);
        }
    }
}
