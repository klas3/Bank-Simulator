namespace Bank
{
    class PaymentsMean
    {
        public int CustomerId { get; private set; }
        public float Balance { get; protected set; }

        public PaymentsMean(int customerId, float balance)
        {
            CustomerId = customerId;
            Balance = balance;
        }
    }
}
