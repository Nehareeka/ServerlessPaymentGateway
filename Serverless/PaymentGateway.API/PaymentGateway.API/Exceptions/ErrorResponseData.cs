using Newtonsoft.Json;

namespace PaymentGateway.API.Exceptions
{
    internal class ErrorResponseData
    {
        public ErrorResponseData()
        {
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}