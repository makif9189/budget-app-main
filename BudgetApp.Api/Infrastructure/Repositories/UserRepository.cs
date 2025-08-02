using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Infrastructure.Data;

namespace BudgetApp.Api.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email.ToLower());
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email.ToLower());
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _dbSet.AnyAsync(u => u.Username == username);
    }
}