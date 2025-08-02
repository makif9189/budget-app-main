namespace BudgetApp.Api.Core.DTOs;

public class ExpenseDto
{
    public int ExpenseItemId { get; set; }
    public int ExpenseCategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
}

public class CreateExpenseDto
{
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
}