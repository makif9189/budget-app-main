namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single expense item.
    /// Corresponds to the 'expense_items' table.
    /// </summary>
    public class ExpenseItem : IAuditable
    {
        public int Expense_Item_Id { get; set; }
        public int User_Id { get; set; }
        public int Expense_Category_Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Transaction_Date { get; set; }
        public string? Description { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ExpenseCategory ExpenseCategory { get; set; } = new ExpenseCategory();
        public Transaction Transaction { get; set; } = new Transaction();
    }
}
