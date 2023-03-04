using Moq;
using Moq.EntityFrameworkCore;
using PaymentGateway.API.Data;
using PaymentGateway.API.Models;
using PaymentGateway.API.Payments.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.API.Tests
{
    public class PaymentRespositoryTests
    {
        [Fact]
        public async Task ShouldGetPayment()
        {
            var paymentDetails = new PaymentDetails()
            {
                Currency = Currency.USD,
                Amount = 20,
                CustomerName = "Jane Doe",
                MerchantId = Guid.Empty.ToString(),
                MaskedCardNumber = "2385",
                PaymentTimeStamp = DateTime.Now,
                Id = Guid.Empty.ToString(),
                Status = "Paid"
            };
            var paymentItem = new PaymentDetails[]
            {
                paymentDetails
            };

            var mockDbContext = new Mock<PaymentDbContext>();
            mockDbContext.Setup(ctx => ctx.Payments).ReturnsDbSet(paymentItem);
            mockDbContext.Setup(c => c.FindAsync<PaymentDetails>(It.IsAny<object[]>())).ReturnsAsync(paymentDetails);
            var mockRepo = new PaymentRepository(mockDbContext.Object);

            var response = mockRepo.GetPayment(Guid.Empty.ToString());

            Assert.NotNull(response.Result);
            Assert.Equal(paymentDetails, response.Result);
        }

    }
}
