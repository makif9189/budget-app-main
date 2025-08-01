using BudgetApp.Api.Core.Enums;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single financial transaction, linking various types of financial activities.
    /// Corresponds to the 'transactions' table.
    /// </summary>
    public class Transaction : IAuditable
    {
        public int transaction_id { get; set; }
        public int user_id { get; set; }
        public DateTime transaction_date { get; set; }
        public string? description { get; set; }
        public decimal amount { get; set; }
        public TransactionTypeEnum type { get; set; }

        // Foreign keys to specific transaction types (nullable)
        public int? income_item_id { get; set; }
        public int? expense_item_id { get; set; }
        public int? credit_card_id { get; set; }
        public int? installment_definition_id { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public IncomeItem? IncomeItem { get; set; }
        public ExpenseItem? ExpenseItem { get; set; }
        public CreditCard? CreditCard { get; set; }
        public InstallmentDefinition? InstallmentDefinition { get; set; }
    }
}
