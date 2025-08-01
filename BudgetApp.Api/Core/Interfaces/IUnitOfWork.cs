namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Represents the Unit of Work pattern.
    /// Manages repositories and handles the transactionality of business operations
    /// to ensure data integrity.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Add repository interfaces here
        // Example: IUserRepository Users { get; }
        // For this project, we might access repositories dynamically or inject them into services directly.

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> CompleteAsync();
    }
}
