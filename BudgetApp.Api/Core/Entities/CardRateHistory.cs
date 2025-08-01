namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents the interest rate history for a credit card.
    /// Corresponds to the 'card_rate_history' table.
    /// </summary>
    public class CardRateHistory
    {
        public int rate_history_id { get; set; }
        public int credit_card_id { get; set; }
        public DateTime effective_date { get; set; }
        public decimal interest_rate { get; set; }
        public decimal kkdf_rate { get; set; }
        public decimal bsmv_rate { get; set; }
        public decimal minimum_payment_rate { get; set; }
        public decimal? absolute_minimum_payment { get; set; }

        // Navigation Property
        public CreditCard CreditCard { get; set; }
    }
}
