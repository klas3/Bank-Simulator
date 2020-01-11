using System;
using System.Windows.Forms;
using Bank.Accounts;
using Bank.EventArguments;
using Bank.Exceptions;

namespace Bank.Forms
{
    partial class Transfer : Form
    {
        private Account account;
        private Bank bank;

        public Transfer(Account account, Bank bank)
        {
            InitializeComponent();
            this.account = account;
            this.bank = bank;
        }

        private void OnRefillAnotherAccount(object sender, RefillAnotherAccountEventArgs e)
        {
            MessageBox.Show($"Вы успешно перечислили ${e.Sum} на счет №{e.Account.AccountId}");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int sum;
            int id;

            if(int.TryParse(textBox1.Text, out id))
            {
                Account accountForRefill = bank.Accounts.GetAccountById(id);

                if (accountForRefill != null)
                {
                    if(int.TryParse(textBox2.Text, out sum) && sum > 0)
                    {
                        try
                        {
                            account.RefillAnotherAccountNotifier += OnRefillAnotherAccount;
                            account.RefillAnotherAccount(accountForRefill, int.Parse(textBox2.Text));
                            bank.CreateRecord("Перевод средств на другой счет.", sum, account.Balance, account.CustomerId);
                            account.RefillAnotherAccountNotifier -= OnRefillAnotherAccount;

                            this.Close();
                        }
                        catch(SameItemsTransactionException)
                        {
                            account.RefillAnotherAccountNotifier -= OnRefillAnotherAccount;
                            MessageBox.Show("Укажите другой счет для перевода!");
                        }
                        catch(MoneyWithdrawBeforeTermException exception)
                        {
                            account.RefillAnotherAccountNotifier -= OnRefillAnotherAccount;
                            MessageBox.Show(exception.Message);
                        }
                        catch(InsufficientMoneyAmountException exception)
                        {
                            account.RefillAnotherAccountNotifier -= OnRefillAnotherAccount;
                            MessageBox.Show(exception.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильно введенная сумма!");
                    }
                }
                else
                {
                    MessageBox.Show("Такого счета не существует!");
                }
            }
            else
            {
                MessageBox.Show("Неправильно введенный номер счета!");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
        }
    }
}
