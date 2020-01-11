using System;
using System.Windows.Forms;
using Bank.Other;
using Bank.Cards;
using Bank.Accounts;
using Bank.Collections;
using Bank.EventArguments;

namespace Bank.Forms
{
    partial class CardsRefiilsForm : Form
    {
        private (CreditCard, SavingAccount, PaymentsCard) cards;
        private Customer customer;
        private Bank bank;

        public CardsRefiilsForm(Customer customer, Bank bank)
        {
            InitializeComponent();
            this.customer = customer;
            this.cards = customer.GetCardsAndAccountInfo();
            this.bank = bank;
        }

        private void OnTransferFromCardToCard(object sender, TransferFromCardToCardEventArgs e)
        {
            MessageBox.Show($"Вы успешно перевели ${e.Sum} с карты {e.CardFrom.Number} на карту {e.CardTo.Number}");
        }

        private void CardsRefiilsForm_Load(object sender, EventArgs e)
        {
            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            if (cards.Item1 != null)
            {
                comboBox1.Items.Add(cards.Item1.Number);
                comboBox2.Items.Add(cards.Item1.Number);
            }

            if(cards.Item3 != null)
            {
                comboBox1.Items.Add(cards.Item3.Number);
                comboBox2.Items.Add(cards.Item3.Number);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int sum;
            CardsCollection collection = bank.Cards;
            Card cardFrom = collection.GetCardByNumber(comboBox1.Text);
            Card cardTo = collection.GetCardByNumber(comboBox2.Text);

            if(comboBox1.Text != comboBox2.Text)
            {
                if(int.TryParse(textBox2.Text, out sum) && sum > 0)
                {
                    if(cardFrom != null && cardTo != null)
                    {
                        try
                        {
                            customer.TransferFromCardToCardNotifier += OnTransferFromCardToCard;
                            customer.TransferFromCardToCard(cardFrom, cardTo, sum);
                            bank.CreateRecord("Перевод на другую карту.", sum, cardFrom.Balance, cardFrom.CustomerId);
                            customer.TransferFromCardToCardNotifier -= OnTransferFromCardToCard;
                        }
                        catch (Exception exception)
                        {
                            customer.TransferFromCardToCardNotifier -= OnTransferFromCardToCard;
                            MessageBox.Show(exception.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильно выбраны карты!");
                    }
                }
                else
                {
                    MessageBox.Show("Неверно введена сумма для перевода!");
                }
            }
            else
            {
                MessageBox.Show("Выберите другую карту для перечисления!");
            }

            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
