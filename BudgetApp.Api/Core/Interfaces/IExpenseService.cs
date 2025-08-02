using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface IExpenseService
{
    Task<ApiResponse<IEnumerable<ExpenseDto>>> GetExpensesByUserIdAsync(int userId);
    Task<ApiResponse<ExpenseDto>> CreateExpenseAsync(int userId, CreateExpenseDto expenseDto);
}