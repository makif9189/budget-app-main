// BudgetApp.Api/Application/Services/DashboardService.cs
using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;

namespace BudgetApp.Api.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public DashboardService(
        IIncomeRepository incomeRepository,
        IExpenseRepository expenseRepository,
        ICreditCardRepository creditCardRepository,
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        _incomeRepository = incomeRepository;
        _expenseRepository = expenseRepository;
        _creditCardRepository = creditCardRepository;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<DashboardSummaryDto>> GetDashboardSummaryAsync(int userId, DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            // Eğer tarih belirtilmemişse, bu ayın başından bugüne kadar
            if (startDate == null)
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (endDate == null)
                endDate = DateTime.Now;

            var summary = new DashboardSummaryDto();

            // Gelirleri getir
            var incomes = await _incomeRepository.GetByUserIdWithSourceAsync(userId);
            var filteredIncomes = incomes.Where(i => i.TransactionDate >= startDate && i.TransactionDate <= endDate);
            
            summary.TotalIncome = filteredIncomes.Sum(i => i.Amount);
            summary.IncomeBySource = filteredIncomes
                .GroupBy(i => i.IncomeSource.Name)
                .Select(g => new IncomeBySourceDto 
                { 
                    SourceName = g.Key, 
                    Amount = g.Sum(i => i.Amount) 
                })
                .ToList();

            // Giderleri getir
            var expenses = await _expenseRepository.GetByUserIdWithCategoryAsync(userId);
            var filteredExpenses = expenses.Where(e => e.TransactionDate >= startDate && e.TransactionDate <= endDate);
            
            summary.TotalExpenses = filteredExpenses.Sum(e => e.Amount);
            summary.ExpenseByCategory = filteredExpenses
                .GroupBy(e => e.ExpenseCategory.Name)
                .Select(g => new ExpenseByCategoryDto 
                { 
                    CategoryName = g.Key, 
                    Amount = g.Sum(e => e.Amount) 
                })
                .ToList();

            // Net bakiye hesapla
            summary.NetBalance = summary.TotalIncome - summary.TotalExpenses;

            // Son işlemler
            var transactions = await _transactionRepository.GetByUserIdWithDetailsAsync(userId, startDate, endDate);
            summary.RecentTransactions = transactions
                .Take(10)
                .Select(t => new RecentTransactionDto
                {
                    Description = t.Description ?? "İşlem",
                    Amount = Math.Abs(t.Amount),
                    Date = t.TransactionDate,
                    Type = t.Type == 1 ? "Income" : "Expense"
                })
                .ToList();

            // Kredi kartı borçları (basitleştirilmiş)
            var creditCards = await _creditCardRepository.GetByUserIdAsync(userId);
            summary.TotalCreditCardDebt = 0; // Bu hesaplama daha karmaşık olacak
            
            // Yaklaşan ödemeler (örnek)
            summary.UpcomingPayments = creditCards.Select(cc => new UpcomingPaymentDto
            {
                CardName = cc.Name,
                DueDate = DateTime.Now.AddDays(cc.PaymentDueDateOffset),
                Amount = 1000 // Bu gerçek borç hesaplamasından gelecek
            }).ToList();

            return ApiResponse<DashboardSummaryDto>.SuccessResult(summary);
        }
        catch (Exception ex)
        {
            return ApiResponse<DashboardSummaryDto>.ErrorResult($"Failed to get dashboard summary: {ex.Message}");
        }
    }
}