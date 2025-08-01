using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Enums;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Services.Implementations
{
    /// <summary>
    /// Service for handling business logic related to expenses.
    /// </summary>
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ExpenseItem> _expenseRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public ExpenseService(IUnitOfWork unitOfWork, IRepository<ExpenseItem> expenseRepository, IRepository<Transaction> transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _expenseRepository = expenseRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<ExpenseDto> CreateExpenseAsync(int userId, CreateExpenseDto expenseDto)
        {
            var expenseItem = new ExpenseItem
            {
                user_id = userId,
                expense_category_id = expenseDto.ExpenseCategoryId,
                amount = expenseDto.Amount,
                transaction_date = expenseDto.TransactionDate,
                description = expenseDto.Description
            };

            await _expenseRepository.AddAsync(expenseItem);
            // We must save here to get the expense_item_id for the transaction
            await _unitOfWork.CompleteAsync();

            var transaction = new Transaction
            {
                user_id = userId,
                transaction_date = expenseItem.transaction_date,
                description = expenseItem.description,
                amount = expenseItem.amount,
                type = TransactionTypeEnum.HARCAMA, // Or GENEL_GIDER based on logic
                expense_item_id = expenseItem.expense_item_id
            };

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            // This mapping would be better with a library like AutoMapper
            return new ExpenseDto
            {
                ExpenseItemId = expenseItem.expense_item_id,
                ExpenseCategoryId = expenseItem.expense_category_id,
                Amount = expenseItem.amount,
                TransactionDate = expenseItem.transaction_date,
                Description = expenseItem.description,
                // CategoryName would need another repository call to get category details
                CategoryName = "N/A" 
            };
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpensesByUserIdAsync(int userId)
        {
            var expenses = await _expenseRepository.FindAsync(e => e.user_id == userId);
            // In a real app, you would join with ExpenseCategory to get the name
            // and map to ExpenseDto.
            return expenses.Select(e => new ExpenseDto {
                 ExpenseItemId = e.expense_item_id,
                 ExpenseCategoryId = e.expense_category_id,
                 Amount = e.amount,
                 TransactionDate = e.transaction_date,
                 Description = e.description,
                 CategoryName = "N/A"
            });
        }
    }
}
