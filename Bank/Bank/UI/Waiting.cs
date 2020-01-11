using System;
using System.Windows.Forms;

namespace Bank.Forms
{
    partial class Waiting : Form
    {
        private Timer MyTimer;

        public Waiting()
        {
            InitializeComponent();
        }

        private void Waiting_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://thumbs.gfycat.com/RespectfulBelovedArachnid-small.gif";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            MyTimer = new Timer();
            MyTimer.Interval = (5 * 1000);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            MyTimer.Stop();
            this.Close();
        }
    }
}
