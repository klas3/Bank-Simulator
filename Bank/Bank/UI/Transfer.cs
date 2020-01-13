using System;
using System.Windows.Forms;
using Bank.Accounts;
using Bank.EventArguments;
using Bank.Exceptions;

namespace Bank.Forms
{
    partial class Transfer : Form
    {
        private const string wrongAccountNumberMessage = "Такого счета не существует!";
        private const string wrongFilledNumberMessage = "Неправильно введенный номер счета!";
        private const string sameItemsTransactionExceptionMessage = "Укажите другой счет для перевода!";

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

            if(int.TryParse(accountNumber.Text, out id))
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
                            bank.CreateRecord(RecordsCommentsConfig.AccountToAccountComment, sum, account.Balance, account.CustomerId);

                            this.Close();
                        }
                        catch(SameItemsTransactionException)
                        {
                            MessageBox.Show(sameItemsTransactionExceptionMessage);
                        }
                        catch(Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }

                        account.RefillAnotherAccountNotifier -= OnRefillAnotherAccount;
                    }
                    else
                    {
                        MessageBox.Show(Config.IncorrectSumMessage);
                    }
                }
                else
                {
                    MessageBox.Show(wrongAccountNumberMessage);
                }
            }
            else
            {
                MessageBox.Show(wrongFilledNumberMessage);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            button3 = Forms.NormalizeBackButton(button3);
        }
    }
}
