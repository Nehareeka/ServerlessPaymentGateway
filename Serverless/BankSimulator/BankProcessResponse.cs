namespace BankSimulator
{
    public class BankProcessResponse
    {
        public BankProcessResponse()
        {

        }
        public BankProcessResponse(string PaymentId, string status, string message)
        {
            this.PaymentId = PaymentId;
            Status = status;
            Message = message;
        }
        public string PaymentId { get; set; }
        public string Status { get; set; }
        public string? Message { get; set; }
    }
}