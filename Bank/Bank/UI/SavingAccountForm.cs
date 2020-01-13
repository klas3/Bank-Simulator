using System;
using System.Windows.Forms;
using Bank.Accounts;

namespace Bank.Forms
{
    partial class SavingAccountForm : Form
    {
        private SavingAccount account;
        private Bank bank;

        public SavingAccountForm(SavingAccount account, Bank bank)
        {
            InitializeComponent();
            this.account = account;
            this.bank = bank;
        }

        private void CreditCardForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://cdn.discordapp.com/attachments/615120043646124057/635493739498045470/Account.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            button3 = Forms.NormalizeBackButton(button3);

            label5.Text = account.Balance.ToString();
            label6.Text = account.AccountId.ToString();
            label2.Text = account.WithdrawCountForMonth.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form form = new Transfer(account, bank);
            Forms.LoadForm(form, this);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form form = new TransferFromAccountToCard(account, bank);

            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { this.Show(); };
            form.Show();
            this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
