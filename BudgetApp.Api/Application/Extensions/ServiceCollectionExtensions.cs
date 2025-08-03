using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Services;
using BudgetApp.Api.Application.Validators;
using BudgetApp.Api.Core.DTOs;
using System.Reflection;
using BudgetApp.Api.Core.Mappings;


namespace BudgetApp.Api.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register Application Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICreditCardService, CreditCardService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<ITransactionService, TransactionService>();

        // Register FluentValidation Validators
        services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
        services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
        services.AddScoped<IValidator<CreateCreditCardDto>, CreateCreditCardDtoValidator>();
        services.AddScoped<IValidator<CreateExpenseDto>, CreateExpenseDtoValidator>();
        services.AddScoped<IValidator<CreateIncomeDto>, CreateIncomeDtoValidator>();

        // Add AutoMapper with assembly scanning
        services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); });
        return services;
    }
}