using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Interfaces;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Infrastructure.Data;
using BudgetApp.Api.Infrastructure.Repositories;
using BudgetApp.Api.Infrastructure.Services;

namespace BudgetApp.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        string connectionString)
    {
        // Add DbContext
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging(false); // Disable in production
            options.EnableServiceProviderCaching();
        });

        // Add Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Add Generic Repository
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        // Add Specific Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICreditCardRepository, CreditCardRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        // Add Infrastructure Services
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}