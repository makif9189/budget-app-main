using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Exceptions;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Core.Interfaces;
using BudgetApp.Api.Infrastructure.Services;

namespace BudgetApp.Api.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            // Check if email already exists (validator should catch this, but double-check)
            if (await _userRepository.ExistsByEmailAsync(registerDto.Email))
            {
                return ApiResponse<AuthResponseDto>.ErrorResult("Email is already registered.");
            }

            // Create user entity
            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = PasswordService.HashPassword(registerDto.Password);

            // Save user
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Generate token and response
            var token = _tokenService.CreateToken(user);
            var authResponse = new AuthResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Token = token
            };

            return ApiResponse<AuthResponseDto>.SuccessResult(authResponse, "Registration successful.");
        }
        catch (Exception ex)
        {
            return ApiResponse<AuthResponseDto>.ErrorResult($"Registration failed: {ex.Message}");
        }
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ApiResponse<AuthResponseDto>.ErrorResult("Invalid email or password.");
            }

            // Verify password
            if (!PasswordService.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                return ApiResponse<AuthResponseDto>.ErrorResult("Invalid email or password.");
            }

            // Generate token and response
            var token = _tokenService.CreateToken(user);
            var authResponse = new AuthResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Token = token
            };

            return ApiResponse<AuthResponseDto>.SuccessResult(authResponse, "Login successful.");
        }
        catch (Exception ex)
        {
            return ApiResponse<AuthResponseDto>.ErrorResult($"Login failed: {ex.Message}");
        }
    }
}