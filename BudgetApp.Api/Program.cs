using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BudgetApp.Api.Presentation.Endpoints;
using BudgetApp.Api.Presentation.Middleware;
using BudgetApp.Api.Infrastructure.Extensions;
using BudgetApp.Api.Application.Extensions;
using BudgetApp.Api.Core.Constants;
using Serilog;
using BudgetApp.Api.Presentation.Extensions;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Get configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Database connection string is not configured.");


var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
    ?? throw new InvalidOperationException("JWT_KEY environment variable is not set.");

// Add services to the container
builder.Services.AddInfrastructureServices(connectionString);
builder.Services.AddApplicationServices();

// Add Authentication & Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero // Remove default 5 minute tolerance
        };

        // Add custom claim mapping
        options.MapInboundClaims = false; // Don't map claims to Microsoft format
        options.TokenValidationParameters.NameClaimType = ApplicationConstants.Claims.Username;
        options.TokenValidationParameters.RoleClaimType = "role";
    });

builder.Services.AddAuthorization();

// Add API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Secure Budget App API", 
        Version = "v1",
        Description = "A secure budget management API built with .NET 9",
        Contact = new OpenApiContact
        {
            Name = "Budget App Team",
            Email = "support@budgetapp.com"
        }
    });

    // Add JWT Bearer authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Enable annotations for better documentation
    options.EnableAnnotations();
});

// Add CORS (if needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000") // React app
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Secure Budget App API v1");
        c.DisplayRequestDuration();
        c.EnableTryItOutByDefault();
    });
}

// Add security headers
app.UseSecurityHeaders();

// Add global exception handling
app.UseMiddleware<GlobalExceptionMiddleware>();

// Add request logging
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

// Add health check endpoint
app.MapHealthChecks("/health");

// Map API endpoints
app.MapAuthEndpoints();
app.MapCreditCardEndpoints();
app.MapExpenseEndpoints();
app.MapIncomeEndpoints();
app.MapTransactionEndpoints();

app.Run();