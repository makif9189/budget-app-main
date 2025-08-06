using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ExpenseCategoryDto>>> GetExpenseCategoriesAsync(int userId)
    {
        try
        {
            // Repository'den kategorileri getir - bu repository'yi oluşturmanız gerekecek
            // Şimdilik varsayılan kategoriler döndür
            var defaultCategories = new List<ExpenseCategory>
            {
                new() { ExpenseCategoryId = 1, Name = "Kira", UserId = userId },
                new() { ExpenseCategoryId = 2, Name = "Market", UserId = userId },
                new() { ExpenseCategoryId = 3, Name = "Ulaşım", UserId = userId },
                new() { ExpenseCategoryId = 4, Name = "Faturalar", UserId = userId },
                new() { ExpenseCategoryId = 5, Name = "Diğer", UserId = userId }
            };
            
            var categoryDtos = _mapper.Map<IEnumerable<ExpenseCategoryDto>>(defaultCategories);
            return ApiResponse<IEnumerable<ExpenseCategoryDto>>.SuccessResult(categoryDtos);
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<ExpenseCategoryDto>>.ErrorResult($"Failed to retrieve expense categories: {ex.Message}");
        }
    }

    public async Task<ApiResponse<IEnumerable<IncomeSourceDto>>> GetIncomeSourcesAsync(int userId)
    {
        try
        {
            var defaultSources = new List<IncomeSource>
            {
                new() { IncomeSourceId = 1, Name = "Maaş", UserId = userId },
                new() { IncomeSourceId = 2, Name = "Freelance", UserId = userId },
                new() { IncomeSourceId = 3, Name = "Yatırım", UserId = userId },
                new() { IncomeSourceId = 4, Name = "Diğer", UserId = userId }
            };

            var sourceDtos = _mapper.Map<IEnumerable<IncomeSourceDto>>(defaultSources);
            return ApiResponse<IEnumerable<IncomeSourceDto>>.SuccessResult(sourceDtos, "Income Source created successfully.");
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<IncomeSourceDto>>.ErrorResult($"Failed to create category: {ex.Message}");
        }
    }

    public async Task<ApiResponse<ExpenseCategoryDto>> CreateExpenseCategoryAsync(int userId, string categoryName)
    {
        try
        {
            var category = new ExpenseCategory
            {
                Name = categoryName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Repository işlemleri burada yapılacak
            var categoryDto = _mapper.Map<ExpenseCategoryDto>(category);
            return ApiResponse<ExpenseCategoryDto>.SuccessResult(categoryDto, "Category created successfully.");
        }
        catch (Exception ex)
        {
            return ApiResponse<ExpenseCategoryDto>.ErrorResult($"Failed to create category: {ex.Message}");
        }
    }

    public async Task<ApiResponse<IncomeSourceDto>> CreateIncomeSourceAsync(int userId, string sourceName)
    {
        try
        {
            var source = new IncomeSource
            {
                Name = sourceName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var sourceDto = _mapper.Map<IncomeSourceDto>(source);
            return ApiResponse<IncomeSourceDto>.SuccessResult(sourceDto, "Income source created successfully.");
        }
        catch (Exception ex)
        {
            return ApiResponse<IncomeSourceDto>.ErrorResult($"Failed to create income source: {ex.Message}");
        }
    }
}