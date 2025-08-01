using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Services.Implementations
{
    /// <summary>
    /// Service for handling business logic related to credit cards.
    /// </summary>
    public class CreditCardService : ICreditCardService
    {
        private readonly IRepository<CreditCard> _creditCardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreditCardService(IRepository<CreditCard> creditCardRepository, IUnitOfWork unitOfWork)
        {
            _creditCardRepository = creditCardRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreditCardDto> CreateCreditCardAsync(int userId, CreateCreditCardDto creditCardDto)
        {
            // Map DTO to Entity
            var creditCard = new CreditCard
            {
                User_Id = userId,
                Name = creditCardDto.Name,
                Bank_Name = creditCardDto.BankName,
                Last_4_Digits = creditCardDto.Last4Digits,
                Statement_Day = creditCardDto.StatementDay,
                Payment_Due_Date_Offset = creditCardDto.PaymentDueDateOffset,
                Card_Limit = creditCardDto.CardLimit,
                Expiration_Date = creditCardDto.ExpirationDate
            };

            await _creditCardRepository.AddAsync(creditCard);
            await _unitOfWork.CompleteAsync();

            // Map Entity back to DTO
            return MapToDto(creditCard);
        }

        public async Task<bool> DeleteCreditCardAsync(int id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);
            if (creditCard == null)
            {
                return false;
            }

            _creditCardRepository.Remove(creditCard);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<CreditCardDto> GetCreditCardByIdAsync(int id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);
            if (creditCard == null)
            {
                return null;
            }
            return MapToDto(creditCard);
        }

        public async Task<IEnumerable<CreditCardDto>> GetCreditCardsByUserIdAsync(int userId)
        {
            var creditCards = await _creditCardRepository.FindAsync(c => c.User_Id == userId);
            return creditCards.Select(MapToDto);
        }

        public async Task<bool> UpdateCreditCardAsync(int id, CreateCreditCardDto creditCardDto)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);
            if (creditCard == null)
            {
                return false;
            }

            // Update properties
            creditCard.Name = creditCardDto.Name;
            creditCard.Bank_Name = creditCardDto.BankName;
            creditCard.Last_4_Digits = creditCardDto.Last4Digits;
            creditCard.Statement_Day = creditCardDto.StatementDay;
            creditCard.Payment_Due_Date_Offset = creditCardDto.PaymentDueDateOffset;
            creditCard.Card_Limit = creditCardDto.CardLimit;
            creditCard.Expiration_Date = creditCardDto.ExpirationDate;

            // The 'updated_at' field will be handled by AppDbContext
            await _unitOfWork.CompleteAsync();
            return true;
        }

        /// <summary>
        /// Helper method to map a CreditCard entity to a CreditCardDto.
        /// </summary>
        private CreditCardDto MapToDto(CreditCard creditCard)
        {
            return new CreditCardDto
            {
                CreditCardId = creditCard.Credit_Card_Id,
                Name = creditCard.Name,
                BankName = creditCard.Bank_Name,
                Last4Digits = creditCard.Last_4_Digits,
                StatementDay = creditCard.Statement_Day,
                PaymentDueDateOffset = creditCard.Payment_Due_Date_Offset,
                CardLimit = creditCard.Card_Limit,
                ExpirationDate = creditCard.Expiration_Date
            };
        }
    }
}
