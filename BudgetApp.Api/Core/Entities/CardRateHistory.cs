using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("card_rate_histories")]
public class CardRateHistory
{
    [Key]
    [Column("rate_history_id")]
    public int RateHistoryId { get; set; }
    
    [Column("credit_card_id")]
    public int CreditCardId { get; set; }
    
    [Column("effective_date")]
    public DateTime EffectiveDate { get; set; }
    
    [Column("interest_rate")]
    public decimal InterestRate { get; set; }
    
    [Column("kkdf_rate")]
    public decimal KkdfRate { get; set; }
    
    [Column("bsmv_rate")]
    public decimal BsmvRate { get; set; }
    
    [Column("minimum_payment_rate")]
    public decimal MinimumPaymentRate { get; set; }
    
    [Column("absolute_minimum_payment")]
    public decimal? AbsoluteMinimumPayment { get; set; }

    // Navigation Property
    public CreditCard CreditCard { get; set; } = null!;
}