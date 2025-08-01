using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that creates JSON Web Tokens (JWT).
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a JWT for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to create the token.</param>
        /// <returns>A string representing the JWT.</returns>
        string CreateToken(User user);
    }
}
