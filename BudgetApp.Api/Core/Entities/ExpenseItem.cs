namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single expense item.
    /// Corresponds to the 'expense_items' table.
    /// </summary>
    public class ExpenseItem : IAuditable
    {
        public int expense_item_id { get; set; }
        public int user_id { get; set; }
        public int expense_category_id { get; set; }
        public decimal amount { get; set; }
        public DateTime transaction_date { get; set; }
        public string? description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public Transaction Transaction { get; set; }
    }
}
