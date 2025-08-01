using System.Linq.Expressions;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Represents a generic repository interface for data access operations for a specific entity type.
    /// Defines the standard contract for all repositories.
    /// </summary>
    /// <typeparam name="T">The entity type this repository works with.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Gets all entities of this type.
        /// </summary>
        /// <returns>A collection of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Finds entities based on a predicate.
        /// </summary>
        /// <param name="predicate">The expression to filter entities.</param>
        /// <returns>A collection of entities that satisfy the condition.</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Adds a range of new entities to the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Removes an entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(T entity);

        /// <summary>
        /// Removes a range of entities from the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to remove.</param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
