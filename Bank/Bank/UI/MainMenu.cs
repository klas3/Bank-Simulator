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

        private void LoadForm(Form form)
        {
            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { this.Show(); };
            form.Show();
            this.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://img.icons8.com/plasticine/2x/user.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            (string, string, string, string) info = customer.GetInfo();

            label2.Text = info.Item1;
            label6.Text = info.Item2;
            label7.Text = info.Item4;

            if (cardsAndAccounts.Item1 == null)
            {
                linkLabel2.Text = "создать";
            }

            if (cardsAndAccounts.Item2 == null)
            {
                linkLabel3.Text = "создать";
            }

            if (cardsAndAccounts.Item3 == null)
            {
                linkLabel4.Text = "создать";
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
                LoadForm(form);
            }
            else
            {
                if (customer.Age >= 18)
                {
                    CreditCard card = new CreditCard(customer.Id);
                    bank.Cards.AddCard(card);

                    form = new Waiting();
                    LoadForm(form);

                    try
                    {
                        customer.AssignCreditCard(card);
                        linkLabel2.Text = "информация";
                    }
                    catch(ExistingItemException)
                    {
                        MessageBox.Show("Вы уже имете кредитную карту банка!");
                    }
                }
                else
                {
                    MessageBox.Show("Вы не достигли 18 лет, чтобы создать кредитную карту!");
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
                    linkLabel3.Text = "информация";
                }
                catch(ExistingItemException)
                {
                    MessageBox.Show("Вы уже имете дебетовую карту банка!");
                }
            }

            LoadForm(form);
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form;
            cardsAndAccounts = customer.GetCardsAndAccountInfo();

            if (cardsAndAccounts.Item2 != null)
            {
                form = new SavingAccountForm(cardsAndAccounts.Item2, bank);
                LoadForm(form);
            }
            else
            {
                if (customer.Age >= 18)
                {
                    SavingAccount account = new SavingAccount(customer.Id);
                    bank.Accounts.AddAccount(account);

                    form = new Waiting();
                    LoadForm(form);

                    try
                    {
                        customer.AssignSavingAccount(account);
                        linkLabel4.Text = "информация";
                    }
                    catch(ExistingItemException)
                    {
                        MessageBox.Show("Вы уже имеете сберегательный счет банка!");
                    }
                }
                else
                {
                    MessageBox.Show("Вы не достигли 18 лет, чтобы создать сберегательный счет!");
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form form = new RecordsForm(bank.Records, customer.Id);
            LoadForm(form);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form form = new CardsRefiilsForm(customer, bank);
            LoadForm(form);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
