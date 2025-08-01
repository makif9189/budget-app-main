namespace BudgetApp.Api.Core.DTOs
{
    /// <summary>
    /// A comprehensive DTO to represent any financial transaction in a unified way.
    /// </summary>
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Provides context for the transaction, e.g., "Maaş" for income or "Fatura" for an expense.
        /// </summary>
        public string CategoryOrSource { get; set; }
    }
}
