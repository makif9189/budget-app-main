using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using System.Security.Claims;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class LookupEndpoints
{
    public static void MapLookupEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/lookups")
            .RequireAuthorization()
            .WithTags("Lookups")
            .WithOpenApi();

        // Transaction types için enum değerler
        group.MapGet("/transaction-types", () =>
        {
            var transactionTypes = new[]
            {
                new { Id = 1, Name = "Gelir" },
                new { Id = 2, Name = "Gider" }
            };
            
            return Results.Ok(new { Success = true, Data = transactionTypes });
        })
        .WithSummary("Get transaction types")
        .WithDescription("Returns available transaction types");

        // Recurring frequency options
        group.MapGet("/recurring-frequencies", () =>
        {
            var frequencies = new[]
            {
                new { Id = "monthly", Name = "Aylık" },
                new { Id = "weekly", Name = "Haftalık" },
                new { Id = "yearly", Name = "Yıllık" }
            };
            
            return Results.Ok(new { Success = true, Data = frequencies });
        })
        .WithSummary("Get recurring frequencies")
        .WithDescription("Returns available recurring frequency options");

        // Payment methods for expenses
        group.MapGet("/payment-methods", () =>
        {
            var paymentMethods = new[]
            {
                new { Id = "cash", Name = "Nakit" },
                new { Id = "bank_transfer", Name = "Banka Transferi" },
                new { Id = "debit_card", Name = "Banka Kartı" },
                new { Id = "other", Name = "Diğer" }
            };
            
            return Results.Ok(new { Success = true, Data = paymentMethods });
        })
        .WithSummary("Get payment methods")
        .WithDescription("Returns available payment methods for expenses");
    }
}