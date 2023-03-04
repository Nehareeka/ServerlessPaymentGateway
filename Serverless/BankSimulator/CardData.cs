using Amazon.DynamoDBv2.DataModel;

namespace BankSimulator
{
    [DynamoDBTable("carddata")]
    public class CardData
    {

        [DynamoDBHashKey("cardnumber")]
        public string CardNumber { get; set; }

        [DynamoDBProperty("cardtype")]
        public string CardType { get; set; }

        [DynamoDBProperty("responsecode")]
        public int ResponseCode { get; set; }

        [DynamoDBProperty("description")]
        public string Description { get; set; }
    }
}