namespace BudgetApp.Api.Core.DTOs;

public class TransactionDto
{
    public int TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CategoryOrSource { get; set; } = string.Empty;
}