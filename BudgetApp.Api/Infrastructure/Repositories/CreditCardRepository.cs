using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Infrastructure.Data;

namespace BudgetApp.Api.Infrastructure.Repositories;

public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRepository
{
    public CreditCardRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CreditCard>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<CreditCard?> GetByIdAndUserIdAsync(int id, int userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.CreditCardId == id && c.UserId == userId);
    }
}