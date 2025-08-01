namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a credit card belonging to a user.
    /// Corresponds to the 'credit_cards' table.
    /// </summary>
    public class CreditCard : IAuditable
    {
        public int credit_card_id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string? bank_name { get; set; }
        public string? last_4_digits { get; set; }
        public int statement_day { get; set; }
        public int payment_due_date_offset { get; set; }
        public decimal? card_limit { get; set; }
        public string? expiration_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ICollection<CardRateHistory> CardRateHistories { get; set; } = new List<CardRateHistory>();
        public ICollection<InstallmentDefinition> InstallmentDefinitions { get; set; } = new List<InstallmentDefinition>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
