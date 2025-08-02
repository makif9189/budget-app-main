using BCrypt.Net;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Services.Implementations
{
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
            var existingUser = (await _userRepository.FindAsync(u => u.Email == registerDto.Email.ToLower())).FirstOrDefault();
            if (existingUser != null)
                throw new ApplicationException("Email is already taken.");

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email.ToLower(),
                Password_Hash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
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
            var user = (await _userRepository.FindAsync(u => u.Email == loginDto.Email.ToLower())).FirstOrDefault()
                       ?? throw new UnauthorizedAccessException("Invalid email or password.");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password_Hash))
                throw new UnauthorizedAccessException("Invalid email or password.");

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
