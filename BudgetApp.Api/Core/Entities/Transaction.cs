using BudgetApp.Api.Core.Enums;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single financial transaction, linking various types of financial activities.
    /// Corresponds to the 'transactions' table.
    /// </summary>
    public class Transaction : IAuditable
    {
        public int Transaction_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime Transaction_Date { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum Type { get; set; }

        // Foreign keys to specific transaction types (nullable)
        public int? Income_Item_Id { get; set; }
        public int? Expense_Item_Id { get; set; }
        public int? Credit_Card_Id { get; set; }
        public int? Installment_Definition_Id { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public IncomeItem? IncomeItem { get; set; }
        public ExpenseItem? ExpenseItem { get; set; }
        public CreditCard? CreditCard { get; set; }
        public InstallmentDefinition? InstallmentDefinition { get; set; }
    }
}
