using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Api.Core.DTOs
{
    /// <summary>
    /// DTO for returning expense item details.
    /// </summary>
    public class ExpenseDto
    {
        public int ExpenseItemId { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO for creating a new expense item.
    /// </summary>
    public class CreateExpenseDto
    {
        [Required]
        public int ExpenseCategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
