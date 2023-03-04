using PaymentGateway.API.Data;
using PaymentGateway.API.Models;

namespace PaymentGateway.API.Payments.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext dbContext;
        public PaymentRepository(PaymentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddPayment(PaymentDetails payment)
        {
            dbContext.Payments.Add(payment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PaymentDetails> GetPayment(string id)
        {
            var payment = await dbContext.FindAsync<PaymentDetails>(id);
            return payment;
        }
    }
}
