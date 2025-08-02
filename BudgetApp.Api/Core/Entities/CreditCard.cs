using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("credit_cards")]
public class CreditCard : IAuditable
{
    [Column("credit_card_id")]
    public int CreditCardId { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = null!;
    
    [Column("bank_name")]
    public string? BankName { get; set; }
    
    [Column("last_4_digits")]
    public string? Last4Digits { get; set; }
    
    [Column("statement_day")]
    public int StatementDay { get; set; }
    
    [Column("payment_due_date_offset")]
    public int PaymentDueDateOffset { get; set; }
    
    [Column("card_limit")]
    public decimal? CardLimit { get; set; }
    
    [Column("expiration_date")]
    public string? ExpirationDate { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public ICollection<CardRateHistory> CardRateHistories { get; set; } = new List<CardRateHistory>();
    public ICollection<InstallmentDefinition> InstallmentDefinitions { get; set; } = new List<InstallmentDefinition>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}