using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        group.MapPost("/register", async (
            IAuthService authService,
            IValidator<RegisterDto> validator,
            [FromBody] RegisterDto registerDto) =>
        {
            // Validate input
            var validationResult = await validator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var result = await authService.RegisterAsync(registerDto);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .AllowAnonymous()
        .WithSummary("Register a new user")
        .WithDescription("Creates a new user account with username, email and password");

        group.MapPost("/login", async (
            IAuthService authService,
            IValidator<LoginDto> validator,
            [FromBody] LoginDto loginDto) =>
        {
            // Validate input
            var validationResult = await validator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var result = await authService.LoginAsync(loginDto);
            return result.Success ? Results.Ok(result) : Results.Unauthorized();
        })
        .AllowAnonymous()
        .WithSummary("Login user")
        .WithDescription("Authenticates user with email and password, returns JWT token");
    }
}