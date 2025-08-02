using BudgetApp.Api.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single financial transaction, linking various types of financial activities.
    /// Corresponds to the 'transactions' table.
    /// </summary>
    [Table("transactions")]
    public class Transaction : IAuditable
    {
        [Column("transaction_id")]
        public int Transaction_Id { get; set; }
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("transaction_date")]
        public DateTime Transaction_Date { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("type")]
        public TransactionTypeEnum Type { get; set; }
        [Column("income_item_id")]
        public int? Income_Item_Id { get; set; }
        [Column("expense_item_id")]
        public int? Expense_Item_Id { get; set; }
        [Column("credit_card_id")]
        public int? Credit_Card_Id { get; set; }
        [Column("installment_definition_id")]
        public int? Installment_Definition_Id { get; set; }
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public IncomeItem? IncomeItem { get; set; }
        public ExpenseItem? ExpenseItem { get; set; }
        public CreditCard? CreditCard { get; set; }
        public InstallmentDefinition? InstallmentDefinition { get; set; }
    }
}
