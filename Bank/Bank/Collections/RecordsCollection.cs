using System.Collections.Generic;
using System.Windows.Forms;
using Bank.Other;

namespace Bank.Collections
{
    sealed class RecordsCollection
    {
        public Dictionary<int, Record> Records { get; private set; }

        public RecordsCollection()
        {
            this.Records = new Dictionary<int, Record>();
        }

        public void AddRecord(Record record)
        {
            Records.Add(record.RecordId, record);
        }

        public ListBox FillTextBoxByID(int id)
        {
            ListBox result = new ListBox();

            foreach(KeyValuePair<int, Record> element in Records)
            {
                if(element.Value.CustomerId == id)
                {
                    result.Items.Add(element.Value.ToString());
                }
            }

            return result;
        }
    }
}
