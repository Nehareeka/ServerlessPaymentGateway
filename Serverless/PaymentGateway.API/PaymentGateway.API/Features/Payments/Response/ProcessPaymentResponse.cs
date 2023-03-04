namespace PaymentGateway.API.Models
{
    public class ProcessPaymentResponse
    {
        public string PaymentId { get; set; }
        public string Status { get; set; }
        public string? Message { get; set; }
    }
}