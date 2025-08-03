using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExpenseService(
        IExpenseRepository expenseRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ExpenseDto>>> GetExpensesByUserIdAsync(int userId)
    {
        try
        {
            var expenses = await _expenseRepository.GetByUserIdWithCategoryAsync(userId);
            var expenseDtos = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
            
            return ApiResponse<IEnumerable<ExpenseDto>>.SuccessResult(expenseDtos);
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<ExpenseDto>>.ErrorResult($"Failed to retrieve expenses: {ex.Message}");
        }
    }

    public async Task<ApiResponse<ExpenseDto>> CreateExpenseAsync(int userId, CreateExpenseDto expenseDto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Create expense item
            var expenseItem = _mapper.Map<ExpenseItem>(expenseDto);
            expenseItem.UserId = userId;

            await _expenseRepository.AddAsync(expenseItem);
            await _unitOfWork.SaveChangesAsync(); // Save to get the ID

            // Create corresponding transaction
            var transaction = new Transaction
            {
                UserId = userId,
                TransactionDate = expenseItem.TransactionDate,
                Description = expenseItem.Description ?? "Expense",
                Amount = expenseItem.Amount,
                Type = 2,
                ExpenseItemId = expenseItem.ExpenseItemId
            };

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            // Get the created expense with category info
            var createdExpense = await _expenseRepository.GetByUserIdWithCategoryAsync(userId);
            var result = createdExpense.FirstOrDefault(e => e.ExpenseItemId == expenseItem.ExpenseItemId);
            
            if (result != null)
            {
                var expenseDto_result = _mapper.Map<ExpenseDto>(result);
                return ApiResponse<ExpenseDto>.SuccessResult(expenseDto_result, "Expense created successfully.");
            }

            return ApiResponse<ExpenseDto>.ErrorResult("Failed to retrieve created expense.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ApiResponse<ExpenseDto>.ErrorResult($"Failed to create expense: {ex.Message}");
        }
    }
}