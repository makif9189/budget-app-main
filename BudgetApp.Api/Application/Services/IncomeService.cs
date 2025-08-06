using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Application.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public IncomeService(
        IIncomeRepository incomeRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _incomeRepository = incomeRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<IncomeDto>>> GetIncomesByUserIdAsync(int userId)
    {
        try
        {
            var incomes = await _incomeRepository.GetByUserIdWithSourceAsync(userId);
            var incomeDtos = _mapper.Map<IEnumerable<IncomeDto>>(incomes);

            return ApiResponse<IEnumerable<IncomeDto>>.SuccessResult(incomeDtos);
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<IncomeDto>>.ErrorResult($"Failed to retrieve incomes: {ex.Message}");
        }
    }

    public async Task<ApiResponse<IncomeDto>> CreateIncomeAsync(int userId, CreateIncomeDto incomeDto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Create income item
            var incomeItem = _mapper.Map<IncomeItem>(incomeDto);
            incomeItem.UserId = userId;

            await _incomeRepository.AddAsync(incomeItem);
            await _unitOfWork.SaveChangesAsync(); // Save to get the ID

            // Create corresponding transaction
            var transaction = new Transaction
            {
                UserId = userId,
                TransactionDate = incomeItem.TransactionDate,
                Description = incomeItem.Description ?? "Income",
                Amount = incomeItem.Amount,
                Type = 1,
                IncomeItemId = incomeItem.IncomeItemId
            };

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            // Get the created income with source info
            var createdIncomes = await _incomeRepository.GetByUserIdWithSourceAsync(userId);
            var result = createdIncomes.FirstOrDefault(i => i.IncomeItemId == incomeItem.IncomeItemId);

            if (result != null)
            {
                var incomeDto_result = _mapper.Map<IncomeDto>(result);
                return ApiResponse<IncomeDto>.SuccessResult(incomeDto_result, "Income created successfully.");
            }

            return ApiResponse<IncomeDto>.ErrorResult("Failed to retrieve created income.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ApiResponse<IncomeDto>.ErrorResult($"Failed to create income: {ex.Message}");
        }
    }
    
    public async Task<ApiResponse<IEnumerable<IncomeSourceDto>>> GetIncomeSourcesAsync(int userId)
{
    try
    {
        var sources = new List<IncomeSourceDto>
        {
            new() { IncomeSourceId = 1, Name = "Maaş" },
            new() { IncomeSourceId = 2, Name = "Freelance" },
            new() { IncomeSourceId = 3, Name = "Yatırım" },
            new() { IncomeSourceId = 4, Name = "Fatura Yardımı" },
            new() { IncomeSourceId = 5, Name = "Diğer" }
        };

        return ApiResponse<IEnumerable<IncomeSourceDto>>.SuccessResult(sources);
    }
    catch (Exception ex)
    {
        return ApiResponse<IEnumerable<IncomeSourceDto>>.ErrorResult($"Failed to get income sources: {ex.Message}");
    }
}
}