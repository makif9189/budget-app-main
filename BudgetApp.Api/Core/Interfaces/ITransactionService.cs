using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface ITransactionService
{
    Task<ApiResponse<IEnumerable<TransactionDto>>> GetTransactionsByUserIdAsync(int userId, DateTime? startDate, DateTime? endDate);
}