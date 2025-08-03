using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface IIncomeService
{
    Task<ApiResponse<IEnumerable<IncomeDto>>> GetIncomesByUserIdAsync(int userId);
    Task<ApiResponse<IncomeDto>> CreateIncomeAsync(int userId, CreateIncomeDto incomeDto);
}