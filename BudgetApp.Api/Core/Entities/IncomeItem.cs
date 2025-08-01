namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single income item.
    /// Corresponds to the 'income_items' table.
    /// </summary>
    public class IncomeItem : IAuditable
    {
        public int income_item_id { get; set; }
        public int user_id { get; set; }
        public int income_source_id { get; set; }
        public decimal amount { get; set; }
        public DateTime transaction_date { get; set; }
        public string? description { get; set; }
        public bool is_recurring { get; set; } = false;
        public string? recurring_frequency { get; set; }
        public DateTime? recurring_start_date { get; set; }
        public DateTime? recurring_end_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public IncomeSource IncomeSource { get; set; }
        public Transaction Transaction { get; set; }
    }
}
