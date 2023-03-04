namespace PaymentGateway.API.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse(bool success, T response, string message = null)
        {
            IsSuccess = success;
            ResponseBody = response;
            ErrorMessage = message;
        }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public T ResponseBody { get; set; }
    }
}
