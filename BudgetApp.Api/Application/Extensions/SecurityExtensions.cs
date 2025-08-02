using System.Security.Claims;
using BudgetApp.Api.Core.Constants;
using BudgetApp.Api.Core.Exceptions;

namespace BudgetApp.Api.Application.Extensions;

public static class SecurityExtensions
{
    /// <summary>
    /// Safely extracts user ID from ClaimsPrincipal
    /// </summary>
    /// <param name="user">The ClaimsPrincipal from the current user</param>
    /// <returns>The user ID</returns>
    /// <exception cref="UnauthorizedException">Thrown when user ID is not found or invalid</exception>
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirstValue(ApplicationConstants.Claims.UserId);
        
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedException("User ID not found in token.");
        }

        if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
        {
            throw new UnauthorizedException("Invalid user ID in token.");
        }

        return userId;
    }

    /// <summary>
    /// Safely tries to extract user ID from ClaimsPrincipal
    /// </summary>
    /// <param name="user">The ClaimsPrincipal from the current user</param>
    /// <param name="userId">The extracted user ID if successful</param>
    /// <returns>True if extraction was successful, false otherwise</returns>
    public static bool TryGetUserId(this ClaimsPrincipal user, out int userId)
    {
        userId = 0;
        
        var userIdClaim = user.FindFirstValue(ApplicationConstants.Claims.UserId);
        
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return false;
        }

        return int.TryParse(userIdClaim, out userId) && userId > 0;
    }

    /// <summary>
    /// Gets the username from ClaimsPrincipal
    /// </summary>
    /// <param name="user">The ClaimsPrincipal from the current user</param>
    /// <returns>The username</returns>
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ApplicationConstants.Claims.Username);
    }

    /// <summary>
    /// Gets the email from ClaimsPrincipal
    /// </summary>
    /// <param name="user">The ClaimsPrincipal from the current user</param>
    /// <returns>The email</returns>
    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ApplicationConstants.Claims.Email);
    }
}