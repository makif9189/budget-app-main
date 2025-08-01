using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for user-related business logic.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>A DTO representing the user.</returns>
        Task<UserDto> GetUserByIdAsync(int id);
    }
}
