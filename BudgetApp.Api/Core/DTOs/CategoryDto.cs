namespace BudgetApp.Api.Core.DTOs;

public class ExpenseCategoryDto
{
    public int ExpenseCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class IncomeSourceDto
{
    public int IncomeSourceId { get; set; }
    public string Name { get; set; } = string.Empty;
}
