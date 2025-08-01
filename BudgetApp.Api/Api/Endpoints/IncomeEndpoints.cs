using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetApp.Api.Api.Endpoints
{
    public static class IncomeEndpoints
    {
        public static void MapIncomeEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/incomes").RequireAuthorization();

            group.MapGet("/", async (IIncomeService incomeService, ClaimsPrincipal user) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                return Results.Ok(await incomeService.GetIncomesByUserIdAsync(userId));
            });

            group.MapPost("/", async (IIncomeService incomeService, [FromBody] CreateIncomeDto incomeDto, ClaimsPrincipal user) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await incomeService.CreateIncomeAsync(userId, incomeDto);
                return Results.Ok(result);
            });
        }
    }
}
