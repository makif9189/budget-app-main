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
                user_id = userId,
                name = creditCardDto.Name,
                bank_name = creditCardDto.BankName,
                last_4_digits = creditCardDto.Last4Digits,
                statement_day = creditCardDto.StatementDay,
                payment_due_date_offset = creditCardDto.PaymentDueDateOffset,
                card_limit = creditCardDto.CardLimit,
                expiration_date = creditCardDto.ExpirationDate
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
            var creditCards = await _creditCardRepository.FindAsync(c => c.user_id == userId);
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
            creditCard.name = creditCardDto.Name;
            creditCard.bank_name = creditCardDto.BankName;
            creditCard.last_4_digits = creditCardDto.Last4Digits;
            creditCard.statement_day = creditCardDto.StatementDay;
            creditCard.payment_due_date_offset = creditCardDto.PaymentDueDateOffset;
            creditCard.card_limit = creditCardDto.CardLimit;
            creditCard.expiration_date = creditCardDto.ExpirationDate;

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
                CreditCardId = creditCard.credit_card_id,
                Name = creditCard.name,
                BankName = creditCard.bank_name,
                Last4Digits = creditCard.last_4_digits,
                StatementDay = creditCard.statement_day,
                PaymentDueDateOffset = creditCard.payment_due_date_offset,
                CardLimit = creditCard.card_limit,
                ExpirationDate = creditCard.expiration_date
            };
        }
    }
}
