using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BudgetApp.Api.Api.Endpoints;
using BudgetApp.Api.Core.Interfaces;
using BudgetApp.Api.Data;
using BudgetApp.Api.Data.Repositories;
using BudgetApp.Api.Services.Implementations;
using Microsoft.OpenApi.Models;
using FluentValidation;
using BudgetApp.Api.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the dependency injection container.

// Get connection string from environment variable (provided by Codespaces Secret)
// or fallback to appsettings for local development.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not configured in appsettings.Development.json");
}

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Add Repositories and Unit of Work
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Application Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCreditCardDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateIncomeDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateExpenseDtoValidator>();

var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("JWT_KEY environment variable is not set.");

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            // HATA BURADAYDI: "Secret" yerine "Key" olarak düzeltildi.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();


// Add services for API documentation (Swagger)
builder.Services.AddEndpointsApiExplorer();
// --- SWAGGER YAPILANDIRMASI GÜNCELLENDİ ---
builder.Services.AddSwaggerGen(options =>
{
    // API başlığı ve versiyonu
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BudgetApp API", Version = "v1" });

    // JWT Bearer token için güvenlik şemasını tanımla
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Tanımlanan güvenlik şemasını tüm endpoint'lere uygula
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
            new string[]{}
        }
    });
});

var app = builder.Build();

// 2. Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// 3. Map endpoints
app.MapAuthEndpoints();
app.MapCreditCardEndpoints();
app.MapExpenseEndpoints();
app.MapIncomeEndpoints();
app.MapTransactionEndpoints();

app.Run();
