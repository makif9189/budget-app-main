using System.Security.Claims;
using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for credit card related services.
    /// </summary>
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCardDto>> GetCreditCardsByUserIdAsync(int userId);
        Task<CreditCardDto> GetCreditCardByIdAsync(int id,int userId);
        Task<CreditCardDto> CreateCreditCardAsync(int userId, CreateCreditCardDto creditCardDto);
        Task<bool> UpdateCreditCardAsync(int id,CreditCardDto creditCardDto);
        Task<bool> DeleteCreditCardAsync(int id);
    }
}
