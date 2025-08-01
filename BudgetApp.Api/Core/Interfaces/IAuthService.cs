using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for authentication services.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registerDto">The user registration data.</param>
        /// <returns>An authentication response with user details and a JWT upon success.</returns>
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Logs a user into the system.
        /// </summary>
        /// <param name="loginDto">The user login credentials.</param>
        /// <returns>An authentication response with user details and a JWT upon success.</returns>
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
