using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Services.Implementations
{
    /// <summary>
    /// Service for handling user-related business logic, such as retrieving user profiles.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Retrieves a user by their ID and maps them to a safe DTO.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A UserDto containing non-sensitive user information.</returns>
        /// <exception cref="ApplicationException">Thrown if no user is found with the given ID.</exception>
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ApplicationException($"User with ID {id} not found.");
            }

            // Map the User entity to a UserDto to avoid exposing the password hash.
            return new UserDto
            {
                UserId = user.user_id,
                Username = user.username,
                Email = user.email,
                CreatedAt = user.created_at
            };
        }
    }
}
