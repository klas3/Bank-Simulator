using System.Collections.Generic;
using Bank.Accounts;
using Bank.Exceptions;

namespace Bank.Collections
{
    sealed class AccountsCollection
    {
        private Dictionary<int, Account> accounts;

        public AccountsCollection()
        {
            this.accounts = new Dictionary<int, Account>();
        }

        public void AddAccount(Account account)
        {
            if (!accounts.ContainsKey(account.AccountId))
            {
                accounts.Add(account.AccountId, account);
            }
            else
            {
                throw new ExistingItemException();
            }
        }

        public void RemoveAccount(int accountId)
        {
            if (accounts.ContainsKey(accountId))
            {
                accounts.Remove(accountId);
            }
            else
            {
                throw new UnexistingItemException();
            }
        }

        public Account GetAccountById(int id)
        {
            foreach(KeyValuePair<int, Account> account in accounts)
            {
                if(account.Value.AccountId == id)
                {
                    return account.Value;
                }
            }

            return null;
        }
    }
}
