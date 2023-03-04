namespace PaymentGateway.API.Models
{
    public class PaymentDetails
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public string MaskedCardNumber { get; set; }
        public string MerchantId { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public DateTime PaymentTimeStamp { get; set; }
    }
}