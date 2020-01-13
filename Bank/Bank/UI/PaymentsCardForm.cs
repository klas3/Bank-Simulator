using System;
using System.Windows.Forms;
using Bank.Cards;
using Bank.Accounts;
using Bank.Exceptions;

namespace Bank.Forms
{
    partial class PaymentsCardForm : Form
    {
        private const string existingDepositAccountMessage = "Вы уже имеете депозитный счет!";

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

        private void CreditCardForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "http://vkarasenko.ru/wp-content/uploads/bank-karta-maket-400x282.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            button3 = Forms.NormalizeBackButton(button3);

            label9.Text = card.Balance.ToString();
            label6.Text = card.CardId.ToString();
            label7.Text = card.Number;
            
            if(card.DepositAccount == null)
            {
                linkLabel1.Text = Config.DisplayingUnexistingItemString;
            }

            if (card.IsBlocked)
            {
                linkLabel3.Text = Config.BlockedCardStatusString;
            }
            else
            {
                linkLabel3.Text = Config.UnblockedCardStatusString;
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form;

            if (card.DepositAccount != null)
            {
                form = new DepositAccountForm(card.DepositAccount, bank);
            }
            else
            {
                DepositAccount account = new DepositAccount(customerId);
                bank.Accounts.AddAccount(account);

                form = new Waiting();

                try
                {
                    card.AssignDepositAccount(account);
                    linkLabel1.Text = Config.DisplayingExistingItemString;
                }
                catch(ExistingItemException)
                {
                    MessageBox.Show(existingDepositAccountMessage);
                }
            }

            Forms.LoadForm(form, this);
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            card.ChangeCardStatus();

            if (card.IsBlocked)
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
    }
}
