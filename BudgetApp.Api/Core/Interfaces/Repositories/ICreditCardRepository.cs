using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Core.Interfaces.Repositories;

public interface ICreditCardRepository : IRepository<CreditCard>
{
    Task<IEnumerable<CreditCard>> GetByUserIdAsync(int userId);
    Task<CreditCard?> GetByIdAndUserIdAsync(int id, int userId);
}