using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface ICategoryService
{
    Task<ApiResponse<IEnumerable<ExpenseCategoryDto>>> GetExpenseCategoriesAsync(int userId);
    Task<ApiResponse<IEnumerable<IncomeSourceDto>>> GetIncomeSourcesAsync(int userId);
    Task<ApiResponse<ExpenseCategoryDto>> CreateExpenseCategoryAsync(int userId, string categoryName);
    Task<ApiResponse<IncomeSourceDto>> CreateIncomeSourceAsync(int userId, string sourceName);
}

// BudgetApp.Api/Core/DTOs/CategoryDto.cs
