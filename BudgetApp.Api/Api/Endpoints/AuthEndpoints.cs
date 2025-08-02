using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Api.Api.Endpoints
{
    /// <summary>
    /// Maps authentication endpoints.
    /// </summary>
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/register", async (IAuthService authService, [FromBody] RegisterDto registerDto) =>
            {
                try
                {
                    var result = await authService.RegisterAsync(registerDto);
                    return Results.Ok(result);
                }
                catch (ApplicationException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .AllowAnonymous() // Anyone can register
            .WithTags("Authentication"); // <-- Bu endpoint'i "Authentication" grubuna ekler

            group.MapPost("/login", async (IAuthService authService, [FromBody] LoginDto loginDto) =>
            {
                try
                {
                    var result = await authService.LoginAsync(loginDto);
                    return Results.Ok(result);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Results.Unauthorized();
                }
            })
            .AllowAnonymous() // Anyone can register
            .WithTags("Authentication"); // <-- Bu endpoint'i "Authentication" grubuna ekler
        }
    }
}
