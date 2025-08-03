using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Infrastructure.Data;

namespace BudgetApp.Api.Infrastructure.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdWithDetailsAsync(int userId, DateTime? startDate, DateTime? endDate)
    {
        var query = _dbSet
            //.Include(t => t.ExpenseItem!)
            //    .ThenInclude(ei => ei.ExpenseCategory)
            .Include(t => t.IncomeItem!)
                .ThenInclude(ii => ii.IncomeSource)
            .Include(t => t.CreditCard)
            .Include(t => t.InstallmentDefinition)
            .Where(t => t.UserId == userId);

        if (startDate.HasValue)
        {
            query = query.Where(t => t.TransactionDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(t => t.TransactionDate <= endDate.Value);
        }

        return await query
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }
}