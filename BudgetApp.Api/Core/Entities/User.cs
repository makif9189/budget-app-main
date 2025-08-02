using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a user in the system.
    /// Corresponds to the 'users' table in the database.
    /// </summary>
    [Table("users")]
    public class User : IAuditable
    {
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("username")]
        public string Username { get; set; } = null!;
        [Column("email")]
        public string Email { get; set; } = null!;
        [Column("password_hash")]
        public string Password_Hash { get; set; } = null!;
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public ICollection<CreditCard> CreditCards { get; set; } = [];
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; } = [];
        public ICollection<ExpenseItem> ExpenseItems { get; set; } = [];
        public ICollection<IncomeSource> IncomeSources { get; set; } = [];
        public ICollection<IncomeItem> IncomeItems { get; set; } = [];
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
