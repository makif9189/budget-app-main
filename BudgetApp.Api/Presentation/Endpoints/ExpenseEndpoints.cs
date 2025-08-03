using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class ExpenseEndpoints
{
    public static void MapExpenseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/expenses")
            .RequireAuthorization()
            .WithTags("Expenses")
            .WithOpenApi();

        group.MapGet("/", async (IExpenseService expenseService, ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId(); // Secure extraction
            var result = await expenseService.GetExpensesByUserIdAsync(userId);
            return Results.Ok(result);
        })
        .WithSummary("Get user's expenses")
        .WithDescription("Retrieves all expenses belonging to the authenticated user");

        group.MapPost("/", async (
            IExpenseService expenseService,
            IValidator<CreateExpenseDto> validator,
            [FromBody] CreateExpenseDto expenseDto,
            ClaimsPrincipal user) =>
        {
            // Validate input
            var validationResult = await validator.ValidateAsync(expenseDto);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var userId = user.GetUserId();
            var result = await expenseService.CreateExpenseAsync(userId, expenseDto);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithSummary("Create expense")
        .WithDescription("Creates a new expense record for the authenticated user");
    }
}