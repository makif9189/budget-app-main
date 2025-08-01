using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces;
using BudgetApp.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Api.Services.Implementations
{
    /// <summary>
    /// Service for querying and consolidating financial transactions.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly AppDbContext _context; // Use context for complex queries

        public TransactionService(IRepository<Transaction> transactionRepository, AppDbContext context)
        {
            _transactionRepository = transactionRepository;
            _context = context;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transactions
                .Where(t => t.User_Id == userId);

            if (startDate.HasValue)
            {
                query = query.Where(t => t.Transaction_Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.Transaction_Date <= endDate.Value);
            }

            var transactions = await query
                .Include(t => t.ExpenseItem)
                    .ThenInclude(ei => ei.ExpenseCategory)
                .Include(t => t.IncomeItem)
                    .ThenInclude(ii => ii.IncomeSource)
                .OrderByDescending(t => t.Transaction_Date)
                .ToListAsync();

            // Map the complex entity to a simple DTO
            return transactions.Select(t => new TransactionDto
            {
                TransactionId = t.Transaction_Id,
                TransactionDate = t.Transaction_Date,
                Type = t.Type.ToString(),
                Amount = t.Amount,
                Description = t.Description ?? string.Empty,
                CategoryOrSource = t.Type == Core.Enums.TransactionTypeEnum.GELIR 
                                   ? t.IncomeItem?.IncomeSource?.Name ?? "Bilinmeyen Gelir"
                                   : t.ExpenseItem?.ExpenseCategory?.Name ?? "Genel Gider"
            });
        }
    }
}
