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
            // Bütün endpoint grubunu tek seferde koruma altına al ve etiketle.
            var group = app.MapGroup("/api/credit-cards")
                           .RequireAuthorization()
                           .WithTags("Credit Cards");

            // Sadece o anki giriş yapmış kullanıcının kredi kartlarını getirir.
            group.MapGet("/", async (ICreditCardService cardService, ClaimsPrincipal user) =>
            {
                // Token'dan kullanıcı ID'sini güvenli bir şekilde al.
                var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out var userId))
                {
                    return Results.Unauthorized();
                }

                var cards = await cardService.GetCreditCardsByUserIdAsync(userId);
                return Results.Ok(cards);
            });

            // Belirli bir kredi kartını ID ile getirir.
            group.MapGet("/{id:int}", async (ICreditCardService cardService, int id,ClaimsPrincipal user) =>
            {
                var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out var userId))
                {
                    return Results.Unauthorized();
                }
                // Servis katmanında bu kartın istenen kullanıcıya ait olduğu kontrol edilmelidir.
                var card = await cardService.GetCreditCardByIdAsync(id,userId);
                return card is not null ? Results.Ok(card) : Results.NotFound();
            })
            .WithName("GetCreditCardById"); // CreatedAtRoute'un çalışması için isimlendirme yapıldı.

            // Yeni bir kredi kartı oluşturur.
            group.MapPost("/", async (ICreditCardService cardService, [FromBody] CreateCreditCardDto cardDto, ClaimsPrincipal user) =>
            {
                var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out var userId))
                {
                    return Results.Unauthorized();
                }

                // Servis metoduna userId gönderilerek kartın doğru kullanıcıya atanması sağlanır.
                var newCard = await cardService.CreateCreditCardAsync(userId, cardDto);
                
                // İsimlendirilmiş route'a referans vererek 201 Created yanıtı döndür.
                return Results.CreatedAtRoute("GetCreditCardById", new { id = newCard.CreditCardId }, newCard);
            });

            // Mevcut bir kredi kartını günceller.
            group.MapPut("/{id:int}", async (ICreditCardService cardService, int id, [FromBody] CreditCardDto cardDto, ClaimsPrincipal user) =>
            {
                var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out var userId))
                {
                    return Results.Unauthorized();
                }

                var updatedCard = await cardService.UpdateCreditCardAsync(id, cardDto);
                return Results.Ok(updatedCard);
            });

            // Bir kredi kartını siler.
            group.MapDelete("/{id:int}", async (ICreditCardService cardService, int id,ClaimsPrincipal user) =>
            {
                var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out var userId))
                {
                    return Results.Unauthorized();
                }
                var success = await cardService.DeleteCreditCardAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
