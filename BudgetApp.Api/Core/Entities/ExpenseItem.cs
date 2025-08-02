using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("expense_items")]
public class ExpenseItem : IAuditable
{
    [Column("expense_item_id")]
    public int ExpenseItemId { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("expense_category_id")]
    public int ExpenseCategoryId { get; set; }
    
    [Column("amount")]
    public decimal Amount { get; set; }
    
    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public ExpenseCategory ExpenseCategory { get; set; } = null!;
    public Transaction? Transaction { get; set; }
}