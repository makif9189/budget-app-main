using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single expense item.
    /// Corresponds to the 'expense_items' table.
    /// </summary>
    [Table("expense_items")]
    public class ExpenseItem : IAuditable
    {
        [Column("expense_item_id")]
        public int Expense_Item_Id { get; set; }
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("expense_category_id")]
        public int Expense_Category_Id { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("transaction_date")]
        public DateTime Transaction_Date { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ExpenseCategory ExpenseCategory { get; set; } = new ExpenseCategory();
        public Transaction Transaction { get; set; } = new Transaction();
    }
}
