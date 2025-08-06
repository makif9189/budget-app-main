using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories")
            .RequireAuthorization()
            .WithTags("Categories")
            .WithOpenApi();

        group.MapGet("/expense-categories", async (ICategoryService categoryService, ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            var result = await categoryService.GetExpenseCategoriesAsync(userId);
            return Results.Ok(result);
        })
        .WithSummary("Get expense categories")
        .WithDescription("Retrieves all expense categories for the authenticated user");

        group.MapGet("/income-sources", async (ICategoryService categoryService, ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            var result = await categoryService.GetIncomeSourcesAsync(userId);
            return Results.Ok(result);
        })
        .WithSummary("Get income sources")
        .WithDescription("Retrieves all income sources for the authenticated user");

        group.MapPost("/expense-categories", async (
            ICategoryService categoryService,
            [FromBody] string categoryName,
            ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            var result = await categoryService.CreateExpenseCategoryAsync(userId, categoryName);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithSummary("Create expense category")
        .WithDescription("Creates a new expense category for the authenticated user");

        group.MapPost("/income-sources", async (
            ICategoryService categoryService,
            [FromBody] string sourceName,
            ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            var result = await categoryService.CreateIncomeSourceAsync(userId, sourceName);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithSummary("Create income source")
        .WithDescription("Creates a new income source for the authenticated user");
    }
}