namespace BudgetApp.Api.Infrastructure.Services;

public static class PasswordService
{
    private const int WorkFactor = 12; // BCrypt work factor for security

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}