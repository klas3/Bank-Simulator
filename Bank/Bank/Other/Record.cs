using System;

namespace Bank.Other
{
    class Record
    {
        private static int recordsCount = 1;

        public int RecordId { get; private set; }
        public int CustomerId { get; private set; }

        private string recordComment;
        private float sumChange;
        private float currentBalance;
        private DateTime recordDate;

        public Record(string recordComment, float sumChange, float currentBalance, int customerId)
        {
            this.recordComment = recordComment;
            this.sumChange = sumChange;
            this.currentBalance = currentBalance;
            this.RecordId = recordsCount;
            this.CustomerId = customerId;
            this.recordDate = DateTime.Now;

            recordsCount++;
        }

        public override string ToString()
        {
            return $"{recordDate.ToString()}; {recordComment}; Changing sum: {sumChange}; Current sum: {currentBalance};";
        }
    }
}
