using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a category for expenses.
    /// Corresponds to the 'expense_categories' table.
    /// </summary>
    [Table("expense_categories")]
    public class ExpenseCategory : IAuditable
    {
        [Key]
        [Column("expense_category_id")]
        public int Expense_Category_Id { get; set; }
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<ExpenseItem> ExpenseItems { get; set; } = [];
    }
}
