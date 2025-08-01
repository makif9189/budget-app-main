using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetApp.Api.Api.Endpoints
{
    /// <summary>
    /// Maps credit card related endpoints.
    /// </summary>
    public static class CreditCardEndpoints
    {
        public static void MapCreditCardEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/credit-cards").RequireAuthorization(); // Secure this whole group

            group.MapGet("/", async (ICreditCardService cardService, ClaimsPrincipal user) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                var cards = await cardService.GetCreditCardsByUserIdAsync(userId);
                return Results.Ok(cards);
            });

            group.MapGet("/{id:int}", async (ICreditCardService cardService, int id) =>
            {
                var card = await cardService.GetCreditCardByIdAsync(id);
                return card is not null ? Results.Ok(card) : Results.NotFound();
            });

            group.MapPost("/", async (ICreditCardService cardService, [FromBody] CreateCreditCardDto cardDto, ClaimsPrincipal user) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                var newCard = await cardService.CreateCreditCardAsync(userId, cardDto);
                return Results.CreatedAtRoute(null, new { id = newCard.CreditCardId }, newCard);
            });

            group.MapPut("/{id:int}", async (ICreditCardService cardService, int id, [FromBody] CreateCreditCardDto cardDto) =>
            {
                var success = await cardService.UpdateCreditCardAsync(id, cardDto);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id:int}", async (ICreditCardService cardService, int id) =>
            {
                var success = await cardService.DeleteCreditCardAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
