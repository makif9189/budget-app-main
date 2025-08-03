using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface IUserService
{
    Task<ApiResponse<UserDto>> GetUserByIdAsync(int id);
}