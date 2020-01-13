using System;
using System.Windows.Forms;
using Bank.Accounts;
using Bank.Other;
using Bank.Cards;
using Bank.Exceptions;

namespace Bank.Forms
{
    partial class MainMenu : Form
    {
        private const string yearRestrictionMessage = "Вы не достигли 18 лет, чтобы создать ";
        private const string existingItemMessage = "Вы уже имете ";
        private const string creditCardString = "кредитную карту.";
        private const string paymentsCardString = "дебетовую карту.";
        private const string savingAccountString = "сберегательный счет";

        private const int requiredCustomerAge = 18;

        private Bank bank;
        private Form prevForm;
        private Customer customer;
        private (CreditCard, SavingAccount, PaymentsCard) cardsAndAccounts;

        public MainMenu(Form prevForm, Customer customer, Bank bank)
        {
            InitializeComponent();
            this.prevForm = prevForm;
            this.customer = customer;
            this.cardsAndAccounts = customer.GetCardsAndAccountInfo();
            this.bank = bank;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://img.icons8.com/plasticine/2x/user.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            button3 = Forms.NormalizeBackButton(button3);

            (string, string, string, string) info = customer.GetInfo();

            label2.Text = info.Item1;
            label6.Text = info.Item2;
            label7.Text = info.Item4;

            if (cardsAndAccounts.Item1 == null)
            {
                linkLabel2.Text = Config.DisplayingUnexistingItemString;
            }

            if (cardsAndAccounts.Item2 == null)
            {
                linkLabel3.Text = Config.DisplayingUnexistingItemString;
            }

            if (cardsAndAccounts.Item3 == null)
            {
                linkLabel4.Text = Config.DisplayingUnexistingItemString;
            }
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            prevForm.Close();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form;
            cardsAndAccounts = customer.GetCardsAndAccountInfo();

            if (cardsAndAccounts.Item1 != null)
            {
                form = new CreditCardForm(cardsAndAccounts.Item1, bank);
                Forms.LoadForm(form, this);
            }
            else
            {
                if (customer.Age >= requiredCustomerAge)
                {
                    CreditCard card = new CreditCard(customer.Id);
                    bank.Cards.AddCard(card);

                    form = new Waiting();
                    Forms.LoadForm(form, this);

                    try
                    {
                        customer.AssignCreditCard(card);
                        linkLabel2.Text = Config.DisplayingExistingItemString;
                    }
                    catch(ExistingItemException)
                    {
                        MessageBox.Show(existingItemMessage + creditCardString);
                    }
                }
                else
                {
                    MessageBox.Show(yearRestrictionMessage + creditCardString);
                }
            }
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form;
            cardsAndAccounts = customer.GetCardsAndAccountInfo();

            if (cardsAndAccounts.Item3 != null)
            {
                form = new PaymentsCardForm(cardsAndAccounts.Item3, bank, customer.Id);
            }
            else
            {
                PaymentsCard card = new PaymentsCard(customer.Id);
                bank.Cards.AddCard(card);

                form = new Waiting();

                try
                {
                    customer.AssignPaymentsCard(card);
                    linkLabel3.Text = Config.DisplayingExistingItemString;
                }
                catch(ExistingItemException)
                {
                    MessageBox.Show(existingItemMessage + paymentsCardString);
                }
            }

            Forms.LoadForm(form, this);
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form;
            cardsAndAccounts = customer.GetCardsAndAccountInfo();

            if (cardsAndAccounts.Item2 != null)
            {
                form = new SavingAccountForm(cardsAndAccounts.Item2, bank);
                Forms.LoadForm(form, this);
            }
            else
            {
                if (customer.Age >= requiredCustomerAge)
                {
                    SavingAccount account = new SavingAccount(customer.Id);
                    bank.Accounts.AddAccount(account);

                    form = new Waiting();
                    Forms.LoadForm(form, this);

                    try
                    {
                        customer.AssignSavingAccount(account);
                        linkLabel4.Text = Config.DisplayingExistingItemString;
                    }
                    catch(ExistingItemException)
                    {
                        MessageBox.Show(existingItemMessage + savingAccountString);
                    }
                }
                else
                {
                    MessageBox.Show(yearRestrictionMessage + savingAccountString);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form form = new RecordsForm(bank.Records, customer.Id);
            Forms.LoadForm(form, this);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form form = new CardsRefiilsForm(customer, bank);
            Forms.LoadForm(form, this);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
