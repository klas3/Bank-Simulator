using System;
using System.Windows.Forms;
using Bank.Cards;
using Bank.Exceptions;
using Bank.EventArguments;

namespace Bank.Forms
{
    partial class TakingCredit : Form
    {
        private CreditCard card;
        private Bank bank;
        private int customerId;

        public TakingCredit(CreditCard card, Bank bank, int customerId)
        {
            InitializeComponent();
            this.card = card;
            this.bank = bank;
            this.customerId = customerId;
        }

        private void OnTakeCredit(object sender, TakingCreditEventArgs e)
        {
            MessageBox.Show($"Вы успешно пополнили кредитный счет на ${e.Sum}!");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int sum;

            if(int.TryParse(textBox1.Text, out sum))
            {
                try
                {
                    card.TakingCreditNotifier += OnTakeCredit;
                    card.TakeCredit(sum, 1);
                    bank.CreateRecord("Пополнение кредитного счета.", sum, card.Balance, card.CustomerId);
                    card.TakingCreditNotifier -= OnTakeCredit;
                }
                catch(SumExcessException)
                {
                    card.TakingCreditNotifier -= OnTakeCredit;
                    MessageBox.Show($"Укажите сумму меньше чем {card.MaxCreditTakingCount}!");
                }
                catch(TakingCreditException)
                {
                    card.TakingCreditNotifier -= OnTakeCredit;
                    MessageBox.Show("Вы не можете пополнить кредитный счет, так как вы не выплатили прошлое пополнение!");
                }
                catch(BlockedCardException exception)
                {
                    card.TakingCreditNotifier -= OnTakeCredit;
                    MessageBox.Show(exception.Message);
                }

                this.Close();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TakingCredit_Load(object sender, EventArgs e)
        {
            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
        }
    }
}
