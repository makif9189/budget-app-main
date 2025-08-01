using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Enums;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Services.Implementations
{
    /// <summary>
    /// Service for handling business logic related to incomes.
    /// </summary>
    public class IncomeService : IIncomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<IncomeItem> _incomeRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public IncomeService(IUnitOfWork unitOfWork, IRepository<IncomeItem> incomeRepository, IRepository<Transaction> transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _incomeRepository = incomeRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<IncomeDto> CreateIncomeAsync(int userId, CreateIncomeDto incomeDto)
        {
            var incomeItem = new IncomeItem
            {
                user_id = userId,
                income_source_id = incomeDto.IncomeSourceId,
                amount = incomeDto.Amount,
                transaction_date = incomeDto.TransactionDate,
                description = incomeDto.Description,
                is_recurring = incomeDto.IsRecurring
            };

            await _incomeRepository.AddAsync(incomeItem);
            await _unitOfWork.CompleteAsync(); // Save to get the ID

            var transaction = new Transaction
            {
                user_id = userId,
                transaction_date = incomeItem.transaction_date,
                description = incomeItem.description,
                amount = incomeItem.amount,
                type = TransactionTypeEnum.GELIR,
                income_item_id = incomeItem.income_item_id
            };

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            return new IncomeDto
            {
                IncomeItemId = incomeItem.income_item_id,
                IncomeSourceId = incomeItem.income_source_id,
                Amount = incomeItem.amount,
                TransactionDate = incomeItem.transaction_date,
                Description = incomeItem.description,
                IsRecurring = incomeItem.is_recurring,
                SourceName = "N/A" // Needs join/mapping
            };
        }

        public async Task<IEnumerable<IncomeDto>> GetIncomesByUserIdAsync(int userId)
        {
            var incomes = await _incomeRepository.FindAsync(i => i.user_id == userId);
            return incomes.Select(i => new IncomeDto {
                IncomeItemId = i.income_item_id,
                IncomeSourceId = i.income_source_id,
                Amount = i.amount,
                TransactionDate = i.transaction_date,
                Description = i.description,
                IsRecurring = i.is_recurring,
                SourceName = "N/A"
            });
        }
    }
}
