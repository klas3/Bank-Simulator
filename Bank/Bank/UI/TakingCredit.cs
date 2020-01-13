using System;
using System.Windows.Forms;
using Bank.Cards;
using Bank.Exceptions;
using Bank.EventArguments;

namespace Bank.Forms
{
    partial class TakingCredit : Form
    {
        private const string UnpayedCreditExceptionMessage = "Вы не можете пополнить кредитный счет, так как вы не выплатили прошлое пополнение!";

        private const int creditMonthsCount = 1;
        private CreditCard card;
        private Bank bank;

        public TakingCredit(CreditCard card, Bank bank)
        {
            InitializeComponent();
            this.card = card;
            this.bank = bank;
        }

        private void OnTakeCredit(object sender, TakingCreditEventArgs e)
        {
            MessageBox.Show($"Вы успешно пополнили кредитный счет на ${e.Sum}!");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int sum;

            if(int.TryParse(textBox1.Text, out sum) && sum > 0)
            {
                try
                {
                    card.TakingCreditNotifier += OnTakeCredit;
                    card.TakeCredit(sum, creditMonthsCount);
                    bank.CreateRecord(RecordsCommentsConfig.CreditRefillComment, sum, card.Balance, card.CustomerId);

                    this.Close();
                }
                catch(SumExcessException)
                {
                    MessageBox.Show($"Укажите сумму меньше чем {card.MaxCreditTakingCount}!");
                }
                catch(TakingCreditException)
                {
                    MessageBox.Show(UnpayedCreditExceptionMessage);
                }
                catch(BlockedCardException exception)
                {
                    MessageBox.Show(exception.Message);
                }

                card.TakingCreditNotifier -= OnTakeCredit;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TakingCredit_Load(object sender, EventArgs e)
        {
            button3 = Forms.NormalizeBackButton(button3);
        }
    }
}
