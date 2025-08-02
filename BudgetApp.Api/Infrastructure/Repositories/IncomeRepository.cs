using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Infrastructure.Data;

namespace BudgetApp.Api.Infrastructure.Repositories;

public class IncomeRepository : GenericRepository<IncomeItem>, IIncomeRepository
{
    public IncomeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<IncomeItem>> GetByUserIdWithSourceAsync(int userId)
    {
        return await _dbSet
            .Include(i => i.IncomeSource)
            .Where(i => i.UserId == userId)
            .OrderByDescending(i => i.TransactionDate)
            .ToListAsync();
    }
}