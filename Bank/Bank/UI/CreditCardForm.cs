using System;
using System.Windows.Forms;
using Bank.Cards;

namespace Bank.Forms
{
    partial class CreditCardForm : Form
    {
        private const string succesfullPayingCreditMessage = "Вы успешно выплатили кредит!";

        private CreditCard card;
        private Bank bank;

        public CreditCardForm(CreditCard card, Bank bank)
        {
            InitializeComponent();
            this.card = card;
            this.bank = bank;
            
            if(!card.IsCredit)
            {
                button2.Visible = false;
            }
        }

        private void OnPayCredit(object sender, EventArgs e)
        {
            MessageBox.Show(succesfullPayingCreditMessage);
        }

        private void CreditCardForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "http://vkarasenko.ru/wp-content/uploads/bank-karta-maket-400x282.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            button3 = Forms.NormalizeBackButton(button3);

            label5.Text = card.Balance.ToString();
            label6.Text = card.CardId.ToString();
            label7.Text = card.Number;

            if(card.IsBlocked)
            {
                linkLabel3.Text = Config.BlockedCardStatusString;
            }
            else
            {
                linkLabel3.Text = Config.UnblockedCardStatusString;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form form = new TakingCredit(card, bank);
            Forms.LoadForm(form, this);
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            card.ChangeCardStatus();

            if(card.IsBlocked)
            {
                linkLabel3.Text = Config.BlockedCardStatusString;
                MessageBox.Show(Config.BlockedCardMessage);
            }
            else
            {
                linkLabel3.Text = Config.UnblockedCardStatusString;
                MessageBox.Show(Config.UnblockedCardMessage);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                card.PayCreditNotifier += OnPayCredit;
                card.PayCredit();
                card.PayCreditNotifier -= OnPayCredit;
            }
            catch(Exception exception)
            {
                card.PayCreditNotifier -= OnPayCredit;
                MessageBox.Show(exception.Message);
            }
        }
    }
}
