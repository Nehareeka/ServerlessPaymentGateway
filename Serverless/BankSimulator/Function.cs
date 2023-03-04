using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BankSimulator;

public class Function
{
    /// <summary>
    /// Gets the payment details stored in dynamo DB
    /// </summary>
    /// <param name="request">request with Id in path parameter</param>
    /// <param name="context"></param>
    /// <returns>Success or Not Found</returns>
    public async Task<APIGatewayHttpApiV2ProxyResponse> GetPaymentByIdAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        DynamoDBContext dbContext = new DynamoDBContext(client);
        string id = request.PathParameters["id"];
        var paymentDetails = await dbContext.LoadAsync<PaymentDetails>(id);
        if(paymentDetails == null)
        {
            var message = $"Payment with Id {id} does not exist";
            LambdaLogger.Log(message);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                Body = message,
                StatusCode = 404
            };
        }
        return new APIGatewayHttpApiV2ProxyResponse
        {
            Body = JsonConvert.SerializeObject(paymentDetails),
            StatusCode = 200
        };
    }

    /// <summary>
    /// Processes payment and stores resulti in dynamodb
    /// </summary>
    /// <param name="request">card details to process payment</param>
    /// <param name="context"></param>
    /// <returns>Transaction status</returns>
    public async Task<APIGatewayHttpApiV2ProxyResponse> ProcessPaymentAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        var paymentRequest = JsonConvert.DeserializeObject<BankProcessRequest>(request.Body);
        AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        DynamoDBContext dbContext = new DynamoDBContext(client);


        string paymentId = Guid.NewGuid().ToString();
        LambdaLogger.Log($"Payment with Id {paymentId} Created");
        var cardDetails = await dbContext.LoadAsync<CardData>(paymentRequest?.CardNumber);


        var response = (cardDetails == null) ? new BankProcessResponse(paymentId, GetPaymentStatusName(PaymentStatus.Paid), "Payment Success")
            : new BankProcessResponse(paymentId, GetPaymentStatus(cardDetails.ResponseCode), cardDetails?.Description);
        var paymentDetails = new PaymentDetails()
        {
            Amount = paymentRequest.Amount,
            Currency = paymentRequest.Currency,
            PaymentTimeStamp = DateTime.Now,
            Status = response?.Status,
            Id = paymentId,
            MaskedCardNumber = paymentRequest?.CardNumber?.Substring(paymentRequest.CardNumber.Length - 4, 4),
            StatusMessage = response?.Message
        };

        await dbContext.SaveAsync(paymentDetails);

        LambdaLogger.Log($"Payment with Id {paymentId} status: {response.Status}");
        return new APIGatewayHttpApiV2ProxyResponse
        {
            Body = JsonConvert.SerializeObject(response),
            StatusCode = 200
        };
    }

    private string GetPaymentStatusName(PaymentStatus paymentStatus)
    {
        return Enum.GetName(typeof(PaymentStatus), paymentStatus);
    }

    private string GetPaymentStatus(int responseCode)
    {
        PaymentStatus status;
        switch (responseCode)
        {
            case 20012:
            case 20051:
            case 20062:
            case 20063:
            case 20059:
            case 20061:
                status = PaymentStatus.Declined;
                break;
            case 20068:
                status = PaymentStatus.Expired;
                break;
            default:
                status = PaymentStatus.Pending;
                break;
        }
        return GetPaymentStatusName(status);
    }
}
