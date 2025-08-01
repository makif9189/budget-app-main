using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetApp.Api.Api.Endpoints
{
    public static class ExpenseEndpoints
    {
        public static void MapExpenseEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/expenses").RequireAuthorization();

            group.MapGet("/", async (IExpenseService expenseService, ClaimsPrincipal user) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                return Results.Ok(await expenseService.GetExpensesByUserIdAsync(userId));
            });

            group.MapPost("/", async (IExpenseService expenseService, [FromBody] CreateExpenseDto expenseDto, ClaimsPrincipal user) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await expenseService.CreateExpenseAsync(userId, expenseDto);
                return Results.Ok(result);
            });
        }
    }
}
