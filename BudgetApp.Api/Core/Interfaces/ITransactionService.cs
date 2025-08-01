using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for services that handle financial transactions.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Gets all transactions for a specific user, optionally filtered by date.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <param name="startDate">The start date of the filter range.</param>
        /// <param name="endDate">The end date of the filter range.</param>
        /// <returns>A collection of unified transaction DTOs.</returns>
        Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(int userId, DateTime? startDate, DateTime? endDate);
    }
}
