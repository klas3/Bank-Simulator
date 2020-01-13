using System;
using System.Windows.Forms;
using Bank.Other;

namespace Bank.Forms
{
    public partial class Login : Form
    {
        private const string wrongDataMessage = "Неправильно введенные даные!";

        private Bank bank;

        public Login()
        {
            InitializeComponent();
            this.bank = new Bank();
            this.CenterToScreen();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Customer customer = bank.Login(textBox1.Text, textBox2.Text);

            if (customer != null)
            {
                Form mainMenu = new MainMenu(this, customer, bank);
                Forms.LoadForm(mainMenu, this);
            }
            else
            {
                MessageBox.Show(wrongDataMessage);
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form registerForm = new Register(bank, this);
            Forms.LoadForm(registerForm, this);
        }
    }
}
