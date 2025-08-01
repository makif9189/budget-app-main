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
                User_Id = userId,
                Expense_Category_Id = expenseDto.ExpenseCategoryId,
                Amount = expenseDto.Amount,
                Transaction_Date = expenseDto.TransactionDate,
                Description = expenseDto.Description
            };

            await _expenseRepository.AddAsync(expenseItem);
            // We must save here to get the expense_item_id for the transaction
            await _unitOfWork.CompleteAsync();

            var transaction = new Transaction
            {
                User_Id = userId,
                Transaction_Date = expenseItem.Transaction_Date,
                Description = expenseItem.Description,
                Amount = expenseItem.Amount,
                Type = TransactionTypeEnum.HARCAMA, // Or GENEL_GIDER based on logic
                Expense_Item_Id = expenseItem.Expense_Item_Id
            };

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            // This mapping would be better with a library like AutoMapper
            return new ExpenseDto
            {
                ExpenseItemId = expenseItem.Expense_Item_Id,
                ExpenseCategoryId = expenseItem.Expense_Category_Id,
                Amount = expenseItem.Amount,
                TransactionDate = expenseItem.Transaction_Date,
                Description = expenseItem.Description,
                // CategoryName would need another repository call to get category details
                CategoryName = "N/A" 
            };
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpensesByUserIdAsync(int userId)
        {
            var expenses = await _expenseRepository.FindAsync(e => e.User_Id == userId);
            // In a real app, you would join with ExpenseCategory to get the name
            // and map to ExpenseDto.
            return expenses.Select(e => new ExpenseDto {
                 ExpenseItemId = e.Expense_Item_Id,
                 ExpenseCategoryId = e.Expense_Category_Id,
                 Amount = e.Amount,
                 TransactionDate = e.Transaction_Date,
                 Description = e.Description,
                 CategoryName = "N/A"
            });
        }
    }
}
