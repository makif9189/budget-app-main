using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Core.Interfaces.Repositories;

public interface IExpenseRepository : IRepository<ExpenseItem>
{
    Task<IEnumerable<ExpenseItem>> GetByUserIdWithCategoryAsync(int userId);
}