using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for income related services.
    /// </summary>
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeDto>> GetIncomesByUserIdAsync(int userId);
        Task<IncomeDto> CreateIncomeAsync(int userId, CreateIncomeDto incomeDto);
    }
}
