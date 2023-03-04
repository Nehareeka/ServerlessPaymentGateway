namespace BankSimulator
{
    public class BankProcessRequest
    {
        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string CardHolderName { get; set; }

        public string Cvv { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
    }
}