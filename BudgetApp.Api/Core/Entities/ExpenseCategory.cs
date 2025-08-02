using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("expense_categories")]
public class ExpenseCategory : IAuditable
{
    [Key]
    [Column("expense_category_id")]
    public int ExpenseCategoryId { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = null!;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public ICollection<ExpenseItem> ExpenseItems { get; set; } = new List<ExpenseItem>();
}