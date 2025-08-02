using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents the interest rate history for a credit card.
    /// Corresponds to the 'card_rate_history' table.
    /// </summary>
    [Table("card_rate_histories")]
    public class CardRateHistory
    {
        [Key]
        [Column("rate_history_id")]
        public int Rate_History_Id { get; set; }
        [Column("credit_card_id")]
        public int Credit_Card_Id { get; set; }
        [Column("effective_date")]
        public DateTime Effective_Date { get; set; }
        [Column("interest_rate")]
        public decimal Interest_Rate { get; set; }
        [Column("kkdf_rate")]
        public decimal KKDF_Rate { get; set; }
        [Column("bsmv_rate")]
        public decimal Bsmv_Rate { get; set; }
        [Column("minimum_payment_rate")]
        public decimal Minimum_Payment_Rate { get; set; }
        [Column("absolute_minimum_payment")]
        public decimal? Absolute_Minimum_Payment { get; set; }

        // Navigation Property
        public CreditCard CreditCard { get; set; } = new CreditCard();
    }
}
