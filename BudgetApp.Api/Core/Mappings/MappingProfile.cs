using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Core.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreditCards, opt => opt.Ignore())
            .ForMember(dest => dest.ExpenseCategories, opt => opt.Ignore())
            .ForMember(dest => dest.ExpenseItems, opt => opt.Ignore())
            .ForMember(dest => dest.IncomeSources, opt => opt.Ignore())
            .ForMember(dest => dest.IncomeItems, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        // CreditCard mappings
        CreateMap<CreditCard, CreditCardDto>()
            .ForMember(dest => dest.CreditCardId, opt => opt.MapFrom(src => src.CreditCardId));

        CreateMap<CreateCreditCardDto, CreditCard>()
            .ForMember(dest => dest.CreditCardId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.CardRateHistories, opt => opt.Ignore())
            .ForMember(dest => dest.InstallmentDefinitions, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        CreateMap<CreditCardDto, CreditCard>()
            .ForMember(dest => dest.CreditCardId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.CardRateHistories, opt => opt.Ignore())
            .ForMember(dest => dest.InstallmentDefinitions, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        // ExpenseItem mappings
        CreateMap<ExpenseItem, ExpenseDto>()
            .ForMember(dest => dest.ExpenseItemId, opt => opt.MapFrom(src => src.ExpenseItemId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ExpenseCategory.Name));

        CreateMap<CreateExpenseDto, ExpenseItem>()
            .ForMember(dest => dest.ExpenseItemId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.ExpenseCategory, opt => opt.Ignore())
            .ForMember(dest => dest.Transaction, opt => opt.Ignore());

        // IncomeItem mappings
        CreateMap<IncomeItem, IncomeDto>()
            .ForMember(dest => dest.IncomeItemId, opt => opt.MapFrom(src => src.IncomeItemId))
            .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.IncomeSource.Name));

        CreateMap<CreateIncomeDto, IncomeItem>()
            .ForMember(dest => dest.IncomeItemId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(dest => dest.RecurringFrequency, opt => opt.Ignore())
            .ForMember(dest => dest.RecurringStartDate, opt => opt.Ignore())
            .ForMember(dest => dest.RecurringEndDate, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.IncomeSource, opt => opt.Ignore())
            .ForMember(dest => dest.Transaction, opt => opt.Ignore());

        // Transaction mappings
        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty))
            .ForMember(dest => dest.CategoryOrSource, opt => opt.MapFrom(src =>
                src.Type == 1
                    ? (src.IncomeItem != null ? src.IncomeItem.IncomeSource.Name : "Bilinmeyen Gelir")
                    : (src.ExpenseItem != null ? src.ExpenseItem.ExpenseCategory.Name : "Genel Gider")));
    }
}