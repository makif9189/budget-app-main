using AutoMapper;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Exceptions;
using BudgetApp.Api.Core.Interfaces.Repositories;
using BudgetApp.Api.Core.Interfaces.Services;

namespace BudgetApp.Api.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserDto>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<UserDto>.ErrorResult("User not found.");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.SuccessResult(userDto);
        }
        catch (Exception ex)
        {
            return ApiResponse<UserDto>.ErrorResult($"Failed to retrieve user: {ex.Message}");
        }
    }
}