using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Application.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetApp.Api.Presentation.Endpoints;

public static class CreditCardEndpoints
{
    public static void MapCreditCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/credit-cards")
            .RequireAuthorization()
            .WithTags("Credit Cards")
            .WithOpenApi();

        group.MapGet("/", async (ICreditCardService cardService, ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId(); // Using secure extension method
            var result = await cardService.GetCreditCardsByUserIdAsync(userId);
            return Results.Ok(result);
        })
        .WithSummary("Get user's credit cards")
        .WithDescription("Retrieves all credit cards belonging to the authenticated user");

        group.MapGet("/{id:int}", async (
            ICreditCardService cardService,
            int id,
            ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            var result = await cardService.GetCreditCardByIdAsync(id, userId);
            return result.Success ? Results.Ok(result) : Results.NotFound(result);
        })
        .WithName("GetCreditCardById")
        .WithSummary("Get credit card by ID")
        .WithDescription("Retrieves a specific credit card if it belongs to the authenticated user");

        group.MapPost("/", async (
            ICreditCardService cardService,
            IValidator<CreateCreditCardDto> validator,
            [FromBody] CreateCreditCardDto cardDto,
            ClaimsPrincipal user) =>
        {
            // Validate input
            var validationResult = await validator.ValidateAsync(cardDto);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var userId = user.GetUserId();
            var result = await cardService.CreateCreditCardAsync(userId, cardDto);
            
            if (result.Success && result.Data != null)
            {
                return Results.CreatedAtRoute("GetCreditCardById", 
                    new { id = result.Data.CreditCardId }, result);
            }
            
            return Results.BadRequest(result);
        })
        .WithSummary("Create credit card")
        .WithDescription("Creates a new credit card for the authenticated user");

        group.MapPut("/{id:int}", async (
            ICreditCardService cardService,
            int id,
            [FromBody] CreditCardDto cardDto,
            ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            var result = await cardService.UpdateCreditCardAsync(id, userId, cardDto);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithSummary("Update credit card")
        .WithDescription("Updates an existing credit card if it belongs to the authenticated user");

        group.MapDelete("/{id:int}", async (
            ICreditCardService cardService,
            int id,
            ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            var result = await cardService.DeleteCreditCardAsync(id, userId);
            return result.Success ? Results.NoContent() : Results.NotFound(result);
        })
        .WithSummary("Delete credit card")
        .WithDescription("Deletes a credit card if it belongs to the authenticated user");
    }
}