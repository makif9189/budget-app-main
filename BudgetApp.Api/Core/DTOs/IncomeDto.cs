using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Api.Core.DTOs
{
    /// <summary>
    /// DTO for returning income item details.
    /// </summary>
    public class IncomeDto
    {
        public int IncomeItemId { get; set; }
        public int IncomeSourceId { get; set; }
        public string SourceName { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public bool IsRecurring { get; set; }
    }

    /// <summary>
    /// DTO for creating a new income item.
    /// </summary>
    public class CreateIncomeDto
    {
        [Required]
        public int IncomeSourceId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        
        public bool IsRecurring { get; set; } = false;
    }
}
