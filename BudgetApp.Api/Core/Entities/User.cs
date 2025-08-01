using System.Collections.Generic;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a user in the system.
    /// Corresponds to the 'users' table in the database.
    /// </summary>
    public class User : IAuditable
    {
        public int User_Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; }=null!;
        public string Password_Hash { get; set; }=null!;
        public DateTime Created_At { get; set; }
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
