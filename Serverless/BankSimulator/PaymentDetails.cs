using Amazon.DynamoDBv2.DataModel;

namespace BankSimulator
{
    [DynamoDBTable("paymentdetails")]
    public class PaymentDetails
    {

        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("amount")]
        public double Amount { get; set; }

        [DynamoDBProperty("currency")]
        public Currency Currency { get; set; }

        [DynamoDBProperty("maskedcardnumber")]
        public string MaskedCardNumber { get; set; }

        [DynamoDBProperty("merchantid")]
        public string MerchantId { get; set; }

        [DynamoDBProperty("customername")]
        public string CustomerName { get; set; }

        [DynamoDBProperty("status")]
        public string Status { get; set; }

        [DynamoDBProperty("statusmessage")]
        public string StatusMessage { get; set; }

        [DynamoDBProperty("paymenttimestamp")]
        public DateTime PaymentTimeStamp { get; set; }
    }

    public enum Currency
    {
        USD,
        GBP,
        EUR,
        INR
    }
}
