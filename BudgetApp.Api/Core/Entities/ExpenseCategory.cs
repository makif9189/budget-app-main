namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a category for expenses.
    /// Corresponds to the 'expense_categories' table.
    /// </summary>
    public class ExpenseCategory : IAuditable
    {
        public int expense_category_id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ICollection<ExpenseItem> ExpenseItems { get; set; } = new List<ExpenseItem>();
    }
}
