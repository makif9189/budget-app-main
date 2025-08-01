using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BudgetApp.Api.Api.Endpoints;
using BudgetApp.Api.Core.Interfaces;
using BudgetApp.Api.Data;
using BudgetApp.Api.Data.Repositories;
using BudgetApp.Api.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the dependency injection container.

// Get connection string from environment variable (provided by Codespaces Secret)
// or fallback to appsettings for local development.
var connectionString = builder.Configuration["NEON_CONNECTION_STRING"] 
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not configured. Set NEON_CONNECTION_STRING as a Codespaces secret.");
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });
builder.Services.AddAuthorization();


// Add services for API documentation (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
