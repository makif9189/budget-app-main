using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("installment_definitions")]
public class InstallmentDefinition : IAuditable
{
    [Key]
    [Column("installment_definition_id")]
    public int InstallmentDefinitionId { get; set; }
    
    [Column("credit_card_id")]
    public int CreditCardId { get; set; }
    
    [Column("description")]
    public string Description { get; set; } = null!;
    
    [Column("monthly_amount")]
    public decimal MonthlyAmount { get; set; }
    
    [Column("total_installments")]
    public int TotalInstallments { get; set; }
    
    [Column("first_installment_date")]
    public DateTime FirstInstallmentDate { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public CreditCard CreditCard { get; set; } = null!;
    public Transaction? Transaction { get; set; }
}