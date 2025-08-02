using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("income_items")]
public class IncomeItem : IAuditable
{
    [Column("income_item_id")]
    public int IncomeItemId { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("income_source_id")]
    public int IncomeSourceId { get; set; }
    
    [Column("amount")]
    public decimal Amount { get; set; }
    
    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("is_recurring")]
    public bool IsRecurring { get; set; } = false;
    
    [Column("recurring_frequency")]
    public string? RecurringFrequency { get; set; }
    
    [Column("recurring_start_date")]
    public DateTime? RecurringStartDate { get; set; }
    
    [Column("recurring_end_date")]
    public DateTime? RecurringEndDate { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public IncomeSource IncomeSource { get; set; } = null!;
    public Transaction? Transaction { get; set; }
}