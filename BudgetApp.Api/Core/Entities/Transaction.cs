using BudgetApp.Api.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("transactions")]
public class Transaction : IAuditable
{
    [Column("transaction_id")]
    public int TransactionId { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("amount")]
    public decimal Amount { get; set; }
    
    [Column("type")]
    public TransactionTypeEnum Type { get; set; }
    
    [Column("income_item_id")]
    public int? IncomeItemId { get; set; }
    
    [Column("expense_item_id")]
    public int? ExpenseItemId { get; set; }
    
    [Column("credit_card_id")]
    public int? CreditCardId { get; set; }
    
    [Column("installment_definition_id")]
    public int? InstallmentDefinitionId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public IncomeItem? IncomeItem { get; set; }
    public ExpenseItem? ExpenseItem { get; set; }
    public CreditCard? CreditCard { get; set; }
    public InstallmentDefinition? InstallmentDefinition { get; set; }
}