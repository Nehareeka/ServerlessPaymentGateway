using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.API.Models
{
    public class ProcessPaymentRequest
    {
        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number should be of 16 digits")]
        [RegularExpression(@"\d+$", ErrorMessage = "Only digits are allowed.")]
        public string CardNumber { get; set; }

        [Required]
        [Range(1,12)]
        public int ExpiryMonth { get; set; }

        [Required]
        [Range(1900, 2040, ErrorMessage = "Invalid Expiry Year")]
        public int ExpiryYear { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV should be of 3 digits")]
        [RegularExpression(@"\d+$", ErrorMessage = "Only digits are allowed.")]
        public string Cvv { get; set; }

        public string MerchantId { get; set; }
        public string CustomerName { get; set; }

        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public double Amount { get; set; }

        [Required]
        public Currency Currency { get; set; }

    }
}
