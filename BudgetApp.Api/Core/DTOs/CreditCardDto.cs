namespace BudgetApp.Api.Core.DTOs;

public class CreditCardDto
{
    public int CreditCardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? BankName { get; set; }
    public string? Last4Digits { get; set; }
    public int StatementDay { get; set; }
    public int PaymentDueDateOffset { get; set; }
    public decimal? CardLimit { get; set; }
    public string? ExpirationDate { get; set; }
}

public class CreateCreditCardDto
{
    public string Name { get; set; } = string.Empty;
    public string? BankName { get; set; }
    public string? Last4Digits { get; set; }
    public int StatementDay { get; set; }
    public int PaymentDueDateOffset { get; set; }
    public decimal? CardLimit { get; set; }
    public string? ExpirationDate { get; set; }
}