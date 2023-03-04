using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.Models;
using PaymentGateway.API.Services;

namespace PaymentGateway.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsService _paymentsService;

        public PaymentsController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        /// <summary>
        /// Process the payment request
        /// </summary>
        /// <param name="paymentRequest">Card Payment details</param>
        /// <returns>Status of the transaction</returns>
        [HttpPost("Process")]
        public async Task<ActionResult<ProcessPaymentResponse>> Post(ProcessPaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input invalid");
            }
            var response = await _paymentsService.ProcessPaymentAsync(paymentRequest);

            return CreatedAtAction("Get", new { id = response.PaymentId }, response);
        }

        /// <summary>
        /// Gets the details of a payment by Id
        /// </summary>
        /// <param name="id">Id of the payment transaction</param>
        /// <returns>Details of the payment matching the Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> Get(string id)
        {
            var response = await _paymentsService.GetPaymentDetailsAsync(id);

            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }
    }
}
