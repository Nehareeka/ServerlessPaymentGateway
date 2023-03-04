using PaymentGateway.API.Models;

namespace PaymentGateway.API.Payments.Data
{
    public interface IPaymentRepository
    {
        Task AddPayment(PaymentDetails payment);
        Task<PaymentDetails> GetPayment(string id);
    }
}
