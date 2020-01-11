using System;
using System.Windows.Forms;
using Bank.Cards;
using Bank.Accounts;

namespace Bank.Forms
{
    partial class PaymentsCardForm : Form
    {
        private PaymentsCard card;
        private Bank bank;
        private int customerId;

        public PaymentsCardForm(PaymentsCard card, Bank bank, int id)
        {
            InitializeComponent();
            this.card = card;
            this.bank = bank;
            customerId = id;
        }

        private void LoadForm(Form form)
        {
            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { this.Show(); };
            form.Show();
            this.Hide();
        }

        private void CreditCardForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "http://vkarasenko.ru/wp-content/uploads/bank-karta-maket-400x282.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            label9.Text = card.Balance.ToString();
            label6.Text = card.CardId.ToString();
            label7.Text = card.Number;
            
            if(card.DepositAccount == null)
            {
                linkLabel1.Text = "создать";
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form;

            if (card.DepositAccount != null)
            {
                form = new DepositAccountForm(card.DepositAccount, bank);

                form.Location = this.Location;
                form.StartPosition = FormStartPosition.Manual;
                form.FormClosing += delegate { this.Show(); };
                form.Show();
                this.Hide();
            }
            else
            {
                DepositAccount account = new DepositAccount(customerId);
                bank.Accounts.AddAccount(account);

                form = new Waiting();

                try
                {
                    card.AssignDepositAccount(account);
                    linkLabel1.Text = "информация";
                }
                catch
                {
                    MessageBox.Show("Вы уже имеете депозитный счет!");
                }
            }

            LoadForm(form);
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            card.ChangeCardStatus();

            if (card.IsBlocked)
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
    }
}
