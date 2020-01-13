using System;
using System.Windows.Forms;
using Bank.Other;

namespace Bank.Forms
{
    partial class Register : Form
    {
        private const string wrongNameMessage = "Неверено введено имя!";
        private const string wrongAgeMessage = "Неверно введен возраст!";
        private const string unfilledFieldsMessage = "Вы не заполнили все поля!";

        private Bank bank;
        private Form prevForm;

        public Register(Bank bank, Form prevForm)
        {
            InitializeComponent();
            this.bank = bank;
            this.prevForm = prevForm;
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            prevForm.Close();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            int age;
            int name;

            if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                if (int.TryParse(textBox2.Text, out age) && age > 0)
                {
                    if (!int.TryParse(textBox1.Text, out name))
                    {
                        Customer customer = new Customer(textBox1.Text, textBox4.Text, textBox3.Text, age);
                        bank.Customers.AddCustomer(customer);

                        Form mainMenu = new MainMenu(this, customer, bank);
                        Forms.LoadForm(mainMenu, this);
                    }
                    else
                    {
                        MessageBox.Show(wrongNameMessage);
                    }
                }
                else
                {
                    MessageBox.Show(wrongAgeMessage);
                }
            }
            else
            {
                MessageBox.Show(unfilledFieldsMessage);
            }
        }
    }
}
