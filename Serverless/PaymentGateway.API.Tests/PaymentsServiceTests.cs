//using Microsoft.Extensions.Logging;
//using Moq;
//using PaymentGateway.API.Payments.Data;
//using PaymentGateway.API.Models;
//using PaymentGateway.API.Services;
//using System.Net.Http;
//using System.Net.Http.Json;
//using Xunit;
//using System;
//using PaymentGateway.API.Tests.TestData;
//using System.Text.Json;
//using Newtonsoft.Json;

//namespace PaymentGateway.API.Tests
//{
//    public class PaymentsServiceTests
//    {
//        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
//        private readonly Mock<IPaymentRepository> _paymentsRepo = new Mock<IPaymentRepository>();
//        private readonly Mock<ILogger<PaymentsService>> _logger = new Mock<ILogger<PaymentsService>>();
//        private readonly Mock<HttpClient> _httpClient = new Mock<HttpClient>();

//        public PaymentsServiceTests()
//        {
//        }

//        [Fact]
//        private async void ProcessPayment()
//        {
//            var paymentRequest = PaymentTestData.GetTestProcessPaymentRequest();
//            var paymentResponse = PaymentTestData.GetTestProcessPaymentResponse();

//            //var httpClient = new HttpClient();
//            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);
//            _httpClient.Setup(client => client.SendAsync(It.IsAny<string>(), It.IsAny<ProcessPaymentRequest>(), It.IsAny<JsonSerializerOptions>(), default))
//                .ReturnsAsync(new HttpResponseMessage
//                {
//                    StatusCode = System.Net.HttpStatusCode.OK,
//                    Content = new StringContent(JsonConvert.SerializeObject(paymentResponse))
//                });
//            //_httpClient.Setup(httpClient =>
//            //        httpClient.PostAsJsonAsync<PaymentResponse>(PaymentsPath, _authorization, paymentRequest,
//            //            CancellationToken.None, null))
//            //    .ReturnsAsync(() => paymentResponse);
//            var paymentsService = new PaymentsService(_paymentsRepo.Object, _httpClientFactory.Object, _logger.Object);
//            //;
//            //paymentsService.Setup(service => service.ProcessPaymentAsync(It.IsAny<ProcessPaymentRequest>()))
//            //    .Returns((ProcessPaymentRequest request) => new ApiResponse<ProcessPaymentResponse>(true, new ProcessPaymentResponse()
//            //        {
//            //            PaymentId = System.Guid.Empty,
//            //            Message = "Success",
//            //            Status = PaymentStatus.Active
//            //        }
//            //    )

//            //    );




//            var response = await paymentsService.ProcessPaymentAsync(paymentRequest);

//            Assert.NotNull(response);
//            Assert.Equal(response.IsSuccess, true);
//            Assert.Equal(response, paymentResponse);
//        }

//        [Fact]
//        public void GetPayment()
//        {
//            //var service = new Payment
//        }
//    }
//}