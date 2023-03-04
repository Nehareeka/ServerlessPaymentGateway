using PaymentGateway.API.Models;

namespace PaymentGateway.API.Services
{
    public interface IPaymentsService
    {
        Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest payment);
        Task<PaymentDetails> GetPaymentDetailsAsync(string id);

    }
}
