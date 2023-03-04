namespace PaymentGateway.API.Exceptions
{
    public class CardDetailsException : Exception
    {
        public string PaymentId { get; set; }
        public CardDetailsException()
        {

        }
        public CardDetailsException(string message) : base(message)
        {

        }

        public CardDetailsException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public CardDetailsException(string message, string paymentId) : base(message)
        {
            PaymentId = paymentId;
        }
    }
}
