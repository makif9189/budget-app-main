using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("users")]
public class User : IAuditable
{
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("username")]
    public string Username { get; set; } = null!;
    
    [Column("email")]
    public string Email { get; set; } = null!;
    
    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    public ICollection<ExpenseCategory> ExpenseCategories { get; set; } = new List<ExpenseCategory>();
    public ICollection<ExpenseItem> ExpenseItems { get; set; } = new List<ExpenseItem>();
    public ICollection<IncomeSource> IncomeSources { get; set; } = new List<IncomeSource>();
    public ICollection<IncomeItem> IncomeItems { get; set; } = new List<IncomeItem>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}