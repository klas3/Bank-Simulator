using System.Collections.Generic;
using System.Windows.Forms;
using Bank.Other;

namespace Bank.Collections
{
    sealed class RecordsCollection
    {
        public Dictionary<int, Record> records;

        public RecordsCollection()
        {
            this.records = new Dictionary<int, Record>();
        }

        public Dictionary<int, Record> GetDictionary()
        {
            return records;
        }

        public void AddRecord(Record record)
        {
            records.Add(record.RecordId, record);
        }

        public ListBox FillTextBoxByID(int id)
        {
            ListBox result = new ListBox();

            foreach(KeyValuePair<int, Record> element in records)
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
