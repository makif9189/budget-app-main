using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using System.Security.Claims;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/transactions")
            .RequireAuthorization()
            .WithTags("Transactions")
            .WithOpenApi();

        group.MapGet("/", async (
            ITransactionService transactionService,
            ClaimsPrincipal user,
            DateTime? startDate = null,
            DateTime? endDate = null) =>
        {
            var userId = user.GetUserId(); // Secure extraction
            var result = await transactionService.GetTransactionsByUserIdAsync(userId, startDate, endDate);
            return Results.Ok(result);
        })
        .WithSummary("Get user's transactions")
        .WithDescription("Retrieves all transactions for the authenticated user with optional date filtering");
    }
}