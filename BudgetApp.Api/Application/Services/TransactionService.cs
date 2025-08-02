using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;

namespace BudgetApp.Api.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<TransactionDto>>> GetTransactionsByUserIdAsync(
        int userId, 
        DateTime? startDate, 
        DateTime? endDate)
    {
        try
        {
            var transactions = await _transactionRepository.GetByUserIdWithDetailsAsync(userId, startDate, endDate);
            var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
            
            return ApiResponse<IEnumerable<TransactionDto>>.SuccessResult(transactionDtos);
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<TransactionDto>>.ErrorResult($"Failed to retrieve transactions: {ex.Message}");
        }
    }
}