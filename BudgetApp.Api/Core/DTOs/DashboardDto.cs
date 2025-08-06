namespace BudgetApp.Api.Core.DTOs;

public class DashboardSummaryDto
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetBalance { get; set; }
    public decimal TotalCreditCardDebt { get; set; }
    public List<IncomeBySourceDto> IncomeBySource { get; set; } = new();
    public List<ExpenseByCategoryDto> ExpenseByCategory { get; set; } = new();
    public List<RecentTransactionDto> RecentTransactions { get; set; } = new();
    public List<UpcomingPaymentDto> UpcomingPayments { get; set; } = new();
}

public class IncomeBySourceDto
{
    public string SourceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class ExpenseByCategoryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class RecentTransactionDto
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty; // "Income" or "Expense"
}

public class UpcomingPaymentDto
{
    public string CardName { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
}