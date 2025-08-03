using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class IncomeEndpoints
{
    public static void MapIncomeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/incomes")
            .RequireAuthorization()
            .WithTags("Incomes")
            .WithOpenApi();

        group.MapGet("/", async (IIncomeService incomeService, ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId(); // Secure extraction
            var result = await incomeService.GetIncomesByUserIdAsync(userId);
            return Results.Ok(result);
        })
        .WithSummary("Get user's incomes")
        .WithDescription("Retrieves all income records belonging to the authenticated user");

        group.MapPost("/", async (
            IIncomeService incomeService,
            IValidator<CreateIncomeDto> validator,
            [FromBody] CreateIncomeDto incomeDto,
            ClaimsPrincipal user) =>
        {
            // Validate input
            var validationResult = await validator.ValidateAsync(incomeDto);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var userId = user.GetUserId();
            var result = await incomeService.CreateIncomeAsync(userId, incomeDto);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithSummary("Create income")
        .WithDescription("Creates a new income record for the authenticated user");
    }
}