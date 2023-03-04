using Microsoft.EntityFrameworkCore;
using PaymentGateway.API.Models;

namespace PaymentGateway.API.Data
{
    public class PaymentDbContext : DbContext
    {
        public virtual DbSet<PaymentDetails> Payments { get; set; }
        public PaymentDbContext() : base() { }
        public PaymentDbContext(DbContextOptions options) : base(options) { }
    }
}
