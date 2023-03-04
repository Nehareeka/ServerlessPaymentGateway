using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentGateway.API.Controllers;
using PaymentGateway.API.Models;
using PaymentGateway.API.Services;
using PaymentGateway.API.Tests.TestData;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.API.Tests
{
    public class PaymentsControllerTests
    {
        Mock<IPaymentsService> mockService = new Mock<IPaymentsService>();

        [Fact]
        public async Task ShouldProcessPayment_InvalidPaymentModel_ReturnsBadRequest()
        {
            var controller = new PaymentsController(mockService.Object);
            controller.ModelState.AddModelError("cvv", "Wrong cvv input");
            var response = await controller.Post(new ProcessPaymentRequest());

            var result = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ShouldProcessPayment_ValidPaymentInput_ReturnsSuccess()
        {
            mockService.Setup(service => service.ProcessPaymentAsync(It.IsAny<ProcessPaymentRequest>()))
                .ReturnsAsync(PaymentTestData.GetTestProcessPaymentResponse());

            var controller = new PaymentsController(mockService.Object);
            var response = await controller.Post(PaymentTestData.GetTestProcessPaymentRequest());

            var result = Assert.IsType<CreatedAtActionResult>(response.Result);
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task ShouldGetPayment_InvalidId_ReturnsNotFound()
        {
            mockService.Setup(service => service.GetPaymentDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var controller = new PaymentsController(mockService.Object);
            var response = await controller.Get(Guid.Empty.ToString());

            var result = Assert.IsType<NotFoundResult>(response.Result);
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task ShouldGetPayment_ReturnsPayment()
        {
            mockService.Setup(service => service.GetPaymentDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(PaymentTestData.GetTestPaymentDetailsResponse());

            var controller = new PaymentsController(mockService.Object);
            var response = await controller.Get(Guid.Empty.ToString());

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
