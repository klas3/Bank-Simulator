using System;
using System.Windows.Forms;
using Bank.Accounts;

namespace Bank.Forms
{
    partial class DepositAccountForm : Form
    {
        private DepositAccount account;
        private Bank bank;

        public DepositAccountForm(DepositAccount account, Bank bank)
        {
            InitializeComponent();
            this.account = account;
            this.bank = bank;
        }

        private void DepositAccountForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://cdn.discordapp.com/attachments/615120043646124057/635493739498045470/Account.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            if (account.DayForWithdraw.ToString() == "1/1/0001 12:00:00 AM") label2.Text = "вы не имеете депозита!";
            else label2.Text = account.DayForWithdraw.ToString();

            label5.Text = account.Balance.ToString();
            label6.Text = account.AccountId.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form form = new Transfer(account, bank);

            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { this.Show(); };
            form.Show();
            this.Hide();
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
