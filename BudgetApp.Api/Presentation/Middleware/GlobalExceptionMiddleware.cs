using System.Net;
using System.Text.Json;
using BudgetApp.Api.Core.DTOs.Common;
using BudgetApp.Api.Core.Exceptions;
using FluentValidation;

namespace BudgetApp.Api.Presentation.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = exception switch
        {
            ValidationException validationEx => new ApiResponse
            {
                Success = false,
                Message = "Validation failed",
                Errors = validationEx.Errors.SelectMany(x => x.Value).ToList()
            },
            
            FluentValidation.ValidationException fluentValidationEx => new ApiResponse
            {
                Success = false,
                Message = "Validation failed",
                Errors = fluentValidationEx.Errors.Select(x => x.ErrorMessage).ToList()
            },

            UnauthorizedException unauthorizedEx => new ApiResponse
            {
                Success = false,
                Message = unauthorizedEx.Message,
                Errors = new List<string> { "Unauthorized access" }
            },

            NotFoundException notFoundEx => new ApiResponse
            {
                Success = false,
                Message = notFoundEx.Message,
                Errors = new List<string> { "Resource not found" }
            },

            BusinessException businessEx => new ApiResponse
            {
                Success = false,
                Message = businessEx.Message,
                Errors = new List<string> { "Business rule violation" }
            },

            SecurityException securityEx => new ApiResponse
            {
                Success = false,
                Message = "Security violation",
                Errors = new List<string> { "Access denied" }
            },

            _ => new ApiResponse
            {
                Success = false,
                Message = "An error occurred while processing your request",
                Errors = new List<string> { "Internal server error" }
            }
        };

        context.Response.StatusCode = exception switch
        {
            ValidationException => (int)HttpStatusCode.BadRequest,
            FluentValidation.ValidationException => (int)HttpStatusCode.BadRequest,
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            NotFoundException => (int)HttpStatusCode.NotFound,
            BusinessException => (int)HttpStatusCode.BadRequest,
            SecurityException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}