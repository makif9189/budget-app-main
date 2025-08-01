using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Data
{
    /// <summary>
    /// Implements the Unit of Work pattern to manage database transactions.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        // You can add specific repository properties here if needed,
        // but for this project, services will likely get them via DI.
        // public IUserRepository Users { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            // Users = new UserRepository(_context);
        }

        /// <summary>
        /// Commits all changes to the database as a single transaction.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the database context.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
