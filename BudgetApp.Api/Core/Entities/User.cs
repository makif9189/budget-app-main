using System.Collections.Generic;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a user in the system.
    /// Corresponds to the 'users' table in the database.
    /// </summary>
    public class User : IAuditable
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Properties
        public ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; } = new List<ExpenseCategory>();
        public ICollection<ExpenseItem> ExpenseItems { get; set; } = new List<ExpenseItem>();
        public ICollection<IncomeSource> IncomeSources { get; set; } = new List<IncomeSource>();
        public ICollection<IncomeItem> IncomeItems { get; set; } = new List<IncomeItem>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
