// BudgetApp.Api/Core/Configuration/JwtSettings.cs
namespace BudgetApp.Api.Core.Configuration;

public class JwtSettings
{
    public const string SectionName = "JwtSettings"; // appsettings.json'daki bölüm adı

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; }
    // public int RefreshTokenExpirationDays { get; set; } // İleride
}