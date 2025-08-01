using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Api.Core.DTOs
{
    /// <summary>
    /// DTO for returning credit card details.
    /// </summary>
    public class CreditCardDto
    {
        public int CreditCardId { get; set; }
        public string Name { get; set; }
        public string? BankName { get; set; }
        public string? Last4Digits { get; set; }
        public int StatementDay { get; set; }
        public int PaymentDueDateOffset { get; set; }
        public decimal? CardLimit { get; set; }
        public string? ExpirationDate { get; set; }
    }

    /// <summary>
    /// DTO for creating a new credit card.
    /// </summary>
    public class CreateCreditCardDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string? BankName { get; set; }

        [StringLength(4)]
        public string? Last4Digits { get; set; }

        [Required]
        [Range(1, 31)]
        public int StatementDay { get; set; }

        [Required]
        [Range(0, 30)]
        public int PaymentDueDateOffset { get; set; }

        public decimal? CardLimit { get; set; }

        [StringLength(5)] // MM/YY
        public string? ExpirationDate { get; set; }
    }
}
