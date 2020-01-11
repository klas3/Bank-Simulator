using System;
using System.Windows.Forms;
using Bank.Cards;

namespace Bank.Forms
{
    partial class CreditCardForm : Form
    {
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
            MessageBox.Show("Вы успешно выплатили кредит!");
        }

        private void CreditCardForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "http://vkarasenko.ru/wp-content/uploads/bank-karta-maket-400x282.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            label5.Text = card.Balance.ToString();
            label6.Text = card.CardId.ToString();
            label7.Text = card.Number;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form form = new TakingCredit(card, bank, card.CustomerId);

            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { this.Show(); };
            form.Show();
            this.Hide();
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            card.ChangeCardStatus();

            if(card.IsBlocked)
            {
                linkLabel3.Text = "true";
                MessageBox.Show("Карта была успешно заблокирована!");
            }
            else
            {
                linkLabel3.Text = "false";
                MessageBox.Show("Ваша карта была успешно разблокирована!");
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
