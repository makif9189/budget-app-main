using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for credit card related services.
    /// </summary>
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCardDto>> GetCreditCardsByUserIdAsync(int userId);
        Task<CreditCardDto> GetCreditCardByIdAsync(int id);
        Task<CreditCardDto> CreateCreditCardAsync(int userId, CreateCreditCardDto creditCardDto);
        Task<bool> UpdateCreditCardAsync(int id, CreateCreditCardDto creditCardDto);
        Task<bool> DeleteCreditCardAsync(int id);
    }
}
