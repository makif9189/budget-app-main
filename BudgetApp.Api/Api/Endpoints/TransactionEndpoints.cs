using BudgetApp.Api.Core.Interfaces;
using System.Security.Claims;

namespace BudgetApp.Api.Api.Endpoints
{
    public static class TransactionEndpoints
    {
        public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/transactions").RequireAuthorization();

            group.MapGet("/", async (ITransactionService transactionService, ClaimsPrincipal user, DateTime? startDate, DateTime? endDate) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                var transactions = await transactionService.GetTransactionsByUserIdAsync(userId, startDate, endDate);
                return Results.Ok(transactions);
            });
        }
    }
}
