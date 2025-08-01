using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for expense related services.
    /// </summary>
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetExpensesByUserIdAsync(int userId);
        Task<ExpenseDto> CreateExpenseAsync(int userId, CreateExpenseDto expenseDto);
    }
}
