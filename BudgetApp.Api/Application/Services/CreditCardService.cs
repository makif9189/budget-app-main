using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Application.Services;

public class CreditCardService : ICreditCardService
{
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreditCardService(
        ICreditCardRepository creditCardRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _creditCardRepository = creditCardRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<CreditCardDto>>> GetCreditCardsByUserIdAsync(int userId)
    {
        try
        {
            var creditCards = await _creditCardRepository.GetByUserIdAsync(userId);
            var creditCardDtos = _mapper.Map<IEnumerable<CreditCardDto>>(creditCards);
            
            return ApiResponse<IEnumerable<CreditCardDto>>.SuccessResult(creditCardDtos);
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<CreditCardDto>>.ErrorResult($"Failed to retrieve credit cards: {ex.Message}");
        }
    }

    public async Task<ApiResponse<CreditCardDto>> GetCreditCardByIdAsync(int id, int userId)
    {
        try
        {
            var creditCard = await _creditCardRepository.GetByIdAndUserIdAsync(id, userId);
            if (creditCard == null)
            {
                return ApiResponse<CreditCardDto>.ErrorResult("Credit card not found or access denied.");
            }

            var creditCardDto = _mapper.Map<CreditCardDto>(creditCard);
            return ApiResponse<CreditCardDto>.SuccessResult(creditCardDto);
        }
        catch (Exception ex)
        {
            return ApiResponse<CreditCardDto>.ErrorResult($"Failed to retrieve credit card: {ex.Message}");
        }
    }

    public async Task<ApiResponse<CreditCardDto>> CreateCreditCardAsync(int userId, CreateCreditCardDto creditCardDto)
    {
        try
        {
            var creditCard = _mapper.Map<CreditCard>(creditCardDto);
            creditCard.UserId = userId;

            await _creditCardRepository.AddAsync(creditCard);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<CreditCardDto>(creditCard);
            return ApiResponse<CreditCardDto>.SuccessResult(result, "Credit card created successfully.");
        }
        catch (Exception ex)
        {
            return ApiResponse<CreditCardDto>.ErrorResult($"Failed to create credit card: {ex.Message}");
        }
    }

    public async Task<ApiResponse> UpdateCreditCardAsync(int id, int userId, CreditCardDto creditCardDto)
    {
        try
        {
            var existingCard = await _creditCardRepository.GetByIdAndUserIdAsync(id, userId);
            if (existingCard == null)
            {
                return ApiResponse.ErrorResult("Credit card not found or access denied.");
            }

            _mapper.Map(creditCardDto, existingCard);
            _creditCardRepository.Update(existingCard);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse.SuccessResult("Credit card updated successfully.");
        }
        catch (Exception ex)
        {
            return ApiResponse.ErrorResult($"Failed to update credit card: {ex.Message}");
        }
    }

    public async Task<ApiResponse> DeleteCreditCardAsync(int id, int userId)
    {
        try
        {
            var creditCard = await _creditCardRepository.GetByIdAndUserIdAsync(id, userId);
            if (creditCard == null)
            {
                return ApiResponse.ErrorResult("Credit card not found or access denied.");
            }

            _creditCardRepository.Remove(creditCard);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse.SuccessResult("Credit card deleted successfully.");
        }
        catch (Exception ex)
        {
            return ApiResponse.ErrorResult($"Failed to delete credit card: {ex.Message}");
        }
    }
}