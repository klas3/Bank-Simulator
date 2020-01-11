using System;
using System.Windows.Forms;
using Bank.Accounts;
using Bank.Cards;
using Bank.EventArguments;

namespace Bank.Forms
{
    partial class TransferFromAccountToCard : Form
    {
        private Bank bank;
        private DepositAccount depositAccount;
        private SavingAccount savingAccount;

        public TransferFromAccountToCard(DepositAccount depositAccount, Bank bank)
        {
            InitializeComponent();
            this.depositAccount = depositAccount;
            this.bank = bank;
        }

        public TransferFromAccountToCard(SavingAccount savingAccount, Bank bank)
        {
            InitializeComponent();
            this.savingAccount = savingAccount;
            this.bank = bank;
        }

        private void OnRefillCard(object sender, RefillCardEventArgs e)
        {
            MessageBox.Show($"Вы успешно перечислили ${e.Sum} на карту {e.Card.Number}");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int sum;
            Card card = bank.Cards.GetCardByNumber(textBox1.Text);

            if(int.TryParse(textBox2.Text, out sum) && sum > 0)
            {
                if(card != null)
                {
                    if(depositAccount != null)
                    {
                        try
                        {
                            depositAccount.RefillCardNotifier += OnRefillCard;
                            depositAccount.RefillCard(sum, card);
                            bank.CreateRecord("Перевод со счета на карту.", sum, depositAccount.Balance, depositAccount.CustomerId);
                            depositAccount.RefillCardNotifier -= OnRefillCard;
                        }
                        catch(Exception exception)
                        {
                            depositAccount.RefillCardNotifier -= OnRefillCard;
                            MessageBox.Show(exception.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            savingAccount.RefillCardNotifier += OnRefillCard;
                            savingAccount.RefillCard(sum, card);
                            bank.CreateRecord("Перевод со счета на карту.", sum, savingAccount.Balance, savingAccount.CustomerId);
                            savingAccount.RefillCardNotifier -= OnRefillCard;
                        }
                        catch (Exception exception)
                        {
                            savingAccount.RefillCardNotifier -= OnRefillCard;
                            MessageBox.Show(exception.Message);
                        }
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введенной карты не существует!");
                }
            }
            else
            {
                MessageBox.Show("Неправильно введена сумма для перевода!");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TransferFromAccountToCard_Load(object sender, EventArgs e)
        {
            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
        }
    }
}
