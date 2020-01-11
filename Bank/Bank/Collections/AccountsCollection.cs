using System.Collections.Generic;
using Bank.Accounts;

namespace Bank.Collections
{
    sealed class AccountsCollection
    {
        private Dictionary<int, Account> accounts;

        public AccountsCollection()
        {
            this.accounts = new Dictionary<int, Account>();
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

        public void AddAccount(Account account)
        {
            accounts.Add(account.AccountId, account);
        }
    }
}
