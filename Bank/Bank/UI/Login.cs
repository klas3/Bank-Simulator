using System;
using System.Windows.Forms;
using Bank.Other;

namespace Bank.Forms
{
    public partial class Login : Form
    {
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

                mainMenu.Location = this.Location;
                mainMenu.StartPosition = FormStartPosition.Manual;
                mainMenu.FormClosing += delegate { this.Show(); };
                mainMenu.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrond data!");
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form registerForm = new Register(bank, this);

            registerForm.Location = this.Location;
            registerForm.StartPosition = FormStartPosition.Manual;
            registerForm.FormClosing += delegate { this.Show(); };
            registerForm.Show();

            this.Hide();
        }
    }
}
