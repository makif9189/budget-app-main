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
                User_Id = userId,
                Income_Source_Id = incomeDto.IncomeSourceId,
                Amount = incomeDto.Amount,
                Transaction_Date = incomeDto.TransactionDate,
                Description = incomeDto.Description,
                Is_Recurring = incomeDto.IsRecurring
            };

            await _incomeRepository.AddAsync(incomeItem);
            await _unitOfWork.CompleteAsync(); // Save to get the ID

            var transaction = new Transaction
            {
                User_Id = userId,
                Transaction_Date = incomeItem.Transaction_Date,
                Description = incomeItem.Description,
                Amount = incomeItem.Amount,
                Type = TransactionTypeEnum.GELIR,
                Income_Item_Id = incomeItem.Income_Item_Id
            };

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            return new IncomeDto
            {
                IncomeItemId = incomeItem.Income_Item_Id,
                IncomeSourceId = incomeItem.Income_Source_Id,
                Amount = incomeItem.Amount,
                TransactionDate = incomeItem.Transaction_Date,
                Description = incomeItem.Description,
                IsRecurring = incomeItem.Is_Recurring,
                SourceName = "N/A" // Needs join/mapping
            };
        }

        public async Task<IEnumerable<IncomeDto>> GetIncomesByUserIdAsync(int userId)
        {
            var incomes = await _incomeRepository.FindAsync(i => i.User_Id == userId);
            return incomes.Select(i => new IncomeDto {
                IncomeItemId = i.Income_Item_Id,
                IncomeSourceId = i.Income_Source_Id,
                Amount = i.Amount,
                TransactionDate = i.Transaction_Date,
                Description = i.Description,
                IsRecurring = i.Is_Recurring,
                SourceName = "N/A"
            });
        }
    }
}
