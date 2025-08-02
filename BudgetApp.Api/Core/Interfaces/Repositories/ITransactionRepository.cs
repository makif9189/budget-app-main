using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Core.Interfaces.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetByUserIdWithDetailsAsync(int userId, DateTime? startDate, DateTime? endDate);
}