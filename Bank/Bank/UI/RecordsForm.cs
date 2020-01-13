using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bank.Collections;
using Bank.Other;

namespace Bank.Forms
{
    partial class RecordsForm : Form
    {
        private RecordsCollection records;
        private int customerId;

        public RecordsForm(RecordsCollection records, int customerId)
        {
            InitializeComponent();
            this.records = records;
            this.customerId = customerId;
        }

        private void RecordsForm_Load(object sender, EventArgs e)
        {
            button3 = Forms.NormalizeBackButton(button3);

            foreach (KeyValuePair<int, Record> element in records.Records)
            {
                if (element.Value.CustomerId == customerId)
                {
                    listBox1.Items.Add(element.Value.ToString());
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
