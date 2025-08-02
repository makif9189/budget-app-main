using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Core.Interfaces.Repositories;

public interface IIncomeRepository : IRepository<IncomeItem>
{
    Task<IEnumerable<IncomeItem>> GetByUserIdWithSourceAsync(int userId);
}