using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface IDashboardService
{
    Task<ApiResponse<DashboardSummaryDto>> GetDashboardSummaryAsync(int userId, DateTime? startDate = null, DateTime? endDate = null);
}

