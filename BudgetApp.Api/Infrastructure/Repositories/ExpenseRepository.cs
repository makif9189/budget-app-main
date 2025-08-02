using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Infrastructure.Data;

namespace BudgetApp.Api.Infrastructure.Repositories;

public class ExpenseRepository : GenericRepository<ExpenseItem>, IExpenseRepository
{
    public ExpenseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ExpenseItem>> GetByUserIdWithCategoryAsync(int userId)
    {
        return await _dbSet
            .Include(e => e.ExpenseCategory)
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.TransactionDate)
            .ToListAsync();
    }
}