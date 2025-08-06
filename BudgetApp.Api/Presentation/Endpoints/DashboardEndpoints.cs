using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using System.Security.Claims;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class DashboardEndpoints
{
    public static void MapDashboardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dashboard")
            .RequireAuthorization()
            .WithTags("Dashboard")
            .WithOpenApi();

        group.MapGet("/summary", async (
            IDashboardService dashboardService,
            ClaimsPrincipal user,
            DateTime? startDate = null,
            DateTime? endDate = null) =>
        {
            var userId = user.GetUserId();
            var result = await dashboardService.GetDashboardSummaryAsync(userId, startDate, endDate);
            return Results.Ok(result);
        })
        .WithSummary("Get dashboard summary")
        .WithDescription("Returns dashboard summary with income, expenses, and credit card information");
    }
}