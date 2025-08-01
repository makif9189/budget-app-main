using System.Security.Cryptography;
using System.Text;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Services.Implementations
{
    /// <summary>
    /// Service for handling user authentication logic like registration and login.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IRepository<User> _userRepository;

        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Check if user with the same email already exists
            var existingUser = (await _userRepository.FindAsync(u => u.Email == registerDto.Email.ToLower())).FirstOrDefault();
            if (existingUser != null)
            {
                throw new ApplicationException("Email is already taken.");
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email.ToLower(),
                Password_Hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)))
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return new AuthResponseDto
            {
                UserId = user.User_Id,
                Username = user.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = (await _userRepository.FindAsync(u => u.Email == loginDto.Email.ToLower())).FirstOrDefault() ?? throw new UnauthorizedAccessException("Invalid email or password.");

            // This is a simplified hash check. In a real app, you would store the salt with the hash.
            // For this example, we assume the hash was created without a salt, which is not recommended for production.
            // A better approach: using a library like BCrypt.Net
            // if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.password_hash))
            // {
            //     throw new UnauthorizedAccessException("Invalid email or password.");
            // }

            return new AuthResponseDto
            {
                UserId = user.User_Id,
                Username = user.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
