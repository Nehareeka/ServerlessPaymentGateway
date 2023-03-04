using PaymentGateway.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.API.Tests.TestData
{
    public static class PaymentTestData
    {
        public static ProcessPaymentRequest GetTestProcessPaymentRequest()
        {
            var paymentRequest = new ProcessPaymentRequest
            {
                CardHolderName = "John Doe",
                CardNumber = "1234432112344321",
                CustomerName = "John",
                MerchantId = Guid.NewGuid().ToString(),
                Amount = 10,
                Currency = Currency.GBP,
                Cvv = "101",
                ExpiryMonth = 7,
                ExpiryYear = 2050
            };
            return paymentRequest;
        }


        public static ProcessPaymentResponse GetTestProcessPaymentResponse()
        {
            var response = new ProcessPaymentResponse
            {
                PaymentId = Guid.Empty.ToString(),
                Status = "Paid",
                Message = "Payment Success"
            };
            return response;
        }

        public static PaymentDetails GetTestPaymentDetailsResponse()
        {
            var response = new PaymentDetails
            {
                PaymentTimeStamp = DateTime.Now,
                Id = Guid.Empty.ToString(),
                Status = "Paid",
                Amount = 10,
                Currency = Currency.USD,
                CustomerName = "John",
                MaskedCardNumber = "1234",
                MerchantId = "mid",
                StatusMessage = "Success"
            };
            return response;
        }
    }
}
