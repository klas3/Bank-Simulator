using System.Collections.Generic;
using Bank.Other;
using Bank.Exceptions;

namespace Bank.Collections
{
    sealed class CustomersCollection
    {
        private Dictionary<int, Customer> customers;

        public CustomersCollection()
        {
            this.customers = new Dictionary<int, Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            if(!customers.ContainsKey(customer.Id))
            {
                customers.Add(customer.Id, customer);
            }
            else
            {
                throw new ExistingItemException();
            }
        }

        public void RemoveCustomer(int customerId)
        {
            if(customers.ContainsKey(customerId))
            {
                customers.Remove(customerId);
            }
            else
            {
                throw new UnexistingItemException();
            }
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
    }
}
