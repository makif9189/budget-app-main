namespace BudgetApp.Api.Core.DTOs;

public class IncomeDto
{
    public int IncomeItemId { get; set; }
    public int IncomeSourceId { get; set; }
    public string SourceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
    public bool IsRecurring { get; set; }
}

public class CreateIncomeDto
{
    public int IncomeSourceId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
    public bool IsRecurring { get; set; } = false;
}