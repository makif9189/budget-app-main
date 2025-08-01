namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents the interest rate history for a credit card.
    /// Corresponds to the 'card_rate_history' table.
    /// </summary>
    public class CardRateHistory
    {
        public int Rate_History_Id { get; set; }
        public int Credit_Card_Id { get; set; }
        public DateTime Effective_Date { get; set; }
        public decimal Interest_Rate { get; set; }
        public decimal KKDF_Rate { get; set; }
        public decimal Bsmv_Rate { get; set; }
        public decimal Minimum_Payment_Rate { get; set; }
        public decimal? Absolute_Minimum_Payment { get; set; }

        // Navigation Property
        public CreditCard CreditCard { get; set; } = new CreditCard();
    }
}
