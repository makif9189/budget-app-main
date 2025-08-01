namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a category for expenses.
    /// Corresponds to the 'expense_categories' table.
    /// </summary>
    public class ExpenseCategory : IAuditable
    {
        public int Expense_Category_Id { get; set; }
        public int User_Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<ExpenseItem> ExpenseItems { get; set; } = [];
    }
}
