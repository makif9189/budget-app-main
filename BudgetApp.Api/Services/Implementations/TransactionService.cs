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
                .Where(t => t.user_id == userId);

            if (startDate.HasValue)
            {
                query = query.Where(t => t.transaction_date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.transaction_date <= endDate.Value);
            }

            var transactions = await query
                .Include(t => t.ExpenseItem)
                    .ThenInclude(ei => ei.ExpenseCategory)
                .Include(t => t.IncomeItem)
                    .ThenInclude(ii => ii.IncomeSource)
                .OrderByDescending(t => t.transaction_date)
                .ToListAsync();

            // Map the complex entity to a simple DTO
            return transactions.Select(t => new TransactionDto
            {
                TransactionId = t.transaction_id,
                TransactionDate = t.transaction_date,
                Type = t.type.ToString(),
                Amount = t.amount,
                Description = t.description,
                CategoryOrSource = t.type == Core.Enums.TransactionTypeEnum.GELIR 
                                   ? t.IncomeItem?.IncomeSource?.name ?? "Bilinmeyen Gelir"
                                   : t.ExpenseItem?.ExpenseCategory?.name ?? "Genel Gider"
            });
        }
    }
}
