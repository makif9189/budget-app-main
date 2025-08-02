using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;

namespace BudgetApp.Api.Core.Interfaces.Services;

public interface ICreditCardService
{
    Task<ApiResponse<IEnumerable<CreditCardDto>>> GetCreditCardsByUserIdAsync(int userId);
    Task<ApiResponse<CreditCardDto>> GetCreditCardByIdAsync(int id, int userId);
    Task<ApiResponse<CreditCardDto>> CreateCreditCardAsync(int userId, CreateCreditCardDto creditCardDto);
    Task<ApiResponse> UpdateCreditCardAsync(int id, int userId, CreditCardDto creditCardDto);
    Task<ApiResponse> DeleteCreditCardAsync(int id, int userId);
}